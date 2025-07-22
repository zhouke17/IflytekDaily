using System;

namespace WebsocketClient
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
            this.panel_top = new System.Windows.Forms.Panel();
            this.summaryNote = new System.Windows.Forms.Button();
            this.fullNote = new System.Windows.Forms.Button();
            this.realtiemTransparent = new System.Windows.Forms.Button();
            this.courtState = new System.Windows.Forms.Button();
            this.closeCourt = new System.Windows.Forms.Button();
            this.recoveryCourt = new System.Windows.Forms.Button();
            this.pauseCourt = new System.Windows.Forms.Button();
            this.openCourt = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.raiseClient = new System.Windows.Forms.Button();
            this.panel_main = new System.Windows.Forms.Panel();
            this.btn_start = new System.Windows.Forms.Button();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.btn_start);
            this.panel_top.Controls.Add(this.summaryNote);
            this.panel_top.Controls.Add(this.fullNote);
            this.panel_top.Controls.Add(this.realtiemTransparent);
            this.panel_top.Controls.Add(this.courtState);
            this.panel_top.Controls.Add(this.closeCourt);
            this.panel_top.Controls.Add(this.recoveryCourt);
            this.panel_top.Controls.Add(this.pauseCourt);
            this.panel_top.Controls.Add(this.openCourt);
            this.panel_top.Controls.Add(this.restart);
            this.panel_top.Controls.Add(this.close);
            this.panel_top.Controls.Add(this.raiseClient);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(1302, 100);
            this.panel_top.TabIndex = 0;
            // 
            // summaryNote
            // 
            this.summaryNote.Location = new System.Drawing.Point(1185, 43);
            this.summaryNote.Name = "summaryNote";
            this.summaryNote.Size = new System.Drawing.Size(75, 23);
            this.summaryNote.TabIndex = 10;
            this.summaryNote.Text = "摘要笔录";
            this.summaryNote.UseVisualStyleBackColor = true;
            this.summaryNote.Click += new System.EventHandler(this.summaryNote_Click);
            // 
            // fullNote
            // 
            this.fullNote.Location = new System.Drawing.Point(1080, 43);
            this.fullNote.Name = "fullNote";
            this.fullNote.Size = new System.Drawing.Size(75, 23);
            this.fullNote.TabIndex = 9;
            this.fullNote.Text = "全文笔录";
            this.fullNote.UseVisualStyleBackColor = true;
            this.fullNote.Click += new System.EventHandler(this.fullNote_Click);
            // 
            // realtiemTransparent
            // 
            this.realtiemTransparent.Location = new System.Drawing.Point(839, 43);
            this.realtiemTransparent.Name = "realtiemTransparent";
            this.realtiemTransparent.Size = new System.Drawing.Size(75, 23);
            this.realtiemTransparent.TabIndex = 8;
            this.realtiemTransparent.Text = "实时转写";
            this.realtiemTransparent.UseVisualStyleBackColor = true;
            this.realtiemTransparent.Click += new System.EventHandler(this.realtiemTransparent_Click);
            // 
            // courtState
            // 
            this.courtState.Location = new System.Drawing.Point(959, 43);
            this.courtState.Name = "courtState";
            this.courtState.Size = new System.Drawing.Size(75, 23);
            this.courtState.TabIndex = 7;
            this.courtState.Text = "庭审状态";
            this.courtState.UseVisualStyleBackColor = true;
            this.courtState.Click += new System.EventHandler(this.courtState_Click);
            // 
            // closeCourt
            // 
            this.closeCourt.Location = new System.Drawing.Point(722, 43);
            this.closeCourt.Name = "closeCourt";
            this.closeCourt.Size = new System.Drawing.Size(75, 23);
            this.closeCourt.TabIndex = 6;
            this.closeCourt.Text = "闭庭";
            this.closeCourt.UseVisualStyleBackColor = true;
            this.closeCourt.Click += new System.EventHandler(this.closeCourt_Click);
            // 
            // recoveryCourt
            // 
            this.recoveryCourt.Location = new System.Drawing.Point(603, 43);
            this.recoveryCourt.Name = "recoveryCourt";
            this.recoveryCourt.Size = new System.Drawing.Size(75, 23);
            this.recoveryCourt.TabIndex = 5;
            this.recoveryCourt.Text = "再次开庭";
            this.recoveryCourt.UseVisualStyleBackColor = true;
            this.recoveryCourt.Click += new System.EventHandler(this.recoveryCourt_Click);
            // 
            // pauseCourt
            // 
            this.pauseCourt.Location = new System.Drawing.Point(485, 43);
            this.pauseCourt.Name = "pauseCourt";
            this.pauseCourt.Size = new System.Drawing.Size(75, 23);
            this.pauseCourt.TabIndex = 4;
            this.pauseCourt.Text = "休庭";
            this.pauseCourt.UseVisualStyleBackColor = true;
            this.pauseCourt.Click += new System.EventHandler(this.pauseCourt_Click);
            // 
            // openCourt
            // 
            this.openCourt.Location = new System.Drawing.Point(364, 43);
            this.openCourt.Name = "openCourt";
            this.openCourt.Size = new System.Drawing.Size(75, 23);
            this.openCourt.TabIndex = 3;
            this.openCourt.Text = "开庭";
            this.openCourt.UseVisualStyleBackColor = true;
            this.openCourt.Click += new System.EventHandler(this.openCourt_Click);
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(249, 43);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(75, 23);
            this.restart.TabIndex = 2;
            this.restart.Text = "重启";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(132, 43);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // raiseClient
            // 
            this.raiseClient.Location = new System.Drawing.Point(22, 71);
            this.raiseClient.Name = "raiseClient";
            this.raiseClient.Size = new System.Drawing.Size(75, 23);
            this.raiseClient.TabIndex = 0;
            this.raiseClient.Text = "一键拉起";
            this.raiseClient.UseVisualStyleBackColor = true;
            this.raiseClient.Click += new System.EventHandler(this.raiseClient_Click);
            // 
            // panel_main
            // 
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 100);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1302, 557);
            this.panel_main.TabIndex = 1;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(22, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 11;
            this.btn_start.Text = "连接庭审服务";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 657);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_top);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Button raiseClient;
        private System.Windows.Forms.Button closeCourt;
        private System.Windows.Forms.Button recoveryCourt;
        private System.Windows.Forms.Button pauseCourt;
        private System.Windows.Forms.Button openCourt;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button realtiemTransparent;
        private System.Windows.Forms.Button courtState;
        private System.Windows.Forms.Button summaryNote;
        private System.Windows.Forms.Button fullNote;
        private System.Windows.Forms.Button btn_start;
    }
}

