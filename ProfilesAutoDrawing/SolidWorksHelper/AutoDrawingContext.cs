using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProfilesAutoDrawing.Model;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// 策略模式上下文,选择以何种方式绘制型材，在这里判断新增的型材
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
                if(typeList.Exists(t=>t== importData.ProfileType))continue;
                typeList.Add(importData.ProfileType);
                Debug.Print(importData.ProfileType);
            }
            //根据不同的型材绘图
            foreach (string s in typeList)
            {
                switch (s)
                {
                    case "U"://U型材，根据Excel表中填写的型材型号判断
                        autoDrawing = new UAutoDrawing();
                        break;
                    case "E"://E型材
                        autoDrawing = new EAutoDrawing();
                        break;
                        //在这里扩展其他的型材...



                }
                //执行具体绘图过程
                autoDrawing.AutoProfiles(importDataList.Where(i=>i.ProfileType==s).ToList(), filePath);
            }
        }
    }
}
