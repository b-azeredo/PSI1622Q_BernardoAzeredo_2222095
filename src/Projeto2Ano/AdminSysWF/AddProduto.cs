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
    public partial class AddProduto : Form
    {
        int UserID;
        public AddProduto(int id)
        {
            InitializeComponent();
            UserID = id;
            FillCategoriasComboBox();
        }

        public void FillCategoriasComboBox()
        {
            DataTable categorias = Database.GetCategorias(UserID);

            if (categorias != null)
            {
                categoriasComboBox.DataSource = categorias;
                categoriasComboBox.DisplayMember = "NOME";
                categoriasComboBox.ValueMember = "ID";
            }
            else
            {
                MessageBox.Show("Erro ao carregar as categorias.");
            }
        }

        private void ComfirmAddProduto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txb_NomeProduto.Text))
                {
                    MessageBox.Show("Por favor, insira um nome para o produto.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txb_QuantidadeProduto.Text, out int quantidade) || quantidade <= 0)
                {
                    MessageBox.Show("Por favor, insira uma quantidade válida para o produto.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (categoriasComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione uma categoria para o produto.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Database.AddProduto(UserID, txb_NomeProduto.Text, quantidade, int.Parse(categoriasComboBox.SelectedValue.ToString())))
                {
                    MessageBox.Show("Produto adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar produto.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar produto: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
