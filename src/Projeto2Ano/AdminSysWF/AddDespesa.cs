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
    public partial class AddDespesa : Form
    {
        public int userID;
        public AddDespesa(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void ComfirmAddDespesa_Click(object sender, EventArgs e)
        {
            string desc = txb_DescDespesa.Text;
            string valor = txb_ValorDespesa.Text;
            float valorInt;
            if (float.TryParse(valor, out valorInt) && desc.Length > 0)
            {
                if (Database.addDespesa(userID, desc, valorInt))
                {
                    MessageBox.Show("Despesa adicionada com sucesso.");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Preencha corretamente os valores.");
            }
        }
    }
}
