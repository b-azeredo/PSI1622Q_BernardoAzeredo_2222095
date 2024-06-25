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

        public MainPanel(int userID)
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
            refreshCargosDataGridView();
            refreshFornecedoresDataGridView();
            refreshInvestimentosDataGridView();
            refreshInvestimentosChart();
            refreshDiasComboBox();
            AtualizarInfoGanhos();


            if (Database.isAdmin(userID))
            {
                tabPage8.Visible = true;
                refreshUtilizadoresDataGridView();
                utilizadoresDataGridView.CellValueChanged += UtilizadoresDataGridView_CellValueChanged;
                utilizadoresDataGridView.CurrentCellDirtyStateChanged += UtilizadoresDataGridView_CurrentCellDirtyStateChanged;
            }
            else
            {
                tabPage8.Visible = false;
                guna2TabControl1.TabPages.Remove(tabPage8);
            }

        }

        private void UtilizadoresDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (utilizadoresDataGridView.IsCurrentCellDirty)
            {
                utilizadoresDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void UtilizadoresDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == utilizadoresDataGridView.Columns["ADMIN"].Index)
            {
                int userId = Convert.ToInt32(utilizadoresDataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                bool isAdmin = Convert.ToBoolean(utilizadoresDataGridView.Rows[e.RowIndex].Cells["ADMIN"].Value);

                if (Database.AlterarAdmin(userId, isAdmin))
                {
                    MessageBox.Show("Estado do utilizador atualizado com sucesso.");
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar o estado do utilizador.");
                }
            }
        }

        private void refreshUtilizadoresDataGridView()
        {
            DataTable dt = Database.GetUtilizadores(UserID);
            utilizadoresDataGridView.DataSource = dt;
            utilizadoresDataGridView.Columns[0].Visible = false;
            utilizadoresDataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            utilizadoresDataGridView.Columns[1].ReadOnly = true;
            utilizadoresDataGridView.Columns[2].ReadOnly = true;
            utilizadoresDataGridView.Columns[3].ReadOnly = false;
        }

        private void AtualizarInfoGanhos()
        {
            lbl_GanhosMensais.Text = Database.GetGanhosMensal(UserID).ToString();
            lbl_DespesasMensais.Text = Database.GetDespesasMensal(UserID).ToString();
            if (Database.CalcularPorcentagemVariacaoMensal(UserID) == 0)
            {
                lbl_UltimoMesGanho.Text = "Sem dados";
            }
            else
            {
                lbl_UltimoMesGanho.Text = Database.CalcularPorcentagemVariacaoMensal(UserID).ToString() + "%";
            }
            if (Database.CalcularPorcentagemVariacaoDespesasMensal(UserID) == 0)
            {
                lbl_UltimoMesDespesa.Text = "Sem dados";
            }
            else
            {
                lbl_UltimoMesDespesa.Text = Database.CalcularPorcentagemVariacaoDespesasMensal(UserID).ToString() + "%";
            }
            int diasNoMesAtual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            lbl_MediaGanhos.Text = (Database.GetGanhosMensal(UserID) / diasNoMesAtual).ToString("F2");
            lbl_MediaDespesas.Text = (Database.GetDespesasMensal(UserID) / diasNoMesAtual).ToString("F2");
        }

        private void refreshDiasComboBox()
        {
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                diasComboBox.Items.Add(i.ToString());
            }
            var definicoes = Database.GetDefinicoes(UserID);
            diasComboBox.SelectedIndex = definicoes.diaFuncionario - 1;
        }

        private void diasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.AlterarDiaFuncionario(UserID, diasComboBox.SelectedIndex + 1);
            refreshDespesasDataGridView();
        }

        private void refreshInvestimentosChart()
        {
            DataTable dt = Database.GetHistoricoInvestimentos(UserID);

            gunaChart4.Legend.Display = false;
            gunaChart4.Datasets.Clear();

            var investimentosData = dt.AsEnumerable()
                .GroupBy(row => new { InvestimentoID = row.Field<int>("InvestimentoID"), Descricao = row.Field<string>("DESCRICAO") });

            foreach (var investimentoGroup in investimentosData)
            {
                var series = new GunaLineDataset
                {
                    Label = investimentoGroup.Key.Descricao,
                    BorderWidth = 2,
                    BorderColor = Color.White,
                };

                foreach (var row in investimentoGroup)
                {
                    DateTime data = row.Field<DateTime>("DATA");
                    double valorTotal = row.Field<double>("VALOR_TOTAL");
                    series.DataPoints.Add(data.ToString("dd/MM/yyyy"), valorTotal);
                }

                gunaChart4.Datasets.Add(series);
            }

            gunaChart4.Update();

            float valorInicial = Database.GetInvestimentosValorInicial(UserID);
            float valorTotal2 = Database.GetInvestimentosValorTotal(UserID);

            lbl_ValorInicial.Text = valorInicial.ToString("F2");
            lbl_ValorTotal.Text = valorTotal2.ToString("F2");

            if (valorInicial != 0)
            {
                float ganhoAbsoluto = valorTotal2 - valorInicial;
                float taxaDeGanho = (ganhoAbsoluto / valorInicial) * 100;
                lbl_PercentagemGanho.Text = taxaDeGanho.ToString("F0") + "%";
            }
            else
            {
                lbl_PercentagemGanho.Text = "N/A";
            }

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

        private void refreshCargosDataGridView()
        {
            DataTable dt = Database.GetCargos(UserID);
            CargosDataGridView.DataSource = dt;
            CargosDataGridView.Columns[0].Visible = false;
            CargosDataGridView.Columns[1].Visible = false;
            SetDataGridViewReadOnly(CargosDataGridView);
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
            dt.Columns.Add("PorcentagemGanhoPerda", typeof(string));
            InvestimentosDataGridView.DataSource = dt;
            InvestimentosDataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            InvestimentosDataGridView.Columns[0].Visible = false;
            InvestimentosDataGridView.Columns[1].Visible = false;
            InvestimentosDataGridView.Columns[2].HeaderText = "Descrição";
            InvestimentosDataGridView.Columns[3].HeaderText = "Tipo";
            InvestimentosDataGridView.Columns[4].HeaderText = "Valor Inicial";
            InvestimentosDataGridView.Columns[5].HeaderText = "Valor Atual";
            InvestimentosDataGridView.Columns[6].HeaderText = "Ganho/Perda";

            foreach (DataRow row in dt.Rows)
            {
                double valorInicial = Convert.ToDouble(row[3]);
                double valorAtual = Convert.ToDouble(row[4]);
                double porcentagemGanhoPerda = 0;

                if (valorInicial != 0)
                {
                    porcentagemGanhoPerda = ((valorAtual - valorInicial) / valorInicial) * 100;
                }

                row["PorcentagemGanhoPerda"] = porcentagemGanhoPerda.ToString("F1") + "%";
            }

            SetDataGridViewReadOnly(InvestimentosDataGridView);
            refreshInvestimentosChart();
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
            RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
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
            AtualizarInfoGanhos();
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
            DateTime dataFinal;

            if (days == 30)
            {
                dataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            else
            {
                dataFinal = dataInicial.AddDays(days);
            }

            for (DateTime data = dataInicial; data <= dataFinal; data = data.AddDays(1))
            {
                dias.Add(data);
            }

            chart.Legend.Display = false;

            var series = new GunaLineDataset
            {
                Label = "Valor",
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

                    string weekLabel = $"{startOfWeek:dd} - {endOfWeek:dd}";
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
            if (Database.GetNumeroCargos(UserID) > 0)
            {
                AddFuncionario addFuncionario = new AddFuncionario(UserID);
                addFuncionario.ShowDialog();
                refreshFuncionariosDataGridView();
                refreshDespesasDataGridView();
            }
            else
            {
                MessageBox.Show("Adicione ao menos um cargo antes de adicionar um funcionário.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            HandleDeletion(CargosDataGridView, "dataGridViewCheckBoxColumn5", Database.RemoverCargo);
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
                refreshCargosDataGridView();
                refreshCategoriasDataGridView();
                refreshEstoqueDataGridView();
                RefreshChart(gunaChart1, 7, Database.GetLucroDia);
                RefreshChart(gunaChart2, 30, Database.GetGanhosDia);
                refreshUtilizadoresDataGridView();
                RefreshChart(gunaChart3, 30, Database.GetDespesaDia);
                refreshInvestimentosDataGridView();
                refreshInvestimentosChart();
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
                    case Edit3Campos.Tabelas.Investimento:
                        refreshInvestimentosDataGridView();
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
            refreshDespesasDataGridView();
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

        private void InvestimentosDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InvestimentosDataGridView.Tag = e;
            HandleCellClick(InvestimentosDataGridView, "dataGridViewCheckBoxColumn1", Edit3Campos.Tabelas.Investimento, new List<string> { "Descrição", "Tipo", "Valor Atual" });
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

        private void guna2Panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            AddCargo addCargo = new AddCargo(UserID);
            addCargo.ShowDialog();
            refreshCargosDataGridView();
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            AddUtilizador addUser = new AddUtilizador();
            addUser.ShowDialog();
            refreshUtilizadoresDataGridView();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            HandleDeletion(utilizadoresDataGridView, "dataGridViewCheckBoxColumn2", Database.RemoverUtilizador);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int diasNoMesAtual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            Relatorio.Lucros relatorioLucros = new Relatorio.Lucros
            {
                LucroSemanal = Database.GetLucroSemanal(UserID),
                LucroMensal = Database.GetLucroMensal(UserID),
                LucroUltimoMes = Database.GetLucroMensalMesAnterior(UserID),
                mediaDiaria = (Database.GetLucroMensal(UserID) / diasNoMesAtual).ToString("F2")
            };

            DefinicoesGrafico def = new DefinicoesGrafico(gunaChart1, UserID, relatorioLucros);
            def.ShowDialog();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            int diasNoMesAtual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            Relatorio.Ganhos relatorioGanhos = new Relatorio.Ganhos
            {
                GanhosMensal = Database.GetGanhosMensal(UserID),
                GanhosUltimoMes = Database.GetGanhosMensalMesAnterior(UserID), 
                TaxaEmRelacaoAoUltimoMes = Database.CalcularPorcentagemVariacaoMensal(UserID).ToString() + "%",
                mediaDiaria = (Database.GetGanhosMensal(UserID) / diasNoMesAtual).ToString("F2")
            };

            DefinicoesGrafico def = new DefinicoesGrafico(gunaChart2, UserID, relatorioGanhos);
            def.ShowDialog();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            int diasNoMesAtual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            Relatorio.Despesas relatorioDespesas = new Relatorio.Despesas
            {
                DespesasMensal = Database.GetDespesasMensal(UserID),
                DespesasUltimoMes = Database.GetDespesasMensalMesAnterior(UserID),
                TaxaEmRelacaoAoUltimoMes = Database.CalcularPorcentagemVariacaoDespesasMensal(UserID),
                mediaDiaria = (Database.GetDespesasMensal(UserID) / diasNoMesAtual).ToString("F2")
            };

            DefinicoesGrafico def = new DefinicoesGrafico(gunaChart3, UserID, relatorioDespesas);
            def.ShowDialog();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            float valorInicial = Database.GetInvestimentosValorInicial(UserID);
            float valorTotal2 = Database.GetInvestimentosValorTotal(UserID);
            string variacao;

            if (valorInicial != 0)
            {
                float ganhoAbsoluto = valorTotal2 - valorInicial;
                float taxaDeGanho = (ganhoAbsoluto / valorInicial) * 100;
                variacao = taxaDeGanho.ToString("F0") + "%";
            }
            else
            {
                variacao = "N/A";
            }

            Relatorio.Investimentos relatorioInvestimentos = new Relatorio.Investimentos
            {
                ValorInicial = Database.GetInvestimentosValorInicial(UserID),
                ValorTotal = Database.GetInvestimentosValorTotal(UserID),
                TaxaVariacao = variacao,
                MelhorAtivo = Database.GetMelhorInvestimento(UserID),
                PiorAtivo = Database.GetPiorInvestimento(UserID)
            };

            DefinicoesGrafico def = new DefinicoesGrafico(gunaChart4, UserID, relatorioInvestimentos);
            def.ShowDialog();
        }

    }
}
