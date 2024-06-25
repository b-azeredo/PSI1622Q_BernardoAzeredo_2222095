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

            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("A descrição do ganho não pode estar vazia.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (desc.Length > 100)
            {
                MessageBox.Show("A descrição do ganho deve ter no máximo 100 caracteres.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(valor, out float valorFloat) || valorFloat <= 0)
            {
                MessageBox.Show("O valor fornecido não é válido ou é menor ou igual a zero.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Database.addLucro(userID, desc, valorFloat))
            {
                MessageBox.Show("Ganho adicionado com sucesso.", "Adicionado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar ganho.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
