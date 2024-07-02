using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminSysWF
{
    public partial class Edit4Campos : Form
    {
        public enum Tabelas
        {
            Fornecedor
        }

        private int id;
        private Tabelas tabela;
        private Guna2ComboBox cb = new Guna2ComboBox();

        public Edit4Campos(int userid, int id, Tabelas tabela, string label1, string label2, string label3, string label4, string placeholder1, string placeholder2, string placeholder3, string placeholder4)
        {
            this.id = id;
            this.tabela = tabela;
            InitializeComponent();
            if (tabela == Tabelas.Fornecedor)
            {
                txb_edit4.Visible = false;
                cb.Location = new Point(77, 253);
                cb.Size = new Size(193, 36);
                cb.BorderRadius = 17;
                cb.BorderThickness = 1;
                cb.BorderColor = Color.FromArgb(213, 218, 223);
                cb.ForeColor = Color.White;
                cb.FillColor = Color.FromArgb(34, 34, 46);
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                DataTable categorias = Database.GetCategorias(userid);
                this.Controls.Add(cb);

                if (categorias != null)
                {
                    cb.DataSource = categorias;
                    cb.DisplayMember = "NOME";
                    cb.ValueMember = "ID";
                }
                else
                {
                    MessageBox.Show("Erro ao carregar as categorias.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (DataRow row in categorias.Rows)
                {
                    if (row["NOME"].ToString() == placeholder4)
                    {
                        cb.SelectedValue = int.Parse(row["ID"].ToString());
                        break;
                    }
                }
            }
            lbl_edit1.Text = label1;
            lbl_edit2.Text = label2;
            lbl_edit3.Text = label3;
            lbl_edit4.Text = label4;
            txb_edit1.Text = placeholder1;
            txb_edit2.Text = placeholder2;
            txb_edit3.Text = placeholder3;
            txb_edit4.Text = placeholder4;
        }

        private void ConfirmEdit_Click(object sender, EventArgs e)
        {
            bool result = false;
            switch (tabela)
            {
                case Tabelas.Fornecedor:
                    Database.EditFornecedor(id, txb_edit1.Text, txb_edit2.Text, txb_edit3.Text, int.Parse(cb.SelectedValue.ToString()));
                    break;
            }
            if (result)
            {
                MessageBox.Show("Registo editado com sucesso.", "Editado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}

