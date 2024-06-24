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
    public partial class AddUtilizador : Form
    {
        public AddUtilizador()
        {
            InitializeComponent();
        }

        private void ComfirmAddCategoria_Click(object sender, EventArgs e)
        {
            if (Database.AdicionarUtilizador(txb_nomeUtilizador.Text, txb_PalavraPasse.Text, guna2CheckBox1.Checked))
            {
                MessageBox.Show("Utilizador adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
