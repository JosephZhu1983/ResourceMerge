using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ResourceMerge.Core
{
    internal class UrlConvertor
    {
        internal static string AppSettings(string url)
        {
            string pre = "$$appSettings.";
            string suffix = "$$";
            if (url.Contains(pre))
            {
                string s = url.Substring(url.LastIndexOf(pre) + pre.Length);
                if (s.Contains(suffix))
                    s = s.Substring(0, s.IndexOf(suffix));
                return url.Replace(pre + s + suffix, ConfigurationManager.AppSettings[s]);
            }
            return url;
        }
    }
}
