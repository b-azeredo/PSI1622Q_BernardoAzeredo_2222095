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
    public partial class AddCategoria : Form
    {
        public int userID;
        public AddCategoria(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void ComfirmAddCategoria_Click(object sender, EventArgs e)
        {
            if (Database.AddCategoria(userID, txb_nomeCategoria.Text))
            {
                MessageBox.Show("Categoria adicionada com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar a categoria.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
