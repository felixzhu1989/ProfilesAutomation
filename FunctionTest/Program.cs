using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace FunctionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("连接SolidWorks...");
            SldWorks swApp = ConnectSolidWorks.GetApplication();

            

            //string fileName = @"D:\标准型材库\U.SLDPRT";
            //if(!System.IO.File.Exists(fileName))return;
            //string configName = "默认";
            //string bitmapPathName = @"D:\标准型材库\U.bmp";


            //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "请选择SolidWorks文件所在文件夹";
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (string.IsNullOrEmpty(dialog.SelectedPath))
            //    {
            //        Console.WriteLine("文件夹路径不能为空");
            //        return;
            //    }
            //}

            string strPath = @"C:\Users\felix.zhu\Desktop\测试";
            var files = System.IO.Directory.GetFiles(strPath, "*.SLDPRT");
            foreach (var fileName in files)
            {
                Console.WriteLine(fileName);
                int errors = 0;
                int warnings = 0;
                ModelDoc2 swModel = swApp.OpenDoc6(fileName, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
                if(swModel==null)continue;
                for (int viewId = 1; viewId < 7; viewId++)
                {
                    swModel.ShowNamedView2($"View{viewId}", viewId);
                    swModel.ViewZoomtofit2();
                    var status = swModel.SaveBMP($"{fileName.Substring(0,fileName.Length-7)}-{viewId}.bmp", 1420, 716);
                }
                swApp.CloseDoc(swModel.GetPathName());
            }
                
            

            

            //var status = swApp.GetPreviewBitmapFile(fileName, configName, bitmapPathName);
            //if (status) Console.WriteLine("预览图获取完成。");
            //else Console.WriteLine("预览图获取失败.");

            Console.WriteLine("预览图获取完成。");
            Console.Read();
        }
    }
}
