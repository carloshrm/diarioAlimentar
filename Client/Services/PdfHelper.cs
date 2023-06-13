using iTextSharp.text.pdf;
using iTextSharp.text;
using diarioAlimentar.Shared;

namespace diarioAlimentar.Client.Services;
public static class PdfHelper
{
    public static byte[] GerarPdf(string nome, string titulo, ICollection<Refeicao> refs)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var document = new Document(PageSize.A4, 4, 4, 4, 4))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;

                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Paragraph titleParagraph = new Paragraph(titulo, titleFont);
                titleParagraph.Alignment = Element.ALIGN_CENTER;
                titleParagraph.SpacingAfter = 4f;
                document.Add(titleParagraph);

                Font bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                foreach (var r in refs)
                {
                    Paragraph bodyParagraph = new Paragraph(r.ToString(), bodyFont);
                    bodyParagraph.Alignment = Element.ALIGN_JUSTIFIED;
                    bodyParagraph.SpacingAfter = 2f;
                    bodyParagraph.FirstLineIndent = 2f;
                    document.Add(bodyParagraph);
                }
                document.Close();
            }
            return memoryStream.ToArray();
        }
    }
}
