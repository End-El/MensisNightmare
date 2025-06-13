using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using Avalonia.Data;
using Avalonia.Controls.ApplicationLifetimes;

namespace VKR2025.ViewModel
{
    public class TestingViewModel : INotifyPropertyChanged
    {
        private bool _stage1Visible;
        public bool Stage1Visible
        {
            get { return _stage1Visible; }
            set
            {
                if (_stage1Visible != value) // Исправлено условие с = на !=
                {
                    _stage1Visible = value;
                    OnPropertyChanged(nameof(Stage1Visible));
                }
            }
        }

        private bool _stage2Visible;
        public bool Stage2Visible
        {
            get { return _stage2Visible; }
            set
            {
                if (_stage2Visible != value) // Исправлено условие с = на !=
                {
                    _stage2Visible = value;
                    OnPropertyChanged(nameof(Stage2Visible));
                }
            }
        }

        private bool _stage3Visible;
        public bool Stage3Visible
        {
            get { return _stage3Visible; }
            set
            {
                if (_stage3Visible != value) // Исправлено условие с = на !=
                {
                    _stage3Visible = value;
                    OnPropertyChanged(nameof(Stage3Visible));
                }
            }
        }

        private bool _stage4Visible;
        public bool Stage4Visible
        {
            get { return _stage4Visible; }
            set
            {
                if (_stage4Visible != value) // Исправлено условие с = на !=
                {
                    _stage4Visible = value;
                    OnPropertyChanged(nameof(Stage4Visible));
                }
            }
        }

        private bool _stage5Visible;
        public bool Stage5Visible
        {
            get { return _stage5Visible; }
            set
            {
                if (_stage5Visible != value) // Исправлено условие с = на !=
                {
                    _stage5Visible = value;
                    OnPropertyChanged(nameof(Stage5Visible));
                }
            }
        }

        public ICommand OpenRegistryCommand { get; }
        public Action? CloseWindowAction { get; set; }

        public TestingViewModel()
        {
            OpenRegistryCommand = new RelayCommand(OpenRegistry);
        }

        private void OpenRegistry()
        {
            var registryWindow = new Registry();
            registryWindow.Show();
            CloseWindowAction?.Invoke();
        }
    

    // Добавьте класс RelayCommand, если его еще нет
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
