using GalaSoft.MvvmLight;

namespace ProfilesAutoDrawing.Model
{
    /// <summary>
    /// Excel导入数据模版
    /// </summary>
    public class ImportDataModel: ObservableObject
    {
        //序号
        public int Id { get; set; }
        //图号，保存的文件名
        public string PartName { get; set; }
        //判断型材的依据
        public string ProfileType { get; set; }
        //三视图形状特征
        public string TopView { get; set; }
        public string BackView { get; set; }
        public string LeftView { get; set; }
        //最大外形尺寸
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        //俯视图板面椭圆孔
        public bool TopTyHole { get; set; }
        //俯视图板面圆孔
        public double TopHoleDia { get; set; }
        //俯视图圆孔位坐标
        public double TopHoleY { get; set; }
        public double TopHoleX1 { get; set; }
        public double TopHoleX2 { get; set; }
        public double TopHoleX3 { get; set; }
        public double TopHoleX4 { get; set; }
        public double TopHoleX5 { get; set; }
        public double TopHoleX6 { get; set; }
        public double TopHoleX7 { get; set; }
        public double TopHoleX8 { get; set; }
        //俯视图UQ外形检测尺寸
        public double TopUqX { get; set; }
        public double TopUqY { get; set; }
        //俯视图踏步穿板孔
        public double TopTbDia { get; set; }
        public double TopTbY { get; set; }
        public double TopTbX1 { get; set; }
        public double TopTbX2 { get; set; }

        //前视
        public double FrontHoleLeftX1 { get; set; }
        public double FrontHoleLeftX2 { get; set; }
        public double FrontHoleRightX1 { get; set; }
        public double FrontHoleRightX2 { get; set; }

        //U板
        //后视图孔
        public double BackHoleLeftX1 { get; set; }
        public double BackHoleLeftX2 { get; set; }
        public double BackHoleRightX1 { get; set; }
        public double BackHoleRightX2 { get; set; }
        //左视图孔
        public double LeftHoleFrontY1 { get; set; }
        public double LeftHoleFrontY2 { get; set; }
        public double LeftHoleBackY1 { get; set; }
        public double LeftHoleBackY2 { get; set; }
        //右视图孔
        public double RightHoleFrontY1 { get; set; }
        public double RightHoleFrontY2 { get; set; }
        public double RightHoleBackY1 { get; set; }
        public double RightHoleBackY2 { get; set; }

        //E板
        //俯视
        public double ETopHoleLeftX1 { get; set; }
        public double ETopHoleLeftX2 { get; set; }
        public double ETopHoleRightX1 { get; set; }
        public double ETopHoleRightX2 { get; set; }
        
        //如果有扩展在这里加数据，同时ImportDataFormExcel.cs也要增加数据





    }
}
