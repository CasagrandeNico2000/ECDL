namespace Esami_ECDL
{
    partial class FormAccount
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.checkBoxCriptazionePW = new System.Windows.Forms.CheckBox();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.textBoxCreaPassword = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.textBoxCreaUsername = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelCreaAccount = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 462);
            this.panel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.buttonClear);
            this.panel7.Controls.Add(this.panel16);
            this.panel7.Controls.Add(this.panel11);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 127);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(645, 257);
            this.panel7.TabIndex = 5;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(514, 108);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(90, 33);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "CLEAR";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // panel16
            // 
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.panel17);
            this.panel16.Controls.Add(this.panel18);
            this.panel16.Controls.Add(this.panel19);
            this.panel16.Location = new System.Drawing.Point(167, 171);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(300, 40);
            this.panel16.TabIndex = 5;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.checkBoxCriptazionePW);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(43, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(255, 38);
            this.panel17.TabIndex = 2;
            // 
            // checkBoxCriptazionePW
            // 
            this.checkBoxCriptazionePW.AutoSize = true;
            this.checkBoxCriptazionePW.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCriptazionePW.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxCriptazionePW.Location = new System.Drawing.Point(11, 5);
            this.checkBoxCriptazionePW.Name = "checkBoxCriptazionePW";
            this.checkBoxCriptazionePW.Size = new System.Drawing.Size(224, 29);
            this.checkBoxCriptazionePW.TabIndex = 6;
            this.checkBoxCriptazionePW.TabStop = false;
            this.checkBoxCriptazionePW.Text = "Criptazione Password";
            this.checkBoxCriptazionePW.UseVisualStyleBackColor = true;
            this.checkBoxCriptazionePW.CheckedChanged += new System.EventHandler(this.checkBoxCriptazionePW_CheckedChanged);
            // 
            // panel18
            // 
            this.panel18.BackgroundImage = global::Esami_ECDL.Properties.Resources.criptazione;
            this.panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel18.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel18.Location = new System.Drawing.Point(3, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(40, 38);
            this.panel18.TabIndex = 1;
            this.panel18.Click += new System.EventHandler(this.panel18_Click);
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.SteelBlue;
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(3, 38);
            this.panel19.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Controls.Add(this.panel13);
            this.panel11.Controls.Add(this.panel14);
            this.panel11.Location = new System.Drawing.Point(167, 102);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(300, 40);
            this.panel11.TabIndex = 3;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.textBoxCreaPassword);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(43, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(255, 38);
            this.panel12.TabIndex = 2;
            // 
            // textBoxCreaPassword
            // 
            this.textBoxCreaPassword.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCreaPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCreaPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreaPassword.ForeColor = System.Drawing.Color.DimGray;
            this.textBoxCreaPassword.Location = new System.Drawing.Point(11, 7);
            this.textBoxCreaPassword.Name = "textBoxCreaPassword";
            this.textBoxCreaPassword.Size = new System.Drawing.Size(233, 23);
            this.textBoxCreaPassword.TabIndex = 0;
            this.textBoxCreaPassword.TabStop = false;
            this.textBoxCreaPassword.Text = "Password...";
            this.textBoxCreaPassword.Click += new System.EventHandler(this.textBoxCreaPassword_Click);
            this.textBoxCreaPassword.Leave += new System.EventHandler(this.textBoxCreaPassword_Leave);
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = global::Esami_ECDL.Properties.Resources.pw;
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(3, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(40, 38);
            this.panel13.TabIndex = 1;
            this.panel13.Click += new System.EventHandler(this.panel13_Click);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.SteelBlue;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(3, 38);
            this.panel14.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Controls.Add(this.panel15);
            this.panel8.Location = new System.Drawing.Point(167, 33);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(300, 40);
            this.panel8.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.textBoxCreaUsername);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(43, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(255, 38);
            this.panel10.TabIndex = 2;
            // 
            // textBoxCreaUsername
            // 
            this.textBoxCreaUsername.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCreaUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCreaUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreaUsername.ForeColor = System.Drawing.Color.DimGray;
            this.textBoxCreaUsername.Location = new System.Drawing.Point(11, 7);
            this.textBoxCreaUsername.Name = "textBoxCreaUsername";
            this.textBoxCreaUsername.Size = new System.Drawing.Size(233, 23);
            this.textBoxCreaUsername.TabIndex = 0;
            this.textBoxCreaUsername.TabStop = false;
            this.textBoxCreaUsername.Text = "Username...";
            this.textBoxCreaUsername.Click += new System.EventHandler(this.textBoxCreaUsername_Click);
            this.textBoxCreaUsername.Leave += new System.EventHandler(this.textBoxCreaUsername_Leave);
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::Esami_ECDL.Properties.Resources.username;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(3, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(40, 38);
            this.panel9.TabIndex = 1;
            this.panel9.Click += new System.EventHandler(this.panel9_Click);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.SteelBlue;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(3, 38);
            this.panel15.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 78);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(645, 49);
            this.panel6.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(182, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Inserire dati per Account";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SteelBlue;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 384);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(645, 3);
            this.panel5.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.Controls.Add(this.labelCreaAccount);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 387);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(645, 75);
            this.panel4.TabIndex = 2;
            // 
            // labelCreaAccount
            // 
            this.labelCreaAccount.AutoSize = true;
            this.labelCreaAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreaAccount.ForeColor = System.Drawing.Color.DimGray;
            this.labelCreaAccount.Location = new System.Drawing.Point(203, 18);
            this.labelCreaAccount.Name = "labelCreaAccount";
            this.labelCreaAccount.Size = new System.Drawing.Size(216, 38);
            this.labelCreaAccount.TabIndex = 0;
            this.labelCreaAccount.Text = "Crea Account";
            this.labelCreaAccount.Click += new System.EventHandler(this.labelCreaAccount_Click);
            this.labelCreaAccount.MouseEnter += new System.EventHandler(this.labelCreaAccount_MouseEnter);
            this.labelCreaAccount.MouseLeave += new System.EventHandler(this.labelCreaAccount_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 3);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 75);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(166, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Creazione Account";
            // 
            // FormAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 486);
            this.Controls.Add(this.panel1);
            this.Name = "FormAccount";
            this.Text = "Creazione account";
            this.Load += new System.EventHandler(this.FormAccount_Load);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCreaAccount;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TextBox textBoxCreaPassword;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox textBoxCreaUsername;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.CheckBox checkBoxCriptazionePW;
        private System.Windows.Forms.Button buttonClear;
    }
}