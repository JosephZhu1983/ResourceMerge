using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ResourceMerge.Core
{
    public class MergeService
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Start()
        {
            ConfigProvider.Init();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Stop()
        {

        }

        public static void AddResource(ResourceItem resource)
        {
            if (string.IsNullOrEmpty(resource.Url) && string.IsNullOrEmpty(resource.Content)) return;
            List<ResourceItem> resourceList = HttpContext.Current.Items[ConfigProvider.HttpItemResourceListKey] as List<ResourceItem>;
            if (resourceList == null) resourceList = new List<ResourceItem>();
            if (resourceList.Count == 0 ||
                (!string.IsNullOrEmpty(resource.Url) && !resourceList.Exists(item => item.Url == resource.Url))
                || (string.IsNullOrEmpty(resource.Url) && !resourceList.Exists(item => item.Content == resource.Content)))
            {
                var type = resource.ResourceType;
                if (type == ResourceType.Auto && resource.Url.EndsWith(".css", StringComparison.InvariantCultureIgnoreCase))
                    type = ResourceType.Style;
                if (type == ResourceType.Auto && resource.Url.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                    type = ResourceType.Script;
                resourceList.Add(new ResourceItem
                {
                    IsMerge = resource.IsMerge,
                    IsMinify = resource.IsMinify,
                    ResourceType = type,
                    Url = resource.Url,
                    Charset = resource.Charset,
                    Content = resource.Content,
                    RenderPriority = resource.RenderPriority,
                    RenderLocation = resource.RenderLocation,
                });
            }
            HttpContext.Current.Items[ConfigProvider.HttpItemResourceListKey] = resourceList;
        }

        public static void AddResouece(string url)
        {
            AddResource(new ResourceItem
            {
                Url = url
            });
        }

        internal static string Merge(string token, List<ResourceItem> resources, ConfigSet cs)
        {
            StringBuilder content = new StringBuilder();

            for (int i = 0; i < resources.Count; i++)
            {
                var resource = resources[i];
                string segment = string.Empty;
                if (!string.IsNullOrEmpty(resource.Url))
                {
                    try
                    {
                        if (resource.Url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) || resource.Url.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                        {
                            segment = DownloadResource(resource.Url, cs.GetCompressedRemoteResource, resource.Charset ?? cs.Charset);
                            if (!string.IsNullOrEmpty(cs.StaticResrouceSuffix) || !string.IsNullOrEmpty(cs.StaticResroucePreffix))
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    if ((!string.IsNullOrEmpty(cs.StaticResrouceSuffix) && segment.EndsWith(cs.StaticResrouceSuffix)) &&
                                        (!string.IsNullOrEmpty(cs.StaticResroucePreffix) && segment.StartsWith(cs.StaticResroucePreffix)))
                                        break;
                                    segment = DownloadResource(resource.Url, cs.GetCompressedRemoteResource, resource.Charset ?? cs.Charset);
                                    if (j == 1)
                                        throw new Exception(string.Format("Failed to check file suffix {0} for {1}", cs.StaticResrouceSuffix,  resource.Url));
                                }
                            }
                        }
                        else
                        {
                            string filePath = HttpContext.Current.Server.MapPath(resource.Url);
                            if (File.Exists(filePath))
                                segment = File.ReadAllText(filePath, Encoding.GetEncoding(resource.Charset ?? cs.Charset));
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("")))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(""));
                            }
                            File.AppendAllText(HttpContext.Current.Server.MapPath("ResourceMergeLog.txt"),
                                                DateTime.Now + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);
                        }
                        catch
                        {
                        }
                        continue;
                    }
                }
                else
                {
                    segment = resource.Content;
                    resource.Url = "Inline " + resource.ResourceType.ToString();
                }

                if (resource.IsMinify.Value)
                {
                    string minified = string.Empty;
                    try
                    {
                        if (resource.ResourceType == ResourceType.Style)
                        {
                            minified = CssMinifier.CssMinify(segment, 0);
                        }
                        if (resource.ResourceType == ResourceType.Script)
                        {
                            minified = JsMinifier.GetMinifiedCode(segment);
                        }
                        segment = minified;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (HttpContext.Current.IsDebuggingEnabled)
                            {
                                if (!Directory.Exists(HttpContext.Current.Server.MapPath("")))
                                {
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(""));
                                }
                                File.AppendAllText(HttpContext.Current.Server.MapPath("ResourceMergeLog.txt"),
                                                    DateTime.Now + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                content.AppendFormat("{4}/******************** {0} ********************/{1}{2}{3}", resource.Url, Environment.NewLine, segment, Environment.NewLine, Environment.NewLine);
            }
            return content.ToString();

        }

        private static string DownloadResource(string url, bool getCompressedRemoteResource, string charset)
        {
            var webClient = new WebClient();
            if (getCompressedRemoteResource)
                webClient.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
            var bytes = webClient.DownloadData(url);
            var acceptEncoding = webClient.ResponseHeaders[HttpResponseHeader.ContentEncoding];
            if (acceptEncoding != null)
            {
                if (acceptEncoding.Contains("gzip"))
                    bytes = Helper.UnCompress(bytes, Helper.CompressionType.GZip);
                else if (acceptEncoding.Contains("deflate"))
                    bytes = Helper.UnCompress(bytes, Helper.CompressionType.Delfate);
            }
            return Encoding.GetEncoding(charset).GetString(bytes);
        }

        internal static string ExtractImportAndRemoveCharsetInStyle(string content)
        {
            var matches = Regex.Matches(content, "@import .*?;", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            if (matches.Count > 0)
            {
                content = content + Environment.NewLine;
                foreach (Match match in matches)
                {
                    content = content.Replace(match.Value, string.Format("/*{0}*/", match.Value));
                    content = string.Format("{0}{1}", match.Value, Environment.NewLine) + content;
                }
            }
            content = Regex.Replace(content, "@charset.*?;", "", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            return content;
        }
    }
}
