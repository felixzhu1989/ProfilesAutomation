using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProfilesAutoDrawing.Model;
using ProfilesAutoDrawing.View;

namespace ProfilesAutoDrawing.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region 参考资料
        /* treeview selected item https://stackoverflow.com/questions/7153813/wpf-mvvm-treeview-selecteditem
         * C# Json https://www.bilibili.com/video/BV1Yt41127rC?p=3
         *
         */
        #endregion
        private string _dir = @"D:\Profiles";
        public MainViewModel()
        {
            InitTreeType();
            ProjectName = "";

        }
        private string projectName;
        public string ProjectName
        {
            get => projectName;
            set { projectName = value; RaisePropertyChanged(() => ProjectName); }
        }
        #region 项目树
        private ObservableCollection<TreeNodeModel> treeProject;
        public ObservableCollection<TreeNodeModel> TreeProject
        {
            get => treeProject;
            set { treeProject = value; RaisePropertyChanged(() => TreeProject); }
        }


        #endregion

        #region 型号树
        private ObservableCollection<TreeNodeModel> treeType;
        public ObservableCollection<TreeNodeModel> TreeType
        {
            get => treeType;
            set { treeType = value; RaisePropertyChanged(()=>TreeType); }
        }
        void InitTreeType()
        {
            TreeType = new ObservableCollection<TreeNodeModel>()
            {
                new TreeNodeModel()
                {
                    NodeID = "1", NodeName = "选择型材", Children = new ObservableCollection<TreeNodeModel>()
                    {
                        new TreeNodeModel(){NodeID = "1.1",NodeName = "TypeU"},
                        new TreeNodeModel(){NodeID = "1.2",NodeName = "TypeX"},
                    }
                }
            };
        }
        private string selectType;
        public string SelectType
        {
            get => selectType;
            set { selectType = value;RaisePropertyChanged(()=> SelectType); }
        }



        #endregion

        #region 自定义控件



        private TypeUView typeU;
        /// <summary>
        /// 用户控件模板列表
        /// </summary>
        public TypeUView TypeU
        {
            get => typeU;
            set { typeU = value; RaisePropertyChanged(() => TypeU); }
        }



        #endregion

        #region 命令
        private RelayCommand loadProject;
        public RelayCommand LoadProject
        {
            get
            {
                if (loadProject == null) return new RelayCommand(ExecuteLoadProject);
                return loadProject;
            }
            set => loadProject = value;
        }
        //加载数据
        void ExecuteLoadProject()
        {
            if (ProjectName.Length == 0) MessageBox.Show("请填写项目编号");
            CreateDir(out string projectPath);
            TreeProject = new ObservableCollection<TreeNodeModel>();
            string jsonPath = Path.Combine(projectPath, "TreeProject.json");
            if (File.Exists(jsonPath))
            {
                string strJson = File.ReadAllText(jsonPath);
                TreeProject = JsonConvert.DeserializeObject<ObservableCollection<TreeNodeModel>>(strJson);
            }
            MessageBox.Show("加载完成！");
        }

        void CreateDir(out string projectPath)
        {
            projectPath = Path.Combine(_dir, ProjectName);
            if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);
        }

        private RelayCommand editData;
        public RelayCommand EditData
        {
            get
            {
                if (editData == null) return new RelayCommand(ExecuteEditData);
                return editData;
            }
            set => editData = value;
        }
        //编辑参数
        void ExecuteEditData()
        {
            foreach (TreeNodeModel item in TreeProject)
            {
                if (item.IsSelected)
                {
                    MessageBox.Show(item.NodeName);
                }
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
        //自动绘图
        void ExecuteAutoDrawing()
        {
            MessageBox.Show("绘图完成！");
        }

        private RelayCommand chooseType;
        public RelayCommand ChooseType
        {
            get
            {
                if (chooseType == null) return new RelayCommand(ExecuteChooseType);
                return chooseType;
            }
            set => autoDrawing = value;
        }
        //选择型号
        void ExecuteChooseType()
        {
            foreach (TreeNodeModel item in TreeType[0].Children)
            {
                if (item.IsSelected)
                {
                    if (TreeProject == null){ MessageBox.Show("请先加载项目");return;}
                    int? id = TreeProject?.Count + 1;
                    TreeProject?.Add(new TreeNodeModel(){NodeName =item.NodeName,NodeID = id.ToString()});
                    //保存json
                    string outputJson = JsonConvert.SerializeObject(TreeProject);
                    CreateDir(out string projectPath);
                    File.WriteAllText(Path.Combine(projectPath, "TreeProject.json"), outputJson);
                    MessageBox.Show("添加成功！");
                }
            }
           
        }
        #endregion
    }
}