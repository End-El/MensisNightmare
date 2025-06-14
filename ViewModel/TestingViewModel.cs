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
using VKR2025.View;
using Tmds.DBus.Protocol;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Data.SqlTypes;
using System.Collections.ObjectModel;
using VKR2025.Model;

namespace VKR2025.ViewModel
{
    public class TestingViewModel : INotifyPropertyChanged
    {
        private Testing? _testingWindow;

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

        private bool _instructionVisible;
        public bool InstructionVisible
        {
            get { return _instructionVisible; }
            set
            {
                if (_instructionVisible != value) // Исправлено условие с = на !=
                {
                    _instructionVisible = value;
                    OnPropertyChanged(nameof(InstructionVisible));
                }
            }
        }

        private bool _warningVisible;
        public bool WarningVisible
        {
            get { return _warningVisible; }
            set
            {
                if (_warningVisible != value) // Исправлено условие с = на !=
                {
                    _warningVisible = value;
                    OnPropertyChanged(nameof(WarningVisible));
                }
            }
        }

        private bool _endingVisible;
        public bool EndingVisible
        {
            get { return _endingVisible; }
            set
            {
                if (_endingVisible != value) // Исправлено условие с = на !=
                {
                    _endingVisible = value;
                    OnPropertyChanged(nameof(EndingVisible));
                }
            }
        }

        public ICommand OpenRegistryCommand { get; }
        public ICommand OpenTestingCommand { get; }
        public ICommand OpenAboutCommand { get; }
        public ICommand OpenResultsCommand { get; }
        public ICommand GoTestingCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand NextCommand { get; }
        public Action? CloseMainWindowAction { get; set; }
        public Action? CloseAboutAction { get; set; }
        public Action? CloseResultsAction { get; set; }
        public Action? CloseRegistryAction { get; set; }

        public TestingViewModel()
        {
            GenerateInitialCollections();
            OpenRegistryCommand = new RelayCommand(OpenRegistry);
            OpenTestingCommand = new RelayCommand(OpenTesting);
            OpenAboutCommand = new RelayCommand(OpenAbout);
            OpenResultsCommand = new RelayCommand(OpenResults);
            GoTestingCommand = new RelayCommand(GoStage1);
            ConfirmCommand = new RelayCommand(Confirm);
            NextCommand = new RelayCommand(Next);
        }

        private void GenerateInitialCollections()
        {
            Stimuli.Clear();
            for (int i = 0; i < 8; i++)
            {
                Stimuli.Add('А'); // или любой другой фиксированный символ
                Responses.Add("");
            }

        }
        private void SetStageVisibility(bool s1, bool s2, bool s3, bool s4, bool s5, bool s6, bool s7, bool s8)
        {
            Stage1Visible = s1;
            Stage2Visible = s2;
            Stage3Visible = s3;
            Stage4Visible = s4;
            Stage5Visible = s5;
            InstructionVisible = s6;
            WarningVisible = s7;
            EndingVisible = s8;
        }

        public void OpenRegistry()
        {
            var registryWindow = new Registry();
            registryWindow.Show();
            CloseMainWindowAction?.Invoke();
        }

        private TaskCompletionSource<bool>? _trialCompletionSource;
        public void Confirm()
        {
            _trialCompletionSource?.TrySetResult(true);
        }

        public ObservableCollection<char> Stimuli { get; } = new();
        public ObservableCollection<string> Responses { get; } = new();

        private bool _stimuliVisible; //отображение полей для предъявления стимулов
        public bool StimuliVisible
        {
            get => _stimuliVisible;
            set { _stimuliVisible = value; OnPropertyChanged(); }
        }

        private bool _inputVisible; //отображение полей для ввода
        public bool InputVisible
        {
            get => _inputVisible;
            set { _inputVisible = value; OnPropertyChanged(); }
        }

        private string _warningText; //вспомогательный текст
        public string WarningText
        {
            get => _warningText;
            set { _warningText = value; OnPropertyChanged(); }
        }

        private int _warningSize; //вспомогательный текст
        public int WarningSize
        {
            get => _warningSize;
            set { _warningSize = value; OnPropertyChanged(); }
        }

        private string _endTitle;
        public string EndTitle
        {
            get => _endTitle;
            set { _endTitle = value; OnPropertyChanged(); }
        }

        private string _endText;
        public string EndText
        {
            get => _endText;
            set { _endText = value; OnPropertyChanged(); }
        }

        private string _endButton;
        public string EndButton
        {
            get => _endButton;
            set { _endButton = value; OnPropertyChanged(); }
        }

        public void Next()
        {
            var parts = EndText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var word = parts[3].ToLower();
            switch (word)
            {
                case "первый":
                    //переход ко 2
                break;

                case "второй":
                    //переход к 3
                break;

                case "третий":
                    //переход к 4    
                break;

                case "четвертый":
                    //переход к 5
                break;

                case "тестирование":
                    //показ результатов
                break;
            }
        }

