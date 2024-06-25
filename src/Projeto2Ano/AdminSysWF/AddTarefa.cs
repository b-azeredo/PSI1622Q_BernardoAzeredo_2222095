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
    public partial class AddTarefa : Form
    {
        int userID;
        public AddTarefa(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void ComfirmAddDespesa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txb_DescTarefa.Text))
                {
                    MessageBox.Show("Por favor, insira uma descrição para a tarefa.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Database.AddTarefa(userID, txb_DescTarefa.Text))
                {
                    MessageBox.Show("Tarefa adicionada com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar tarefa.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar tarefa: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

    }
}
