using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using VKR2025.ViewModel;

namespace VKR2025.View;

public partial class Testing : Window
{
    public Testing()
    {
        InitializeComponent();
        DataContext = new TestingViewModel();
    }

    public ObservableCollection<string> userSelections = new ObservableCollection<string>();

    private void ToggleButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && DataContext is TestingViewModel viewModel)
        {
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            string coord = $"{row}{col}";

            if (!viewModel.UserSelections.Contains(coord))
                viewModel.UserSelections.Add(coord);
        }
    }

    private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && DataContext is TestingViewModel viewModel)
        {
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            string coord = $"{row}{col}";

            if (viewModel.UserSelections.Contains(coord))
                viewModel.UserSelections.Remove(coord);
        }
    }

    public void ResetToggleButtons()
    {
        foreach (var child in MyMatrixGrid.Children)
        {
            if (child is ToggleButton button)
                button.IsChecked = false;
        }
    }
}