using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace ProfilesAutoDrawing.Model
{
    public class TreeNodeModel : ObservableObject
    {
        public string NodeID { get; set; }
        public string NodeName { get; set; }
        public ObservableCollection<TreeNodeModel> Children { get; set; }
        public bool IsSelected { get; set; } // 节点是否选中
    }
}
