using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
   public class AutoDrawingContext
    {
       private AutoDrawing autoDrawing;

       public AutoDrawingContext(AutoDrawing autoDrawing)
       {
           this.autoDrawing = autoDrawing;
       }
       public void ContextInterface()
       {
           autoDrawing.AutoProfiles();
       }
   }
}
