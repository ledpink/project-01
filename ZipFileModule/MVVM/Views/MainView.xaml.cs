using System.Windows.Controls;
using ZipFileModule.MVVM.ViewModels;

namespace ZipFileModule.MVVM.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
