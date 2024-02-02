using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Func
{
    class Func2RuleClass
    {
        private string no;
        private string name;
        private List<string> strList;
        private string addStr;
        private string alertMsg;
        private bool isCheckedOpt01;
        private bool isCheckedOpt02;

        public Func2RuleClass()
        {

        }

        public string No { get => no; set => no = value; }
        public string Name { get => name; set => name = value; }
        public List<string> StrList { get => strList; set => strList = value; }
        public string AddStr { get => addStr; set => addStr = value; }
        public string AlertMsg { get => alertMsg; set => alertMsg = value; }
        public bool IsCheckedOpt01 { get => isCheckedOpt01; set => isCheckedOpt01 = value; }
        public bool IsCheckedOpt02 { get => isCheckedOpt02; set => isCheckedOpt02 = value; }
    }
}
