namespace AdminSysWF
{
    partial class AddCategoria
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
            this.ComfirmAddCategoria = new Guna.UI2.WinForms.Guna2Button();
            this.txb_nomeCategoria = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // ComfirmAddCategoria
            // 
            this.ComfirmAddCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.ComfirmAddCategoria.BorderRadius = 15;
            this.ComfirmAddCategoria.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ComfirmAddCategoria.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ComfirmAddCategoria.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ComfirmAddCategoria.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ComfirmAddCategoria.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.ComfirmAddCategoria.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComfirmAddCategoria.ForeColor = System.Drawing.Color.White;
            this.ComfirmAddCategoria.Location = new System.Drawing.Point(79, 110);
            this.ComfirmAddCategoria.Name = "ComfirmAddCategoria";
            this.ComfirmAddCategoria.Size = new System.Drawing.Size(193, 39);
            this.ComfirmAddCategoria.TabIndex = 19;
            this.ComfirmAddCategoria.Text = "Adicionar";
            this.ComfirmAddCategoria.Click += new System.EventHandler(this.ComfirmAddCategoria_Click);
            // 
            // txb_nomeCategoria
            // 
            this.txb_nomeCategoria.Animated = true;
            this.txb_nomeCategoria.AutoRoundedCorners = true;
            this.txb_nomeCategoria.BorderRadius = 17;
            this.txb_nomeCategoria.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_nomeCategoria.DefaultText = "";
            this.txb_nomeCategoria.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txb_nomeCategoria.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txb_nomeCategoria.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_nomeCategoria.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_nomeCategoria.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.txb_nomeCategoria.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_nomeCategoria.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_nomeCategoria.ForeColor = System.Drawing.Color.White;
            this.txb_nomeCategoria.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_nomeCategoria.Location = new System.Drawing.Point(79, 68);
            this.txb_nomeCategoria.Name = "txb_nomeCategoria";
            this.txb_nomeCategoria.PasswordChar = '\0';
            this.txb_nomeCategoria.PlaceholderText = "Nome da Categoria";
            this.txb_nomeCategoria.SelectedText = "";
            this.txb_nomeCategoria.Size = new System.Drawing.Size(193, 36);
            this.txb_nomeCategoria.TabIndex = 17;
            // 
            // AddCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(351, 209);
            this.Controls.Add(this.ComfirmAddCategoria);
            this.Controls.Add(this.txb_nomeCategoria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Categoria";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button ComfirmAddCategoria;
        private Guna.UI2.WinForms.Guna2TextBox txb_nomeCategoria;
    }
}