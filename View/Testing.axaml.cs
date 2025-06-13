using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VKR2025.ViewModel;

namespace VKR2025.View;

public partial class Testing : Window
{
    public Testing()
    {
        InitializeComponent();
        DataContext = new TestingViewModel();
    }
}