using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Model
{
    class ModelFunc1
    {
        private List<Func1RuleClass> ruleList;

        private static ModelFunc1 instance = null;
        private bool _isChecked01 = false;

        private ModelFunc1()
        {
            RuleList = new List<Func1RuleClass>();
        }

        public static ModelFunc1 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelFunc1();
                }
                return instance;
            }
        }


        internal List<Func1RuleClass> RuleList { get => ruleList; set => ruleList = value; }
        public bool IsChecked01 { get => _isChecked01; set => _isChecked01 = value; }
    }
}
