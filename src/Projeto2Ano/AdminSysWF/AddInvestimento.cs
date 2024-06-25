using System;
using System.Data;
using System.Windows.Forms;

namespace AdminSysWF
{
    public partial class AddInvestimento : Form
    {
        int UserID;
        public AddInvestimento(int userID)
        {
            InitializeComponent();
            this.UserID = userID;
            LoadTiposInvestimentos();
        }

        private void LoadTiposInvestimentos()
        {
            try
            {
                DataTable tiposInvestimentos = Database.GetTiposInvestimentos();
                tiposComboBox.DataSource = tiposInvestimentos;
                tiposComboBox.DisplayMember = "TIPO";
                tiposComboBox.ValueMember = "ID";
                tiposComboBox.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os tipos de investimentos: " + ex.Message);
            }
        }

        private void ComfirmAddCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txb_DescricaoInvestimento.Text))
                {
                    MessageBox.Show("Por favor, preencha a descrição do investimento.");
                    return;
                }

                if (tiposComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um tipo de investimento.");
                    return;
                }

                if (!decimal.TryParse(ValorInvestido.Text, out decimal valorInvestido) || valorInvestido <= 0)
                {
                    MessageBox.Show("Por favor, insira um valor investido válido e positivo.");
                    return;
                }

                int tipoInvestimento = int.Parse(tiposComboBox.SelectedValue.ToString());
                string descricao = txb_DescricaoInvestimento.Text;
                decimal valorTotal = valorInvestido;

                if (Database.AddInvestimento(UserID, tipoInvestimento, descricao, valorInvestido, valorTotal))
                {
                    MessageBox.Show("Investimento adicionado com sucesso!");
                    tiposComboBox.SelectedIndex = -1;
                    txb_DescricaoInvestimento.Clear();
                    ValorInvestido.Clear();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar o investimento.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar o investimento: " + ex.Message);
            }
            this.Close();
        }
    }
}
