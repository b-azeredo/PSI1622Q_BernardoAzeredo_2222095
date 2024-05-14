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
        }

        private void ComfirmAddLucro_Click(object sender, EventArgs e)
        {
            float salario = 0;
            if (float.TryParse(txb_SalarioFuncionario.Text, out salario))
            {
                Database.addFuncionario(userID, txb_NomeFuncionario.Text, salario, txb_CargoFuncionario.Text);
                MessageBox.Show("Funcionário adicionado com sucesso.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha os dados corretamente.");
            }
        }
    }
}
