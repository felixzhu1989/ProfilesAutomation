using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProfilesAutoDrawing.Model;
using ProfilesAutoDrawing.SolidWorksHelper;

namespace ProfilesAutoDrawing.ViewModel
{
    /// <summary>
    /// 第一版的窗口逻辑，已经废弃
    /// </summary>
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
            TypeUData = new TypeU();
        }
        private string projectName;
        public string ProjectName
        {
            get => projectName;
            set { projectName = value; RaisePropertyChanged(() => ProjectName); }
        }


        #region 参数
        private TypeU typeUData;
        public TypeU TypeUData
        {
            get => typeUData;
            set { typeUData = value; RaisePropertyChanged(() => TypeUData); }
        }

        #endregion

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
            set { treeType = value; RaisePropertyChanged(() => TreeType); }
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
                        //new TreeNodeModel(){NodeID = "1.2",NodeName = "TypeX"},
                    }
                }
            };
        }
        private string selectType;
        public string SelectType
        {
            get => selectType;
            set { selectType = value; RaisePropertyChanged(() => SelectType); }
        }



        #endregion

        #region 自定义控件



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
            
            TreeProject = new ObservableCollection<TreeNodeModel>();
            TreeProject = JsonConvert.DeserializeObject<ObservableCollection<TreeNodeModel>>(ReadJsonStr("TreeProject.json"));
            if(ReadJsonStr("TypeUData.json").Length>30)
            TypeUData= JsonConvert.DeserializeObject<TypeU>(ReadJsonStr("TypeUData.json"));
            MessageBox.Show("加载完成！");
        }
        void CreateDir(out string projectPath)
        {
            projectPath = Path.Combine(_dir, ProjectName);
            if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);
        }
        string ReadJsonStr(string jsonFileName)
        {
            string strJson = "";
            CreateDir(out string projectPath);
            string jsonPath = Path.Combine(projectPath, jsonFileName);
            if (File.Exists(jsonPath))
            {
                strJson = File.ReadAllText(jsonPath);
            }
            return strJson;
        }

        private RelayCommand saveData;
        public RelayCommand SaveData
        {
            get
            {
                if (saveData == null) return new RelayCommand(ExecuteSaveData);
                return saveData;
            }
            set => saveData = value;
        }
        //保存参数
        void ExecuteSaveData()
        {
            SaveJson(TypeUData, "TypeUData.json");
            MessageBox.Show("保存成功！");
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
        private RelayCommand deleteData;
        public RelayCommand DeleteData
        {
            get
            {
                if (deleteData == null) return new RelayCommand(ExecuteDeleteData);
                return deleteData;
            }
            set => deleteData = value;
        }
        //删除参数
        void ExecuteDeleteData()
        {
            TreeNodeModel deleteItem = null;
            foreach (TreeNodeModel item in TreeProject)
            {
                if (item.IsSelected)
                {
                    deleteItem = item;
                }
            }
            if (deleteItem == null) return;
            TreeProject.Remove(deleteItem);
            SaveJson(TreeProject, "TreeProject.json");
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
            TypeUAutoDrawing ad = new TypeUAutoDrawing(TypeUData);
            ad.AutoDrawing();
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
                    if (TreeProject == null) { MessageBox.Show("请先加载项目"); return; }
                    List<int> intList = new List<int>();
                    foreach (TreeNodeModel node in TreeProject)
                    {
                        intList.Add(Convert.ToInt32(node.NodeID));
                    }
                    int id = 1;
                    for (int i = 1; i < intList.Count+2; i++)
                    {
                        if(!intList.Exists(t=>t==i))//判断存在
                        {
                            id = i;//不存在就进来
                            break;
                        }
                    }
                    TreeProject?.Add(new TreeNodeModel() { NodeName = item.NodeName, NodeID = id.ToString() });
                    SaveJson(TreeProject, "TreeProject.json");
                    MessageBox.Show("添加成功！");
                }
            }
        }

        private void SaveJson(object target,string jsonFileName)
        {
            //保存json
            string outputJson = JsonConvert.SerializeObject(target);
            CreateDir(out string projectPath);
            File.WriteAllText(Path.Combine(projectPath, jsonFileName), outputJson);
        }
        #endregion


    }
}