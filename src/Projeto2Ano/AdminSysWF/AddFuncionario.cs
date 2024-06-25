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
    public partial class AddFuncionario : Form
    {
        int userID;
        public AddFuncionario(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            FillCargosComboBox();
        }

        public void FillCargosComboBox()
        {
            DataTable cargos = Database.GetCargos(userID);

            if (cargos != null)
            {
                cargosComboBox.DataSource = cargos;
                cargosComboBox.DisplayMember = "NOME";
                cargosComboBox.ValueMember = "ID";
            }
            else
            {
                MessageBox.Show("Erro ao carregar os cargos.");
            }
        }

        private void ComfirmAddLucro_Click(object sender, EventArgs e)
        {
            string nomeFuncionario = txb_NomeFuncionario.Text;
            string salarioText = txb_SalarioFuncionario.Text;
            int cargoID = int.Parse(cargosComboBox.SelectedValue.ToString());

            if (string.IsNullOrWhiteSpace(nomeFuncionario))
            {
                MessageBox.Show("O nome do funcionário não pode estar vazio.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nomeFuncionario.Length > 50)
            {
                MessageBox.Show("O nome do funcionário deve ter no máximo 50 caracteres.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(salarioText, out float salario) || salario <= 0)
            {
                MessageBox.Show("O salário fornecido não é válido ou é menor ou igual a zero.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cargoID <= 0)
            {
                MessageBox.Show("Por favor, selecione um cargo válido.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Database.addFuncionario(userID, nomeFuncionario, salario, cargoID))
            {
                MessageBox.Show("Funcionário adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar o funcionário.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
