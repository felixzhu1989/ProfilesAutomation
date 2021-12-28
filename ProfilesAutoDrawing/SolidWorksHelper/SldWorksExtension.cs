using System;
using System.IO;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// SolidWorks扩展方法
    /// </summary>
   public static class SldWorksExtension
    {



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
        public static void DrawingHole(this ModelDoc2 swModel, double holeDia, double holeY, double totalY, double holeX, string featName, string disHoleDia, string disHoleY, string disHoleX)
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


        /// <summary>
        /// 模型打包
        /// </summary>
        /// <param name="swApp">SW程序</param>
        /// <param name="modelPath">模型地址</param>
        /// <param name="itemPath">目标地址</param>
        public static string PackAndGoFunc(this SldWorks swApp, string modelPath, string itemPath, string suffix)
        {
            int warnings = 0;
            int errors = 0;
            //打开需要pack的模型
            var swModel = swApp.OpenDoc6(modelPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swModelDocExt = swModel.Extension;
            var swPackAndGo = swModelDocExt.GetPackAndGo();
            swPackAndGo.IncludeDrawings = false;
            swPackAndGo.IncludeSimulationResults = false;
            swPackAndGo.IncludeToolboxComponents = false;
            swPackAndGo.IncludeSuppressed = true;

            // Set folder where to save the files,目标存放地址
            swPackAndGo.SetSaveToName(true, itemPath);
            //将文件展开到一个文件夹内，不要原始模型的文件夹结构
            // Flatten the Pack and Go folder structure; save all files to the root directory
            swPackAndGo.FlattenToSingleFolder = true;

            // Add a prefix and suffix to the filenames
            //swPackAndGo.AddPrefix = "SW_";添加后缀
            swPackAndGo.AddSuffix = suffix;
            try
            {
                // Pack and Go，执行PackAndGo
                swModelDocExt.SavePackAndGo(swPackAndGo);
            }
            catch (Exception ex)
            {
                throw new Exception("PackandGo过程中出现异常：" + ex.Message);
            }
            finally
            {
                swApp.CloseDoc(swModel.GetTitle());
            }
            string modelName = Path.GetFileNameWithoutExtension(modelPath);
            //返回packandgo后模型的地址
            return Path.Combine(itemPath, $"{modelName}{suffix}.SLDPRT");
        }


        public static void WithToggleState(this SldWorks swApp, swUserPreferenceToggle_e swUserPreference,
            bool desState, Action action)
        {
            var sourceState = swApp.GetUserPreferenceToggle((int)swUserPreference);
            swApp.SetUserPreferenceToggle((int)swUserPreference, desState);
            action?.Invoke();
            swApp.SetUserPreferenceToggle((int)swUserPreference, sourceState);
        }
    }
}
