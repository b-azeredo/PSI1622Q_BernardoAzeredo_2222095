namespace AdminSysWF
{
    partial class AddCargo
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
            this.txb_nomeCargo = new Guna.UI2.WinForms.Guna2TextBox();
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
            this.ComfirmAddCategoria.Location = new System.Drawing.Point(79, 106);
            this.ComfirmAddCategoria.Name = "ComfirmAddCategoria";
            this.ComfirmAddCategoria.Size = new System.Drawing.Size(193, 39);
            this.ComfirmAddCategoria.TabIndex = 21;
            this.ComfirmAddCategoria.Text = "Adicionar";
            this.ComfirmAddCategoria.Click += new System.EventHandler(this.ComfirmAddCategoria_Click);
            // 
            // txb_nomeCargo
            // 
            this.txb_nomeCargo.Animated = true;
            this.txb_nomeCargo.AutoRoundedCorners = true;
            this.txb_nomeCargo.BorderRadius = 17;
            this.txb_nomeCargo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_nomeCargo.DefaultText = "";
            this.txb_nomeCargo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txb_nomeCargo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txb_nomeCargo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_nomeCargo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_nomeCargo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.txb_nomeCargo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_nomeCargo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_nomeCargo.ForeColor = System.Drawing.Color.White;
            this.txb_nomeCargo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_nomeCargo.Location = new System.Drawing.Point(79, 64);
            this.txb_nomeCargo.Name = "txb_nomeCargo";
            this.txb_nomeCargo.PasswordChar = '\0';
            this.txb_nomeCargo.PlaceholderText = "Nome do Cargo";
            this.txb_nomeCargo.SelectedText = "";
            this.txb_nomeCargo.Size = new System.Drawing.Size(193, 36);
            this.txb_nomeCargo.TabIndex = 20;
            // 
            // AddCargo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(351, 209);
            this.Controls.Add(this.ComfirmAddCategoria);
            this.Controls.Add(this.txb_nomeCargo);
            this.Name = "AddCargo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Cargo";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button ComfirmAddCategoria;
        private Guna.UI2.WinForms.Guna2TextBox txb_nomeCargo;
    }
}