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
        if (sender is ToggleButton button && button.Tag is string id)
        {
            if (!userSelections.Contains(id))
                userSelections.Add(id);
        }
    }

    private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && button.Tag is string id)
        {
            if (userSelections.Contains(id))
                userSelections.Remove(id);
        }
    }
}