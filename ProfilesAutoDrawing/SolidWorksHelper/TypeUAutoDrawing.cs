using ProfilesAutoDrawing.Model;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace ProfilesAutoDrawing.SolidWorksHelper
{
    /// <summary>
    /// 从0开始绘制模型，该方式已经废除
    /// </summary>
    public class TypeUAutoDrawing
    {
        SldWorks swApp = ConnectSolidWorks.GetApplication();
        private TypeU item;
        public TypeUAutoDrawing(TypeU item)
        {
            this.item = item;
        }
        public void AutoDrawing()
        {
            //swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swSketchInference, true);
            //获取用户默认模版
            string defaultPartTemplate =
                swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);

            ModelDoc2 swModel = swApp.NewDocument(defaultPartTemplate, 0, 0, 0);
            if (swModel == null) return;
            Feature swFeat = swModel.FeatureByPositionReverse(3);
            swFeat.Name = "Front";
            swFeat = swModel.FeatureByPositionReverse(2);
            swFeat.Name = "Top";
            swFeat = swModel.FeatureByPositionReverse(1);
            swFeat.Name = "Right";
            object datumDisp = "Point1@Origin";

            swModel.Extension.SelectByID2("Right", "PLANE", 0, 0, 0, false, 0, null, 0);

            #region 绘制型材截面
            swModel.InsertSketch2(true);
            swModel.CreateLine2(-item.Width / 2000.0, 0, 0, item.Width / 2000.0, 0, 0);
            swModel.CreateLine2(item.Width / 2000.0, 0, 0, item.Width / 2000.0, 7.0 / 1000.0, 0);
            swModel.CreateLine2(item.Width / 2000.0, 7.0 / 1000.0, 0, (item.Width - 6.0) / 2000.0, 10 / 1000.0, 0);
            swModel.CreateLine2((item.Width - 6.0) / 2000.0, 10.0 / 1000.0, 0, (item.Width - 6.0) / 2000.0, (item.Height - 10) / 1000.0, 0);
            swModel.CreateLine2((item.Width - 6.0) / 2000.0, (item.Height - 10.0) / 1000.0, 0, item.Width / 2000.0, (item.Height - 7.0) / 1000.0, 0);
            swModel.CreateLine2(item.Width / 2000.0, (item.Height - 7.0) / 1000.0, 0, item.Width / 2000.0, item.Height / 1000.0, 0);
            swModel.CreateLine2(item.Width / 2000.0, item.Height / 1000.0, 0, -item.Width / 2000.0, item.Height / 1000.0, 0);
            swModel.CreateLine2(-item.Width / 2000.0, item.Height / 1000.0, 0, -item.Width / 2000.0, (item.Height - 7.0) / 1000.0, 0);
            swModel.CreateLine2(-item.Width / 2000.0, (item.Height - 7.0) / 1000.0, 0, -(item.Width - 6.0) / 2000.0, (item.Height - 10.0) / 1000.0, 0);
            swModel.CreateLine2(-(item.Width - 6.0) / 2000.0, (item.Height - 10.0) / 1000.0, 0, -(item.Width - 6.0) / 2000.0, 10.0 / 1000.0, 0);
            swModel.CreateLine2(-(item.Width - 6.0) / 2000.0, 10 / 1000.0, 0, -item.Width / 2000.0, 7.0 / 1000.0, 0);
            swModel.CreateLine2(-item.Width / 2000.0, 7.0 / 1000.0, 0, -item.Width / 2000.0, 0, 0);
            #endregion
            swModel.SketchManager.FullyDefineSketch(true, true,
               (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Vertical |
               (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Horizontal, true,
               (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp,
               (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp,
               (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementBelow,
               (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementLeft);
            swModel.InsertSketch2(true);
            swModel.ForceRebuild3(true);
            swFeat = swModel.FeatureByPositionReverse(0);
            swFeat.Name = "ProfileSketch";


            swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, item.Height / 1000.0, 8, 0, 0, 0);
            swFeat = swModel.FeatureByPositionReverse(0);
            swFeat.Name = "TopRefPlane";
            swModel.Extension.SelectByID2("TopRefPlane", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.BlankRefGeom();//隐藏

            swModel.Extension.SelectByID2("TopRefPlane", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.InsertSketch2(true);

            swModel.CreateLine2(6.0 / 1000.0, -(item.Width - 16.0) / 2000.0, 0, (item.Length - 6.0) / 1000.0,
                -(item.Width - 16.0) / 2000.0, 0);
            swModel.CreateLine2((item.Length - 6.0) / 1000.0, -(item.Width - 16.0) / 2000.0, 0,
                (item.Length - 6.0) / 1000.0, (item.Width - 16.0) / 2000.0, 0);
            swModel.CreateLine2((item.Length - 6.0) / 1000.0, (item.Width - 16.0) / 2000.0, 0, 6.0 / 1000.0,
                (item.Width - 16.0) / 2000.0, 0);
            swModel.CreateLine2(6.0 / 1000.0, (item.Width - 16.0) / 2000.0, 0, 6.0 / 1000.0,
                -(item.Width - 16.0) / 2000.0, 0);
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swSketchInference, true);

            swModel.SketchManager.FullyDefineSketch(true, true, (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Vertical | (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Horizontal, true, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementBelow, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementLeft);

            swModel.InsertSketch2(true);
            swModel.ForceRebuild3(true);
            swFeat = swModel.FeatureByPositionReverse(0);
            swFeat.Name = "CavitySketch";


            swModel.Extension.SelectByID2("ProfileSketch", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swFeat = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, item.Length / 1000.0, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);
            swFeat.Name = "ProfileModel";

            swModel.Extension.SelectByID2("CavitySketch", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureCut4(true, false, false, 0, 0, (item.Height - 4.0) / 1000.0, (item.Height - 4.0) / 1000.0, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false, false);
            swModel.ForceRebuild3(true);
            if (item.TopViewHoleX1 != 0)
            {
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, item.TopViewHoleX1, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX2 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance,item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX3 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2+ item.TopViewHoleX3;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX4 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2+ item.TopViewHoleX3+ item.TopViewHoleX4;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX5 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2+ item.TopViewHoleX3+ item.TopViewHoleX4+ item.TopViewHoleX5;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX6 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2+ item.TopViewHoleX3+ item.TopViewHoleX4+ item.TopViewHoleX5+ item.TopViewHoleX6;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX7 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2+ item.TopViewHoleX3 + item.TopViewHoleX4+ item.TopViewHoleX5+item.TopViewHoleX6+item.TopViewHoleX7;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }
            if (item.TopViewHoleX8 != 0)
            {
                double distance = item.TopViewHoleX1 + item.TopViewHoleX2 + item.TopViewHoleX3 + item.TopViewHoleX4+ item.TopViewHoleX5+ item.TopViewHoleX6+item.TopViewHoleX7+ item.TopViewHoleX8;
                CreateHole(swModel, datumDisp, "TopRefPlane", item.TopViewHoleDia, distance, item.TopViewHoleY, item.Width, item.Height, false);
            }

            if (item.FrontViewHoleLeftX1 != 0)
            {
                CreateHole(swModel, datumDisp, "Front", item.TopViewHoleDia, item.FrontViewHoleLeftX1, item.Height, item.Height, item.Width, false);
            }
            if (item.FrontViewHoleLeftX2 != 0)
            {
                double distance = item.FrontViewHoleLeftX1 + item.FrontViewHoleLeftX2;
                CreateHole(swModel, datumDisp, "Front", item.TopViewHoleDia, distance, item.Height, item.Height , item.Width, false);
            }

            if (item.FrontViewHoleRightX1 != 0)
            {
                double distance = item.Length - item.FrontViewHoleRightX1;
                CreateHole(swModel, datumDisp, "Front", item.TopViewHoleDia, distance, item.Height, item.Height , item.Width, false);
            }
            if (item.FrontViewHoleRightX2 != 0)
            {
                double distance = item.Length - item.FrontViewHoleRightX1 - item.FrontViewHoleRightX2;
                CreateHole(swModel, datumDisp, "Front", item.TopViewHoleDia, distance, item.Height, item.Height, item.Width, false);
            }

            if (item.LeftViewHoleLeftY1 != 0)
            {
                double distance = item.LeftViewHoleLeftY1 - item.Width / 2.0;
                CreateHole(swModel, datumDisp, "Right", item.TopViewHoleDia, distance, item.Height, item.Height, item.Length, true);
            }
            if (item.LeftViewHoleLeftY2 != 0)
            {
                double distance = item.LeftViewHoleLeftY1+ item.LeftViewHoleLeftY2 - item.Width / 2.0;
                CreateHole(swModel, datumDisp, "Right", item.TopViewHoleDia, distance, item.Height, item.Height, item.Length, true);
            }

            if (item.LeftViewHoleRightY1 != 0)
            {
                double distance = item.Width / 2.0 - item.LeftViewHoleRightY1;
                CreateHole(swModel, datumDisp, "Right", item.TopViewHoleDia, distance, item.Height, item.Height, item.Length, true);
            }
            if (item.LeftViewHoleRightY2 != 0)
            {
                double distance = item.Width / 2.0 - item.LeftViewHoleRightY1- item.LeftViewHoleRightY2;
                CreateHole(swModel, datumDisp, "Right", item.TopViewHoleDia, distance, item.Height, item.Height, item.Length,true);
            }
            swModel.ViewZoomtofit2();

        }

        private void CreateHole(ModelDoc2 swModel, object datumDisp, string panel, double dia, double distanceX,double distanceY, double width, double height,bool dir)
        {
            swModel.Extension.SelectByID2(panel, "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.InsertSketch2(true);
            swApp.WithToggleState(swUserPreferenceToggle_e.swSketchInference,false, () =>
            {
                swModel.CreateCircleByRadius2(distanceX / 1000.0, (distanceY - width / 2.0) / 1000.0, 0, dia / 2000.0);
            });

            swModel.SketchManager.FullyDefineSketch(true, true, (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Vertical | (int)swSketchFullyDefineRelationType_e.swSketchFullyDefineRelationType_Horizontal, true, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimScheme_e.swAutodimSchemeBaseline, datumDisp, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementBelow, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementLeft);
            swModel.FeatureManager.FeatureCut4(false,false , dir, 9, 1, height / 1000.0, height / 1000.0, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false, false);
        }
    }
}
