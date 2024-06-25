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
            float valorFloat;

            if (desc.Length > 25)
            {
                MessageBox.Show("A descrição da despesa deve ter no máximo 25 letras.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (float.TryParse(valor, out valorFloat) && valorFloat > 0 && valorFloat <= 10000)
            {
                if (Database.addDespesa(userID, desc, valorFloat))
                {
                    MessageBox.Show("Despesa adicionada com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar a despesa.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("O valor da despesa deve ser um número válido, positivo e não exceder 10000.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
