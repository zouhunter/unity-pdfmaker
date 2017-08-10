using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ReportMaker
{
    [System.Serializable]
    public class ReportSetting
    {
        public string theoryJsonFile = "theory.json";//实验原理分级说明
        public string cadPic = "cad.jpg";//配制结果图
        public string datafile = "data.csv";//实验数据
        public string rootPath;
        public string GetFilePath(string filename) {
            return System.IO.Path.Combine(rootPath, filename);
        }
    }
}
