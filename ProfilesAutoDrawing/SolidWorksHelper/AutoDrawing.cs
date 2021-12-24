using System.Collections.Generic;
using ProfilesAutoDrawing.Model;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// 自动绘图抽象类，不要修改
    /// </summary>
    public abstract class AutoDrawing
    {
        public abstract void AutoProfiles(List<ImportDataModel> list, string filePath);
    }
}
