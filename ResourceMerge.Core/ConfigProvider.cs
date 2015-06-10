using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Web;
using System.Configuration;
using System.Web.Caching;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;

namespace ResourceMerge.Core
{
    internal static class ConfigProvider
    {
        internal static int NeedUpdate = 0;

        internal static readonly string HttpItemResourceListKey = "HttpItemResourceListKey";

        internal static readonly string QueryStringTokenKey = "token";

        internal static readonly string QueryStringTypeKey = "type";

        internal const string QueryStringActionKey = "action";

        internal const string QueryStringClearCacheValue = "clear";

        internal const string QueryStringShowCacheInfoValue = "info";

        internal static readonly string QueryStringTimestampKey = "v";

        internal static readonly string QueryStringConfigSetNameKey = "cs";

        internal static readonly string StyleTemplate = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" charset=\"{1}\"/>";

        internal static readonly string InlineStyleTemplate = "<style type=\"text/css\" charset=\"{1}\">{0}</style>";

        internal static readonly string ScriptTemplate = "<script language=\"javascript\" type=\"text/javascript\" src=\"{0}\" charset=\"{1}\"></script>";

        internal static readonly string InlineScriptTemplate = "<script language=\"javascript\" type=\"text/javascript\" charset=\"{1}\">{0}</script>";

        private static readonly string CacheResourceMergeConfigFilePathDependencyKey = "CacheResourceMergeConfigFilePathDependency";

        private static string ResourceMergeConfigFilePath = "~/Config/ResourceMerge.config";

        private static readonly string WebConfigResourceMergeConfigFilePathKey = "ResourceMergeConfigFilePath";

        private static List<ConfigSet> ConfigSetList = new List<ConfigSet>();
        private static Dictionary<string, string> PageConfigMapping = new Dictionary<string, string>();
        private static object locker = new object();

        private static void ResourceMergeConfigFileUpdated(string key, object value, CacheItemRemovedReason reason)
        {
            Interlocked.Exchange(ref NeedUpdate, 1);
        }

        internal static class Common
        {
            internal static string MergeHandlerUrl = "~/MergeHandler.ashx";

            internal static string CacheKeyPrefix = "ResourceMerge_";

            internal static string GlobalConfigSetName = "Default";

            internal static string ScriptHolderID = "ScriptHolder";

            internal static string StyleHolderID = "StyleHolder";
        }

        internal static ConfigSet GetConfigSetFromPageName(string pageName)
        {
            lock (locker)
            {
                string configSetName = ConfigProvider.Common.GlobalConfigSetName;
                foreach (string s in PageConfigMapping.Keys)
                {
                    if (Regex.IsMatch(pageName, s, RegexOptions.IgnoreCase) && ConfigSetList.Exists(c => c.Name == PageConfigMapping[s]))
                    {
                        configSetName = PageConfigMapping[s];
                        break;
                    }
                }
                return ConfigSetList.Find(c => c.Name == configSetName) ?? ConfigSetList.Find(c => c.Name == "Default");
            }
        }

