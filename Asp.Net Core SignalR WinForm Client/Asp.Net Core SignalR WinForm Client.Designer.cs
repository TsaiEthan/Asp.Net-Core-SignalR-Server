namespace Asp.Net_Core_SignalR_WinForm_Client
{
    partial class SignalRClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChatMessage = new System.Windows.Forms.TextBox();
            this.txtChatHistory = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblConnectionId = new System.Windows.Forms.Label();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.CheckStatebutton = new System.Windows.Forms.Button();
            this.ShowState_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtChatMessage
            // 
            this.txtChatMessage.Location = new System.Drawing.Point(14, 85);
            this.txtChatMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtChatMessage.Name = "txtChatMessage";
            this.txtChatMessage.Size = new System.Drawing.Size(320, 23);
            this.txtChatMessage.TabIndex = 0;
            // 
            // txtChatHistory
            // 
            this.txtChatHistory.Location = new System.Drawing.Point(14, 148);
            this.txtChatHistory.Margin = new System.Windows.Forms.Padding(4);
            this.txtChatHistory.Name = "txtChatHistory";
            this.txtChatHistory.Size = new System.Drawing.Size(798, 385);
            this.txtChatHistory.TabIndex = 1;
            this.txtChatHistory.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(355, 78);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 35);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chat History:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(18, 14);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 31);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblConnectionId
            // 
            this.lblConnectionId.AutoSize = true;
            this.lblConnectionId.Location = new System.Drawing.Point(156, 22);
            this.lblConnectionId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectionId.Name = "lblConnectionId";
            this.lblConnectionId.Size = new System.Drawing.Size(0, 15);
            this.lblConnectionId.TabIndex = 6;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Location = new System.Drawing.Point(184, 14);
            this.btnDisConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(120, 31);
            this.btnDisConnect.TabIndex = 7;
            this.btnDisConnect.Text = "DisConnect";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // CheckStatebutton
            // 
            this.CheckStatebutton.Location = new System.Drawing.Point(355, 15);
            this.CheckStatebutton.Margin = new System.Windows.Forms.Padding(4);
            this.CheckStatebutton.Name = "CheckStatebutton";
            this.CheckStatebutton.Size = new System.Drawing.Size(120, 31);
            this.CheckStatebutton.TabIndex = 8;
            this.CheckStatebutton.Text = "CheckState";
            this.CheckStatebutton.UseVisualStyleBackColor = true;
            this.CheckStatebutton.Click += new System.EventHandler(this.CheckStatebutton_Click);
            // 
            // ShowState_textBox
            // 
            this.ShowState_textBox.BackColor = System.Drawing.SystemColors.Menu;
            this.ShowState_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShowState_textBox.Location = new System.Drawing.Point(357, 58);
            this.ShowState_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.ShowState_textBox.Name = "ShowState_textBox";
            this.ShowState_textBox.Size = new System.Drawing.Size(117, 16);
            this.ShowState_textBox.TabIndex = 9;
            this.ShowState_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SignalRClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(877, 548);
            this.Controls.Add(this.ShowState_textBox);
            this.Controls.Add(this.CheckStatebutton);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.lblConnectionId);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChatHistory);
            this.Controls.Add(this.txtChatMessage);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SignalRClient";
            this.Text = "Asp.Net Core SignalR Client - WinForm Application";
            this.Load += new System.EventHandler(this.SignalRClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChatMessage;
        private System.Windows.Forms.RichTextBox txtChatHistory;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblConnectionId;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Button CheckStatebutton;
        private System.Windows.Forms.TextBox ShowState_textBox;
    }
}
