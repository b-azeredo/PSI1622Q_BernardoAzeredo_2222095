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
            try
            {
                if (string.IsNullOrWhiteSpace(txb_nomeUtilizador.Text))
                {
                    MessageBox.Show("Por favor, insira um nome de utilizador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txb_PalavraPasse.Text))
                {
                    MessageBox.Show("Por favor, insira uma palavra-passe.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Database.AdicionarUtilizador(txb_nomeUtilizador.Text, txb_PalavraPasse.Text, guna2CheckBox1.Checked))
                {
                    MessageBox.Show("Utilizador adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar utilizador.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar utilizador: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
