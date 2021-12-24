using System;
using SolidWorks.Interop.sldworks;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// 连接SolidWorks程序，不要修改
    /// </summary>
   public class ConnectSolidWorks
   {
       private static SldWorks swApp;
       public static SldWorks GetApplication()
       {
           if (swApp == null)
           {
               swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
               swApp.Visible = true;
               return swApp;
           }
           return swApp;
       }
    }
}
