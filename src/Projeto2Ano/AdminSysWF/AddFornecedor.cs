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
    public partial class AddFornecedor : Form
    {
        int UserID;
        public AddFornecedor(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
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

        private void ComfirmAddLucro_Click(object sender, EventArgs e)
        {
            if (Database.AddFornecedor(UserID, txb_NomeFornecedor.Text, txb_EmailFornecedor.Text, txb_NumeroFornecedor.Text, int.Parse(categoriasComboBox.SelectedValue.ToString())))
            {
                MessageBox.Show("Fornecedor adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar a funcionário.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
