﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Net;

namespace ResourceMerge.Core
{
    public class MergeHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context == null || context.Request == null || context.Response == null) return;
                HttpRequest request = context.Request;
                HttpResponse response = context.Response;
                string token = request.QueryString[ConfigProvider.QueryStringTokenKey];
                string type = request.QueryString[ConfigProvider.QueryStringTypeKey] ?? string.Empty;
                string action = request.QueryString[ConfigProvider.QueryStringActionKey] ?? string.Empty;
                string csName = request.QueryString[ConfigProvider.QueryStringConfigSetNameKey] ?? string.Empty;
                ConfigSet cs = ConfigProvider.GetConfigSetFromConfigSetName(csName);

                if (!string.IsNullOrEmpty(action))
                {
                    switch (action)
                    {
                        case ConfigProvider.QueryStringClearCacheValue:
                            {
                                ResourceCache.ResourceContent.Clear();
                                return;
                            }
                        case ConfigProvider.QueryStringShowCacheInfoValue:
                            {
                                response.Write(ResourceCache.ResourceContent.Info());
                                return;
                            }
                        default:
                            {
                                return;
                            }
                    }
                }

                if (string.IsNullOrEmpty(token)) return;
                List<ResourceItem> resourceList = ResourceCache.ResourceMapping.Get(token);
                if (resourceList == null || resourceList.Count < 1) return;

                Stopwatch sw = Stopwatch.StartNew();
                string content = ResourceCache.ResourceContent.Get(token);
                bool incache = true;
                if (string.IsNullOrEmpty(content))
                {
                    incache = false;
                    content = MergeService.Merge(token, resourceList, cs);
                    if (type == "style")
                        content = MergeService.ExtractImportAndRemoveCharsetInStyle(content);
                    content = string.Format("/******************** Content generated by {0},{1},{2} ********************/{3}", HttpContext.Current.Server.MachineName, DateTime.Now.ToString(), string.Concat(sw.ElapsedMilliseconds, " ms"), Environment.NewLine) + content;
                    if (cs.UseServerCache)
                        ResourceCache.ResourceContent.Set(type, cs.Name, token, content, cs.ServerCacheDuration);
                }

                switch (type)
                {
                    case "script":
                        {
                            response.ContentType = "application/x-javascript";
                            break;
                        }
                    case "style":
                        {
                            response.ContentType = "text/css";
                            break;
                        }
                    default:
                        {
                            response.ContentType = "text/plain";
                            break;
                        }
                }

                if (cs.UseClientCache)
                {
                    var cache = response.Cache;
                    cache.SetOmitVaryStar(true);
                    cache.SetMaxAge(TimeSpan.FromSeconds(cs.ClientCacheDuration));
                    cache.SetProxyMaxAge(TimeSpan.FromSeconds(cs.ClientCacheDuration));
                    cache.SetLastModified(DateTime.Now);
                    cache.SetExpires(DateTime.Now.AddSeconds(cs.ClientCacheDuration));
                    cache.SetValidUntilExpires(true);
                    cache.SetCacheability(HttpCacheability.Public);
                    cache.VaryByParams[ConfigProvider.QueryStringTokenKey] = true;
                    cache.VaryByParams[ConfigProvider.QueryStringTypeKey] = true;
                }

                response.ContentEncoding = Encoding.UTF8;
                content = string.Format("/******************** Request handled by {0},{1},{2},{3} ********************/{4}", HttpContext.Current.Server.MachineName, DateTime.Now.ToString(), string.Concat(sw.ElapsedMilliseconds, " ms"), incache ? "In cache" : "Not in cache", Environment.NewLine) + content;
                var acceptEncoding = request.Headers["Accept-Encoding"];
                if (acceptEncoding != null && cs.IsCompress)
                {
                    if (acceptEncoding.Contains("gzip"))
                    {
                        Helper.Compress(content, response.OutputStream, Helper.CompressionType.GZip);
                        response.AppendHeader("Content-Encoding", "gzip");
                    }
                    else if (acceptEncoding.Contains("deflate"))
                    {
                        Helper.Compress(content, response.OutputStream, Helper.CompressionType.Delfate);
                        response.AppendHeader("Content-Encoding", "deflate");
                    }
                }
                else
                    response.Write(content);
            }
            catch (Exception ex)
            {
                try
                {
                    if (!Directory.Exists(context.Server.MapPath("")))
                    {
                        Directory.CreateDirectory(context.Server.MapPath(""));
                    }
                    File.AppendAllText(context.Server.MapPath("ResourceMerge_MergeHandler_Exception.txt"),
                                       DateTime.Now + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);
                }
                catch
                {
                }
            }
        }
    }
}