using System;
using System.Data;
using System.Data.OleDb;

namespace ProfilesAutoDrawing.Model
{
    /// <summary>
    /// 提供Excel数据访问支持
    /// </summary>
    public class OleDbHelper
    {
        private static string connString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;";
        public static DataSet GetDataSet(string sql, string path)
        {
            OleDbConnection conn = new OleDbConnection(string.Format(connString, path));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);//创建数据适配器对象
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
