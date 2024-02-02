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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biden.View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 아래 패키지 설치
    /// Install-Package FontAwesome.WPF
    public partial class MainWindow : BaseWindow
    {
        private static MainWindow instance = null;
        private ViewModelMain _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModelMain();
            this.DataContext = _viewModel;
        }
        public static MainWindow getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }

        public void SetStateString(int x, int y, System.Drawing.Color curColor)
        {
            _viewModel.SetStateString(x, y, curColor);
        }
        public void SetSyncCheckBox()
        {
            this.DataContext = _viewModel;
            _viewModel.IsChecked01 = ModelFunc1.getInstance.IsChecked01;
            _viewModel.IsChecked02 = ModelFunc2.getInstance.IsChecked02;
            _viewModel.IsChecked03 = ModelFunc3.getInstance.IsChecked03;
        }

    }
}
