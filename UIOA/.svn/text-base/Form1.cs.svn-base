using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Text.RegularExpressions;
using UIOA.Common;

namespace UIOA
{
    public partial class Form1 : Form
    {

        //字段
        int m_allFiles = 0;        //文件总数
        ArrayList m_pathList = new ArrayList();//包含所有文件路径的数组
        string m_currentFileContent = "";//当前的文件内容
        string m_currentFileContentAfter = "";//替换后的文件的内容




        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开网站目录.选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOpenFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "请选择网站目录";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbSiteFileName.Text = dlg.SelectedPath;//设置网站目录
                }
            }
        }

        /// <summary>
        /// 查找目录下的指定的文件类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSerarchAllFile_Click(object sender, EventArgs e)
        {
            if (tbSiteFileName.Text != "")
            {
                listBox1.Items.Clear();
                m_pathList.Clear();
                m_allFiles = 0;
                GetAllFiles(tbSiteFileName.Text);
            }

        }


        /// <summary>
        /// 得到指定目录下的所有文件
        /// </summary>
        /// <param name="path">路径</param>
        private void GetAllFiles(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                foreach (string filename in files)
                {
                    FileInfo inf = new FileInfo(filename);
                    if (IsUseFileExtension(inf.Name))
                    {
                        m_allFiles += 1;
                        listBox1.Items.Add(filename);
                        m_pathList.Add(filename);
                        lbAllFileCount.Text = "共有" + m_allFiles + "个文件";
                    }
                }
            }
        }


        /// <summary>
        /// 判断文件的扩展名
        /// </summary>
        /// <param name="strExtension"></param>
        /// <returns></returns>
        private bool IsUseFileExtension(string strExtension)
        {

            if (strExtension.LastIndexOf(".") >= 0)
            {
                strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
            }

            if (!string.IsNullOrEmpty(strExtension))
            {
                strExtension = strExtension.ToLowerInvariant();
                if (strExtension == ".aspx" || strExtension == ".ascx" || strExtension == ".master")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 替换标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStartReplace_Click(object sender, EventArgs e)
        {
            if (tbCurrentFileName.Text != "")
            {
                //打开文件
                this.m_currentFileContent = OpenFile(tbCurrentFileName.Text);

                tbFileTextBefore.Text = this.m_currentFileContent;

                //替换文件内容
                this.m_currentFileContentAfter = ReplaceStyleAndScriptInPage(this.m_currentFileContent);

                //把<%=替换成<%#
                this.m_currentFileContentAfter = ReplaceDenghaoToJinghao(tbCurrentFileName.Text, this.m_currentFileContentAfter);

                tbFileTextAfter.Text = this.m_currentFileContentAfter;
            }

        }

        /// <summary>
        /// 替换一个页面中的所有样式
        /// </summary>
        /// <param name="FileName"></param>
        private void RepalceOneFile(string FileName)
        {

            //打开文件
            this.m_currentFileContent = OpenFile(FileName);

            //替换文件内容
            this.m_currentFileContentAfter = ReplaceStyleAndScriptInPage(this.m_currentFileContent);

            //把<%=替换成<%#
            this.m_currentFileContentAfter = ReplaceDenghaoToJinghao(FileName, this.m_currentFileContentAfter);


            //保存文件
            if (this.m_currentFileContent != this.m_currentFileContentAfter)
            {
                WriteAllText(FileName, this.m_currentFileContentAfter);
                LogService.WriteLog(LogServiceType.Info, FileName + " 已修改,并重新保存");
            }
        }




        /// <summary>
        /// 替换网页中的样式和js,返回修改后的网页
        /// </summary>
        /// <param name="htmlContent">网页源码</param>
        /// <returns></returns>
        private string ReplaceStyleAndScriptInPage(string htmlContent)
        {
            //替换样式表文件
            htmlContent = ReplaceCssToResource(htmlContent);

            //替换样式片段
            htmlContent = ReplaceCssPartToResource(htmlContent);

            //替换脚本文件
            htmlContent = ReplaceScriptToResource(htmlContent);

            //替换脚本片段
            htmlContent = ReplaceScriptPartToResource(htmlContent);





            return htmlContent;
        }


        /// <summary>
        /// 把css样式替换成新的Resource
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private string ReplaceCssToResource(string htmlContent)
        {
            string oldVaule = "";
            string newValue = "";
            string newCssUrl = "";
            string pat = "<link[^%<]*/>";//查找所有样式的正则表达式
            MatchCollection mc = Regex.Matches(htmlContent, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            for (int i = 0; i < mc.Count; i++)
            {
                oldVaule = mc[i].Value;
                newCssUrl = GetCssUrl(oldVaule);
                if (!string.IsNullOrEmpty(newCssUrl))
                {
                    newValue = GetResourceControlCode(newCssUrl, GetChartSet(oldVaule), "Style");
                    htmlContent = htmlContent.Replace(oldVaule, newValue);//替换样式
                }
            }
            return htmlContent;

        }

        /// <summary>
        /// 把页面中的css片段替换成新的Resource
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private string ReplaceCssPartToResource(string htmlContent)
        {
            string oldVaule = "";
            string newValue = "";
            string newContent = "";
            string pat = "<style[^>]*>[^%<]*</style>";//查找所有样式的正则表达式
            MatchCollection mc = Regex.Matches(htmlContent, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            for (int i = 0; i < mc.Count; i++)
            {
                oldVaule = mc[i].Value;
                newContent = GetStyleContent(oldVaule);
                if (!string.IsNullOrEmpty(newContent))
                {
                    newValue = GetResourcePartControlCode(newContent, "Style");
                    htmlContent = htmlContent.Replace(oldVaule, newValue);//替换样式
                }
            }
            return htmlContent;
        }

        /// <summary>
        /// 获取样式表片段的内容
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetStyleContent(string htmlCode)
        {

            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = "<style[^>]*>(?<objValue>[^%<]*)</style>";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        strValue += NextMatch.Groups["objValue"].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;

        }


        /// <summary>
        /// 获取css片段的文件类型
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetCssTypePart(string htmlCode)
        {

            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = "(type=\"(?<objValue>[^%\"]*)\")";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        strValue += NextMatch.Groups["objValue"].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;

        }




        /// <summary>
        /// 把js替换成新的Resource
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private string ReplaceScriptToResource(string htmlContent)
        {
            string oldVaule = "";
            string newValue = "";
            string newScriptUrl = string.Empty;
            string pat = @"<script[^<%>]*></script>";//查找所有样式的正则表达式
            MatchCollection mc = Regex.Matches(htmlContent, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (int i = 0; i < mc.Count; i++)
            {
                oldVaule = mc[i].Value;
                if (CanReplace(oldVaule))
                {
                    newScriptUrl = GetScriptUrl(oldVaule);
                    if (!string.IsNullOrEmpty(newScriptUrl))
                    {
                        newValue = GetResourceControlCode(newScriptUrl, GetChartSet(oldVaule), "Script");
                        htmlContent = htmlContent.Replace(oldVaule, newValue);//替换样式
                    }
                }
            }
            return htmlContent;

        }

        /// <summary>
        /// 是否能替换
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果有js框架就返回false,否则返回true</returns>
        private bool CanReplace(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.ToLowerInvariant().IndexOf("jquery") > 0) return false;
            if (value.ToLowerInvariant().IndexOf("prototype") > 0) return false;
            return true;
        }



        /// <summary>
        /// 把页面中js片段替换成新的Resource
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private string ReplaceScriptPartToResource(string htmlContent)
        {
            string oldVaule = "";
            string newValue = "";
            string newScriptUrl = string.Empty;
            string pat = @"<script[^<%>]*>\s+[^%<]*</script>";//查找所有脚本片段的正则表达式
            MatchCollection mc = Regex.Matches(htmlContent, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (int i = 0; i < mc.Count; i++)
            {
                oldVaule = mc[i].Value;
                newScriptUrl = GetScriptContent(oldVaule);
                if (!string.IsNullOrEmpty(newScriptUrl))
                {
                    newValue = GetResourcePartControlCode(newScriptUrl, "Script");
                    htmlContent = htmlContent.Replace(oldVaule, newValue);//替换样式
                }
            }
            return htmlContent;
        }

        /// <summary>
        /// 获取js脚本的src地址
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetScriptUrl(string htmlCode)
        {
            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = "src=\"(?<objValue>[^\"]*)\"";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        strValue += NextMatch.Groups["objValue"].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;

        }

        /// <summary>
        /// 获取js代码段的内容
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetScriptContent(string htmlCode)
        {
            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = @"<script[^>]*>(?<objValue>[^%<]*)</script>";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        if (NextMatch.Groups["objValue"].Value.IndexOf(';') > 0)//判断是否有分号
                        {
                            if (NextMatch.Groups["objValue"].Value.IndexOf(".domain") > 0)
                            {
                                strValue += NextMatch.Groups["objValue"].Value + ",";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;

        }


        /// <summary>
        /// 获取样式表中url地址
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetCssUrl(string htmlCode)
        {
            string strValue = string.Empty;
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = "(href=\"(?<objValue>[^%\"]*)\")|(href='(?<objValue>[^%\"]*)')";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        strValue += NextMatch.Groups["objValue"].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;

        }

        /// <summary>
        /// 获取样式或者js的编码类型
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        private string GetChartSet(string htmlCode)
        {
            string strValue = "";
            if (!string.IsNullOrEmpty(htmlCode))
            {
                string pat = "charset=\"(?<objValue>[^\"]*)\"";
                MatchCollection mc = Regex.Matches(htmlCode, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                foreach (Match NextMatch in mc)
                {
                    if (NextMatch != null)
                    {
                        strValue += NextMatch.Groups["objValue"].Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.TrimEnd(',');
                }
            }
            return strValue;
        }

        /// <summary>
        /// 获取样式表和js的控件地址
        /// </summary>
        /// <param name="urlAddress">url地址</param>
        /// <param name="chartSet">编码格式:gb2312,utf-8</param>
        /// <param name="ResourceType">资源类型:Script,Style</param>
        /// <returns></returns>
        private string GetResourceControlCode(string urlAddress, string chartSet, string ResourceType)
        {
            if (string.IsNullOrEmpty(urlAddress)) { return ""; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<ResourceMerge:RawResource ID=\"d");
            sb.Append(Guid.NewGuid().ToString().Replace("-", ""));//id处理
            sb.Append("\" Url=\"");
            sb.Append(urlAddress);//url地址处理
            sb.Append("\"");

            //编码格式处理
            if (!string.IsNullOrEmpty(chartSet))
            {
                sb.Append(" charset=\"");
                sb.Append(chartSet);
                sb.Append("\"");
            }

            //资源类型处理
            if (!string.IsNullOrEmpty(ResourceType))
            {
                sb.Append(" ResourceType=\"");
                sb.Append(ResourceType);
                sb.Append("\"");
            }

            sb.Append(" runat=\"server\" />");
            return sb.ToString();
        }


        /// <summary>
        /// 处理style片段和Script片段
        /// </summary>
        /// <param name="content"></param>
        /// <param name="ResourceType"></param>
        /// <returns></returns>
        private string GetResourcePartControlCode(string content, string ResourceType)
        {

            if (string.IsNullOrEmpty(content)) { return ""; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<ResourceMerge:RawResource ID=\"d");
            sb.Append(Guid.NewGuid().ToString().Replace("-", ""));//id处理
            sb.Append("\"");

            //资源类型处理
            if (!string.IsNullOrEmpty(ResourceType))
            {
                sb.Append(" ResourceType=\"");
                sb.Append(ResourceType);
                sb.Append("\"");
            }

            sb.Append(" runat=\"server\" > ");

            //内容
            sb.Append(content);

            sb.Append(" </ResourceMerge:RawResource>");


            return sb.ToString();
        }



        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName">完整的文件名</param>
        /// <returns>文件内容</returns>
        public static string OpenFile(string fileName)
        {
            return File.ReadAllText(fileName, Encoding.Default);
        }

        /// <summary>
        /// 往文件中写入内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        public static void WriteAllText(string fileName, string contents)
        {
            //Encoding fileEncoding = TxtFileEncoding.GetEncoding(fileName, Encoding.GetEncoding("GB2312"));//取得这txt文件的编码
            //File.WriteAllText(fileName, contents, fileEncoding);
            File.WriteAllText(fileName, contents, Encoding.Default);
        }




        /// <summary>
        /// 批量合成所有页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_pathList != null && m_pathList.Count > 0)
            {
                for (int i = 0; i < m_pathList.Count; i++)
                {
                    RepalceOneFile(m_pathList[i].ToString());
                }
            }
            lblMessage.Text = "全部替换成功!共替换文件:" + m_pathList.Count + "个";
            m_pathList.Clear();

        }





        // 把已aspx和master为扩展名的文件中<%=替换成<%#号
        private string ReplaceDenghaoToJinghao(string fileName, string htmlContent)
        {
            if(string.IsNullOrEmpty(fileName))return "";
            if(string.IsNullOrEmpty(htmlContent))return "";
            if (fileName.ToLowerInvariant().LastIndexOf(".aspx") > 0 || fileName.ToLowerInvariant().LastIndexOf(".master") > 0)
            {
                htmlContent = htmlContent.Replace("<%=", "<%#");

            }
            return htmlContent;
        }



    }
}
