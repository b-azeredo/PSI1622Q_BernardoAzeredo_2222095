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
            string nomeFornecedor = txb_NomeFornecedor.Text;
            string emailFornecedor = txb_EmailFornecedor.Text;
            string numeroFornecedor = txb_NumeroFornecedor.Text;
            int categoriaID = int.Parse(categoriasComboBox.SelectedValue.ToString());

            if (nomeFornecedor.Length > 25)
            {
                MessageBox.Show("O nome do fornecedor deve ter no máximo 25 caracteres.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(emailFornecedor))
            {
                MessageBox.Show("O e-mail fornecido não é válido.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidPhoneNumber(numeroFornecedor))
            {
                MessageBox.Show("O número de telefone fornecido não é válido. Deve conter apenas dígitos e ter no mínimo 9 dígitos.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Database.AddFornecedor(UserID, nomeFornecedor, emailFornecedor, numeroFornecedor, categoriaID))
            {
                MessageBox.Show("Fornecedor adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar o fornecedor.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 9;
        }


    }
}
