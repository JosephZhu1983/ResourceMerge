using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web.UI.WebControls;

namespace ResourceMerge.Core
{
    public class MergeModule : IHttpModule
    {
        private delegate string UrlConvert(string s);

        static MergeModule()
        {
            UrlConvertList.Add(UrlConvertor.AppSettings);
        }
        private static List<UrlConvert> UrlConvertList = new List<UrlConvert>();

        public void Dispose()
        {

        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(application_BeginRequest);
            application.PreRequestHandlerExecute += new EventHandler(application_PreRequestHandlerExecute);
        }

        private void application_BeginRequest(object sender, EventArgs e)
        {
            if (Interlocked.Exchange(ref ConfigProvider.NeedUpdate, 0) == 1)
            {
                ConfigProvider.Init();
            }
        }

        private void application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context == null) return;

            Page page = context.Handler as Page;
            if (page != null)
            {
                page.Init += new EventHandler(page_Init);
                //如果需要用 <%# 的话自己写Bind()
                //page.Load += new EventHandler(page_Load);
            }
        }

        private void page_Load(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context == null) return;

            Page page = context.Handler as Page;
            if (page == null) return;

            page.DataBind();
        }

        private PlaceHolder GetScriptHolder(Page page)
        {
            if (page.Master == null)
                return FindControlRecursive(page, ConfigProvider.Common.ScriptHolderID) as PlaceHolder;
            else
                return FindControlRecursive(page.Master, ConfigProvider.Common.ScriptHolderID) as PlaceHolder;
        }

        private PlaceHolder GetStyleHolder(Page page)
        {
            if (page.Master == null)
                return FindControlRecursive(page, ConfigProvider.Common.StyleHolderID) as PlaceHolder;
            else
                return FindControlRecursive(page.Master, ConfigProvider.Common.StyleHolderID) as PlaceHolder;
        }

        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            foreach (Control control in root.Controls)
            {
                Control found = FindControlRecursive(control, id);
                if (found != null)
                    return found;
            }

            return null;
        }


        private void page_Init(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context == null) return;

            Page page = context.Handler as Page;
            if (page == null) return;

            try
            {
                ConfigSet cs = ConfigProvider.GetConfigSetFromPageName(context.Request.AppRelativeCurrentExecutionFilePath);

                List<ResourceItem> resourceList = HttpContext.Current.Items[ConfigProvider.HttpItemResourceListKey] as List<ResourceItem>;
                if (resourceList == null) return;

                List<ResourceItem> sortedresourceList = new List<ResourceItem>();
                List<ResourceItem> begin = new List<ResourceItem>();
                begin.AddRange(resourceList.FindAll(r => r.RenderPriority < 0));
                begin.Sort();
                List<ResourceItem> end = new List<ResourceItem>();
                end.AddRange(resourceList.FindAll(r => r.RenderPriority > 0));
                end.Sort();
                sortedresourceList.AddRange(resourceList.FindAll(r => r.RenderPriority == 0));
                sortedresourceList.InsertRange(0, begin);
                sortedresourceList.AddRange(end);
                resourceList = sortedresourceList;

                foreach (ResourceItem r in resourceList)
                {
                    if (!r.IsMerge.HasValue)
                        r.IsMerge = r.ResourceType == ResourceType.Script ? cs.IsMergeScript : cs.IsMergeStyle;
                    if (!r.IsMinify.HasValue)
                        r.IsMinify = r.ResourceType == ResourceType.Script ? cs.IsMinifyScript : cs.IsMinifyStyle;
                    r.Charset = r.Charset ?? cs.Charset;
                    if (UrlConvertList != null && UrlConvertList.Count > 0)
                    {
                        foreach (UrlConvert d in UrlConvertList)
                        {
                            if (r.Url != null) r.Url = d(r.Url);
                        };
                    }
                }

                List<ResourceItem> styleList = resourceList.FindAll(item => item.ResourceType == ResourceType.Style);
                List<ResourceItem> needMergeStyleList = styleList.FindAll(item => item.IsMerge.Value);
                List<ResourceItem> noneedMergeStyleList = styleList;

                PlaceHolder styleHolder = GetStyleHolder(page);

                if (needMergeStyleList.Count > 1)
                {
                    noneedMergeStyleList = styleList.FindAll(item => !item.IsMerge.Value);

                    string mergedStyleToken = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Concat(needMergeStyleList.ConvertAll<string>(item => string.IsNullOrEmpty(item.Url) ? item.Content : item.Url + item.IsMinify).ToArray()), "md5");
                    string mergedStyleUrl = string.Format("{0}?{1}={2}&{3}={4}&{5}={6}", page.ResolveUrl(ConfigProvider.Common.MergeHandlerUrl), ConfigProvider.QueryStringTokenKey, mergedStyleToken, ConfigProvider.QueryStringTypeKey, "style", ConfigProvider.QueryStringConfigSetNameKey, cs.Name);
                    if (!cs.UseClientCache)
                        mergedStyleUrl = string.Format("{0}&{1}={2}", mergedStyleUrl, ConfigProvider.QueryStringTimestampKey, DateTime.Now.Ticks);
                    string mergedStyleTag = string.Format(ConfigProvider.StyleTemplate, mergedStyleUrl, cs.Charset);
                    ResourceCache.ResourceMapping.Set(mergedStyleToken, needMergeStyleList);

                    if (styleHolder != null)
                    {
                        styleHolder.Controls.Add(new LiteralControl(mergedStyleTag + Environment.NewLine));
                    }
                    else if (page.Header != null)
                    {
                        page.Header.Controls.AddAt(page.Header.Controls.Count, new LiteralControl(mergedStyleTag + Environment.NewLine));
                    }
                    else
                    {
                        page.Form.Controls.AddAt(0, new LiteralControl(mergedStyleTag + Environment.NewLine));
                    }
                }

                if (styleHolder != null)
                {
                    for (int i = 0; i < noneedMergeStyleList.Count; i++)
                    {
                        var item = noneedMergeStyleList[i];
                        if (!string.IsNullOrEmpty(item.Url))
                            styleHolder.Controls.Add(new LiteralControl(string.Format(ConfigProvider.StyleTemplate, page.ResolveUrl(item.Url), cs.Charset) + Environment.NewLine));
                        else
                            styleHolder.Controls.Add(new LiteralControl(string.Format(ConfigProvider.InlineStyleTemplate, Environment.NewLine + item.Content + Environment.NewLine, cs.Charset) + Environment.NewLine));

                    }
                }
                else if (page.Header != null)
                {
                    for (int i = 0; i < noneedMergeStyleList.Count; i++)
                    {
                        var item = noneedMergeStyleList[i];
                        if (!string.IsNullOrEmpty(item.Url))
                            page.Header.Controls.AddAt(page.Header.Controls.Count, new LiteralControl(string.Format(ConfigProvider.StyleTemplate, page.ResolveUrl(item.Url), cs.Charset) + Environment.NewLine));
                        else
                            page.Header.Controls.AddAt(page.Header.Controls.Count, new LiteralControl(string.Format(ConfigProvider.InlineStyleTemplate, Environment.NewLine + item.Content + Environment.NewLine, cs.Charset) + Environment.NewLine));

                    }
                }
                else
                {
                    for (int i = 0; i < noneedMergeStyleList.Count; i++)
                    {
                        var item = noneedMergeStyleList[i];
                        if (string.IsNullOrEmpty(item.Content))
                            page.Form.Controls.AddAt(0, new LiteralControl(string.Format(ConfigProvider.StyleTemplate, page.ResolveUrl(item.Url), cs.Charset) + Environment.NewLine));
                        else
                            page.Form.Controls.AddAt(0, new LiteralControl(string.Format(ConfigProvider.InlineStyleTemplate, Environment.NewLine + item.Content + Environment.NewLine, cs.Charset) + Environment.NewLine));

                    }
                }

                List<ResourceItem> scriptList = resourceList.FindAll(item => item.ResourceType == ResourceType.Script);
                List<ResourceItem> needMergeScriptList = scriptList.FindAll(item => item.IsMerge.Value);
                List<ResourceItem> noneedMergeScriptList = scriptList;

                PlaceHolder scriptHolder = GetScriptHolder(page);

                if (needMergeScriptList.Count > 1)
                {
                    noneedMergeScriptList = scriptList.FindAll(item => !item.IsMerge.Value);

                    string mergedScriptToken = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Concat(needMergeScriptList.ConvertAll<string>(item => string.IsNullOrEmpty(item.Url) ? item.Content : item.Url + item.IsMinify).ToArray()), "md5");
                    string mergedScriptUrl = string.Format("{0}?{1}={2}&{3}={4}&{5}={6}", page.ResolveUrl(ConfigProvider.Common.MergeHandlerUrl), ConfigProvider.QueryStringTokenKey, mergedScriptToken, ConfigProvider.QueryStringTypeKey, "script", ConfigProvider.QueryStringConfigSetNameKey, cs.Name);
                    if (!cs.UseClientCache)
                        mergedScriptUrl = string.Format("{0}&{1}={2}", mergedScriptUrl, ConfigProvider.QueryStringTimestampKey, DateTime.Now.Ticks);
                    string mergedScriptTag = string.Format(ConfigProvider.ScriptTemplate, mergedScriptUrl, cs.Charset);
                    ResourceCache.ResourceMapping.Set(mergedScriptToken, needMergeScriptList);

                    if (scriptHolder != null)
                    {
                        scriptHolder.Controls.Add(new LiteralControl(mergedScriptTag + Environment.NewLine));
                    }
                    else
                    {
                        page.Form.Controls.AddAt(page.Form.Controls.Count,
                                                 new LiteralControl(mergedScriptTag + Environment.NewLine));
                    }
                }

                for (int i = 0; i < noneedMergeScriptList.Count; i++)
                {
                    var item = noneedMergeScriptList[i];
                    LiteralControl control;
                    if (!string.IsNullOrEmpty(item.Url))
                        control =
                            new LiteralControl(
                                string.Format(ConfigProvider.ScriptTemplate, page.ResolveUrl(item.Url), item.Charset) +
                                Environment.NewLine);
                    else
                        control =
                            new LiteralControl(
                                string.Format(ConfigProvider.InlineScriptTemplate,
                                              Environment.NewLine + item.Content + Environment.NewLine, item.Charset) +
                                Environment.NewLine);

                    if (scriptHolder != null)
                        scriptHolder.Controls.Add(control);
                    else if (item.RenderLocation == RenderLocation.Auto ||
                             item.RenderLocation == RenderLocation.FormButtom)
                        page.Form.Controls.AddAt(page.Form.Controls.Count, control);
                    else if (item.RenderLocation == RenderLocation.Head && page.Header != null)
                        page.Header.Controls.AddAt(page.Header.Controls.Count, control);
                    else if (item.RenderLocation == RenderLocation.FormTop)
                        page.Form.Controls.AddAt(0, control);
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (!Directory.Exists(context.Server.MapPath("")))
                    {
                        Directory.CreateDirectory(context.Server.MapPath(""));
                    }
                    File.AppendAllText(context.Server.MapPath("ResourceMerge_MergeModule_Exception.txt"),
                        DateTime.Now + Environment.NewLine + ex.ToString() + Environment.NewLine + Environment.NewLine);
                }
                catch
                {

                }
            }
        }
    }
}
