namespace AdminSysWF
{
    partial class AddDespesa
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
            this.txb_DescDespesa = new System.Windows.Forms.TextBox();
            this.txb_ValorDespesa = new System.Windows.Forms.TextBox();
            this.ComfirmAddDespesa = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_DescDespesa
            // 
            this.txb_DescDespesa.Location = new System.Drawing.Point(114, 45);
            this.txb_DescDespesa.Name = "txb_DescDespesa";
            this.txb_DescDespesa.Size = new System.Drawing.Size(100, 20);
            this.txb_DescDespesa.TabIndex = 0;
            // 
            // txb_ValorDespesa
            // 
            this.txb_ValorDespesa.Location = new System.Drawing.Point(114, 84);
            this.txb_ValorDespesa.Name = "txb_ValorDespesa";
            this.txb_ValorDespesa.Size = new System.Drawing.Size(100, 20);
            this.txb_ValorDespesa.TabIndex = 1;
            // 
            // ComfirmAddDespesa
            // 
            this.ComfirmAddDespesa.Location = new System.Drawing.Point(114, 110);
            this.ComfirmAddDespesa.Name = "ComfirmAddDespesa";
            this.ComfirmAddDespesa.Size = new System.Drawing.Size(100, 23);
            this.ComfirmAddDespesa.TabIndex = 2;
            this.ComfirmAddDespesa.Text = "Adicionar";
            this.ComfirmAddDespesa.UseVisualStyleBackColor = true;
            this.ComfirmAddDespesa.Click += new System.EventHandler(this.ComfirmAddDespesa_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Valor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Descrição";
            // 
            // AddDespesa
            // 
            this.AcceptButton = this.ComfirmAddDespesa;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 184);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComfirmAddDespesa);
            this.Controls.Add(this.txb_ValorDespesa);
            this.Controls.Add(this.txb_DescDespesa);
            this.Name = "AddDespesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Despesa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_DescDespesa;
        private System.Windows.Forms.TextBox txb_ValorDespesa;
        private System.Windows.Forms.Button ComfirmAddDespesa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}