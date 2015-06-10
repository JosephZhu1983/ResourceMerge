namespace PreOptTool
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
            this.handlerAllSubFolder = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.selectSourceFolder = new System.Windows.Forms.Button();
            this.handlerOnlyNewFile = new System.Windows.Forms.CheckBox();
            this.messageList = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.doneFiles = new System.Windows.Forms.Label();
            this.sourceFolder = new System.Windows.Forms.Label();
            this.currentWorkingFile = new System.Windows.Forms.Label();
            this.totalFiles = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.enableLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // handlerAllSubFolder
            // 
            this.handlerAllSubFolder.AutoSize = true;
            this.handlerAllSubFolder.Checked = true;
            this.handlerAllSubFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handlerAllSubFolder.Location = new System.Drawing.Point(21, 51);
            this.handlerAllSubFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.handlerAllSubFolder.Name = "handlerAllSubFolder";
            this.handlerAllSubFolder.Size = new System.Drawing.Size(132, 23);
            this.handlerAllSubFolder.TabIndex = 1;
            this.handlerAllSubFolder.Text = "处理所有子文件夹";
            this.handlerAllSubFolder.UseVisualStyleBackColor = true;
            // 
            // selectSourceFolder
            // 
            this.selectSourceFolder.Location = new System.Drawing.Point(449, 14);
            this.selectSourceFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectSourceFolder.Name = "selectSourceFolder";
            this.selectSourceFolder.Size = new System.Drawing.Size(91, 28);
            this.selectSourceFolder.TabIndex = 2;
            this.selectSourceFolder.Text = "浏览……";
            this.selectSourceFolder.UseVisualStyleBackColor = true;
            this.selectSourceFolder.Click += new System.EventHandler(this.selectSourceFolder_Click);
            // 
            // handlerOnlyNewFile
            // 
            this.handlerOnlyNewFile.AutoSize = true;
            this.handlerOnlyNewFile.Checked = true;
            this.handlerOnlyNewFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handlerOnlyNewFile.Location = new System.Drawing.Point(160, 51);
            this.handlerOnlyNewFile.Name = "handlerOnlyNewFile";
            this.handlerOnlyNewFile.Size = new System.Drawing.Size(158, 23);
            this.handlerOnlyNewFile.TabIndex = 3;
            this.handlerOnlyNewFile.Text = "仅处理未处理过的文件";
            this.handlerOnlyNewFile.UseVisualStyleBackColor = true;
            // 
            // messageList
            // 
            this.messageList.Location = new System.Drawing.Point(21, 118);
            this.messageList.Multiline = true;
            this.messageList.Name = "messageList";
            this.messageList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageList.Size = new System.Drawing.Size(519, 216);
            this.messageList.TabIndex = 7;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(449, 80);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(91, 28);
            this.start.TabIndex = 8;
            this.start.Text = "开始处理";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // doneFiles
            // 
            this.doneFiles.AutoSize = true;
            this.doneFiles.Location = new System.Drawing.Point(19, 89);
            this.doneFiles.Name = "doneFiles";
            this.doneFiles.Size = new System.Drawing.Size(17, 19);
            this.doneFiles.TabIndex = 9;
            this.doneFiles.Text = "0";
            // 
            // sourceFolder
            // 
            this.sourceFolder.AutoSize = true;
            this.sourceFolder.Location = new System.Drawing.Point(17, 19);
            this.sourceFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sourceFolder.Name = "sourceFolder";
            this.sourceFolder.Size = new System.Drawing.Size(87, 19);
            this.sourceFolder.TabIndex = 12;
            this.sourceFolder.Text = "请选择文件夹";
            // 
            // currentWorkingFile
            // 
            this.currentWorkingFile.AutoSize = true;
            this.currentWorkingFile.Location = new System.Drawing.Point(118, 89);
            this.currentWorkingFile.Name = "currentWorkingFile";
            this.currentWorkingFile.Size = new System.Drawing.Size(0, 19);
            this.currentWorkingFile.TabIndex = 14;
            this.currentWorkingFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalFiles
            // 
            this.totalFiles.AutoSize = true;
            this.totalFiles.Location = new System.Drawing.Point(73, 89);
            this.totalFiles.Name = "totalFiles";
            this.totalFiles.Size = new System.Drawing.Size(17, 19);
            this.totalFiles.TabIndex = 15;
            this.totalFiles.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "/";
            // 
            // enableLog
            // 
            this.enableLog.AutoSize = true;
            this.enableLog.Checked = true;
            this.enableLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLog.Location = new System.Drawing.Point(324, 51);
            this.enableLog.Name = "enableLog";
            this.enableLog.Size = new System.Drawing.Size(145, 23);
            this.enableLog.TabIndex = 17;
            this.enableLog.Text = "保存错误信息到日志";
            this.enableLog.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 358);
            this.Controls.Add(this.enableLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalFiles);
            this.Controls.Add(this.currentWorkingFile);
            this.Controls.Add(this.sourceFolder);
            this.Controls.Add(this.doneFiles);
            this.Controls.Add(this.start);
            this.Controls.Add(this.messageList);
            this.Controls.Add(this.handlerOnlyNewFile);
            this.Controls.Add(this.selectSourceFolder);
            this.Controls.Add(this.handlerAllSubFolder);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脚本样式预处理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox handlerAllSubFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button selectSourceFolder;
        private System.Windows.Forms.CheckBox handlerOnlyNewFile;
        private System.Windows.Forms.TextBox messageList;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Label doneFiles;
        private System.Windows.Forms.Label sourceFolder;
        private System.Windows.Forms.Label currentWorkingFile;
        private System.Windows.Forms.Label totalFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox enableLog;
    }
}

