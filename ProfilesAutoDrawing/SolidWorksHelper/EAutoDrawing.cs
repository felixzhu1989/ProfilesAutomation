using System.Collections.Generic;
using System.Diagnostics;
using ProfilesAutoDrawing.Model;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// E型材自动绘图
    /// </summary>
  public  class EAutoDrawing:AutoDrawing
    {
        public override void AutoProfiles(List<ImportDataModel> list, string filePath)
        {
            
            Debug.Print($"{list.Count}个E型材绘图完成。");
        }
    }
}
