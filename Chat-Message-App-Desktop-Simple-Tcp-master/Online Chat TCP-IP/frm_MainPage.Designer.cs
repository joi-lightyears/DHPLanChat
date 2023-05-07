namespace Online_Chat_TCP_IP
{
    partial class frm_MainPage
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MainPage));
            this.pic_MainPage = new System.Windows.Forms.PictureBox();
            this.btnCreateServer = new System.Windows.Forms.Button();
            this.btnJoinServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelWelcome = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_MainPage)).BeginInit();
            this.panelWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_MainPage
            // 
            this.pic_MainPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_MainPage.BackgroundImage")));
            this.pic_MainPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_MainPage.Location = new System.Drawing.Point(-19, 34);
            this.pic_MainPage.Margin = new System.Windows.Forms.Padding(2);
            this.pic_MainPage.Name = "pic_MainPage";
            this.pic_MainPage.Size = new System.Drawing.Size(372, 356);
            this.pic_MainPage.TabIndex = 0;
            this.pic_MainPage.TabStop = false;
            // 
            // btnCreateServer
            // 
            this.btnCreateServer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(123)))), ((int)(((byte)(244)))));
            this.btnCreateServer.FlatAppearance.BorderSize = 2;
            this.btnCreateServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateServer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(123)))), ((int)(((byte)(244)))));
            this.btnCreateServer.Location = new System.Drawing.Point(29, 434);
            this.btnCreateServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateServer.Name = "btnCreateServer";
            this.btnCreateServer.Size = new System.Drawing.Size(122, 57);
            this.btnCreateServer.TabIndex = 1;
            this.btnCreateServer.Text = "Create Server";
            this.btnCreateServer.UseVisualStyleBackColor = true;
            this.btnCreateServer.Click += new System.EventHandler(this.btnCreateServer_Click);
            // 
            // btnJoinServer
            // 
            this.btnJoinServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(123)))), ((int)(((byte)(244)))));
            this.btnJoinServer.FlatAppearance.BorderSize = 0;
            this.btnJoinServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinServer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinServer.ForeColor = System.Drawing.Color.PapayaWhip;
            this.btnJoinServer.Location = new System.Drawing.Point(181, 434);
            this.btnJoinServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnJoinServer.Name = "btnJoinServer";
            this.btnJoinServer.Size = new System.Drawing.Size(118, 57);
            this.btnJoinServer.TabIndex = 2;
            this.btnJoinServer.Text = "Join Server";
            this.btnJoinServer.UseVisualStyleBackColor = false;
            this.btnJoinServer.Click += new System.EventHandler(this.btnJoinServer_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 321);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 78);
            this.label1.TabIndex = 3;
            this.label1.Text = "Let\'s chat together";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelWelcome
            // 
            this.panelWelcome.Controls.Add(this.pictureBox1);
            this.panelWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWelcome.Location = new System.Drawing.Point(0, 0);
            this.panelWelcome.Margin = new System.Windows.Forms.Padding(2);
            this.panelWelcome.Name = "panelWelcome";
            this.panelWelcome.Size = new System.Drawing.Size(328, 577);
            this.panelWelcome.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(60)))));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 577);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frm_MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(328, 577);
            this.Controls.Add(this.panelWelcome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnJoinServer);
            this.Controls.Add(this.btnCreateServer);
            this.Controls.Add(this.pic_MainPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_MainPage";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server / Client";
            ((System.ComponentModel.ISupportInitialize)(this.pic_MainPage)).EndInit();
            this.panelWelcome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_MainPage;
        private System.Windows.Forms.Button btnCreateServer;
        private System.Windows.Forms.Button btnJoinServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelWelcome;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

