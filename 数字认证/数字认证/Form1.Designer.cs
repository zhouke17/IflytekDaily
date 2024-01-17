namespace 数字认证
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axASAppCom1 = new AxASAppComLib.AxASAppCom();
            ((System.ComponentModel.ISupportInitialize)(this.axASAppCom1)).BeginInit();
            this.SuspendLayout();
            // 
            // axASAppCom1
            // 
            this.axASAppCom1.Enabled = true;
            this.axASAppCom1.Location = new System.Drawing.Point(74, 129);
            this.axASAppCom1.Name = "axASAppCom1";
            this.axASAppCom1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axASAppCom1.OcxState")));
            this.axASAppCom1.Size = new System.Drawing.Size(100, 50);
            this.axASAppCom1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.axASAppCom1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axASAppCom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxASAppComLib.AxASAppCom axASAppCom1;
    }
}

