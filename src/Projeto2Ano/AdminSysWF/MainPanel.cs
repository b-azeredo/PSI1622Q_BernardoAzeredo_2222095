using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AdminSysWF
{
    public partial class MainPanel : Form
    {
        int UserID;
        public MainPanel(int userID, string username)
        {
            InitializeComponent();
            UserID = userID;

            RefreshChart();
            refreshLucrosDataGridView();
            refreshDespesasDataGridView();
            refreshTarefasDataGridView();
            refreshFuncionariosDataGridView();
            refreshLucroAtual();
            refreshEstoqueDataGridView();
            refreshCategoriasDataGridView();
            refreshFornecedoresDataGridView();
        }

        private void refreshLucroAtual()
        {
            float lucroHoje = Database.GetLucroDia(DateTime.Now, UserID);
            lbl_LucroHoje.Text = lucroHoje.ToString() + "€";
            if (lucroHoje < 0)
            {
                lbl_LucroHoje.ForeColor = Color.Red;
            }
            else
            {
                lbl_LucroHoje.ForeColor = Color.LightGreen;
            }
        }

        private void refreshFornecedoresDataGridView()
        {
            DataTable dt = Database.GetFornecedores(UserID);
            FornecedoresDataGridView.DataSource = dt;
        }

        private void refreshEstoqueDataGridView()
        {
            DataTable dt = Database.GetEstoque(UserID);
            EstoqueDataGridView.DataSource = dt;
        }

        private void refreshCategoriasDataGridView()
        {
            DataTable dt = Database.GetCategorias(UserID);
            CategoriasDataGridView.DataSource = dt;
            CategoriasDataGridView.Columns[0].Visible = false;
        }

        private void refreshTarefasDataGridView()
        {
            DataTable dt = Database.GetTarefas(UserID);
            tarefasDataGridView.DataSource = dt;
            tarefasDataGridView.Columns["Concluir"].DisplayIndex = 2;
            tarefasDataGridView.Columns[1].Visible = false;
        }

        private void refreshLucrosDataGridView()
        {
            DataTable dt = Database.GetGanhos(UserID);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Descrição";
            dataGridView1.Columns[1].HeaderText = "Valor";
            dataGridView1.Columns[2].HeaderText = "Data";
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void refreshDespesasDataGridView()
        {
            DataTable dt = Database.GetDespesas(UserID);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].HeaderText = "Descrição";
            dataGridView2.Columns[1].HeaderText = "Valor";
            dataGridView2.Columns[2].HeaderText = "Data";
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void refreshFuncionariosDataGridView()
        {
            DataTable dt = Database.GetFuncionarios(UserID);
            dataGridView3.DataSource = dt;
            dataGridView3.Columns[0].HeaderText = "Nome";
            dataGridView3.Columns[1].HeaderText = "Salário";
            dataGridView3.Columns[2].HeaderText = "Cargo";
            dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RefreshChart()
        {
            DateTime dataInicial = DateTime.Now.AddDays(-7);
            List<DateTime> ultimos7dias = new List<DateTime>();
            for (int i = 0; i <= 7; i++)
            {
                ultimos7dias.Add(dataInicial.AddDays(i));
            }

            gunaChart1.Legend.Display = false;

            var series = new Guna.Charts.WinForms.GunaLineDataset
            {
                Label = "Lucro",
                BorderWidth = 1,
                BorderColor = Color.DarkGray,
            };

            float lucroMaximo = float.MinValue;

            foreach (DateTime dia in ultimos7dias)
            {
                float lucroDespesaDia = Database.GetLucroDia(dia, UserID);
                series.DataPoints.Add(dia.ToString("dd/MM"), lucroDespesaDia);

                if (lucroDespesaDia > lucroMaximo)
                {
                    lucroMaximo = lucroDespesaDia;
                }
            }

            gunaChart1.Datasets.Clear();
            gunaChart1.Datasets.Add(series);

            gunaChart1.Update();
        }

        private void btn_AddLucro_Click_1(object sender, EventArgs e)
        {
            AddGanho addLucro = new AddGanho(UserID);
            addLucro.ShowDialog();
            RefreshChart();
            refreshLucrosDataGridView();
            refreshLucroAtual();
        }

        private void btn_AddDespesa_Click_1(object sender, EventArgs e)
        {
            AddDespesa addDespesa = new AddDespesa(UserID);
            addDespesa.ShowDialog();
            RefreshChart();
            refreshDespesasDataGridView();
            refreshLucroAtual();
        }

        private void btnAddFuncionario_Click(object sender, EventArgs e)
        {
            AddFuncionario addFuncionario = new AddFuncionario(UserID);
            addFuncionario.ShowDialog();
            refreshFuncionariosDataGridView();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Definicoes definicoes = new Definicoes();
            definicoes.ShowDialog();
        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void tarefasDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tarefasDataGridView.Columns[0].Index && e.RowIndex >= 0)
            {
                int tarefaId = Convert.ToInt32(tarefasDataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                Database.ConcluirTarefa(tarefaId);
                refreshTarefasDataGridView();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddTarefa addTarefa = new AddTarefa(UserID);
            addTarefa.ShowDialog();
            refreshTarefasDataGridView();
        }

        private void tarefasDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (e.ColumnIndex == Concluir.Index)
                {
                    e.CellStyle.BackColor = Color.Green;

                    e.PaintContent(e.CellBounds);

                    using (Pen p = new Pen(Color.Red))
                    {
                        Rectangle rect = e.CellBounds;
                        rect.Width -= 1;
                        rect.Height -= 1;
                        e.Graphics.DrawRectangle(p, rect);
                    }

                    e.Handled = true;
                }
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            AddCategoria addCategoria = new AddCategoria(UserID);
            addCategoria.ShowDialog();
            refreshCategoriasDataGridView();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            AddProduto addProduto = new AddProduto(UserID);
            addProduto.ShowDialog();
            refreshEstoqueDataGridView();
        }

        private void btn_AddFornecedores_Click(object sender, EventArgs e)
        {
            if (Database.GetNumeroDeCategorias(UserID) > 0)
            {
                AddFornecedor addFornecedor = new AddFornecedor(UserID);
                addFornecedor.ShowDialog();
                refreshFornecedoresDataGridView();
            }
            else
            {
                MessageBox.Show("Adicione ao menos uma categoria antes de adicionar um fornecedor.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
