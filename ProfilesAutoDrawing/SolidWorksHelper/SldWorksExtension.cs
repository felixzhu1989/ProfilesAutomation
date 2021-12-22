using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
   public static class SldWorksExtension
    {
        public static void WithToggleState(this SldWorks swApp, swUserPreferenceToggle_e swUserPreference,
            bool desState, Action action)
        {
            var sourceState = swApp.GetUserPreferenceToggle((int)swUserPreference);
            swApp.SetUserPreferenceToggle((int)swUserPreference,desState);
            action?.Invoke();
            swApp.SetUserPreferenceToggle((int)swUserPreference,sourceState);
        }
    }
}
