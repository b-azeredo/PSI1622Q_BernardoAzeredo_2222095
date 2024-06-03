namespace AdminSysWF
{
    partial class DefinicoesGrafico
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
            this.btn_baixarPDF = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btn_baixarPDF
            // 
            this.btn_baixarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.btn_baixarPDF.BorderRadius = 15;
            this.btn_baixarPDF.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_baixarPDF.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_baixarPDF.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_baixarPDF.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_baixarPDF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(46)))));
            this.btn_baixarPDF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_baixarPDF.ForeColor = System.Drawing.Color.White;
            this.btn_baixarPDF.Location = new System.Drawing.Point(84, 56);
            this.btn_baixarPDF.Name = "btn_baixarPDF";
            this.btn_baixarPDF.Size = new System.Drawing.Size(193, 39);
            this.btn_baixarPDF.TabIndex = 20;
            this.btn_baixarPDF.Text = "Baixar PDF";
            this.btn_baixarPDF.Click += new System.EventHandler(this.btn_baixarPDF_Click);
            // 
            // DefinicoesGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(361, 261);
            this.Controls.Add(this.btn_baixarPDF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DefinicoesGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Definições";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_baixarPDF;
    }
}