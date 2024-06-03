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

        public DefinicoesGrafico(Guna.Charts.WinForms.GunaChart chart)
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

                PdfPage page = document.AddPage();
                page.Width = chart.Width;
                page.Height = chart.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                XImage xImage = XImage.FromFile(tempFilePath);

                gfx.DrawImage(xImage, 0, 0, chart.Width, chart.Height);

                string filename = "ChartExport.pdf";
                document.Save(filename);
                Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });

                File.Delete(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_baixarPDF_Click(object sender, EventArgs e)
        {
            ExportChartToPdf();
        }
    }
}
