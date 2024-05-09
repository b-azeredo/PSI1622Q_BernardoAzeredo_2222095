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

            foreach (DateTime dia in ultimos7dias)
            {
                float lucroDespesaDia = Database.GetLucroDia(dia, UserID);
                series.Points.AddXY(dia.ToString("dd/MM"), lucroDespesaDia);
            }

            chart1.Series.Clear();
            chart1.Series.Add(series);

            series.Color = Color.Blue; // Cor da linha
            series.BorderColor = Color.Blue; // Cor da linha
            series.BorderWidth = 2; // Largura da linha
            series.IsXValueIndexed = true;

            chart1.ChartAreas[0].AxisX.Interval = 1; // Intervalo de 1 dia no eixo X
            chart1.ChartAreas[0].AxisY.Interval = 500; 
            chart1.Update();
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
    }
}
