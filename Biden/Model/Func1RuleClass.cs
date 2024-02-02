using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Func
{
    class Func1RuleClass
    {
        private string no;
        private string nameStr;
        private string fromStr;
        private string toStr;
        private string prefixStr;
        private string postfixStr;
        private string findStr;

        /*
            name
            from->to 변환
            delete 삭제
            pre 추가
            post 추가
            find 탐색 시 경고  
            
        */
        public Func1RuleClass()
        {

        }

        public string NameStr { get => nameStr; set => nameStr = value; }
        public string FromStr { get => fromStr; set => fromStr = value; }
        public string ToStr { get => toStr; set => toStr = value; }
        public string PrefixStr { get => prefixStr; set => prefixStr = value; }
        public string PostfixStr { get => postfixStr; set => postfixStr = value; }
        public string No { get => no; set => no = value; }
    }
}
