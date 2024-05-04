namespace AdminSysWF
{
    partial class AddLucro
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
            this.ComfirmAddLucro = new System.Windows.Forms.Button();
            this.txb_ValorDespesa = new System.Windows.Forms.TextBox();
            this.txb_DescDespesa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComfirmAddLucro
            // 
            this.ComfirmAddLucro.Location = new System.Drawing.Point(111, 109);
            this.ComfirmAddLucro.Name = "ComfirmAddLucro";
            this.ComfirmAddLucro.Size = new System.Drawing.Size(100, 23);
            this.ComfirmAddLucro.TabIndex = 5;
            this.ComfirmAddLucro.Text = "Adicionar";
            this.ComfirmAddLucro.UseVisualStyleBackColor = true;
            this.ComfirmAddLucro.Click += new System.EventHandler(this.ComfirmAddDespesa_Click);
            // 
            // txb_ValorDespesa
            // 
            this.txb_ValorDespesa.Location = new System.Drawing.Point(111, 83);
            this.txb_ValorDespesa.Name = "txb_ValorDespesa";
            this.txb_ValorDespesa.Size = new System.Drawing.Size(100, 20);
            this.txb_ValorDespesa.TabIndex = 4;
            // 
            // txb_DescDespesa
            // 
            this.txb_DescDespesa.Location = new System.Drawing.Point(111, 44);
            this.txb_DescDespesa.Name = "txb_DescDespesa";
            this.txb_DescDespesa.Size = new System.Drawing.Size(100, 20);
            this.txb_DescDespesa.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Descrição";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Valor";
            // 
            // AddLucro
            // 
            this.AcceptButton = this.ComfirmAddLucro;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 182);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComfirmAddLucro);
            this.Controls.Add(this.txb_ValorDespesa);
            this.Controls.Add(this.txb_DescDespesa);
            this.Name = "AddLucro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Lucro";
            this.Load += new System.EventHandler(this.AddLucro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ComfirmAddLucro;
        private System.Windows.Forms.TextBox txb_ValorDespesa;
        private System.Windows.Forms.TextBox txb_DescDespesa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}