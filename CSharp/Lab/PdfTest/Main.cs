using System.IO;
using System.Reflection;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace Lab.PdfTest
{
    public class Main
    {
        static readonly PdfFont font = PdfFontFactory.CreateFont("msyh.ttf", PdfEncodings.IDENTITY_H, true);

        public static void Run()
        {
            var pdfDoc = new PdfDocument(new PdfWriter("test.pdf"));
            var doc = new Document(pdfDoc);
            WriteMaster(doc);

            WriteProfile(doc);

            doc.Close();
        }


        static void WriteMaster(Document doc)
        {
            var paragraph = new Paragraph("内部资料 注意保密").SetFont(font);
            paragraph.SetFontSize(10);
            paragraph.SetRelativePosition(-5, -20, 0, 0);
            doc.Add(paragraph);
        }

        static void WriteProfile(Document doc)
        {
            var appName = "全椒麻将 - 应用分析报告";
            var logoSize = 60;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lab.PdfTest.Resources.logo.png");
            var img = Image.Load(stream);
            img.Mutate(o => o.Resize(logoSize, img.Height * logoSize / img.Width));
            var ms = new MemoryStream();
            img.Save(ms, new BmpEncoder());
            var image = new iText.Layout.Element.Image(ImageDataFactory.Create(ms.GetBuffer()));
            image.SetRelativePosition(0, 150, 0, 0);
            image.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            doc.Add(image);

            var paragraph = new Paragraph(appName).SetFont(font);
            paragraph.SetFontSize(24);
            paragraph.SetRelativePosition(0, 150, 0, 0);
            paragraph.SetTextAlignment(TextAlignment.CENTER);
            doc.Add(paragraph);
        }
    }
}