        internal static ConfigSet GetConfigSetFromConfigSetName(string csName)
        {
            lock (locker)
            {
                return ConfigSetList.Find(c => c.Name == csName) ?? ConfigSetList.Find(c => c.Name == "Default");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static void Init()
        {
            if (HttpContext.Current == null || HttpContext.Current.Server == null || HttpContext.Current.Cache == null) return;

            if (ConfigurationManager.AppSettings[WebConfigResourceMergeConfigFilePathKey] != null)
            {
                ResourceMergeConfigFilePath = ConfigurationManager.AppSettings[WebConfigResourceMergeConfigFilePathKey];
            }

            string configFilePath = HttpContext.Current.Server.MapPath(ResourceMergeConfigFilePath);

            HttpContext.Current.Cache.Insert(CacheResourceMergeConfigFilePathDependencyKey, "",
                new CacheDependency(configFilePath),
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default, ResourceMergeConfigFileUpdated);

            XmlDocument xml = new XmlDocument();
            xml.Load(configFilePath);

            XmlNode resourceMerge = xml.SelectSingleNode("ResourceMerge");
            if (resourceMerge != null)
            {
                XmlNode common = resourceMerge.SelectSingleNode("Common");
                if (common != null)
                {
                    if (common.SelectSingleNode("MergeHandlerUrl") != null)
                    {
                        Common.MergeHandlerUrl = common.SelectSingleNode("MergeHandlerUrl").InnerText;
                    }

                    if (common.SelectSingleNode("CacheKeyPrefix") != null)
                    {
                        Common.CacheKeyPrefix = common.SelectSingleNode("CacheKeyPrefix").InnerText;
                    }

                    if (common.SelectSingleNode("GlobalConfigSetName") != null)
                    {
                        Common.GlobalConfigSetName = common.SelectSingleNode("GlobalConfigSetName").InnerText;
                    }

                    if (common.SelectSingleNode("ScriptHolderID") != null)
                    {
                        Common.ScriptHolderID = common.SelectSingleNode("ScriptHolderID").InnerText;
                    }

                    if (common.SelectSingleNode("StyleHolderID") != null)
                    {
                        Common.StyleHolderID = common.SelectSingleNode("StyleHolderID").InnerText;
                    }
                }

                lock (locker)
                {

                    ConfigSetList.Clear();
                    PageConfigMapping.Clear();

                    ConfigSetList.Add(new ConfigSet());
                    XmlNode pages = resourceMerge.SelectSingleNode("Pages");
                    if (pages != null)
                    {
                        XmlNodeList pageList = pages.SelectNodes("Page");
                        foreach (XmlNode page in pageList)
                        {
                            if (page.Attributes["Url"] != null && !PageConfigMapping.ContainsKey(page.Attributes["Url"].InnerText))
                            {
                                PageConfigMapping.Add(page.Attributes["Url"].InnerText, page.Attributes["ConfigSetName"] == null ? string.Empty : page.Attributes["ConfigSetName"].InnerText);
                            }
                        }
                    }
                    XmlNode configSets = resourceMerge.SelectSingleNode("ConfigSets");
                    if (configSets != null)
                    {
                        XmlNodeList configSetList = configSets.SelectNodes("ConfigSet");
                        foreach (XmlNode configSet in configSetList)
                        {
                            if (configSet is XmlComment) continue;

                            ConfigSet cs = new ConfigSet();

                            if (configSet.SelectSingleNode("Name") != null)
                            {
                                cs.Name = configSet.SelectSingleNode("Name").InnerText;

                                if (configSet.SelectSingleNode("Charset") != null)
                                {
                                    cs.Charset = configSet.SelectSingleNode("Charset").InnerText;
                                }

                                if (configSet.SelectSingleNode("IsMergeStyle") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("IsMergeStyle").InnerText,
                                        out cs.IsMergeStyle);
                                }

                                if (configSet.SelectSingleNode("IsMinifyStyle") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("IsMinifyStyle").InnerText,
                                        out cs.IsMinifyStyle);
                                }

                                if (configSet.SelectSingleNode("IsMergeScript") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("IsMergeScript").InnerText,
                                        out cs.IsMergeScript);
                                }

                                if (configSet.SelectSingleNode("IsMinifyScript") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("IsMinifyScript").InnerText,
                                        out cs.IsMinifyScript);
                                }

                                if (configSet.SelectSingleNode("IsCompress") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("IsCompress").InnerText,
                                        out cs.IsCompress);
                                }

                                if (configSet.SelectSingleNode("GetCompressedRemoteResource") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("GetCompressedRemoteResource").InnerText,
                                        out cs.GetCompressedRemoteResource);
                                }

                                if (configSet.SelectSingleNode("UseServerCache") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("UseServerCache").InnerText,
                                        out cs.UseServerCache);
                                }

                                if (configSet.SelectSingleNode("UseClientCache") != null)
                                {
                                    bool.TryParse(configSet.SelectSingleNode("UseClientCache").InnerText,
                                        out cs.UseClientCache);
                                }

                                if (configSet.SelectSingleNode("ServerCacheDuration") != null)
                                {
                                    int.TryParse(configSet.SelectSingleNode("ServerCacheDuration").InnerText,
                                        out cs.ServerCacheDuration);
                                }

                                if (configSet.SelectSingleNode("ClientCacheDuration") != null)
                                {
                                    int.TryParse(configSet.SelectSingleNode("ClientCacheDuration").InnerText,
                                        out cs.ClientCacheDuration);
                                }

                                if (configSet.SelectSingleNode("StaticResrouceSuffix") != null)
                                {
                                    cs.StaticResrouceSuffix = configSet.SelectSingleNode("StaticResrouceSuffix").InnerText;
                                }

                                if (configSet.SelectSingleNode("StaticResroucePreffix") != null)
                                {
                                    cs.StaticResrouceSuffix = configSet.SelectSingleNode("StaticResroucePreffix").InnerText;
                                }

                                ConfigSetList.Add(cs);
                            }

                        }
                    }
                }
            }
        }
    }
}
