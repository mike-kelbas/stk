namespace STK
{
    partial class PersonalEdit
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
            this.TBNamePers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TBLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CBPriveleg = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MTBPassword = new System.Windows.Forms.MaskedTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.CBPost = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TBNamePers
            // 
            this.TBNamePers.Location = new System.Drawing.Point(149, 9);
            this.TBNamePers.Name = "TBNamePers";
            this.TBNamePers.Size = new System.Drawing.Size(125, 20);
            this.TBNamePers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Фамилия инициалы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Должность:";
            // 
            // TBLogin
            // 
            this.TBLogin.Location = new System.Drawing.Point(149, 82);
            this.TBLogin.Name = "TBLogin";
            this.TBLogin.Size = new System.Drawing.Size(125, 20);
            this.TBLogin.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Логин:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Пароль:";
            // 
            // CBPriveleg
            // 
            this.CBPriveleg.FormattingEnabled = true;
            this.CBPriveleg.Location = new System.Drawing.Point(149, 151);
            this.CBPriveleg.Name = "CBPriveleg";
            this.CBPriveleg.Size = new System.Drawing.Size(125, 21);
            this.CBPriveleg.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Тип учетной записи:";
            // 
            // MTBPassword
            // 
            this.MTBPassword.Location = new System.Drawing.Point(149, 120);
            this.MTBPassword.Name = "MTBPassword";
            this.MTBPassword.Size = new System.Drawing.Size(125, 20);
            this.MTBPassword.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 178);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CBPost
            // 
            this.CBPost.FormattingEnabled = true;
            this.CBPost.Location = new System.Drawing.Point(149, 44);
            this.CBPost.Name = "CBPost";
            this.CBPost.Size = new System.Drawing.Size(125, 21);
            this.CBPost.TabIndex = 10;
            // 
            // PersonalEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(284, 216);
            this.Controls.Add(this.CBPost);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.MTBPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CBPriveleg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBLogin);
            this.Controls.Add(this.TBNamePers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonalEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сотрудник";
            this.Load += new System.EventHandler(this.PersonalEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBNamePers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CBPriveleg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox MTBPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox CBPost;
    }
}