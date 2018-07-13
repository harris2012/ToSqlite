namespace Mdf2Sqlite
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MdfFilePathTextBox = new System.Windows.Forms.TextBox();
            this.SqliteFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MdfFilePathTextBox
            // 
            this.MdfFilePathTextBox.Location = new System.Drawing.Point(114, 25);
            this.MdfFilePathTextBox.Name = "MdfFilePathTextBox";
            this.MdfFilePathTextBox.Size = new System.Drawing.Size(674, 21);
            this.MdfFilePathTextBox.TabIndex = 0;
            // 
            // SqliteFilePathTextBox
            // 
            this.SqliteFilePathTextBox.Location = new System.Drawing.Point(114, 94);
            this.SqliteFilePathTextBox.Name = "SqliteFilePathTextBox";
            this.SqliteFilePathTextBox.Size = new System.Drawing.Size(674, 21);
            this.SqliteFilePathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "MDF 文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sqlite 文件";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(114, 152);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "开始";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SqliteFilePathTextBox);
            this.Controls.Add(this.MdfFilePathTextBox);
            this.Name = "MainForm";
            this.Text = "Mdf to Sqlite 转换工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MdfFilePathTextBox;
        private System.Windows.Forms.TextBox SqliteFilePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btStart;
    }
}

