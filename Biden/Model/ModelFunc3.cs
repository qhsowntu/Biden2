using Biden.Func;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Model
{
    class ModelFunc3
    {
        private List<Func3RuleClass> ruleList;
        private List<object> objList;
        private List<DataObject> dataObjList; 
        private List<string> dataObjTypeList; //
        private List<string> strListForView;

        private static ModelFunc3 instance = null;
        private bool _isChecked03 = false;
        private bool isCheckedDupOpt = true;
        private int leftPoint;
        private int topPoint;

        private string _theSelectedRule;
        private List<string> _source;
        private MacroInfo3 selectedItem;

        private string selectedString;
        private string selectedStringIndex;

        private ModelFunc3()
        {
            RuleList = new List<Func3RuleClass>();
            ObjList = new List<object>();
            StrListForView = new List<string>();
            Source = new List<string> {
                "[FIFO] Using The Clipboard as a Queue",
                "[LIFO] Using The Clipboard as a Stack",
                "[Select] Using The Clipboard as a Dictionary"};
            _theSelectedRule = _source.ElementAt(0);
            DataObjList = new List<DataObject>();
            DataObjTypeList = new List<string>();
        }

        public static ModelFunc3 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelFunc3();
                }
                return instance;
            }
        }


        internal List<Func3RuleClass> RuleList { get => ruleList; set => ruleList = value; }
        public bool IsChecked03 { get => _isChecked03; set => _isChecked03 = value; }
        public string TheSelectedRule { get => _theSelectedRule; set => _theSelectedRule = value; }
        public List<string> Source { get => _source; set => _source = value; }
        public string SelectedString { get => selectedString; set => selectedString = value; }
        public bool IsCheckedDupOpt { get => isCheckedDupOpt; set => isCheckedDupOpt = value; }
        public List<string> StrListForView { get => strListForView; set => strListForView = value; }
        public int LeftPoint { get => leftPoint; set => leftPoint = value; }
        public int TopPoint { get => topPoint; set => topPoint = value; }
        public string SelectedStringIndex { get => selectedStringIndex; set => selectedStringIndex = value; }
        public List<object> ObjList { get => objList; set => objList = value; }
        internal MacroInfo3 SelectedItem { get => selectedItem; set => selectedItem = value; }
        public List<DataObject> DataObjList { get => dataObjList; set => dataObjList = value; }
        public List<string> DataObjTypeList { get => dataObjTypeList; set => dataObjTypeList = value; }

        public class MacroInfo3
        {
            public string No { get; set; }
            public object Obj { get; set; }
        }
    }
}
