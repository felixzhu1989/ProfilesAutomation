using System.Collections.Generic;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProfilesAutoDrawing.Model;
using ProfilesAutoDrawing.SolidWorksHelper;

namespace ProfilesAutoDrawing.ViewModel
{
    /// <summary>
    /// 这里是界面背后的逻辑代码
    /// </summary>
    public class ImportDataViewModel : ViewModelBase
    {
        //构造函数，初始化数据
        public ImportDataViewModel()
        {
            ImportDataList = new List<ImportDataModel>();
        }

        #region 文本框和列表绑定的数据
        private string excelPath;
        public string ExcelPath
        {
            get => excelPath;
            set { excelPath = value; RaisePropertyChanged(() => ExcelPath); }
        }

        private List<ImportDataModel> importDataList;
        public List<ImportDataModel> ImportDataList
        {
            get => importDataList;
            set { importDataList = value; RaisePropertyChanged(() => ImportDataList); }
        }
        #endregion

        #region 按钮对应的命令
        private RelayCommand importExcel;
        public RelayCommand ImportExcel
        {
            get
            {
                if (importExcel == null) return new RelayCommand(ExecuteImportExcel);
                return importExcel;
            }
            set => importExcel = value;
        }
        //导入Excel数据
        void ExecuteImportExcel()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if ((bool)ofd.ShowDialog())
                {
                    ExcelPath = ofd.FileName;
                    ImportDataFormExcel idfe = new ImportDataFormExcel();
                    ImportDataList = idfe.GetImportDataByExcel(ExcelPath);
                    MessageBox.Show("导入数据完成！");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }


        private RelayCommand autoDrawing;
        public RelayCommand AutoDrawing
        {
            get
            {
                if (autoDrawing == null) return new RelayCommand(ExecuteAutoDrawing);
                return autoDrawing;
            }
            set => autoDrawing = value;
        }
        //执行自动绘图入口，将数据列表和excel所在的文件夹地址传递给作图程序
        void ExecuteAutoDrawing()
        {
            if (!File.Exists(ExcelPath)) return;
            string filePath = Path.GetDirectoryName(ExcelPath);
            try
            {
                AutoDrawingContext context = new AutoDrawingContext(ImportDataList, filePath);
                context.ContextInterface();
                MessageBox.Show("绘图完成！");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
            System.Diagnostics.Process.Start("explorer.exe", filePath);
        }

        #endregion
    }
}
