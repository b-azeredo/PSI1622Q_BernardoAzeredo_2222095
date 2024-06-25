using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Drawing; // Ensure to add this for Bitmap
using System.IO; // For file operations
using System.Windows.Forms;
using Guna.Charts.WinForms;
using System.Diagnostics;

namespace AdminSysWF
{
    public partial class DefinicoesGrafico : Form
    {
        private Guna.Charts.WinForms.GunaChart chart;

        public DefinicoesGrafico(Guna.Charts.WinForms.GunaChart chart, int userID)
        {
            InitializeComponent();
            this.chart = chart;
        }
        private void ExportChartToPdf()
        {
            try
            {
                Bitmap bitmap = new Bitmap(chart.Width, chart.Height);
                chart.DrawToBitmap(bitmap, new Rectangle(0, 0, chart.Width, chart.Height));

                string tempFilePath = Path.Combine(Path.GetTempPath(), "chart_temp.png");
                bitmap.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Png);

                PdfDocument document = new PdfDocument();
                document.Info.Title = "Chart Export";

                // Adicionar página para o gráfico
                PdfPage chartPage = document.AddPage();
                chartPage.Width = XUnit.FromPoint(chart.Width);
                chartPage.Height = XUnit.FromPoint(chart.Height);
                XGraphics chartGfx = XGraphics.FromPdfPage(chartPage);
                XImage chartImage = XImage.FromFile(tempFilePath);
                chartGfx.DrawImage(chartImage, 0, 0, XUnit.FromPoint(chart.Width), XUnit.FromPoint(chart.Height));

                // Obter as dimensões da primeira página
                double firstPageWidth = chartPage.Width.Point;
                double firstPageHeight = chartPage.Height.Point;

                // Adicionar nova página com o mesmo tamanho da primeira página
                PdfPage textPage = document.AddPage();
                textPage.Width = XUnit.FromPoint(firstPageWidth);
                textPage.Height = XUnit.FromPoint(firstPageHeight);
                XGraphics textGfx = XGraphics.FromPdfPage(textPage);

                // Configurar fonte e tamanho do título
                XFont titleFont = new XFont("Arial", 20);
                XBrush titleBrush = XBrushes.Black;

                // Escrever título centralizado
                string titleText = "Título do Documento PDF";
                XSize titleSize = textGfx.MeasureString(titleText, titleFont);
                XRect titleRect = new XRect((textPage.Width - titleSize.Width) / 2, 40, titleSize.Width, titleSize.Height);
                textGfx.DrawString(titleText, titleFont, titleBrush, titleRect, XStringFormats.TopLeft);

                // Configurar fonte e tamanho do texto
                XFont font = new XFont("Arial", 12);
                XRect rect = new XRect(40, 80, textPage.Width - 80, textPage.Height - 120);
                string bodyText = "Texto personalizado na nova página do PDF.";
                textGfx.DrawString(bodyText, font, XBrushes.Black, rect, XStringFormats.TopLeft);

                // Salvar e abrir o PDF
                string filename = "ChartExport.pdf";
                document.Save(filename);
                Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });

                // Limpar arquivos temporários
                File.Delete(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_baixarPDF_Click(object sender, EventArgs e)
        {
            ExportChartToPdf();
        }

    }
}
