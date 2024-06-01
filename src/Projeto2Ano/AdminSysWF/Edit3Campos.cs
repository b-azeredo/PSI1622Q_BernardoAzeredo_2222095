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

    public partial class Edit3Campos : Form
    {

        public enum Tabelas
        {
            Ganho,
            Despesa,
            Funcionario,
            Produto,
        }

        private int id;
        private Tabelas tabela;
        private Guna2ComboBox cb = new Guna2ComboBox();

        public Edit3Campos(int userid, int id, Tabelas tabela, string label1, string label2, string label3, string placeholder1, string placeholder2, string placeholder3)
        {
            this.id = id;
            this.tabela = tabela;
            InitializeComponent();
            if (tabela == Tabelas.Produto)
            {
                txb_edit3.Visible = false;
                cb.Location = new System.Drawing.Point(75, 184);
                cb.Size = new System.Drawing.Size(193, 36);
                cb.BorderRadius = 17;
                cb.BorderThickness = 1;
                cb.BorderColor = Color.FromArgb(213, 218, 223);
                cb.ForeColor = Color.White;
                cb.FillColor = Color.FromArgb(34, 34, 46);
                cb.DropDownStyle = ComboBoxStyle.DropDownList; 
                DataTable categorias = Database.GetCategorias(userid);

                if (categorias != null)
                {
                    cb.DataSource = categorias;
                    cb.DisplayMember = "NOME";
                    cb.ValueMember = "ID";
                }
                else
                {
                    MessageBox.Show("Erro ao carregar as categorias.");
                }
                this.Controls.Add(cb);
            }
            lbl_edit1.Text = label1;
            lbl_edit2.Text = label2;
            lbl_edit3.Text = label3;
            txb_edit1.Text = placeholder1;
            txb_edit2.Text = placeholder2;
            txb_edit3.Text = placeholder3;
        }

        private void ConfirmEdit_Click(object sender, EventArgs e)
        {
            switch (tabela)
            {
                case Tabelas.Ganho:
                    Database.EditGanho(id, txb_edit1.Text, float.Parse(txb_edit2.Text));
                    break;
                case Tabelas.Despesa:
                    Database.EditDespesa(id, txb_edit1.Text, float.Parse(txb_edit2.Text));
                    break;
                case Tabelas.Funcionario:
                    Database.EditFuncionario(id, txb_edit1.Text, float.Parse(txb_edit2.Text), txb_edit3.Text);
                    break;
                case Tabelas.Produto:
                    Database.EditProduto(id, txb_edit1.Text, int.Parse(txb_edit2.Text), int.Parse(cb.SelectedValue.ToString()));
                    break;
            }
            MessageBox.Show("Registo editado com sucesso.", "Editado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
