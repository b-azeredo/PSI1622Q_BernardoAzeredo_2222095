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

        private void RefreshChart()
        {
            DateTime dataInicial = DateTime.Now.AddDays(-7);
            List<DateTime> ultimos7dias = new List<DateTime>();
            for (int i = 0; i <= 7; i++)
            {
                ultimos7dias.Add(dataInicial.AddDays(i));
            }

            Series series = new Series("Lucro");
            series.ChartType = SeriesChartType.Column;

            float lucroMaximo = float.MinValue;

            foreach (DateTime dia in ultimos7dias)
            {
                float lucroDespesaDia = Database.GetLucroDia(dia, UserID);
                series.Points.AddXY(dia.ToString("dd/MM"), lucroDespesaDia);

                if (lucroDespesaDia > lucroMaximo)
                {
                    lucroMaximo = lucroDespesaDia;
                }
            }

            chart1.Series.Clear();
            chart1.Series.Add(series);

            series.Color = Color.DarkGray;
            series.BorderColor = Color.Black;
            series.BorderWidth = 1;
            series.IsXValueIndexed = true;

            int intervaloY = CalculateIntervalY(lucroMaximo);

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = intervaloY;
            chart1.Update();
        }

        private int CalculateIntervalY(float valorMaximo)
        {
            int[] multiplos = {50, 100, 500, 1000, 5000 };

            int intervaloY = multiplos[0];
            for (int i = 1; i < 5; i++)
            {
                if (valorMaximo > multiplos[i])
                {
                    intervaloY = multiplos[i - 1];
                }
            }

            return intervaloY;
        }



        private void btn_AddLucro_Click_1(object sender, EventArgs e)
        {
            AddGanho addLucro = new AddGanho(UserID);
            addLucro.ShowDialog();
            RefreshChart();
            refreshLucrosDataGridView();
        }

        private void btn_AddDespesa_Click_1(object sender, EventArgs e)
        {
            AddDespesa addDespesa = new AddDespesa(UserID);
            addDespesa.ShowDialog();
            RefreshChart();
            refreshDespesasDataGridView();
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

    }
}
