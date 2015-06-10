using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Ajax.Utilities;
using ResourceMerge.Core;
using System.Configuration;

namespace PreOptTool
{
    public partial class Form1 : Form
    {
        private readonly string prefix = "/*begin*/";
        private readonly string suffix = "/*end*/";
        private readonly List<string> handleFiles = new List<string>();
        private bool calcDone = false;
        private string sourcePath = "";
        private delegate void A();
        private int handledFilesCount = 0;
        private object locker = new object();
        private string messages = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void selectSourceFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sourcePath = sourceFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                MessageBox.Show("请选择处理文件夹");
                return;
            }
            new Thread(() =>
            {
                this.Invoke(new A(() =>
                {
                    messageList.Text = "";
                    handleFiles.Clear();
                    calcDone = false;
                    handledFilesCount = 0;
                    messages = "";
                    handlerAllSubFolder.Enabled = handlerOnlyNewFile.Enabled = start.Enabled = enableLog.Enabled = false;
                    currentWorkingFile.Text = "正在计算需要处理的文件总数...";
                }));

                CalcHandleFiles(sourcePath);

                this.Invoke(new A(() =>
                {
                    totalFiles.Text = handleFiles.Count.ToString();
                }));

                if (handleFiles.Count == 0)
                {
                    this.Invoke(new A(() =>
                    {
                        currentWorkingFile.Text = "没有文件需要处理";
                    }));
                }
                else
                {
                    HandleFiles();
                    while (handledFilesCount != handleFiles.Count)
                    {
                        messageList.BeginInvoke(new A(() =>
                        {
                            messageList.Text = messages;
                        }));
                        Thread.Sleep(100);
                    }
                    this.Invoke(new A(() =>
                    {
                        currentWorkingFile.Text = "所有文件处理完毕";
                    }));
                    if (enableLog.Checked)
                    {
                        string dir = Path.Combine(sourcePath, "log");
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        File.WriteAllText(Path.Combine(dir, DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt"), messages);
                    }
                }

                this.Invoke(new A(() =>
                {
                    handlerAllSubFolder.Enabled = handlerOnlyNewFile.Enabled = start.Enabled = enableLog.Enabled = true;
                }));
            }).Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            sourcePath = sourceFolder.Text = @"F:\bb";
            if (Directory.Exists(sourcePath))
                Directory.Delete(sourcePath, true);
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(@"f:\aa", sourcePath);
#endif
        }

        private void CalcHandleFiles(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] css = di.GetFiles("*.css", handlerAllSubFolder.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            FileInfo[] js = di.GetFiles("*.js", handlerAllSubFolder.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(css);
            files.AddRange(js);
            string[] excludesFiles = ConfigurationManager.AppSettings["excludeFiles"].ToString().Split(',');
            files.ForEach(file =>
            {
                foreach (string excludeFile in excludesFiles)
                {
                    if (string.Compare(excludeFile, file.Name, true) == 0)
                    {
                        files.Remove(file);
                        break;
                    }
                }
            });
            foreach (var item in files)
            {
                if (handlerOnlyNewFile.Checked)
                {
                    string content = File.ReadAllText(item.FullName);
                    if (!content.StartsWith(prefix) || !content.EndsWith(suffix))
                        handleFiles.Add(item.FullName);
                }
                else
                {
                    handleFiles.Add(item.FullName);
                }
            }
            calcDone = true;
        }

        private void HandleFiles()
        {
            foreach (var file in handleFiles)
            {
                ThreadPool.QueueUserWorkItem(f =>
                {
                    string path = f as string;

                    try
                    {
                        string content = File.ReadAllText(path, Encoding.UTF8);
                        string mined = "";

                        Minifier min = new Minifier();

                        if (path.EndsWith(".js"))
                        {
                            JSParser parser = new JSParser(content, null);
                            parser.CompilerError += (s, e) =>
                            {
                                string n = e.Exception.Message;
                                n += string.Format("（第{0}行，第{1}列，错误代码：{2}）", e.Exception.Line, e.Exception.Column, e.Exception.ErrorSegment);
                                string msg = path + "：" + n + Environment.NewLine + Environment.NewLine;
                   
                                lock (locker)
                                    messages += msg;
                            };
                            Block scriptBlock = parser.Parse(null);
                            if (scriptBlock != null)
                                mined = scriptBlock.ToCode();
                            if (string.IsNullOrEmpty(mined))
                                mined = JsMinifier.GetMinifiedCode(content);
                        }
                        if (path.EndsWith(".css"))
                        {
                            CssParser parser = new CssParser();
                            parser.CssError += (s, e) =>
                            {
                                string n = e.Exception.Message;
                                n += string.Format("（第{0}行，第{1}个字符）", e.Exception.Line, e.Exception.Char);
                                string msg = path + "：" + n + Environment.NewLine + Environment.NewLine;
                    
                                lock (locker)
                                    messages += msg;
                            };
                            mined = parser.Parse(content);
                            if (string.IsNullOrEmpty(mined))
                                mined = CssMinifier.CssMinify(content, 0);
                        }

                        if (!mined.StartsWith(prefix))
                            mined = prefix + Environment.NewLine + mined;
                        if (!mined.EndsWith(suffix))
                            mined = mined + Environment.NewLine + suffix;

                        File.WriteAllText(path, mined, Encoding.UTF8);

                        Thread.Sleep(10);
                    }
                    catch (Exception ex)
                    {
                        string n = ex.Message;
                        string msg = path + "：" + n + Environment.NewLine + Environment.NewLine;
             
                        lock (locker)
                            messages += msg;
                    }
                    finally
                    {
                        Interlocked.Increment(ref handledFilesCount);
                        currentWorkingFile.BeginInvoke(new A(() =>
                        {
                            currentWorkingFile.Text = path;
                        }));
                        doneFiles.BeginInvoke(new A(() =>
                        {
                            doneFiles.Text = handledFilesCount.ToString();
                        }));
                    }
                }, file);
            }
        }
    }
}
