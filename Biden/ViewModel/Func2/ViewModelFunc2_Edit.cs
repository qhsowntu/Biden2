using Biden.Func;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.ViewModel
{
    class ViewModelFunc2_Edit : ViewModelFunc2
    {
        public Command CmdFuncBtn01_EditOK { get; set; }
        public Command CmdFuncBtn01_EditCancel { get; set; }
        public Command CmdFuncBtn01_EditStr { get; set; }


        public ViewModelFunc2_Edit()
        {
            CmdFuncBtn01_EditOK = new Command(Execute_FuncBtn01_EditOK, CanExecute_Btn01);
            CmdFuncBtn01_EditCancel = new Command(Execute_FuncBtn01_EditCancel, CanExecute_Btn01);
            CmdFuncBtn01_EditStr = new Command(Execute_FuncBtn01_EditStr, CanExecute_Btn01);

        }

        private void Execute_FuncBtn01_EditOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            for (int i = 0; i < ruleList.Count; i++)
            {
                if(ruleList[i].Name == rule.Name && i != editedIndex) 
                {
                    MessageBox.Show("name already exists");
                    return;
                }
            }
            List<string> tempStr = new List<string>();
            for(int i = 0; i < StrObjectCollection.Count; i++)
            {
                tempStr.Add(StrObjectCollection[i].Str+"");
            }
            ruleList[editedIndex] = (new Func2RuleClass() {
                //No = editedIndex + "",
                Name = nameStr,
                StrList = tempStr,
                AlertMsg = alertStr,
                IsCheckedOpt01 = isCheckedOpt01,
                IsCheckedOpt02 = isCheckedOpt02
            });
            removeStr();
            FuncWindow2_Edit.getInstance.Hide();
        }
        private void Execute_FuncBtn01_EditCancel(object obj)
        {
            FuncWindow2_Edit.getInstance.Hide();
        }

        private void Execute_FuncBtn01_EditStr(object obj)
        {
            //화면 리스트를 tempStr에 저장
            List<string> tempStr = new List<string>();
            for (int i = 0; i < StrObjectCollection.Count; i++)
            {
                tempStr.Add(StrObjectCollection[i].Str + "");
            }
            //추가 Str을 tempStr에 저장
            tempStr.Add(addStr);
            //tempStr을 컬렉션에 저장하여 화면출력
            ObservableCollection<MacroInfo2> tempCollection = new ObservableCollection<MacroInfo2>();
            for (int i = 0; i < tempStr.Count; i++)
            {
                MacroInfo2 tempInfo = new MacroInfo2();
                {
                    tempInfo.No = "" + (i + 1);
                    tempInfo.Str = "" + tempStr[i];
                }
                tempCollection.Add(tempInfo);
            }
            StrObjectCollection = tempCollection;
        }


        public void setInfo()
        {
            nameStr = nameStr;
            StrObjectCollection = StrObjectCollection;
            alertStr = alertStr;
            isCheckedOpt01 = isCheckedOpt01;
            isCheckedOpt02 = isCheckedOpt02;

        }
        

    }
}
