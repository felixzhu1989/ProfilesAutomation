using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    public class ExportBmpFiles
    {
        public void ExportBmp(string[]  files)
        {
            SldWorks swApp = ConnectSolidWorks.GetApplication();
            foreach (var fileName in files)
            {
                int errors = 0;
                int warnings = 0;
                ModelDoc2 swModel = swApp.OpenDoc6(fileName, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
                if (swModel == null) continue;
                for (int viewId = 1; viewId < 7; viewId++)
                {
                    swModel.ShowNamedView2($"View{viewId}", viewId);
                    swModel.ViewZoomtofit2();
                    var status = swModel.SaveBMP($"{fileName.Substring(0, fileName.Length - 7)}-{viewId}.bmp", 1420, 716);
                }
                swApp.CloseDoc(swModel.GetPathName());
            }
        }
        
    }
}
