
namespace Items
{
    partial class HideLogin
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
            this.ILogin = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ILogin
            // 
            this.ILogin.AutoSize = true;
            this.ILogin.Location = new System.Drawing.Point(108, 53);
            this.ILogin.Name = "ILogin";
            this.ILogin.Size = new System.Drawing.Size(54, 13);
            this.ILogin.TabIndex = 0;
            this.ILogin.Text = "Your login";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "return";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HideLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 149);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ILogin);
            this.Name = "HideLogin";
            this.Text = "HideLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ILogin;
        private System.Windows.Forms.Button button1;
    }
}