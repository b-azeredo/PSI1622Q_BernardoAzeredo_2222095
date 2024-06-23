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
            float salario = 0;
            if (float.TryParse(txb_SalarioFuncionario.Text, out salario))
            {
                Database.addFuncionario(userID, txb_NomeFuncionario.Text, salario, int.Parse(cargosComboBox.SelectedValue.ToString()));
                MessageBox.Show("Funcionário adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar a funcionário.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
