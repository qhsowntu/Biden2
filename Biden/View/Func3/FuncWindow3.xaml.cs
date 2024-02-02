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
    public partial class FuncWindow3 : BaseWindow
    {
        private static FuncWindow3 instance = null;
        private ViewModelFunc3 _viewModel;
        private FuncWindow3()
        {
            InitializeComponent();
            _viewModel = new ViewModelFunc3();
            this.DataContext = _viewModel;
        }

        public static FuncWindow3 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow3();
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


        public void SetListView()
        {
            this.DataContext = _viewModel;
            _viewModel.StrObjectAndSync();
            //_viewModel.StrObjectAndSync = ModelFunc3.getInstance.StrList;
        }

        private void ListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