        private void GenerateStimuli() //генерация стимулов
        {
            //Stimuli.Clear();
            var rand = new Random();

            for (int i = 0; i < 8; i++)
            {
                Stimuli[i] = ((char)rand.Next('А', 'Я' + 1));
            }
        }

        private void ResetResponses() //Обнуляем введеные буквы для следующей итерации
        {
            //Responses.Clear();
            for (int i = 0; i < 8; i++)
            {
                Responses[i] = "";
            }
        }

        public async void GoStage1()
        {
            InstructionVisible = false;
            for (int i = 0; i < 4; i++)
            {
                // Показ "ВНИМАНИЕ!" перед стимулом
                WarningSize = 80;
                WarningText = "ВНИМАНИЕ!";
                WarningVisible = true;

                await Task.Delay(1000);

                // Переход к этапу стимулов
                SetStageVisibility(
                    s1: true,  // Включаем Stage1
                    s2: false, s3: false, s4: false, s5: false, //отключаем остальные этапы
                    s6: false, // Instruction
                    s7: false, // Warning
                    s8: false  // Ending
                );

                //await RunTrialAsync(); // ⬅ вставляем циклы сюда
                InputVisible = false;
                StimuliVisible = false;

                await Task.Delay(1000);

                GenerateStimuli();
                ResetResponses();

                StimuliVisible = true;
                await Task.Delay(500);
                StimuliVisible = false;

                InputVisible = true;
                _trialCompletionSource = new TaskCompletionSource<bool>();
                await _trialCompletionSource.Task;

                // Подсчёт
                ResultModel resultModel = new ResultModel();
                int correct = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (Responses[j].Length == 1 &&
                        char.ToUpperInvariant(Responses[j][0]) == Stimuli[j])
                    {
                        correct++;
                    }
                }
                resultModel.AddScore(correct);

                InputVisible = false;

                // Показ результата
                WarningSize = 60;
                WarningText = $"Верных ответов: {correct}/8";
                WarningVisible = true;

                await Task.Delay(1500); // Показ на 1.5 сек

                WarningVisible = false;
                WarningSize = 80;
                WarningText = "ВНИМАНИЕ!"; // Сброс текста

                await Task.Delay(300); // небольшая пауза перед следующей итерацией
            }

            // Всё! Показ финальной надписи
            EndTitle = "Этап завершен";
            EndText = "Вы успешно завершили первый этап.\nНажмите \"Далее\" для перехода на следующий.";
            EndButton = "Далее";
            SetStageVisibility(
                s1: false,
                s2: false, s3: false, s4: false, s5: false,
                s6: false,
                s7: false,
                s8: true  // EndingVisible
            );

        }

        public async Task RunTrialAsync()
        {
            InputVisible = false;
            StimuliVisible = false;

            await Task.Delay(1000);

            GenerateStimuli();
            ResetResponses();

            StimuliVisible = true;
            await Task.Delay(250);
            StimuliVisible = false;

            InputVisible = true;

            // Ждём пользовательский ввод вручную
        }

        public Func<Window?>? GetOwnerWindow { get; set; }
        public void OpenTesting()
        {
            if (Age > 0 && !string.IsNullOrWhiteSpace(Name))
            {
                _testingWindow = new Testing();
                _testingWindow.DataContext = this;
                _testingWindow?.Show();
                CloseRegistryAction?.Invoke();
                SetStageVisibility(false, false, false, false, false, true, false, false);
            }
            else
            {
                var ownerWindow = GetOwnerWindow?.Invoke();
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пожалуйста, заполните все поля.", ButtonEnum.Ok);
                box.ShowWindowDialogAsync(ownerWindow); //  показываем на текущем окне
                return;
            }
        }

        public void OpenAbout()
        {
            var about = new About();
            about.Show();
            //CloseWindowAction.Invoke();
        }
        public void OpenResults()
        {
            var res = new Results();
            res.Show();
        }

        public void CloseAbout()
        {
            CloseAboutAction.Invoke();
        }

        public void CloseResults()
        {
            CloseResultsAction.Invoke();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        private void Do1Stage()
        {
            for (int i = 0; i < 19; i++)
            {

            }
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
} //Эксперимент состоит из двух частей.&#x0a;В каждой части вы увидите серию из 20 кратковременных показов символов.&#x0a;Перед каждым показом на экране появится слово &quot;ВНИМАНИЕ!&quot; - это сигнал о начале попытки. Через 2 секунды после этого на короткое время появятся две строчки с буквами.&#x0a;&#x0a;Ваша задача - воспроизвести все буквы, которые вы успели увидеть, в любом порядке.&#x0a;&#x0a;Пожалуйста, старайтесь отвечать как можно точнее.&#x0a;Нажмите &quot;Начать&quot;, когда будете готовы.

