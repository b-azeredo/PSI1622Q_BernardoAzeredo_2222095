using PdfSharp;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Guna.Charts.WinForms; // Certifique-se de que esta diretiva esteja presente

namespace AdminSysWF
{
    public partial class DefinicoesGrafico : Form
    {
        private Guna.Charts.WinForms.GunaChart chart;

        public DefinicoesGrafico(Guna.Charts.WinForms.GunaChart chart)
        {
            InitializeComponent();
            this.chart = chart;
        }

        private void ExportChartToPdf()
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Gráfico de Lucros";

            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Criar o gráfico no PDF
            Chart pdfChart = new Chart(ChartType.Line);
            Series series = pdfChart.SeriesCollection.AddSeries();
            series.Name = "Lucro";

            // Assumindo que o primeiro dataset é o que queremos
            var dataset = chart.Datasets[0] as GunaLineDataset;

            if (dataset != null)
            {
                for (int i = 0; i < dataset.DataPoints.Count; i++)
                {
                    series.Add(dataset.DataPoints[i].Y);
                }
                // Definir o eixo X
                XSeries xSeries = pdfChart.XValues.AddXSeries();
                for (int i = 0; i < dataset.DataPoints.Count; i++)
                {
                    // Usar o índice como coordenada X
                    xSeries.Add(i.ToString());
                }



            }

            // Adicionar o gráfico ao PDF
            pdfChart.XAxis.Title.Caption = "Dias";
            pdfChart.YAxis.Title.Caption = "Lucro (€)";
            ChartFrame frame = new ChartFrame();
            double x = 50; // Coordenada X do canto superior esquerdo do retângulo
            double y = 50; // Coordenada Y do canto superior esquerdo do retângulo
            double width = page.Width - 100; // Largura do retângulo
            double height = page.Height - 100; // Altura do retângulo

            // Verificar se as dimensões são válidas (não negativas)
            if (width > 0 && height > 0)
            {
                XPoint location = new XPoint(x, y);
                XSize size = new XSize(width, height);
                XRect rect = new XRect(location, size);
                frame.Location = rect;
            }
            else
            {
                MessageBox.Show("Dimensões do retângulo inválidas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            frame.Add(pdfChart);
            frame.Draw(gfx);

            string filename = "GraficoLucroSemanal.pdf";
            document.Save(filename);
            MessageBox.Show("Gráfico exportado para PDF com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }

        private void btn_baixarPDF_Click(object sender, EventArgs e)
        {
            ExportChartToPdf();
        }
    }
}
