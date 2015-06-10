using System;
using System.Collections.Generic;
using System.Text;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System.Web;
using System.Web.Caching;
using System.Collections;
namespace ResourceMerge.Core
{
    internal class ResourceCache
    {
        internal static MemcacheDictionary<List<ResourceItem>> ResourceMapping = new MemcacheDictionary<List<ResourceItem>>("ResourceMapping_");
        private static MemcacheDictionary<MergedResource> CachedResourceList = new MemcacheDictionary<MergedResource>("CachedResourceList_");
        private static MemcacheDictionary<string> CachedResourceContent = new MemcacheDictionary<string>("CachedResourceContent_");

        internal static class ResourceContent
        {
            internal static string Get(string token)
            {
                return CachedResourceContent.Get(token);
            }

            internal static void Set(string type, string configSet, string token, string content, int duration)
            {
                MergedResource mr = new MergedResource
                {
                    Token = token,
                    Type = type,
                    CacheKey = CachedResourceContent.CacheKeyPrefix + token,
                    ContentLength = content.Length,
                    CreateDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddSeconds(duration),
                    ConfigSetName = configSet,
                    InCache = true,
                };
                CachedResourceContent.Set(token, content, TimeSpan.FromSeconds(duration));
                CachedResourceList.Set(token, mr);
            }

            internal static void Clear()
            {
                CachedResourceContent.RemoveAll();
                CachedResourceList.RemoveAll();
            }

            internal static string Info()
            {
                List<MergedResource> data = CachedResourceList.GetAll();
                data.Sort();
                foreach (var item in data)
                {
                    item.InCache = CachedResourceContent.Exists(item.Token);
                }
                StringBuilder info = new StringBuilder();
                info.Append("<div style='font-family:Courier New; font-size:10pt'>");
                info.AppendFormat("Current time : {0} ", DateTime.Now.ToString());
                info.AppendFormat("Current machine : {0} ", Environment.MachineName);
                info.Append("</div><br/>");
                info.Append("<table style='font-family:Courier New; font-size:10pt'>");
                info.Append("<tr>");
                info.AppendFormat("<td><strong>{0}</strong></td>", "View");
                foreach (var column in typeof(MergedResource).GetProperties())
                {
                    if (column.Name == "Token") continue;
                    info.AppendFormat("<td><strong>{0}</strong></td>", column.Name);
                }
                info.Append("</tr>");
                
                foreach (var item in data)
                {
                    info.Append("<tr>");
                    info.AppendFormat("<td><a href='{0}' target='_blank'>View</a></td>", string.Format("{0}?{1}={2}&{3}={4}&{5}={6}", HttpContext.Current.Request.Url.AbsolutePath, ConfigProvider.QueryStringTokenKey, item.Token , ConfigProvider.QueryStringTypeKey, item.Type, ConfigProvider.QueryStringConfigSetNameKey, item.ConfigSetName));
                    foreach (var column in typeof(MergedResource).GetProperties())
                    {
                        if (column.Name == "Token") continue;
                        object value = column.GetValue(item, null);
                        info.AppendFormat("<td>{0}</td>", value);
                    }
                    info.Append("</tr>");
                }
                info.Append("</table>");
                return info.ToString();
            }
        }
    }
}
