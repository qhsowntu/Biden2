using Biden.ViewModel;

namespace Biden.View
{
    public partial class MainWindow : BaseWindow
    {
        private static MainWindow instance = null;
        private ViewModelMain _viewModel;

        public ViewModelMain ViewModel => _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModelMain();
            DataContext = _viewModel;
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
    }
}