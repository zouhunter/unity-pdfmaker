using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using ReportMaker;
public class Lunch : MonoBehaviour
{
   
    public PDFMaker pdfMaker;

    void Start()
    {
        pdfMaker.setting.rootPath = Application.streamingAssetsPath;
        pdfMaker.MakePDF();
    }
}
