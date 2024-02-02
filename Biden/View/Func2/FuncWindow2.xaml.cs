using Biden.Func;
using Biden.Model;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
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
    public partial class FuncWindow2 : BaseWindow
    {
        private static FuncWindow2 instance = null;
        private ViewModelFunc2 _viewModel;
        private FuncWindow2()
        {
            InitializeComponent();
            _viewModel = new ViewModelFunc2();
            this.DataContext = _viewModel;
        }

        public static FuncWindow2 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow2();
                }
                return instance;
            }
        }

        /*
        public override void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(this+"@");
            //Macro.getInstance.PasteModeOn = false;
            //Macro.destroy();
            string windowName = this + "";
            if (windowName.Contains("MainWindow"))
            {
                deleteAllWindow();
                //this.Close();
                return;
            }
            else
            {
                this.Hide();
            }
        }*/

        public void SetCheckBox02()
        {
            this.DataContext = _viewModel;
            _viewModel.IsChecked02 = ModelFunc2.getInstance.IsChecked02;
        }

    }
}
