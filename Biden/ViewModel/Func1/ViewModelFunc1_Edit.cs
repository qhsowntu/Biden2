using Biden.Func;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.ViewModel
{
    class ViewModelFunc1_Edit : ViewModelFunc1
    {
        public Command CmdFuncBtn01_EditOK { get; set; }
        public Command CmdFuncBtn01_EditCancel { get; set; }


        public ViewModelFunc1_Edit()
        {
            CmdFuncBtn01_EditOK = new Command(Execute_FuncBtn01_EditOK, CanExecute_Btn01);
            CmdFuncBtn01_EditCancel = new Command(Execute_FuncBtn01_EditCancel, CanExecute_Btn01);
        }

        private void Execute_FuncBtn01_EditOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            for (int i = 0; i < ruleList.Count; i++)
            {
                if(ruleList[i].NameStr == rule.NameStr && i != editedIndex) 
                {
                    MessageBox.Show("name already exists");
                    return;
                }
            }
            ruleList[editedIndex] = (new Func1RuleClass() {
                //No = editedIndex + "",
                NameStr = nameStr,
                FromStr = fromStr,
                ToStr = toStr,
                PrefixStr = prefixStr,
                PostfixStr = postfixStr
            });
            removeStr();
            FuncWindow1_Edit.getInstance.Hide();
        }
        private void Execute_FuncBtn01_EditCancel(object obj)
        {
            FuncWindow1_Edit.getInstance.Hide();
        }

        public void setInfo()
        {
            nameStr = nameStr;
            fromStr = fromStr;
            toStr = toStr;
            postfixStr = postfixStr;
            prefixStr = prefixStr;
        }
        

    }
}
