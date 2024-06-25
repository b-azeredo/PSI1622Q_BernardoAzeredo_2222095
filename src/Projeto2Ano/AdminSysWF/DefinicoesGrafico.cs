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
        private object relatorio;
        private string tituloRelatorio;

        public DefinicoesGrafico(Guna.Charts.WinForms.GunaChart chart, int userID, object relatorio)
        {
            InitializeComponent();
            this.chart = chart;
            this.relatorio = relatorio;
            this.tituloRelatorio = GerarTituloRelatorio(relatorio);
        }

        private string GerarTituloRelatorio(object relatorio)
        {
            if (relatorio is Relatorio.Lucros) return "Lucros";
            if (relatorio is Relatorio.Ganhos) return "Ganhos";
            if (relatorio is Relatorio.Despesas) return "Despesas";
            if (relatorio is Relatorio.Investimentos) return "Investimentos";
            return "Relatório";
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
                string titleText = this.tituloRelatorio;
                XSize titleSize = textGfx.MeasureString(titleText, titleFont);
                XRect titleRect = new XRect((textPage.Width - titleSize.Width) / 2, 40, titleSize.Width, titleSize.Height);
                textGfx.DrawString(titleText, titleFont, titleBrush, titleRect, XStringFormats.TopLeft);

                // Configurar fonte e tamanho do texto
                XFont font = new XFont("Arial", 12);
                XRect rect = new XRect(40, 80, textPage.Width - 80, textPage.Height - 120);
                string bodyText = GerarTextoRelatorio(this.relatorio);

                // Desenhar cada linha do texto
                var lines = bodyText.Split('\n');
                double lineHeight = textGfx.MeasureString("A", font).Height;
                for (int i = 0; i < lines.Length; i++)
                {
                    textGfx.DrawString(lines[i], font, XBrushes.Black, new XRect(rect.X, rect.Y + i * lineHeight, rect.Width, lineHeight), XStringFormats.TopLeft);
                }

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

        private string GerarTextoRelatorio(object relatorio)
        {
            if (relatorio is Relatorio.Lucros lucros)
            {
                return $"Lucro Semanal: {lucros.LucroSemanal}\nLucro Mensal: {lucros.LucroMensal}\nLucro Último Mês: {lucros.LucroUltimoMes}\nMédia Diária: {lucros.mediaDiaria}";
            }
            else if (relatorio is Relatorio.Ganhos ganhos)
            {
                return $"Ganhos Mensal: {ganhos.GanhosMensal}\nGanhos Último Mês: {ganhos.GanhosUltimoMes}\nTaxa em Relação ao Último Mês: {ganhos.TaxaEmRelacaoAoUltimoMes}\nMédia Diária: {ganhos.mediaDiaria}";
            }
            else if (relatorio is Relatorio.Despesas despesas)
            {
                return $"Despesas Mensal: {despesas.DespesasMensal}\nDespesas Último Mês: {despesas.DespesasUltimoMes}\nTaxa em Relação ao Último Mês: {despesas.TaxaEmRelacaoAoUltimoMes}\nMédia Diária: {despesas.mediaDiaria}";
            }
            else if (relatorio is Relatorio.Investimentos investimentos)
            {
                return $"Valor Inicial: {investimentos.ValorInicial}\nValor Total: {investimentos.ValorTotal}\nTaxa de Variação: {investimentos.TaxaVariacao}\nMelhor Ativo: {investimentos.MelhorAtivo}\nPior Ativo: {investimentos.PiorAtivo}";
            }
            return "Relatório não disponível.";
        }

        private void btn_baixarPDF_Click(object sender, EventArgs e)
        {
            ExportChartToPdf();
        }
    }
}
