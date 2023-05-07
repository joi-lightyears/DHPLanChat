namespace Online_Chat_TCP_IP
{
    partial class frm_ChatMessageServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChatMessageServer));
            this.btnClientDisconnect = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnPeopleConnected = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClientDisconnect
            // 
            this.btnClientDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(136)))), ((int)(((byte)(117)))));
            this.btnClientDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientDisconnect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClientDisconnect.Location = new System.Drawing.Point(274, 40);
            this.btnClientDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnClientDisconnect.Name = "btnClientDisconnect";
            this.btnClientDisconnect.Size = new System.Drawing.Size(35, 32);
            this.btnClientDisconnect.TabIndex = 12;
            this.btnClientDisconnect.Text = "X";
            this.btnClientDisconnect.UseVisualStyleBackColor = false;
            this.btnClientDisconnect.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 21);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 72);
            this.lblTitle.TabIndex = 10;
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.txtSendMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSendMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtSendMessage.Location = new System.Drawing.Point(25, 555);
            this.txtSendMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSendMessage.Size = new System.Drawing.Size(211, 43);
            this.txtSendMessage.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(123)))), ((int)(((byte)(244)))));
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.PapayaWhip;
            this.btnSend.Location = new System.Drawing.Point(241, 555);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(70, 43);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(101)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMessage.Location = new System.Drawing.Point(25, 96);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(286, 440);
            this.txtMessage.TabIndex = 7;
            // 
            // btnPeopleConnected
            // 
            this.btnPeopleConnected.BackColor = System.Drawing.Color.Transparent;
            this.btnPeopleConnected.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPeopleConnected.BackgroundImage")));
            this.btnPeopleConnected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPeopleConnected.FlatAppearance.BorderSize = 0;
            this.btnPeopleConnected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeopleConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeopleConnected.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPeopleConnected.Location = new System.Drawing.Point(226, 40);
            this.btnPeopleConnected.Margin = new System.Windows.Forms.Padding(2);
            this.btnPeopleConnected.Name = "btnPeopleConnected";
            this.btnPeopleConnected.Size = new System.Drawing.Size(35, 32);
            this.btnPeopleConnected.TabIndex = 13;
            this.btnPeopleConnected.UseVisualStyleBackColor = false;
            this.btnPeopleConnected.Click += new System.EventHandler(this.btnPeopleConnected_Click);
            // 
            // frm_ChatMessageServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(328, 617);
            this.Controls.Add(this.btnPeopleConnected);
            this.Controls.Add(this.btnClientDisconnect);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_ChatMessageServer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chat Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClientDisconnect;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnPeopleConnected;
    }
}