using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VKR2025.ViewModel;

namespace VKR2025;

public partial class Results : Window
{
    //public Results()
    //{
    //    InitializeComponent();
    //    var viewmodel = new TestingViewModel();
    //    viewmodel.CloseResultsAction = () => this.Close();
    //    DataContext = viewmodel;
    //}
    public Results(TestingViewModel viewmodel)
    {
        InitializeComponent();
        viewmodel.CloseResultsAction = () => this.Close(); // назначаем делегат
        DataContext = viewmodel;
    }

    public Results() : this(new TestingViewModel())
    {
    }
}