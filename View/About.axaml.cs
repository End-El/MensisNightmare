using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VKR2025.ViewModel;

namespace VKR2025;

public partial class About : Window
{
    public About()
    {
        InitializeComponent();
        var viewmodel = new TestingViewModel();
        viewmodel.CloseAboutAction = () => this.Close();
        DataContext = viewmodel;
    }
}