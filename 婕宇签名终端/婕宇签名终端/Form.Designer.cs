namespace 婕宇签名终端
{
    partial class Form
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
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_Sign = new System.Windows.Forms.Button();
            this.btn_Finger = new System.Windows.Forms.Button();
            this.btn_SignFinger = new System.Windows.Forms.Button();
            this.btn_ReadIDCard = new System.Windows.Forms.Button();
            this.btn_FaceComparison = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(47, 89);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "打开设备";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_Sign
            // 
            this.btn_Sign.Location = new System.Drawing.Point(172, 89);
            this.btn_Sign.Name = "btn_Sign";
            this.btn_Sign.Size = new System.Drawing.Size(75, 23);
            this.btn_Sign.TabIndex = 1;
            this.btn_Sign.Text = "签字";
            this.btn_Sign.UseVisualStyleBackColor = true;
            this.btn_Sign.Click += new System.EventHandler(this.btn_Sign_Click);
            // 
            // btn_Finger
            // 
            this.btn_Finger.Location = new System.Drawing.Point(305, 89);
            this.btn_Finger.Name = "btn_Finger";
            this.btn_Finger.Size = new System.Drawing.Size(75, 23);
            this.btn_Finger.TabIndex = 2;
            this.btn_Finger.Text = "指纹";
            this.btn_Finger.UseVisualStyleBackColor = true;
            this.btn_Finger.Click += new System.EventHandler(this.btn_Finger_Click);
            // 
            // btn_SignFinger
            // 
            this.btn_SignFinger.Location = new System.Drawing.Point(432, 89);
            this.btn_SignFinger.Name = "btn_SignFinger";
            this.btn_SignFinger.Size = new System.Drawing.Size(75, 23);
            this.btn_SignFinger.TabIndex = 3;
            this.btn_SignFinger.Text = "签名按捺";
            this.btn_SignFinger.UseVisualStyleBackColor = true;
            this.btn_SignFinger.Click += new System.EventHandler(this.btn_SignFinger_Click);
            // 
            // btn_ReadIDCard
            // 
            this.btn_ReadIDCard.Location = new System.Drawing.Point(547, 89);
            this.btn_ReadIDCard.Name = "btn_ReadIDCard";
            this.btn_ReadIDCard.Size = new System.Drawing.Size(75, 23);
            this.btn_ReadIDCard.TabIndex = 4;
            this.btn_ReadIDCard.Text = "读取身份证";
            this.btn_ReadIDCard.UseVisualStyleBackColor = true;
            // 
            // btn_FaceComparison
            // 
            this.btn_FaceComparison.Location = new System.Drawing.Point(664, 89);
            this.btn_FaceComparison.Name = "btn_FaceComparison";
            this.btn_FaceComparison.Size = new System.Drawing.Size(75, 23);
            this.btn_FaceComparison.TabIndex = 5;
            this.btn_FaceComparison.Text = "人脸比对";
            this.btn_FaceComparison.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(104, 224);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(576, 181);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_FaceComparison);
            this.Controls.Add(this.btn_ReadIDCard);
            this.Controls.Add(this.btn_SignFinger);
            this.Controls.Add(this.btn_Finger);
            this.Controls.Add(this.btn_Sign);
            this.Controls.Add(this.btn_open);
            this.Name = "Form";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_Sign;
        private System.Windows.Forms.Button btn_Finger;
        private System.Windows.Forms.Button btn_SignFinger;
        private System.Windows.Forms.Button btn_ReadIDCard;
        private System.Windows.Forms.Button btn_FaceComparison;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

