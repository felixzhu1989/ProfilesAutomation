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
    /// U型材自动绘图
    /// </summary>
    public class UAutoDrawing : AutoDrawing
    {
        public override void AutoProfiles(List<ImportDataModel> list, string filePath)
        {
            #region 准备工作
            SldWorks swApp = ConnectSolidWorks.GetApplication();
            //标准模型地址，放在“D:\标准型材库”文件夹中，新增的型材应该修改这里文件地址
            string modelPath = @"D:\标准型材库\U.SLDPRT";
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
                    swModel.Parameter("D1@凸台-拉伸1").SystemValue = item.Length / 1000d;
                    swModel.Parameter("D1@Model").SystemValue = item.Width / 1000d;
                    swModel.Parameter("D3@Model").SystemValue = item.Height / 1000d;

                    //俯视图
                    swModel.DrawingHole(item.TopHoleDia,item.TopHoleY,item.Width, item.TopHoleX1, "TX1", "D1@Sketch1", "D2@Sketch1", "D3@Sketch1");
                    swModel.DrawingHole( item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX2, "TX2", "D1@Sketch2", "D2@Sketch2", "D3@Sketch2");
                    swModel.DrawingHole(item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX3, "TX3", "D1@Sketch3", "D2@Sketch3", "D3@Sketch3");
                    swModel.DrawingHole( item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX4, "TX4", "D1@Sketch4", "D2@Sketch4", "D3@Sketch4");
                    swModel.DrawingHole( item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX5, "TX5", "D1@Sketch5", "D2@Sketch5", "D3@Sketch5");
                    swModel.DrawingHole(item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX6, "TX6", "D1@Sketch6", "D2@Sketch6", "D3@Sketch6");
                    swModel.DrawingHole( item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX7, "TX7", "D1@Sketch7", "D2@Sketch7", "D3@Sketch7");
                    swModel.DrawingHole(item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX8, "TX8", "D1@Sketch8", "D2@Sketch8", "D3@Sketch8");

                    //前视图
                    swModel.DrawingHole( 16d, 0d, item.Height, item.FrontHoleLeftX1, "FLX1", "D1@Sketch9", "D2@Sketch9", "D3@Sketch9");
                    swModel.DrawingHole( 16d, 0d, item.Height, item.FrontHoleLeftX2, "FLX2", "D1@Sketch10", "D2@Sketch10", "D3@Sketch10");
                    swModel.DrawingHole(16d, 0d, item.Height, item.FrontHoleRightX1, "FRX1", "D1@Sketch11", "D2@Sketch11", "D3@Sketch11");
                    swModel.DrawingHole( 16d, 0d, item.Height, item.FrontHoleRightX2, "FRX2", "D1@Sketch12", "D2@Sketch12", "D3@Sketch12");

                    //后视图
                    swModel.DrawingHole(16d, 0d, item.Height, item.UBackHoleLeftX1, "BLX1", "D1@Sketch13", "D2@Sketch13", "D3@Sketch13");
                    swModel.DrawingHole(16d, 0d, item.Height, item.UBackHoleLeftX2, "BLX2", "D1@Sketch14", "D2@Sketch14", "D3@Sketch14");
                    swModel.DrawingHole(16d, 0d, item.Height, item.UBackHoleRightX1, "BRX1", "D1@Sketch15", "D2@Sketch15", "D3@Sketch15");
                    swModel.DrawingHole(16d, 0d, item.Height, item.UBackHoleRightX2, "BRX2", "D1@Sketch16", "D2@Sketch16", "D3@Sketch16");

                    //左视图
                    swModel.DrawingHole(16d, 0d, item.Height, item.ULeftHoleBackY1, "LBY1", "D1@Sketch17", "D2@Sketch17", "D3@Sketch17");
                    swModel.DrawingHole(16d, 0d, item.Height, item.ULeftHoleBackY2, "LBY2", "D1@Sketch18", "D2@Sketch18", "D3@Sketch18");
                    swModel.DrawingHole(16d, 0d, item.Height, item.ULeftHoleFrontY1, "LFY1", "D1@Sketch19", "D2@Sketch19", "D3@Sketch19");
                    swModel.DrawingHole(16d, 0d, item.Height, item.ULeftHoleFrontY2, "LFY2", "D1@Sketch20", "D2@Sketch20", "D3@Sketch20");

                    //右视图
                    swModel.DrawingHole(16d, 0d, item.Height, item.URightHoleBackY1, "RBY1", "D1@Sketch21", "D2@Sketch21", "D3@Sketch21");
                    swModel.DrawingHole(16d, 0d, item.Height, item.URightHoleBackY2, "RBY2", "D1@Sketch22", "D2@Sketch22", "D3@Sketch22");
                    swModel.DrawingHole(16d, 0d, item.Height, item.URightHoleFrontY1, "RFY1", "D1@Sketch23", "D2@Sketch23", "D3@Sketch23");
                    swModel.DrawingHole(16d, 0d, item.Height, item.URightHoleFrontY2, "RFY2", "D1@Sketch24", "D2@Sketch24", "D3@Sketch24");


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
            Debug.Print($"{list.Count}个U型材绘图完成。");
        }
    }
}
