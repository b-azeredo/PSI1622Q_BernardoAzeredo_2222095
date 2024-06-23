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
            if (Database.AddCargo(userID, txb_nomeCargo.Text))
            {
                MessageBox.Show("Cargo adicionada com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar a categoria.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
