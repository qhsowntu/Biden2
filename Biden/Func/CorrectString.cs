using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class CorrectString
    {
        private List<Func1RuleClass> ruleList;
        public CorrectString()
        {
            ruleList = ModelFunc1.getInstance.RuleList;
        }

        public string getModifiedText(String str)
        {
            String res = str;
            for (int i = 0; i < ruleList.Count; i++)
            {
                //replace
                if (ruleList[i].FromStr != null)
                {
                    res = res.Replace(ruleList[i].FromStr + "", ruleList[i].ToStr + "");
                }
                //pre,post 추가
                res = ruleList[i].PrefixStr + res + ruleList[i].PostfixStr;
                //Trim
                if (false)
                {
                    try
                    {
                        res = res.Trim();
                    }
                    catch (Exception e)
                    {
                        res = "Trim Exception!\n\n" + e;
                    }
                }
                //지정된 인덱스에서부터 지정된 숫자만큼 문자삭제. 인덱스 시작은 0부터
                if (false) {
                    try
                    {
                        res = res.Remove(2, 5);
                    }
                    catch (Exception e)
                    {
                        res = "Remove Exception!\n\n 인덱스가 범위를 초과하였습니다.\n\n" + e;
                    }
                }
                //지정된 인덱스에서부터 지정된 숫자만큼 문자남김. 인덱스 시작은 0부터
                if (false)
                {
                    try
                    {
                        res = res.Substring(2, 5);
                    }
                    catch (Exception e)
                    {
                        res = "Substring Exception!\n\n 인덱스가 범위를 초과하였습니다.\n\n" + e;
                    }
                }
                //소문자로
                if (false)
                {
                    try
                    {
                        res = res.ToLower();
                    }
                    catch (Exception e)
                    {
                        res = "ToLower Exception!\n\n 인덱스가 범위를 초과하였습니다.\n\n" + e;
                    }
                }
                //대문자로
                if (false)
                {
                    try
                    {
                        res = res.ToUpper();
                    }
                    catch (Exception e)
                    {
                        res = "ToUpper Exception!\n\n 인덱스가 범위를 초과하였습니다.\n\n" + e;
                    }
                }
            }
            return res;
        }
    }
}
