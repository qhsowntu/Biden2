using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Model
{
    class ModelFunc2
    {
        private List<Func2RuleClass> ruleList;

        private static ModelFunc2 instance = null;
        private bool _isChecked02 = false;

        private ModelFunc2()
        {
            RuleList = new List<Func2RuleClass>();
        }

        public static ModelFunc2 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelFunc2();
                }
                return instance;
            }
        }


        internal List<Func2RuleClass> RuleList { get => ruleList; set => ruleList = value; }
        public bool IsChecked02 { get => _isChecked02; set => _isChecked02 = value; }
    }
}
