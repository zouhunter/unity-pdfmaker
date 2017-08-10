using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ReportMaker
{
    public class FontTool
    {
        public static BaseFont HeiTiBase
        {
            get
            {
                BaseFont bfHei = BaseFont.createFont(@"c:\Windows\fonts\SIMHEI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                return bfHei;
            }
        }
        public static BaseFont SongTiBase
        {
            get
            {
                BaseFont bfSun = BaseFont.createFont(@"c:\Windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                return bfSun;
            }
        }
        /// <summary>
        /// 黑体字
        /// </summary>
        /// <returns></returns>
        public static Font HeiTi(int size,int style)
        {
            Font font = new Font(HeiTiBase, size, style);
            return font;
        }

        public static Font SongTi(int size, int style)
        {
            Font font = new Font(SongTiBase, size,style);
            return font;
        }
    }
}
