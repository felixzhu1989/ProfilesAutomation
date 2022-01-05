using System;
using System.Collections.Generic;
using System.Data;

namespace ProfilesAutoDrawing.Model
{
    /// <summary>
    /// 从Excel文件导入数据，如果Excel文件表头有更改，在这里修改
    /// </summary>
    public class ImportDataFormExcel
    {
        public List<ImportDataModel> GetImportDataByExcel(string path)
        {
            List<ImportDataModel> list = new List<ImportDataModel>();
            try
            {

                //Sheet1是excel文件中的工作簿,用dataset接收整张表
                DataSet ds = OleDbHelper.GetDataSet("select * from [Sheet1$]", path);
                //第一张表存入datatable
                DataTable dt = ds.Tables[0];
                //遍历datatable,封装对象集合，遍历行然后取一列的值
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ImportDataModel()
                    {
                        //根据表头获取对应的值
                        Id = Convert.ToInt32(row["序号"]),
                        PartName = row["零件名"].ToString(),
                        ProfileType = row["型号"].ToString(),
                        TopView = row["俯视图"].ToString(),
                        BackView = row["后视图"].ToString(),
                        LeftView = row["左视图"].ToString(),
                        Length = Convert.ToDouble(row["长度"]),
                        Width = Convert.ToDouble(row["宽度"]),
                        Height = Convert.ToDouble(row["高度"]),
                        //将表中所有的NC替换（快捷键Ctrrl+H）成0，这里判断是否为0即可
                        TopTyHole = row["俯视椭圆孔"].ToString() != "0",
                        //判断是不是空的单元格，如果是空的单元格则会给0
                        TopHoleDia = row["俯视圆孔大小"] != DBNull.Value ? Convert.ToDouble(row["俯视圆孔大小"]) : 0.0,
                        TopHoleY = row["俯视Y"] != DBNull.Value ? Convert.ToDouble(row["俯视Y"]) : 0.0,
                        TopHoleX1 = row["俯视X1"] != DBNull.Value ? Convert.ToDouble(row["俯视X1"]) : 0.0,
                        TopHoleX2 = row["俯视X2"] != DBNull.Value ? Convert.ToDouble(row["俯视X2"]) : 0.0,
                        TopHoleX3 = row["俯视X3"] != DBNull.Value ? Convert.ToDouble(row["俯视X3"]) : 0.0,
                        TopHoleX4 = row["俯视X4"] != DBNull.Value ? Convert.ToDouble(row["俯视X4"]) : 0.0,
                        TopHoleX5 = row["俯视X5"] != DBNull.Value ? Convert.ToDouble(row["俯视X5"]) : 0.0,
                        TopHoleX6 = row["俯视X6"] != DBNull.Value ? Convert.ToDouble(row["俯视X6"]) : 0.0,
                        TopHoleX7 = row["俯视X7"] != DBNull.Value ? Convert.ToDouble(row["俯视X7"]) : 0.0,
                        TopHoleX8 = row["俯视X8"] != DBNull.Value ? Convert.ToDouble(row["俯视X8"]) : 0.0,
                        TopUqX = row["UQ检测X"] != DBNull.Value ? Convert.ToDouble(row["UQ检测X"]) : 0.0,
                        TopUqY = row["UQ检测Y"] != DBNull.Value ? Convert.ToDouble(row["UQ检测Y"]) : 0.0,
                        TopTbDia = row["踏步孔大小"] != DBNull.Value ? Convert.ToDouble(row["踏步孔大小"]) : 0.0,
                        TopTbY = row["踏步Y"] != DBNull.Value ? Convert.ToDouble(row["踏步Y"]) : 0.0,
                        TopTbX1 = row["踏步X1"] != DBNull.Value ? Convert.ToDouble(row["踏步X1"]) : 0.0,
                        TopTbX2 = row["踏步X2"] != DBNull.Value ? Convert.ToDouble(row["踏步X2"]) : 0.0,

                        FrontHoleLeftX1 = row["前左X1"] != DBNull.Value ? Convert.ToDouble(row["前左X1"]) : 0.0,
                        FrontHoleLeftX2 = row["前左X2"] != DBNull.Value ? Convert.ToDouble(row["前左X2"]) : 0.0,
                        FrontHoleRightX1 = row["前右X1"] != DBNull.Value ? Convert.ToDouble(row["前右X1"]) : 0.0,
                        FrontHoleRightX2 = row["前右X2"] != DBNull.Value ? Convert.ToDouble(row["前右X2"]) : 0.0,

                        //U板
                        BackHoleLeftX1 = row["U后左X1"] != DBNull.Value ? Convert.ToDouble(row["U后左X1"]) : 0.0,
                        BackHoleLeftX2 = row["U后左X2"] != DBNull.Value ? Convert.ToDouble(row["U后左X2"]) : 0.0,
                        BackHoleRightX1 = row["U后右X1"] != DBNull.Value ? Convert.ToDouble(row["U后右X1"]) : 0.0,
                        BackHoleRightX2 = row["U后右X2"] != DBNull.Value ? Convert.ToDouble(row["U后右X2"]) : 0.0,
                        LeftHoleFrontY1 = row["U左前Y1"] != DBNull.Value ? Convert.ToDouble(row["U左前Y1"]) : 0.0,
                        LeftHoleFrontY2 = row["U左前Y2"] != DBNull.Value ? Convert.ToDouble(row["U左前Y2"]) : 0.0,
                        LeftHoleBackY1 = row["U左后Y1"] != DBNull.Value ? Convert.ToDouble(row["U左后Y1"]) : 0.0,
                        LeftHoleBackY2 = row["U左后Y2"] != DBNull.Value ? Convert.ToDouble(row["U左后Y2"]) : 0.0,
                        RightHoleFrontY1 = row["U右前Y1"] != DBNull.Value ? Convert.ToDouble(row["U右前Y1"]) : 0.0,
                        RightHoleFrontY2 = row["U右前Y2"] != DBNull.Value ? Convert.ToDouble(row["U右前Y2"]) : 0.0,
                        RightHoleBackY1 = row["U右后Y1"] != DBNull.Value ? Convert.ToDouble(row["U右后Y1"]) : 0.0,
                        RightHoleBackY2 = row["U右后Y2"] != DBNull.Value ? Convert.ToDouble(row["U右后Y2"]) : 0.0,


                        //E板
                        ETopHoleLeftX1 = row["E俯左X1"] != DBNull.Value ? Convert.ToDouble(row["E俯左X1"]) : 0.0,
                        ETopHoleLeftX2 = row["E俯左X2"] != DBNull.Value ? Convert.ToDouble(row["E俯左X2"]) : 0.0,
                        ETopHoleRightX1 = row["E俯右X1"] != DBNull.Value ? Convert.ToDouble(row["E俯右X1"]) : 0.0,
                        ETopHoleRightX2 = row["E俯右X2"] != DBNull.Value ? Convert.ToDouble(row["E俯右X2"]) : 0.0,
                        //如果有扩展在这里加数据，同时ImportDataModel.cs也要增加数据




                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("从Excel导入数据发生异常，详细：" + ex.Message);
            }
            return list;
        }
    }
}
