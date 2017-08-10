using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ReportMaker
{
    [System.Serializable]
    public class ReportInfo
    {
        public string userid;//学号
        public string username;//学生姓名
        public string classname;//班级名
        public string teachername;//老师名
        public string experimentname;//实验名
        public string tempPath;//资源路径

        public ReportInfo(string[] data)
        {
            this.userid = data[0];
            this.username = data[1];
            this.classname = data[2];
            this.teachername = data[3];
            this.experimentname = data[4];
            this.tempPath = data[5];
        }

        public ReportInfo()//默认配制
        {
            this.userid = "1111030125";
            this.username = "邹杭特";
            this.classname = "三班";
            this.teachername = "陆云丹";
            this.experimentname = "混凝土梁正截面抗弯实验";
            this.tempPath = "混凝土梁正截面抗弯实验temp.pdf";
        }
    }
}
