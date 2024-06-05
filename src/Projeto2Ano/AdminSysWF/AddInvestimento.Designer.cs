namespace AdminSysWF
{
    partial class AddInvestimento
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
            this.txb_DescricaoInvestimento = new Guna.UI2.WinForms.Guna2TextBox();
            this.tiposComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ValorInvestido = new Guna.UI2.WinForms.Guna2TextBox();
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
            this.ComfirmAddCategoria.Location = new System.Drawing.Point(80, 167);
            this.ComfirmAddCategoria.Name = "ComfirmAddCategoria";
            this.ComfirmAddCategoria.Size = new System.Drawing.Size(193, 39);
            this.ComfirmAddCategoria.TabIndex = 21;
            this.ComfirmAddCategoria.Text = "Adicionar";
            this.ComfirmAddCategoria.Click += new System.EventHandler(this.ComfirmAddCategoria_Click);
            // 
            // txb_DescricaoInvestimento
            // 
            this.txb_DescricaoInvestimento.Animated = true;
            this.txb_DescricaoInvestimento.AutoRoundedCorners = true;
            this.txb_DescricaoInvestimento.BorderRadius = 17;
            this.txb_DescricaoInvestimento.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_DescricaoInvestimento.DefaultText = "";
            this.txb_DescricaoInvestimento.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txb_DescricaoInvestimento.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txb_DescricaoInvestimento.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_DescricaoInvestimento.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_DescricaoInvestimento.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.txb_DescricaoInvestimento.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_DescricaoInvestimento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_DescricaoInvestimento.ForeColor = System.Drawing.Color.White;
            this.txb_DescricaoInvestimento.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_DescricaoInvestimento.Location = new System.Drawing.Point(80, 41);
            this.txb_DescricaoInvestimento.Name = "txb_DescricaoInvestimento";
            this.txb_DescricaoInvestimento.PasswordChar = '\0';
            this.txb_DescricaoInvestimento.PlaceholderText = "Descrição do Investimento";
            this.txb_DescricaoInvestimento.SelectedText = "";
            this.txb_DescricaoInvestimento.Size = new System.Drawing.Size(193, 36);
            this.txb_DescricaoInvestimento.TabIndex = 20;
            // 
            // tiposComboBox
            // 
            this.tiposComboBox.BackColor = System.Drawing.Color.Transparent;
            this.tiposComboBox.BorderRadius = 17;
            this.tiposComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tiposComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tiposComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.tiposComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tiposComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tiposComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tiposComboBox.ForeColor = System.Drawing.Color.White;
            this.tiposComboBox.ItemHeight = 30;
            this.tiposComboBox.Location = new System.Drawing.Point(80, 83);
            this.tiposComboBox.Name = "tiposComboBox";
            this.tiposComboBox.Size = new System.Drawing.Size(193, 36);
            this.tiposComboBox.TabIndex = 28;
            // 
            // ValorInvestido
            // 
            this.ValorInvestido.Animated = true;
            this.ValorInvestido.AutoRoundedCorners = true;
            this.ValorInvestido.BorderRadius = 17;
            this.ValorInvestido.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ValorInvestido.DefaultText = "";
            this.ValorInvestido.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ValorInvestido.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ValorInvestido.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorInvestido.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorInvestido.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.ValorInvestido.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ValorInvestido.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ValorInvestido.ForeColor = System.Drawing.Color.White;
            this.ValorInvestido.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ValorInvestido.Location = new System.Drawing.Point(80, 125);
            this.ValorInvestido.Name = "ValorInvestido";
            this.ValorInvestido.PasswordChar = '\0';
            this.ValorInvestido.PlaceholderText = "Valor Investido";
            this.ValorInvestido.SelectedText = "";
            this.ValorInvestido.Size = new System.Drawing.Size(193, 36);
            this.ValorInvestido.TabIndex = 29;
            // 
            // AddInvestimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(351, 246);
            this.Controls.Add(this.ValorInvestido);
            this.Controls.Add(this.tiposComboBox);
            this.Controls.Add(this.ComfirmAddCategoria);
            this.Controls.Add(this.txb_DescricaoInvestimento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddInvestimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Investimento";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button ComfirmAddCategoria;
        private Guna.UI2.WinForms.Guna2TextBox txb_DescricaoInvestimento;
        private Guna.UI2.WinForms.Guna2ComboBox tiposComboBox;
        private Guna.UI2.WinForms.Guna2TextBox ValorInvestido;
    }
}