namespace UIOA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btSerarchAllFile = new System.Windows.Forms.Button();
            this.btOpenFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSiteFileName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btStartReplace = new System.Windows.Forms.Button();
            this.tbCurrentFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFileTextAfter = new System.Windows.Forms.TextBox();
            this.tbFileTextBefore = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbAllFileCount = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btSerarchAllFile);
            this.groupBox1.Controls.Add(this.btOpenFolder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSiteFileName);
            this.groupBox1.Location = new System.Drawing.Point(27, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置信息";
            // 
            // btSerarchAllFile
            // 
            this.btSerarchAllFile.Location = new System.Drawing.Point(566, 28);
            this.btSerarchAllFile.Name = "btSerarchAllFile";
            this.btSerarchAllFile.Size = new System.Drawing.Size(166, 23);
            this.btSerarchAllFile.TabIndex = 6;
            this.btSerarchAllFile.Text = "查找文件";
            this.btSerarchAllFile.UseVisualStyleBackColor = true;
            this.btSerarchAllFile.Click += new System.EventHandler(this.btSerarchAllFile_Click);
            // 
            // btOpenFolder
            // 
            this.btOpenFolder.Location = new System.Drawing.Point(460, 29);
            this.btOpenFolder.Name = "btOpenFolder";
            this.btOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btOpenFolder.TabIndex = 2;
            this.btOpenFolder.Text = "选择";
            this.btOpenFolder.UseVisualStyleBackColor = true;
            this.btOpenFolder.Click += new System.EventHandler(this.btOpenFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择网站目录:";
            // 
            // tbSiteFileName
            // 
            this.tbSiteFileName.Location = new System.Drawing.Point(111, 30);
            this.tbSiteFileName.Name = "tbSiteFileName";
            this.tbSiteFileName.Size = new System.Drawing.Size(335, 21);
            this.tbSiteFileName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btStartReplace);
            this.groupBox2.Controls.Add(this.tbCurrentFileName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbFileTextAfter);
            this.groupBox2.Controls.Add(this.tbFileTextBefore);
            this.groupBox2.Location = new System.Drawing.Point(27, 340);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 321);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "页面测试";
            // 
            // btStartReplace
            // 
            this.btStartReplace.Location = new System.Drawing.Point(611, 35);
            this.btStartReplace.Name = "btStartReplace";
            this.btStartReplace.Size = new System.Drawing.Size(75, 23);
            this.btStartReplace.TabIndex = 6;
            this.btStartReplace.Text = "开始替换";
            this.btStartReplace.UseVisualStyleBackColor = true;
            this.btStartReplace.Click += new System.EventHandler(this.btStartReplace_Click);
            // 
            // tbCurrentFileName
            // 
            this.tbCurrentFileName.Location = new System.Drawing.Point(144, 37);
            this.tbCurrentFileName.Name = "tbCurrentFileName";
            this.tbCurrentFileName.Size = new System.Drawing.Size(448, 21);
            this.tbCurrentFileName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "当前处理的文件名:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "替换后:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "替换前:";
            // 
            // tbFileTextAfter
            // 
            this.tbFileTextAfter.Location = new System.Drawing.Point(380, 118);
            this.tbFileTextAfter.Multiline = true;
            this.tbFileTextAfter.Name = "tbFileTextAfter";
            this.tbFileTextAfter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFileTextAfter.Size = new System.Drawing.Size(342, 131);
            this.tbFileTextAfter.TabIndex = 1;
            // 
            // tbFileTextBefore
            // 
            this.tbFileTextBefore.Location = new System.Drawing.Point(25, 118);
            this.tbFileTextBefore.Multiline = true;
            this.tbFileTextBefore.Name = "tbFileTextBefore";
            this.tbFileTextBefore.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFileTextBefore.Size = new System.Drawing.Size(333, 131);
            this.tbFileTextBefore.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblMessage);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.lbAllFileCount);
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Location = new System.Drawing.Point(27, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(755, 196);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "列表";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(284, 164);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "批量合成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbAllFileCount
            // 
            this.lbAllFileCount.AutoSize = true;
            this.lbAllFileCount.Location = new System.Drawing.Point(29, 154);
            this.lbAllFileCount.Name = "lbAllFileCount";
            this.lbAllFileCount.Size = new System.Drawing.Size(71, 12);
            this.lbAllFileCount.TabIndex = 1;
            this.lbAllFileCount.Text = "共有0个文件";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(25, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(707, 112);
            this.listBox1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(852, 708);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(844, 683);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "合并CSS和JS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(844, 683);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "关于UI优化助手";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(41, 36);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(638, 430);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "简介";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "替换以page,ascx和master为文件扩展名的文件.合并文件中的css和js,减少http请求.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 752);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "UI优化助手";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSiteFileName;
        private System.Windows.Forms.Button btOpenFolder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btSerarchAllFile;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lbAllFileCount;
        private System.Windows.Forms.TextBox tbFileTextBefore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFileTextAfter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbCurrentFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btStartReplace;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
    }
}

