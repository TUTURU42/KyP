namespace Byte__
{
    partial class Autorization
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
            this.Autoriz = new System.Windows.Forms.Label();
            this.panel_Autoriz = new System.Windows.Forms.Panel();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.button_autoriz = new System.Windows.Forms.Button();
            this.label_login = new System.Windows.Forms.Label();
            this.label_pass = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel_Autoriz.SuspendLayout();
            this.SuspendLayout();
            // 
            // Autoriz
            // 
            this.Autoriz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Autoriz.Font = new System.Drawing.Font("Comic Sans MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Autoriz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(90)))), ((int)(((byte)(91)))));
            this.Autoriz.Location = new System.Drawing.Point(0, 0);
            this.Autoriz.Name = "Autoriz";
            this.Autoriz.Size = new System.Drawing.Size(382, 100);
            this.Autoriz.TabIndex = 0;
            this.Autoriz.Text = "Авторизация";
            this.Autoriz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_Autoriz
            // 
            this.panel_Autoriz.Controls.Add(this.Autoriz);
            this.panel_Autoriz.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Autoriz.Location = new System.Drawing.Point(0, 0);
            this.panel_Autoriz.Name = "panel_Autoriz";
            this.panel_Autoriz.Size = new System.Drawing.Size(382, 100);
            this.panel_Autoriz.TabIndex = 1;
            // 
            // textBox_login
            // 
            this.textBox_login.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_login.Location = new System.Drawing.Point(152, 123);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(179, 34);
            this.textBox_login.TabIndex = 2;
            // 
            // textBox_pass
            // 
            this.textBox_pass.AcceptsReturn = true;
            this.textBox_pass.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox_pass.Location = new System.Drawing.Point(152, 185);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(179, 34);
            this.textBox_pass.TabIndex = 3;
            this.textBox_pass.UseSystemPasswordChar = true;
            this.textBox_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_pass_KeyDown);
            // 
            // button_autoriz
            // 
            this.button_autoriz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_autoriz.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(151)))), ((int)(((byte)(152)))));
            this.button_autoriz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_autoriz.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_autoriz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(90)))), ((int)(((byte)(91)))));
            this.button_autoriz.Location = new System.Drawing.Point(224, 269);
            this.button_autoriz.Name = "button_autoriz";
            this.button_autoriz.Size = new System.Drawing.Size(107, 48);
            this.button_autoriz.TabIndex = 4;
            this.button_autoriz.Text = "Войти";
            this.button_autoriz.UseVisualStyleBackColor = true;
            this.button_autoriz.Click += new System.EventHandler(this.button_autoriz_Click);
            // 
            // label_login
            // 
            this.label_login.AutoSize = true;
            this.label_login.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(90)))), ((int)(((byte)(91)))));
            this.label_login.Location = new System.Drawing.Point(48, 119);
            this.label_login.Name = "label_login";
            this.label_login.Size = new System.Drawing.Size(98, 38);
            this.label_login.TabIndex = 5;
            this.label_login.Text = "Логин";
            // 
            // label_pass
            // 
            this.label_pass.AutoSize = true;
            this.label_pass.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(90)))), ((int)(((byte)(91)))));
            this.label_pass.Location = new System.Drawing.Point(29, 179);
            this.label_pass.Name = "label_pass";
            this.label_pass.Size = new System.Drawing.Size(117, 38);
            this.label_pass.TabIndex = 6;
            this.label_pass.Text = "Пароль";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(90)))), ((int)(((byte)(91)))));
            this.linkLabel1.Location = new System.Drawing.Point(224, 336);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 16);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Войти как Гость";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Autorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(217)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(382, 414);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label_pass);
            this.Controls.Add(this.label_login);
            this.Controls.Add(this.button_autoriz);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.panel_Autoriz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Autorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorization";
            this.panel_Autoriz.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Autoriz;
        private Panel panel_Autoriz;
        private TextBox textBox_login;
        private TextBox textBox_pass;
        private Button button_autoriz;
        private Label label_login;
        private Label label_pass;
        private LinkLabel linkLabel1;
    }
}