using System;
using SolidWorks.Interop.sldworks;

namespace FunctionTest
{
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
