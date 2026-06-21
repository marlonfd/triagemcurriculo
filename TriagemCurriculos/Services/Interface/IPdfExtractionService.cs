namespace TriagemCurriculos.Services.Interface
{
    public interface IPdfExtractionService
    {
        string ExtractTextFromPdfStream(Stream pdfStream);
    }
}
