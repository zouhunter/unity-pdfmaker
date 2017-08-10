using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.Collections.Generic;
namespace ReportMaker
{
    [System.Serializable]
    public class PDFMaker
    {
        public ReportSetting setting;
        public ReportInfo _info;
        private Font headFont = FontTool.HeiTi(28, Font.BOLD);
        private Font keyFont = FontTool.HeiTi(20, Font.NORMAL);
        private Font valueFont = FontTool.SongTi(20, Font.UNDERLINE);
        private Font footFont = FontTool.SongTi(25, Font.UNDERLINE);
        private Chunk tocenterChunk = new Chunk("                                                         ");

        internal void MakePDF()
        {
            Document document = new Document(PageSize.A4);
            WriteInfomation(document);
            try
            {
                var path = setting.GetFilePath(_info.experimentname + ".pdf");
                var writer = PdfWriter.getInstance(document, new FileStream(path, FileMode.Create));
                document.Open();

                InteralMake(document, writer);//制作的具体过程

                document.Close();

                CombineTemp(path, _info.tempPath);

                ShowMade(path);
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
        }

        private void WriteInfomation(Document document)
        {
            document.addTitle(_info.experimentname);
            document.addSubject(_info.experimentname);
            document.addKeywords(_info.experimentname);
            document.addAuthor(_info.username);
            document.addCreator(_info.username);
            document.addProducer();
            document.addCreationDate();
            document.addHeader(_info.username, _info.userid);
        }
        private void CombineTemp(string path, string tempPath)
        {
            byte[] created = File.ReadAllBytes(path);
            byte[] temp = File.ReadAllBytes(tempPath);
            var newCreated = MergePdfFiles(new List<byte[]> { created, temp });
            File.WriteAllBytes(path, newCreated);
        }
        public static byte[] MergePdfFiles(List<byte[]> pdfInputs)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.getInstance(document, stream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage newPage;
                PdfReader reader;

                foreach (var p in pdfInputs)
                {
                    reader = new PdfReader(p);
                    int iPageNum = reader.NumberOfPages;
                    Console.WriteLine(iPageNum);
                    for (int i = 1; i <= iPageNum; i++)
                    {
                        newPage = writer.getImportedPage(reader, i);
                        cb.addTemplate(newPage, 0, 0);
                        document.newPage();
                    }
                }

                document.Close();
                writer.Close();
                return stream.ToArray();
            }
        }

        private void InteralMake(Document document, PdfWriter writer)
        {
            MakeCover(document, writer);
            ExamTheory(document, writer);
        }

        /// <summary>
        /// 制作封面
        /// </summary>
        private void MakeCover(Document document, PdfWriter writer)
        {
            //PdfContentByte cb = writer.DirectContent;
            //cb.beginText();
            //cb.setFontAndSize(FontTool.HeiTiBase, 28);
            //cb.showTextAligned(PdfContentByte.ALIGN_CENTER, _info.experimentname, 280, 0, 0);
            //cb.endText();

            Image image = Image.getInstance(setting.GetFilePath("school.jpg"));
            image.Alignment = Image.MIDDLE;
            image.scaleAbsolute(120, 120);

            Paragraph expnameGraph = new Paragraph();
            expnameGraph.Add(new Chunk("\n\n"));
            expnameGraph.Add(new Chunk(_info.experimentname, headFont));
            expnameGraph.setAlignment("Center");
            expnameGraph.Add(new Chunk("\n\n\n\n\n\n\n\n\n"));

            Paragraph userInfos = new Paragraph(30);
            userInfos.setAlignment("Left");

            userInfos.Add(tocenterChunk);
            userInfos.Add(new Chunk("班级: ", keyFont));
            userInfos.Add(new Chunk(_info.classname + "\n", valueFont));

            userInfos.Add(tocenterChunk);
            userInfos.Add(new Chunk("学号: ", keyFont));
            userInfos.Add(new Chunk(_info.userid + "\n", valueFont));

            userInfos.Add(tocenterChunk);
            userInfos.Add(new Chunk("姓名: ", keyFont));
            userInfos.Add(new Chunk(_info.username + "\n", valueFont));

            userInfos.Add(tocenterChunk);
            userInfos.Add(new Chunk("指导老师: ", keyFont));
            userInfos.Add(new Chunk(_info.teachername + "\n", valueFont));

            expnameGraph.Add(new Chunk("\n\n\n\n\n"));
            Paragraph rootgraph = new Paragraph(40);
            rootgraph.Add(new Chunk("\n\n"));
            rootgraph.Add(new Chunk("南阳理工结构力学实验研究院\n" + DateTime.Today.ToString("yyyy年MM月dd日"), headFont));
            rootgraph.setAlignment("Center");

            // we Add some more content
            document.Add(image);
            document.Add(expnameGraph);
            document.Add(userInfos);
            document.Add(rootgraph);
        }

        /// <summary>
        /// 实验原理
        /// </summary>
        /// <param name="document"></param>
        /// <param name="writer"></param>
        private void ExamTheory(Document document, PdfWriter writer)
        {
            document.newPage();
        }

        /// <summary>
        /// 展示制作
        /// </summary>
        private void ShowMade(string path)
        {
            System.Diagnostics.Process.Start(path);
        }
    }
}
