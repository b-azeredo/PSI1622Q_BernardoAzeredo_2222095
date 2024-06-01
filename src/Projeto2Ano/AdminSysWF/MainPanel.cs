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

        private void SetDataGridViewReadOnly(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (i != 0) // Índice 0 é a primeira coluna
                {
                    dataGridView.Columns[i].ReadOnly = true;
                }
            }
        }

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
            FornecedoresDataGridView.Columns[0].Visible = false;
            FornecedoresDataGridView.Columns[1].Visible = false;
            FornecedoresDataGridView.Columns[2].HeaderText = "Nome";
            FornecedoresDataGridView.Columns[3].HeaderText = "Email";
            FornecedoresDataGridView.Columns[4].HeaderText = "Telefone";
            FornecedoresDataGridView.Columns[5].HeaderText = "Categoria";
            SetDataGridViewReadOnly(FornecedoresDataGridView);
        }

        private void refreshEstoqueDataGridView()
        {
            DataTable dt = Database.GetEstoque(UserID);
            EstoqueDataGridView.DataSource = dt;
            EstoqueDataGridView.Columns[0].Visible = false;
            EstoqueDataGridView.Columns[1].Visible = false;
            EstoqueDataGridView.Columns[2].HeaderText = "Produto";
            EstoqueDataGridView.Columns[3].HeaderText = "Quantidade";
            EstoqueDataGridView.Columns[4].HeaderText = "Categoria";
            SetDataGridViewReadOnly(EstoqueDataGridView);
        }

        private void refreshCategoriasDataGridView()
        {
            DataTable dt = Database.GetCategorias(UserID);
            CategoriasDataGridView.DataSource = dt;
            CategoriasDataGridView.Columns[0].Visible = false;
            CategoriasDataGridView.Columns[1].Visible = false;
            SetDataGridViewReadOnly(CategoriasDataGridView);
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
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Descrição";
            dataGridView1.Columns[3].HeaderText = "Valor";
            dataGridView1.Columns[4].HeaderText = "Data";
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            refreshLucroAtual();
            SetDataGridViewReadOnly(dataGridView1);
        }

        private void refreshDespesasDataGridView()
        {
            DataTable dt = Database.GetDespesas(UserID);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].HeaderText = "Descrição";
            dataGridView2.Columns[3].HeaderText = "Valor";
            dataGridView2.Columns[4].HeaderText = "Data";
            dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            refreshLucroAtual();
            SetDataGridViewReadOnly(dataGridView2);
        }

        private void refreshFuncionariosDataGridView()
        {
            DataTable dt = Database.GetFuncionarios(UserID);
            dataGridView3.DataSource = dt;
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[2].HeaderText = "Nome";
            dataGridView3.Columns[3].HeaderText = "Salário";
            dataGridView3.Columns[4].HeaderText = "Cargo";
            dataGridView3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SetDataGridViewReadOnly(dataGridView3);
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
            if (Database.GetNumeroDeCategorias(UserID) > 0)
            {
                AddProduto addProduto = new AddProduto(UserID);
                addProduto.ShowDialog();
                refreshEstoqueDataGridView();
            }
            else
            {
                MessageBox.Show("Adicione ao menos uma categoria antes de adicionar um produto.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HandleDeletion(dataGridView1, "CheckboxColumn", Database.RemoverGanho);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            HandleDeletion(dataGridView2, "CheckboxColumn2", Database.RemoverDespesa);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            HandleDeletion(dataGridView3, "CheckboxColumn3", Database.RemoverFuncionario);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            HandleDeletion(FornecedoresDataGridView, "CheckboxColumn4", Database.RemoverFornecedor);
        }
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            HandleDeletion(EstoqueDataGridView, "CheckboxColumn5", Database.RemoverProduto);

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            HandleDeletion(CategoriasDataGridView, "CheckboxColumn6", Database.RemoverCategoria);
        }

        private void HandleDeletion(DataGridView dataGridView, string checkBoxColumnName, Func<int, bool> removeMethod)
        {
            dataGridView.Columns[checkBoxColumnName].DisplayIndex = dataGridView.Columns.Count - 1;

            if (dataGridView.Columns[checkBoxColumnName].Visible == true)
            {
                bool anySelected = false;
                bool allDeleted = true;

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkBoxCell = dataGridView.Rows[i].Cells[checkBoxColumnName] as DataGridViewCheckBoxCell;

                    if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                    {
                        anySelected = true;
                        int id = Convert.ToInt32(dataGridView.Rows[i].Cells[1].Value);
                        bool removed = removeMethod(id);
                        if (!removed)
                        {
                            allDeleted = false;
                        }
                    }
                }

                if (!anySelected)
                {
                    MessageBox.Show("Nenhum registo selecionado para exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (!allDeleted)
                {
                    MessageBox.Show("Alguns registos não puderam ser removidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                dataGridView.Columns[checkBoxColumnName].Visible = false;
                refreshLucrosDataGridView();
                refreshDespesasDataGridView();
                refreshFuncionariosDataGridView();
                refreshFornecedoresDataGridView();
                refreshCategoriasDataGridView();
                refreshEstoqueDataGridView();
            }
            else
            {
                dataGridView.Columns[checkBoxColumnName].Visible = true;
            }
        }
        private void HandleCellClick3Fields(DataGridView dataGridView, string checkBoxColumnName, Edit3Campos.Tabelas tabela, string field1, string field2, string field3)
        {
            if (dataGridView.Columns[checkBoxColumnName].Visible)
            {
                return;
            }

            var e = (DataGridViewCellEventArgs)dataGridView.Tag;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string texto1 = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string texto2 = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                string texto3 = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                int id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                Edit3Campos editDialog = new Edit3Campos(UserID ,id, tabela, field1, field2, field3, texto1, texto2, texto3);
                editDialog.ShowDialog();
                if (tabela == Edit3Campos.Tabelas.Ganho)
                {
                    refreshLucrosDataGridView();
                }
                else if (tabela == Edit3Campos.Tabelas.Despesa)
                {
                    refreshDespesasDataGridView();
                }
                else if (tabela == Edit3Campos.Tabelas.Funcionario)
                {
                    refreshFuncionariosDataGridView();
                }
                else
                {
                    refreshEstoqueDataGridView();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Tag = e;
            HandleCellClick3Fields(dataGridView1, "CheckBoxColumn", Edit3Campos.Tabelas.Ganho, "Descrição", "Valor", "Data");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Tag = e;
            HandleCellClick3Fields(dataGridView2, "CheckBoxColumn2", Edit3Campos.Tabelas.Despesa, "Descrição", "Valor", "Data");
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Tag = e;
            HandleCellClick3Fields(dataGridView3, "CheckBoxColumn3", Edit3Campos.Tabelas.Funcionario, "Nome", "Salário", "Cargo");
        }

        private void EstoqueDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstoqueDataGridView.Tag = e;
            HandleCellClick3Fields(EstoqueDataGridView, "CheckBoxColumn5", Edit3Campos.Tabelas.Produto, "Nome", "Quantidade", "Categoria");
        }
    }
}
