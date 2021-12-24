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
            //PackandGo，将名字设为最后一个数据的名字
            string suffix = list[list.Count - 1].PartName.Substring(1);
            string packModelPath = SldWorksExtension.PackAndGoFunc(swApp, modelPath, filePath, suffix);
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
                    DrawingHole(swModel, item.TopHoleDia,item.TopHoleY,item.Width, item.TopHoleX1, "TX1", "D1@Sketch1", "D2@Sketch1", "D3@Sketch1");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX2, "TX2", "D1@Sketch2", "D2@Sketch2", "D3@Sketch2");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX3, "TX3", "D1@Sketch3", "D2@Sketch3", "D3@Sketch3");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX4, "TX4", "D1@Sketch4", "D2@Sketch4", "D3@Sketch4");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX5, "TX5", "D1@Sketch5", "D2@Sketch5", "D3@Sketch5");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX6, "TX6", "D1@Sketch6", "D2@Sketch6", "D3@Sketch6");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX7, "TX7", "D1@Sketch7", "D2@Sketch7", "D3@Sketch7");
                    DrawingHole(swModel, item.TopHoleDia, item.TopHoleY, item.Width, item.TopHoleX8, "TX8", "D1@Sketch8", "D2@Sketch8", "D3@Sketch8");

                    //前视图
                    DrawingHole(swModel, 16d, 0, item.Height, item.FrontHoleLeftX1, "FLX1", "D1@Sketch9", "D2@Sketch9", "D3@Sketch9");
                    DrawingHole(swModel, 16d, 0, item.Height, item.FrontHoleLeftX2, "FLX2", "D1@Sketch10", "D2@Sketch10", "D3@Sketch10");
                    DrawingHole(swModel, 16d, 0, item.Height, item.FrontHoleRightX1, "FRX1", "D1@Sketch11", "D2@Sketch11", "D3@Sketch11");
                    DrawingHole(swModel, 16d, 0, item.Height, item.FrontHoleRightX2, "FRX2", "D1@Sketch12", "D2@Sketch12", "D3@Sketch12");

                    //后视图

                    
                    //左视图


                    //右视图






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
                    swModel.Extension.SaveAs(Path.Combine(filePath, $"{item.PartName}.SLDPRT"), (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_Copy, null, errors, warnings);
                }
                else
                {
                    //最后一个关闭
                    swModel.Save();
                    swApp.CloseDoc(packModelPath);
                }
                i++; 
                #endregion
            }
            Debug.Print($"{list.Count}个U型材绘图完成。");
        }
        /// <summary>
        /// 更改孔位的参数
        /// </summary>
        /// <param name="swModel"></param>
        /// <param name="holeDia">直径</param>
        /// <param name="holeY">Y值</param>
        /// <param name="totalY">（holeY=0时孔居中）型材宽度/高度</param>
        /// <param name="holeX">X值</param>
        /// <param name="featName">特征名字</param>
        /// <param name="disHoleDia">直径尺寸@草图</param>
        /// <param name="disHoleX">X值@草图</param>
        /// <param name="disHoleY">Y值@草图</param>
        private void DrawingHole(ModelDoc2 swModel,double holeDia,double holeY, double totalY, double holeX, string featName,string disHoleDia, string disHoleY,string disHoleX)
        {
            PartDoc swPart = swModel as PartDoc;
            //如果等于0或者X的值等于0则表示没有孔，压缩这个特征
            if (holeDia == 0d || holeX == 0d)
            {
                //压缩特征，不需要了
                swPart?.FeatureByName(featName).SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //解压缩特征
                swPart?.FeatureByName(featName).SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //直径
                swModel.Parameter(disHoleDia).SystemValue = holeDia / 1000d;
                //Y方向，如果等于0则默认居中，否则为Y的值
                swModel.Parameter(disHoleY).SystemValue = holeY == 0d ? totalY / 2000d : holeY / 1000d;
                //X方向
                swModel.Parameter(disHoleX).SystemValue = holeX / 1000d;
                
            }
        }
    }
}
