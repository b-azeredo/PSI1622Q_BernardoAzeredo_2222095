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
            Investimento
        }

        private int id;
        private Tabelas tabela;
        private Guna2ComboBox cb = new Guna2ComboBox();

        public Edit3Campos(int userid, int id, Tabelas tabela, string label1, string label2, string label3, string placeholder1, string placeholder2, string placeholder3)
        {

            this.id = id;
            this.tabela = tabela;
            InitializeComponent();
            if (tabela == Tabelas.Ganho || tabela == Tabelas.Despesa)
            {
                txb_edit3.Visible = false;
                lbl_edit3.Visible = false;
            }
            if (tabela == Tabelas.Investimento)
            {
                txb_edit3.Text = "";
                txb_edit2.Visible = false;
                cb.Location = new System.Drawing.Point(75, 119);
                cb.Size = new System.Drawing.Size(193, 36);
                cb.BorderRadius = 17;
                cb.BorderThickness = 1;
                cb.BorderColor = Color.FromArgb(213, 218, 223);
                cb.ForeColor = Color.White;
                cb.FillColor = Color.FromArgb(34, 34, 46);
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                DataTable categorias = Database.GetTiposInvestimentos();
                this.Controls.Add(cb);

                if (categorias != null)
                {
                    cb.DataSource = categorias;
                    cb.DisplayMember = "TIPO";
                    cb.ValueMember = "ID";
                }
                else
                {
                    MessageBox.Show("Erro ao carregar as categorias.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (DataRow row in categorias.Rows)
                {
                    if (row["TIPO"].ToString() == placeholder2)
                    {
                        cb.SelectedValue = int.Parse(row["ID"].ToString());
                        break;
                    }
                }
            }
            else if (tabela == Tabelas.Produto)
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
                    if (row["NOME"].ToString() == placeholder3)
                    {
                        cb.SelectedValue = int.Parse(row["ID"].ToString());
                        break;
                    }
                }
            }
            else if (tabela == Tabelas.Funcionario)
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
                DataTable categorias = Database.GetCargos(userid);
                this.Controls.Add(cb);

                if (categorias != null)
                {
                    cb.DataSource = categorias;
                    cb.DisplayMember = "NOME";
                    cb.ValueMember = "ID";
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os cargos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (DataRow row in categorias.Rows)
                {
                    if (row["NOME"].ToString() == placeholder3)
                    {
                        cb.SelectedValue = int.Parse(row["ID"].ToString());
                        break;
                    }
                }
            }
            lbl_edit1.Text = label1;
            lbl_edit2.Text = label2;
            lbl_edit3.Text = label3;
            txb_edit1.Text = placeholder1;
            txb_edit2.Text = placeholder2;
            if (!(tabela == Tabelas.Investimento))
            {
                txb_edit3.Text = placeholder3;
            }
        }

        private void ConfirmEdit_Click(object sender, EventArgs e)
        {
            bool result = false;
            switch (tabela)
            {
                case Tabelas.Ganho:
                    result = Database.EditGanho(id, txb_edit1.Text, txb_edit2.Text);
                    break;
                case Tabelas.Despesa:
                    result = Database.EditDespesa(id, txb_edit1.Text, txb_edit2.Text);
                    break;
                case Tabelas.Funcionario:
                    result = Database.EditFuncionario(id, txb_edit1.Text, txb_edit2.Text, int.Parse(cb.SelectedValue.ToString()));
                    break;
                case Tabelas.Produto:
                    result = Database.EditProduto(id, txb_edit1.Text, txb_edit2.Text, int.Parse(cb.SelectedValue.ToString()));
                    break;
                case Tabelas.Investimento:
                    result = Database.EditInvestimento(id, int.Parse(cb.SelectedValue.ToString()), txb_edit1.Text, txb_edit3.Text);
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
