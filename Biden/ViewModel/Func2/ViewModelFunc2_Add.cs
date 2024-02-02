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
    class ViewModelFunc2_Add : ViewModelFunc2
    {

        public ViewModelFunc2_Add()
        {
        }

        private void Execute_FuncBtn01_AddOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            for (int i = 0; i < ruleList.Count; i++)
            {
                if(ruleList[i].Name == rule.Name) 
                {
                    MessageBox.Show("name already exists");
                    return;
                }
            }
            List<string> tempStr = new List<string>();
            for (int i = 0; i < StrObjectCollection.Count; i++)
            {
                tempStr.Add(StrObjectCollection[i].Str + "");
            }
            Func2RuleClass tempRules = new Func2RuleClass();
            tempRules.No = ruleList.Count+"";
            tempRules.Name = nameStr;
            tempRules.StrList = tempStr;
            tempRules.AddStr = addStr;
            tempRules.AlertMsg = alertStr;
            tempRules.IsCheckedOpt01 = isCheckedOpt01;
            tempRules.IsCheckedOpt02 = isCheckedOpt02;
            ruleList.Add(tempRules);
            removeStr();
            FuncWindow2_Add.getInstance.Hide();
        }
        private void Execute_FuncBtn01_AddCancel(object obj)
        {
            FuncWindow2_Add.getInstance.Hide();
        }
        private void Execute_FuncBtn01_AddStr(object obj)
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

        


    }
}
