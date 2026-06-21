using System.Text;
using TriagemCurriculos.Services.Interface;
using UglyToad.PdfPig;

namespace TriagemCurriculos.Services
{
    public class PdfExtractionService : IPdfExtractionService
    {
        public string ExtractTextFromPdfStream(Stream pdfStream)
        {
            if (pdfStream == null) throw new ArgumentNullException(nameof(pdfStream));

            var text = new StringBuilder();
            
            using (var document = PdfDocument.Open(pdfStream))
            {
                foreach (var page in document.GetPages())
                {
                    text.AppendLine(page.Text);
                }
            }

            return text.ToString();
        }
    }
}
