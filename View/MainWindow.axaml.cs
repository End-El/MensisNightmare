using Avalonia.Controls;
using VKR2025.ViewModel;

namespace VKR2025
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new TestingViewModel();
            // �������������, ��� ������, ����� VM ������ "�������� ����"
            viewModel.CloseMainWindowAction = () => this.Close();
            DataContext = viewModel;
        }
    }
}