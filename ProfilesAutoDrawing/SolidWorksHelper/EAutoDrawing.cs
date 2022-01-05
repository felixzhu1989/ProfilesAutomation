using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ProfilesAutoDrawing.Model;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// E型材自动绘图
    /// </summary>
    public class EAutoDrawing : AutoDrawing
    {
        public override void AutoProfiles(List<ImportDataModel> list, string filePath)
        {
            #region 准备工作
            SldWorks swApp = ConnectSolidWorks.GetApplication();
            //标准模型地址，放在“D:\标准型材库”文件夹中，新增的型材应该修改这里文件地址
            string modelPath = @"D:\标准型材库\E.SLDPRT";
            //PackandGo，将名字设为最后一行数据的名字
            string suffix = list[list.Count - 1].PartName.Substring(1);
            string packModelPath = swApp.PackAndGoFunc(modelPath, filePath, suffix);
            //打开需要pack后的模型
            int warnings = 0;
            int errors = 0;
            var swModel = swApp.OpenDoc6(packModelPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            #endregion

            //循环列表作图，saveas另存为，文件名item.PartName
            int i = 1;//计数
            foreach (ImportDataModel item in list)
            {
                try
                {
                    #region 新的模型需要修改的代码，这里是详细的自动绘图过程
                    //SolidWorks默认单位是米，因此除以1000，后面的d表示double数据类型
                    //长-宽-高
                    swModel.ChangeDim("D1@凸台-拉伸1", item.Length);
                    swModel.ChangeDim("D5@Model", item.Width);
                    swModel.ChangeDim("D3@Model", item.Height);

                    //俯视图
                    swModel.DrawingHole(16d, 0d, 65d, item.ETopHoleLeftX1, "TLX1", "D1@Sketch1", "D2@Sketch1", "D3@Sketch1");
                    swModel.DrawingHole(16d, 0d, 65d, item.ETopHoleLeftX2, "TLX2", "D1@Sketch2", "D2@Sketch2", "D3@Sketch2");
                    swModel.DrawingHole(16d, 0d, 65d, item.ETopHoleRightX1, "TRX1", "D1@Sketch3", "D2@Sketch3", "D3@Sketch3");
                    swModel.DrawingHole(16d, 0d, 65d, item.ETopHoleRightX2, "TRX2", "D1@Sketch4", "D2@Sketch4", "D3@Sketch4");

                    //前视图
                    swModel.DrawingHole(16d, 0d, 65d, item.FrontHoleLeftX1, "FLX1", "D1@Sketch5", "D2@Sketch5", "D3@Sketch5");
                    swModel.DrawingHole(16d, 0d, 65d, item.FrontHoleLeftX2, "FLX2", "D1@Sketch6", "D2@Sketch6", "D3@Sketch6");
                    swModel.DrawingHole(16d, 0d, 65d, item.FrontHoleRightX1, "FRX1", "D1@Sketch7", "D2@Sketch7", "D3@Sketch7");
                    swModel.DrawingHole(16d, 0d, 65d, item.FrontHoleRightX2, "FRX2", "D1@Sketch8", "D2@Sketch8", "D3@Sketch8");


                    #endregion
                }
                catch (Exception ex)
                {
                    //捕获异常
                    throw new Exception(item.PartName + "作图过程发生异常，详细：" + ex.Message);
                }
                #region 另存为
                swModel.ForceRebuild3(true);
                if (i < list.Count)
                {
                    //不是最后一个就另存为
                    swModel.Extension.SaveAs(Path.Combine(filePath, $"{item.PartName}.SLDPRT"), (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_Copy, null, errors, warnings);
                }
                else
                {
                    //最后一个保存然后关闭
                    swModel.Save();
                    swApp.CloseDoc(packModelPath);
                }
                i++;
                #endregion
            }
            Debug.Print($"{list.Count}个E型材绘图完成。");
        }
    }
}
