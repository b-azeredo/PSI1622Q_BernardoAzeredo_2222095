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
            int quantidade;
            if (int.TryParse(txb_QuantidadeProduto.Text, out quantidade))
            {
                if (Database.AddProduto(UserID, txb_NomeProduto.Text, quantidade, int.Parse(categoriasComboBox.SelectedValue.ToString())))
                {
                    MessageBox.Show("Produto adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar produto", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Erro ao adicionar produto, digite uma quantidade válida.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
