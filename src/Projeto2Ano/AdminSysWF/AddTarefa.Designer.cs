namespace AdminSysWF
{
    partial class AddTarefa
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
            this.ComfirmAddDespesa = new Guna.UI2.WinForms.Guna2Button();
            this.txb_DescTarefa = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // ComfirmAddDespesa
            // 
            this.ComfirmAddDespesa.BackColor = System.Drawing.SystemColors.Control;
            this.ComfirmAddDespesa.BorderRadius = 15;
            this.ComfirmAddDespesa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ComfirmAddDespesa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ComfirmAddDespesa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ComfirmAddDespesa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ComfirmAddDespesa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(40)))));
            this.ComfirmAddDespesa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ComfirmAddDespesa.ForeColor = System.Drawing.Color.White;
            this.ComfirmAddDespesa.Location = new System.Drawing.Point(115, 149);
            this.ComfirmAddDespesa.Margin = new System.Windows.Forms.Padding(4);
            this.ComfirmAddDespesa.Name = "ComfirmAddDespesa";
            this.ComfirmAddDespesa.Size = new System.Drawing.Size(257, 48);
            this.ComfirmAddDespesa.TabIndex = 19;
            this.ComfirmAddDespesa.Text = "Adicionar";
            this.ComfirmAddDespesa.Click += new System.EventHandler(this.ComfirmAddDespesa_Click);
            // 
            // txb_DescTarefa
            // 
            this.txb_DescTarefa.Animated = true;
            this.txb_DescTarefa.AutoRoundedCorners = true;
            this.txb_DescTarefa.BorderRadius = 21;
            this.txb_DescTarefa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_DescTarefa.DefaultText = "";
            this.txb_DescTarefa.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txb_DescTarefa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txb_DescTarefa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_DescTarefa.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_DescTarefa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_DescTarefa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_DescTarefa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_DescTarefa.Location = new System.Drawing.Point(115, 97);
            this.txb_DescTarefa.Margin = new System.Windows.Forms.Padding(4);
            this.txb_DescTarefa.Name = "txb_DescTarefa";
            this.txb_DescTarefa.PasswordChar = '\0';
            this.txb_DescTarefa.PlaceholderText = "Descrição da Tarefa";
            this.txb_DescTarefa.SelectedText = "";
            this.txb_DescTarefa.Size = new System.Drawing.Size(257, 44);
            this.txb_DescTarefa.TabIndex = 17;
            // 
            // AddTarefa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 293);
            this.Controls.Add(this.ComfirmAddDespesa);
            this.Controls.Add(this.txb_DescTarefa);
            this.Name = "AddTarefa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Tarefa";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button ComfirmAddDespesa;
        private Guna.UI2.WinForms.Guna2TextBox txb_DescTarefa;
    }
}