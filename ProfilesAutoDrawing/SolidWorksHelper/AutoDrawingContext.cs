using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ProfilesAutoDrawing.Model;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// 策略模式上下文,根据型材类型选择绘图对象
    /// </summary>
    public class AutoDrawingContext
    {
        private AutoDrawing autoDrawing;
        private List<ImportDataModel> importDataList;
        private string filePath;
        public AutoDrawingContext(List<ImportDataModel> importDataList, string filePath)
        {
            this.importDataList = importDataList;
            this.filePath = filePath;
        }
        public void ContextInterface()
        {
            //获取列表中有几种类型的型材
            List<string> typeList = new List<string>();
            foreach (ImportDataModel importData in importDataList)
            {
                if (typeList.Exists(t => t == importData.ProfileType)) continue;
                typeList.Add(importData.ProfileType);
                Debug.Print(importData.ProfileType);
            }
            //根据不同的型材绘图
            foreach (string s in typeList)
            {
                /*
                 //简单工厂，使用switch case语句创建相应的制图类，已使用反射替代
                switch (s)
                {
                    case "U"://U型材，根据Excel表中填写的型材型号判断
                        autoDrawing = new UAutoDrawing();
                        break;
                    case "E"://E型材
                        autoDrawing = new EAutoDrawing();
                        break;
                    //...
                }
                */
                //所有在用简单工厂的地方，都可以考虑用反射技术来去除switch或if，解除分支判断带来的耦合。
                //使用反射创建抽象类实现类的对象
                autoDrawing = (AutoDrawing)Assembly.Load("ProfilesAutoDrawing").CreateInstance($"ProfilesAutoDrawing.SolidWorksHelper.{s}AutoDrawing");

                //执行具体绘图过程
                autoDrawing.AutoProfiles(importDataList.Where(i => i.ProfileType == s).ToList(), filePath);
            }
        }
    }
}
