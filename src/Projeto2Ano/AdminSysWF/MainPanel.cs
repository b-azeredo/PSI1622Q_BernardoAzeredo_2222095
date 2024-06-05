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
                if (i != 0)
                {
                    dataGridView.Columns[i].ReadOnly = true;
                }
            }
        }

        public MainPanel(int userID, string username)
        {
            InitializeComponent();
            UserID = userID;

            RefreshChart(gunaChart1, 7, Database.GetLucroDia);
            RefreshChart(gunaChart2, 30, Database.GetGanhosDia);
            RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
            refreshLucrosDataGridView();
            refreshDespesasDataGridView();
            refreshTarefasDataGridView();
            refreshFuncionariosDataGridView();
            refreshLabels();
            refreshEstoqueDataGridView();
            refreshCategoriasDataGridView();
            refreshFornecedoresDataGridView();
            refreshInvestimentosDataGridView();
        }

        private void refreshNotificacoesDataGridView()
        {
            DataTable dt = Database.GetLowEstoque(UserID);
            dt.Columns.Add("NotificationText", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["NotificationText"] = "Estoque a acabar: " + row["Produto"].ToString();
            }
            NotificacoesDataGridView.DataSource = dt;

            NotificacoesDataGridView.Columns["NotificationText"].Width = 300;
            SetDataGridViewReadOnly(NotificacoesDataGridView);
            NotificacoesDataGridView.Columns["Produto"].Visible = false;

        }

        private void refreshLabels()
        {
            float lucroSemanal = Database.GetLucroSemanal(UserID);
            int tarefas = Database.GetNumeroTarefasConcluidasUltimaSemana(UserID);
            lbl_TarefasConcluidas.Text = tarefas.ToString();
            lbl_LucroSemanal.Text = lucroSemanal.ToString() + "€";
            if (lucroSemanal < 0)
            {
                lbl_LucroSemanal.ForeColor = Color.Red;
            }
            else
            {
                lbl_LucroSemanal.ForeColor = Color.LightGreen;
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
            refreshNotificacoesDataGridView();
            SetDataGridViewReadOnly(EstoqueDataGridView);
        }

        private void refreshInvestimentosDataGridView()
        {
            DataTable dt = Database.GetInvestimentos(UserID);
            InvestimentosDataGridView.DataSource = dt;

            InvestimentosDataGridView.Columns[0].Visible = false;
            InvestimentosDataGridView.Columns[1].Visible = false;
            InvestimentosDataGridView.Columns[2].HeaderText = "Descrição";
            InvestimentosDataGridView.Columns[3].HeaderText = "Tipo";
            InvestimentosDataGridView.Columns[4].HeaderText = "Valor Inicial";
            InvestimentosDataGridView.Columns[5].HeaderText = "Valor Atual";
            SetDataGridViewReadOnly(InvestimentosDataGridView);
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
            refreshLabels();
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
            refreshLabels();
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

        private void RefreshChart(GunaChart chart, int days, Func<DateTime, int, float> metodo)
        {
            DateTime dataInicial;

            if (days == 7)
            {
                dataInicial = DateTime.Now;
                while (dataInicial.DayOfWeek != DayOfWeek.Sunday)
                {
                    dataInicial = dataInicial.AddDays(-1);
                }
            }
            else if (days == 30)
            {
                dataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                dataInicial = DateTime.Now.AddDays(-days);
            }

            List<DateTime> dias = new List<DateTime>();
            for (int i = 0; i <= days; i++)
            {
                dias.Add(dataInicial.AddDays(i));
            }

            chart.Legend.Display = false;

            var series = new GunaLineDataset
            {
                Label = "Lucro",
                BorderWidth = 2,
                BorderColor = Color.White,
            };

            if (days > 7)
            {
                var weeklyData = new Dictionary<string, float>();
                for (int i = 0; i < dias.Count; i += 7)
                {
                    DateTime startOfWeek = dias[i];
                    DateTime endOfWeek = dias[Math.Min(i + 6, dias.Count - 1)];

                    float sum = 0;
                    for (DateTime day = startOfWeek; day <= endOfWeek; day = day.AddDays(1))
                    {
                        sum += metodo(day, UserID);
                    }

                    string weekLabel = $"{startOfWeek.ToString("dd")} - {endOfWeek.ToString("dd")}";
                    weeklyData[weekLabel] = sum;
                }

                foreach (var data in weeklyData)
                {
                    series.DataPoints.Add(data.Key, data.Value);
                }
            }
            else
            {
                foreach (DateTime dia in dias)
                {
                    float eixoY = metodo(dia, UserID);
                    series.DataPoints.Add(dia.ToString("dd/MM"), eixoY);
                }
            }

            chart.Datasets.Clear();
            chart.Datasets.Add(series);

            chart.Update();
        }



        private void btn_AddLucro_Click_1(object sender, EventArgs e)
        {
            AddGanho addLucro = new AddGanho(UserID);
            addLucro.ShowDialog();
            RefreshChart(gunaChart1, 7, Database.GetLucroDia);
            RefreshChart(gunaChart2, 30, Database.GetGanhosDia);
            refreshLucrosDataGridView();
            refreshLabels();
        }

        private void btn_AddDespesa_Click_1(object sender, EventArgs e)
        {
            AddDespesa addDespesa = new AddDespesa(UserID);
            addDespesa.ShowDialog();
            RefreshChart(gunaChart1, 7, Database.GetLucroDia);
            refreshDespesasDataGridView();
            refreshLabels();
            RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
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
            RefreshChart(gunaChart1, 7, Database.GetLucroDia);
        }

        private void tarefasDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tarefasDataGridView.Columns[0].Index && e.RowIndex >= 0)
            {
                int tarefaId = Convert.ToInt32(tarefasDataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                Database.ConcluirTarefa(tarefaId);
                refreshTarefasDataGridView();
                refreshLabels();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddTarefa addTarefa = new AddTarefa(UserID);
            addTarefa.ShowDialog();
            refreshTarefasDataGridView();
            refreshLabels();
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
                RefreshChart(gunaChart1, 7, Database.GetLucroDia);
                RefreshChart(gunaChart2, 30, Database.GetGanhosDia);
                RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
                refreshInvestimentosDataGridView();
            }
            else
            {
                dataGridView.Columns[checkBoxColumnName].Visible = true;
            }
        }
        private void HandleCellClick(DataGridView dataGridView, string checkBoxColumnName, Enum tabela, List<string> fields)
        {
            if (dataGridView.Columns[checkBoxColumnName].Visible)
            {
                return;
            }

            var e = (DataGridViewCellEventArgs)dataGridView.Tag;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                List<string> cellValues = new List<string>();
                for (int i = 0; i < fields.Count; i++)
                {
                    cellValues.Add(dataGridView.Rows[e.RowIndex].Cells[2 + i].Value.ToString());
                }
                int id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                if (fields.Count == 3)
                {
                    Edit3Campos editDialog = new Edit3Campos(UserID, id, (Edit3Campos.Tabelas)tabela, fields[0], fields[1], fields[2], cellValues[0], cellValues[1], cellValues[2]);
                    editDialog.ShowDialog();
                    RefreshDataGridView(tabela);
                }
                else if (fields.Count == 4)
                {
                    Edit4Campos editDialog = new Edit4Campos(UserID, id, (Edit4Campos.Tabelas)tabela, fields[0], fields[1], fields[2], fields[3], cellValues[0], cellValues[1], cellValues[2], cellValues[3]);
                    editDialog.ShowDialog();
                    RefreshDataGridView(tabela);
                }
            }
        }

        private void RefreshDataGridView(Enum tabela)
        {
            if (tabela is Edit3Campos.Tabelas)
            {
                var tabela3Campos = (Edit3Campos.Tabelas)tabela;
                switch (tabela3Campos)
                {
                    case Edit3Campos.Tabelas.Ganho:
                        refreshLucrosDataGridView();
                        RefreshChart(gunaChart1, 7, Database.GetLucroDia);
                        RefreshChart(gunaChart2, 30, Database.GetGanhosDia);
                        break;
                    case Edit3Campos.Tabelas.Despesa:
                        refreshDespesasDataGridView();
                        RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
                        break;
                    case Edit3Campos.Tabelas.Funcionario:
                        refreshFuncionariosDataGridView();
                        break;
                    case Edit3Campos.Tabelas.Produto:
                        refreshEstoqueDataGridView();
                        break;
                }
            }
            else if (tabela is Edit4Campos.Tabelas)
            {
                var tabela4Campos = (Edit4Campos.Tabelas)tabela;
                if (tabela4Campos == Edit4Campos.Tabelas.Fornecedor)
                {
                    refreshFornecedoresDataGridView();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Tag = e;
            HandleCellClick(dataGridView1, "CheckBoxColumn", Edit3Campos.Tabelas.Ganho, new List<string> { "Descrição", "Valor", "Data" });
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Tag = e;
            HandleCellClick(dataGridView2, "CheckBoxColumn2", Edit3Campos.Tabelas.Despesa, new List<string> { "Descrição", "Valor", "Data" });
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Tag = e;
            HandleCellClick(dataGridView3, "CheckBoxColumn3", Edit3Campos.Tabelas.Funcionario, new List<string> { "Nome", "Salário", "Cargo" });
        }

        private void EstoqueDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EstoqueDataGridView.Tag = e;
            HandleCellClick(EstoqueDataGridView, "CheckBoxColumn5", Edit3Campos.Tabelas.Produto, new List<string> { "Nome", "Quantidade", "Categoria" });
        }

        private void FornecedoresDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FornecedoresDataGridView.Tag = e;
            HandleCellClick(FornecedoresDataGridView, "CheckBoxColumn4", Edit4Campos.Tabelas.Fornecedor, new List<string> { "Nome", "Email", "Telefone", "Categoria" });
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DefinicoesGrafico def = new DefinicoesGrafico(gunaChart1);
            def.ShowDialog();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            AddInvestimento add = new AddInvestimento(UserID);
            add.ShowDialog();
            refreshInvestimentosDataGridView();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            HandleDeletion(InvestimentosDataGridView, "dataGridViewCheckBoxColumn1", Database.RemoverInvestimento);
        }

        private void InvestimentosDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
