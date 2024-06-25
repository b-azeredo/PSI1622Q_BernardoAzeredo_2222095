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
    public partial class AddCargo : Form
    {
        public int userID;

        public AddCargo(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void ComfirmAddCategoria_Click(object sender, EventArgs e)
        {
            string nomeCargo = txb_nomeCargo.Text;

            if (nomeCargo.Length > 25)
            {
                MessageBox.Show("O nome do cargo deve ter no máximo 25 letras.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Database.AddCargo(userID, nomeCargo))
            {
                MessageBox.Show("Cargo adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar o cargo.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
