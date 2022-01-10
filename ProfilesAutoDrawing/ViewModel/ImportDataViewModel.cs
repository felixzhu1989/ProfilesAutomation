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

        private string swFilePath;
        public string SwFilePath
        {
            get => swFilePath;
            set { swFilePath = value; RaisePropertyChanged(() => SwFilePath); }
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
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Excel文件|*.xls;*.xlsx",
                    Title = "选择Excel文件",
                    Multiselect = false
                };
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


        private RelayCommand exportBmp;
        public RelayCommand ExportBmp
        {
            get
            {
                if (exportBmp == null) return new RelayCommand(ExecuteExportBmp);
                return exportBmp;
            }
            set => exportBmp = value;
        }
        /// <summary>
        /// 导出BMP视图
        /// </summary>
        private void ExecuteExportBmp()
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择SolidWorks文件所在文件夹";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (string.IsNullOrEmpty(dialog.SelectedPath)) return;
            SwFilePath = dialog.SelectedPath;
            var files = Directory.GetFiles(SwFilePath, "*.SLDPRT");
            ExportBmpFiles epf = new ExportBmpFiles();
            epf.ExportBmp(files);
            System.Diagnostics.Process.Start("explorer.exe", SwFilePath);
        }

        #endregion
    }
}
