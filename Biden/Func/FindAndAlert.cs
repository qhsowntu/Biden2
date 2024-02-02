using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class FindAndAlert
    {
        private List<Func2RuleClass> ruleList;
        public FindAndAlert()
        {
            ruleList = ModelFunc2.getInstance.RuleList;
        }

        public string FindStringAndAlert(String str)
        {
            String msg = "";
            for (int i = 0; i < ruleList.Count; i++)
            {
                bool isFlag = true;
                if (ruleList[i].IsCheckedOpt01 == true && ruleList[i].IsCheckedOpt02 == true)
                {
                    for (int j = 0; j < ruleList[i].StrList.Count; j++)
                    {
                        if ((str.ToUpper().Replace(" ","")).Contains((ruleList[i].StrList[j]).ToUpper().Replace(" ", "")))
                        {
                        }
                        else
                        {
                            isFlag = false;
                            break;
                        }
                    }
                }else if (ruleList[i].IsCheckedOpt01 == true && ruleList[i].IsCheckedOpt02 == false)
                {
                    for (int j = 0; j < ruleList[i].StrList.Count; j++)
                    {
                        if ((str.ToUpper()).Contains((ruleList[i].StrList[j]).ToUpper()))
                        {
                        }
                        else
                        {
                            isFlag = false;
                            break;
                        }
                    }
                }
                else if (ruleList[i].IsCheckedOpt01 == false && ruleList[i].IsCheckedOpt02 == true)
                {
                    for (int j = 0; j < ruleList[i].StrList.Count; j++)
                    {
                        if ((str.Replace(" ", "")).Contains((ruleList[i].StrList[j]).Replace(" ", "")))
                        {
                        }
                        else
                        {
                            isFlag = false;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ruleList[i].StrList.Count; j++)
                    {
                        if (str.Contains(ruleList[i].StrList[j]))
                        {
                        }
                        else
                        {
                            isFlag = false;
                            break;
                        }
                    }
                }
                if (isFlag)
                {
                    msg = msg + (i + 1) + ". " + ruleList[i].AlertMsg + "\n";
                }
            }
            if(msg != "")
            {
                MessageBox.Show(msg, "Find Alert", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                //MessageBox.Show(msg, "Find", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }
            return "";
        }
    }
}
