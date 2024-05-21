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
    public partial class AddGanho : Form
    {
        public int userID;
        public AddGanho(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void AddLucro_Load(object sender, EventArgs e)
        {
            
        }

        private void ComfirmAddLucro_Click(object sender, EventArgs e)
        {
            string desc = txb_DescDespesa.Text;
            string valor = txb_ValorDespesa.Text;
            float valorInt;
            if (float.TryParse(valor, out valorInt) && desc.Length > 0)
            {
                if (Database.addLucro(userID, desc, valorInt))
                {
                    MessageBox.Show("Ganho adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar ganho", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
