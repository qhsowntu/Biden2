using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Func
{
    [Serializable]
    public class AcquisitionParameters
    {
        private string name;
        private double pulse;
        private double range;
        private double offset;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Pulse
        {
            get { return pulse; }
            set { pulse = value; }
        }

        public double Range
        {
            get { return range; }
            set { range = value; }
        }

        public double Offset
        {
            get { return offset; }
            set { offset = value; }
        }


    }
}
