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
        /// 模型打包
        /// </summary>
        /// <param name="swApp">SW程序</param>
        /// <param name="modelPath">模型地址</param>
        /// <param name="itemPath">目标地址</param>
        public static string PackAndGoFunc(SldWorks swApp, string modelPath, string itemPath, string suffix)
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
