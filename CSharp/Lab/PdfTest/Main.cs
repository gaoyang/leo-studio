using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
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
        private static readonly PdfFont font = PdfFontFactory.CreateFont("msyh.ttf", PdfEncodings.IDENTITY_H, true);

        public static void Run()
        {
            var pdfDoc = new PdfDocument(new PdfWriter("test.pdf"));
            // 72DPI 595×842
            var doc = new Document(pdfDoc, PageSize.A4, false);
            doc.SetFont(font);
            WriteProfile(doc);
            WriteRecord(doc);
            WriteOverview(doc);
            WriteData(doc);
            WriteMaster(doc);
            doc.Close();
        }

        private static void WriteProfile(Document doc)
        {
            var appName = "全椒麻将 - 应用分析报告";
            var appVersion = "应用版本本:1.0.7";
            var md5 = "文件MD5:db207522f25a10d16d6441d49e620d4e";
            var date = "分析日期:2020-05-25";

            var logoSize = 60;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lab.PdfTest.Resources.logo.png");
            var img = Image.Load(stream);
            img.Mutate(o => o.Resize(logoSize, img.Height * logoSize / img.Width));
            var ms = new MemoryStream();
            img.Save(ms, new BmpEncoder());

            doc.Add(new iText.Layout.Element.Image(ImageDataFactory.Create(ms.GetBuffer()))
                .SetMarginTop(150)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
            );

            doc.Add(new Paragraph(appName)
                .SetFontSize(24)
                .SetMarginTop(10)
                .SetTextAlignment(TextAlignment.CENTER)
            );

            doc.Add(new Paragraph(appVersion)
                .SetFontSize(15)
                .SetMarginTop(5)
                .SetTextAlignment(TextAlignment.CENTER)
            );

            doc.Add(new Paragraph(md5)
                .SetFontSize(10)
                .SetMarginTop(5)
                .SetTextAlignment(TextAlignment.CENTER)
            );

            doc.Add(new Paragraph(date)
                .SetFontSize(10)
                .SetMarginTop(110)
                .SetTextAlignment(TextAlignment.CENTER)
            );
        }

        private static void WriteRecord(Document doc)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            doc.Add(new Paragraph("目录")
                .SetFontSize(24)
                .SetTextAlignment(TextAlignment.CENTER)
            );

            var dictionary = new Dictionary<string, List<string>>
            {
                {"1. 报告声明", null},
                {"2. 报告概述", null},
                {
                    "3. 报告详情", new List<string>
                    {
                        "3.1 基本信息",
                        "3.2 历史版本",
                        "3.3 可疑域名",
                        "3.4 可疑IP"
                    }
                }
            };

            foreach (var kv in dictionary)
            {
                doc.Add(new Paragraph(kv.Key)
                    .SetFontSize(16)
                );
                if (kv.Value?.Any() != true) continue;
                foreach (var item in kv.Value)
                    doc.Add(new Paragraph(item)
                        .SetFontSize(14)
                        .SetFirstLineIndent(14)
                    );
            }
        }

        private static void WriteOverview(Document doc)
        {
            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            WriteH1(doc, "1. 报告声明");
            WriteText(doc, @"本服务平台对app应用进行独立、客观、公正分析并出具分析报告，不受任何个人或者组织的非法干预。本次分析结果只针对本次上传的app应用有效，本服务平台拥有对本次分析报告的最终解释权。");
            WriteH1(doc, "2. 报告概述");
            WriteText(doc, @"应用全椒麻将于2020-05-20 09:57:36由用户邱本钊上传至本平台，通过对上传的应用包进行自动脱壳、静态特征提取、动态沙箱行为分析、网络行为分析，再结合奇安信威胁情报中心数据分析，系统自动生成本报告。报告内容除基本信息外，共发现风险权限0个，可疑域名12个，可疑IP地址16个，可疑电话0个，可疑邮箱0个。若还需进一步深入分析，请申请专家人工服务。");
        }

        private static void WriteData(Document doc)
        {
            WriteH1(doc, "3. 报告详情");
            {
                WriteH2(doc, "3.1 基本信息");
                var table = new Table(2).UseAllAvailableWidth();
                table.AddCell("app名称").AddCell("全椒麻将");
                table.AddCell("app名称").AddCell("全椒麻将");
                table.AddCell("app名称").AddCell("全椒麻将");
                table.AddCell("app名称").AddCell("全椒麻将");
                table.AddCell("app名称").AddCell("全椒麻将");
                SetTableStyle(table);
                doc.Add(table);
            }

            {
                WriteH2(doc, "3.2 历史版本");
                var table = new Table(4).UseAllAvailableWidth();
                table.AddHeaderCell("序号").AddHeaderCell("版本").AddHeaderCell("版本号").AddHeaderCell("应用数");
                table.AddCell("1").AddCell("1.0.7 ").AddCell("1007").AddCell("1");
                SetTableStyle(table);
                doc.Add(table);
            }

            {
                WriteH2(doc, "3.3 可疑域名");
                WriteH3(doc, "3.3.1 可疑域名（白名单过滤得出）");
                var table = new Table(4).UseAllAvailableWidth();
                table.AddHeaderCell("序号").AddHeaderCell("版本").AddHeaderCell("版本号").AddHeaderCell("应用数");
                SetTableStyle(table);
                doc.Add(table);
            }
        }

        private static void WriteMaster(Document doc)
        {
            var numberOfPages = doc.GetPdfDocument().GetNumberOfPages();
            for (var i = 1; i <= numberOfPages; i++)
            {
                doc.Add(new Paragraph("内部资料 注意保密")
                    .SetFontSize(10)
                    .SetFixedPosition(i, 30, 810, 100)
                );

                if (i == 1) continue;

                for (var j = 0; j < 3; j++)
                    doc.Add(new Paragraph("星鉴APP取证分析系统")
                        .SetFontSize(20)
                        .SetFontColor(new DeviceRgb(0, 0, 255), 0.1f)
                        .SetFixedPosition(100 + j * 100, 600 - j * 200, 200)
                        .SetRotationAngle(0.35)
                    );
            }
        }

        private static void WriteH1(Document doc, string h1)
        {
            doc.Add(new Paragraph(h1)
                .SetFontSize(16)
                .SetMarginTop(50)
            );
        }

        private static void WriteH2(Document doc, string h2)
        {
            doc.Add(new Paragraph(h2)
                .SetFontSize(14)
            );
        }

        private static void WriteH3(Document doc, string h3)
        {
            doc.Add(new Paragraph(h3)
                .SetFontSize(12)
            );
        }

        private static void WriteText(Document doc, string text)
        {
            doc.Add(new Paragraph(text)
                .SetFirstLineIndent(20)
            );
        }

        private static void SetTableStyle(Table table)
        {
            for (var rowIndex = 0; rowIndex < table.GetNumberOfRows(); rowIndex++)
            {
                if (rowIndex % 2 != 0) continue;
                for (var colIndex = 0; colIndex < table.GetNumberOfColumns(); colIndex++)
                    table.GetCell(rowIndex, colIndex).SetBackgroundColor(new DeviceRgb(227, 227, 227));
            }
        }
    }
}