using Biden.Func;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biden.View
{
    /// <summary>
    /// FuncWindow1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FuncWindow1_Add : BaseWindow
    {
        private static FuncWindow1_Add instance = null;

        private FuncWindow1_Add()
        {
            InitializeComponent();
        }
        public static FuncWindow1_Add getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow1_Add();
                }
                return instance;
            }
        }



    }
}
