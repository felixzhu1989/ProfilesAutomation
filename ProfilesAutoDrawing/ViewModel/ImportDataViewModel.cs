using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProfilesAutoDrawing.Model;
using ProfilesAutoDrawing.SolidWorksHelper;

namespace ProfilesAutoDrawing.ViewModel
{
    public class ImportDataViewModel : ViewModelBase
    {
        //构造函数，初始化数据
        public ImportDataViewModel()
        {
            ImportDataList = new List<ImportDataModel>();
        }

        #region 数据
        private string excelPath;
        public string ExcelPath
        {
            get => excelPath;
            set { excelPath = value; RaisePropertyChanged(()=> ExcelPath);}
        }

        private List<ImportDataModel> importDataList;
        public List<ImportDataModel> ImportDataList
        {
            get => importDataList;
            set { importDataList = value; RaisePropertyChanged(()=> ImportDataList);}
        }
        #endregion

        #region 命令
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
        //自动绘图
        void ExecuteImportExcel()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if((bool)ofd.ShowDialog())
            {
                ExcelPath = ofd.FileName;
                ImportDataFormExcel idfe = new ImportDataFormExcel();
                ImportDataList = idfe.GetImportDataByExcel(ExcelPath);
            }
            MessageBox.Show("导入数据完成！");
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
        //自动绘图
        void ExecuteAutoDrawing()
        {
            //

            MessageBox.Show("绘图完成！");
        }

        #endregion
    }
}
