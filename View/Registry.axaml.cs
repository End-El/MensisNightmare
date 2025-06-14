using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System.Linq;
using VKR2025.ViewModel;

namespace VKR2025;

public partial class Registry : Window
{
    public Registry()
    {
        InitializeComponent();
        var viewModel = new TestingViewModel();
        // �������������, ��� ������, ����� VM ������ "�������� ����"
        viewModel.CloseRegistryAction = () => this.Close();
        viewModel.GetOwnerWindow = () => this;
        DataContext = viewModel;
    }
}