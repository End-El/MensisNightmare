using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using NAudio.Wave;
using System.Security.Cryptography.X509Certificates;

namespace VKR2025.ViewModel
{
    public class TestingViewModel : INotifyPropertyChanged
    {
        ResultModel result = new ResultModel();
        private Testing? _testingWindow;
        private string testingStage;

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
        public ICommand PlayAudioCommand { get; }
        //public ICommand CollectWordsCommand { get; }
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
            GoTestingCommand = new RelayCommand(Next);
            ConfirmCommand = new RelayCommand(Confirm);
            PlayAudioCommand = new RelayCommand(PlayAudio);
            //CollectWordsCommand = new RelayCommand(CollectWords);
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

        private string _infoTitle;
        public string InfoTitle
        {
            get => _infoTitle;
            set { _infoTitle = value; OnPropertyChanged(); }
        }

        private string _infoText;
        public string InfoText
        {
            get => _infoText;
            set { _infoText = value; OnPropertyChanged(); }
        }

        //Этап Лурия
        private bool _luriaHear;
        public bool LuriaHear
        {
            get => _luriaHear;
            set { _luriaHear = value; OnPropertyChanged(); }
        }

        private bool _luriaWrite;
        public bool LuriaWrite
        {
            get => _luriaWrite;
            set { _luriaWrite = value; OnPropertyChanged(); }
        }

        private string _luriaWords;
        public string LuriaWords
        {
            get => _luriaWords;
            set { _luriaWords = value; OnPropertyChanged(); }
        }

        private bool _showAudio;
        public bool ShowAudio
        {
            get => _showAudio;
            set { _showAudio = value; OnPropertyChanged(); }
        }

        //Этап Диджит Спан
        private string _spanDigit;
        public string DigitSpan
        {
            get => _spanDigit;
            set { _spanDigit = value; OnPropertyChanged(); }
        }

        private bool _showDigit;
        public bool ShowDigit
        {
            get => _showDigit;
            set { _showDigit = value; OnPropertyChanged(); }
        }

        private string _spanStep;
        public string SpanStep
        {
            get => _spanStep;
            set { _spanStep = value; OnPropertyChanged(); }
        }

        private bool _showStep;
        public bool ShowStep
        {
            get => _showStep;
            set { _showStep = value; OnPropertyChanged(); }
        }

        private bool _enableSpanText;
        public bool EnableSpanText
        {
            get => _enableSpanText;
            set { _enableSpanText = value; OnPropertyChanged(); }
        }

        private string _spanText;
        public string SpanText
        {
            get => _spanText;
            set
            {
                _spanText = value;
                OnPropertyChanged();

                SpanStep = $"Введено цифр {_spanText.Length}/{_currentLength}";
                OnPropertyChanged(nameof(SpanStep));

                // Проверка на полный ввод
                if (!_isChecking && _spanText.Length == _currentLength)
                {
                    _ = CheckInputAsync(); // запуск проверки
                }
            }
        }

        private bool _bernsteinImage;
        public bool BernsteinImage
        {
            get => _bernsteinImage;
            set { _bernsteinImage = value; OnPropertyChanged(); }
        }

        private string _bernsteinText;
        public string BernsteinText 
        {
            get => _bernsteinText;
            set { _bernsteinText = value; OnPropertyChanged(); }
        }

        private bool _bernsteinMatrix;
        public bool BernsteinMatrix
        {
            get => _bernsteinMatrix;
            set { _bernsteinMatrix = value; OnPropertyChanged(); }
        }

        public void Next()
        {
            switch (testingStage)
            {
                case "первый_начало":
                    GoStage1();
                break;

                case "второй_начало":
                    Stage2Begin();
                break;

                case "второй_тест":
                    GoStage2();
                break;

                case "третий_начало":
                    Stage3Begin();
                break;

                case "третий_тест":
                    GoStage3();
                    break;

                case "четвертый_начало":
                    Stage4Begin();
                    break;

                case "четвертый_тест":
                    GoStage4();
                    break;

                case "пятый_начало":
                    Stage5Begin();
                    break;

                case "пятый_тест":
                    GoStage5();
                break;

                case "тестирование":
                    Res1 = result.Stage1Result; // или вычисленное значение
                    Res2 = result.Stage2Result;
                    Res3 = result.Stage3Result;
                    Res4 = result.Stage4Result;
                    Res5 = result.Stage5Result;

                    // Вызов окна:
                    OpenResults();
                break;
            }
        }

        private void GenerateStimuli() //генерация стимулов
        {
            //Stimuli.Clear();
            char[] allowable = new[] //Алфавит без Ь, Ъ и Ё
            {
                'А','Б','В','Г','Д','Е','Ж','З','И','Й',
                'К','Л','М','Н','О','П','Р','С','Т','У',
                'Ф','Х','Ц','Ч','Ш','Щ','Э','Ю','Я'
            };
            var rand = new Random();

            for (int i = 0; i < 8; i++)
            {
                //Stimuli[i] = ((char)rand.Next('А', 'Я' + 1));
                Stimuli[i] = allowable[rand.Next(allowable.Length)];
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

        private async void GoStage1()
        {
            InstructionVisible = false;
            for (int i = 0; i < 2; i++) //<20
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
                int correct = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (Responses[j].Length == 1 &&
                        char.ToUpperInvariant(Responses[j][0]) == Stimuli[j])
                    {
                        correct++;
                    }
                }
                result.AddScore(correct);

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
            result.AverageScore();
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
            testingStage = "второй_начало";

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
            if (int.TryParse(Age, out int ageValue) && ageValue > 0 && !string.IsNullOrWhiteSpace(Name))
            {
                _testingWindow = new Testing();
                _testingWindow.DataContext = this;
                _testingWindow?.Show();
                CloseRegistryAction?.Invoke();

                //EndText = "Вы успешно завершили нулевой этап";
                InfoTitle = "Этап 1 из 4.\nТахистоскопический тест";
                InfoText = "Вы увидите серию из 20 кратковременных показов символов.\nПеред каждым показом на экране появится слово \"ВНИМАНИЕ!\"" +
                    " - это сигнал о начале попытки. Через 2 секунды после этого на короткое время появятся две строчки с буквами.\n\nВаша задача - воспроизвести все буквы, которые вы успели увидеть, " +
                    "в любом порядке.\n\nПожалуйста, старайтесь отвечать как можно точнее.\nНажмите \"Начать\", когда будете готовы.";
                SetStageVisibility(false, false, false, false, false, true, false, false);
                testingStage = "первый_начало";
            }
            else
            {
                var ownerWindow = GetOwnerWindow?.Invoke();
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пожалуйста, проверьте корректность заполнения полей.", ButtonEnum.Ok);
                box.ShowWindowDialogAsync(ownerWindow); //  показываем на текущем окне
                return;
            }
        }

        public void PlayAudio()
        {
            ShowAudio = false;
            var audioFile = new AudioFileReader(Path.Combine(AppContext.BaseDirectory, "Assets", "Luria.mp3"));
            var outputDevice = new WaveOutEvent();

            outputDevice.Init(audioFile);
            outputDevice.Play();

            // Обязательно выгружаем после проигрывания
            outputDevice.PlaybackStopped += (s, e) =>
            {
                outputDevice.Dispose();
                audioFile.Dispose();
                Confirm();
            };
        }

        //public void CollectWords()
        //{

        //}

        private void Stage2Begin()
        {
            InfoTitle = "Этап 2 из 4.\nТест на запоминание 10 слов Александра Лурии";
            InfoText = "Эксперимент состоит из четырёх частей.\n\nВ каждой части вам нужно будет прослушать аудиозапись,а затем написать в появившемся окне все слова, которые вы успели запомнить. " +
                "Слова нужно записывать через пробел, без запятых и других знаков разделения.\n\nПожалуйста, старайтесь отвечать как можно точнее.\nНажмите \"Начать\", когда будете готовы.";
            SetStageVisibility(false, false, false, false, false, true, false, false);
            testingStage = "второй_тест";
        }

        public async void GoStage2()
        {
            IReadOnlyList<string> luriaWords = new List<string>()
            { 
                "лес", "хлеб", "парашют", "океан", "пантограф",
                "брат", "скорость", "разность", "орхидея", "концерт"
            };
            List<string> luriaAnswers = new List<string>();

            SetStageVisibility(false, true, false, false, false, false, false, false);
            for (int i = 0; i < 1; i++) //<3
            {
                luriaAnswers.Clear();
                //отображаем элементы формы для прослушивания аудио записи
                ShowAudio = true;
                LuriaHear = true;
                LuriaWrite = false;

                //ждём, пока аудио запись проиграется (на кнопке сначала играет аудио, потом идёт подтверждение _trialCompletionSource)
                _trialCompletionSource = new TaskCompletionSource<bool>();
                await _trialCompletionSource.Task;

                //отображаем элементы формы для записи слов
                LuriaWords = "";
                LuriaHear = false;
                LuriaWrite = true;

                //ждём, пока пользователь введёт слова и нажмёт на кнопку
                _trialCompletionSource = new TaskCompletionSource<bool>();
                await _trialCompletionSource.Task;

                //собираем введенные слова в luriaAnswers
                luriaAnswers = LuriaWords.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
                
                //сравниваем, скок правильно
                int luriaResults = luriaAnswers.Intersect(luriaWords, StringComparer.OrdinalIgnoreCase).Count();

                //забиваем в модель
                result.LuriaScore(luriaResults);
            }
            luriaAnswers.Clear();
            //отображаем элементы формы для прослушивания аудио записи
            ShowAudio = true;
            LuriaHear = true;
            LuriaWrite = false;

            //ждём, пока аудио запись проиграется (на кнопке сначала играет аудио, потом идёт подтверждение _trialCompletionSource)
            _trialCompletionSource = new TaskCompletionSource<bool>();
            await _trialCompletionSource.Task;

            EndTitle = "Этап завершен";
            EndText = "Вы успешно завершили второй этап.\nНажмите \"Далее\" для перехода на следующий.";
            EndButton = "Далее";
            SetStageVisibility(
                s1: false,
                s2: false, s3: false, s4: false, s5: false,
                s6: false,
                s7: false,
                s8: true  // EndingVisible
            );
            testingStage = "третий_начало";
        }

        private void Stage3Begin()
        {
            InfoTitle = "Этап 3 из 4.\nТест на диапазон цифр";
            InfoText = "Эксперимент состоит из запоминания и воспроизведения числовой последовательности.\n\nВам будет посимвольно представлена числовая последовательность." +
                "Задача состоит в корректном воспроизведении заданной последовательности, используя поле в нижней части формы.\n\nЭтап завершится либо при совершении ошибки при воспроизведении последовательности, " +
                "либо при корректной записи последовательности длиной в 10 символов.\nНажмите \"Начать\", когда будете готовы.";
            SetStageVisibility(false, false, false, false, false, true, false, false);
            testingStage = "третий_тест";
        }

        private int _currentLength;
        private List<int> _digitSequence = new();
        private bool _isError = false;
        private bool _isChecking = false;
        public async void GoStage3()
        {
            // Начальные настройки
            _currentLength = 3;
            _isError = false;

            SetStageVisibility( //отображаем окно
                s1: false, s2: false,
                s3: true, s4: false, s5: false,
                s6: false, s7: false, s8: false
            );

            while (!_isError && _currentLength <= 4) //пока нет ошибки или не ввели всё правильно <=10
            {
                SpanText = "";
                GenerateDigitSequence(_currentLength); //создаём цепочку

                ShowDigit = true;
                EnableSpanText = false;
                ShowStep = false;

                // Показ цифр по одной
                foreach (var digit in _digitSequence)
                {
                    DigitSpan = digit.ToString();

                    await Task.Delay(800);

                    DigitSpan = "";

                    await Task.Delay(300);
                }

                // Подготовка к вводу
                SpanStep = $"Введено цифр 0/{_currentLength}";
                ShowDigit = false;
                EnableSpanText = true;
                ShowStep = true;

                // Ждём завершения ввода (в SpanText setter вызывается CheckInputAsync)
                _isChecking = false; 

                while (!_isChecking)
                await Task.Delay(100);
            }

            //DigitSpan = "Тест завершён";
            ShowDigit = true;
            EnableSpanText = false;
            ShowStep = false;
        }

        private void GenerateDigitSequence(int length)
        {
            var rand = new Random();
            _digitSequence.Clear(); //очищаем

            for (int i = 0; i < length; i++)
            {
                _digitSequence.Add(rand.Next(0, 10)); //рандомно заполняем
            }
        }

        private async Task CheckInputAsync()
        {
            EnableSpanText = false;
            ShowStep = false;

            string entered = SpanText;
            string expected = string.Join("", _digitSequence);

            await Task.Delay(500); // пауза перед переходом

            if (entered == expected)
            {
                _currentLength++;
                if (_currentLength > 4) //> 10
                {
                    EndTitle = "Этап завершен";
                    EndText = "Вы успешно завершили третий этап.\nНажмите \"Далее\" для перехода на следующий.";
                    EndButton = "Далее";
                    SetStageVisibility(
                        s1: false,
                        s2: false, s3: false, s4: false, s5: false,
                        s6: false,
                        s7: false,
                        s8: true  // EndingVisible
                    );
                    result.Stage3Result = _currentLength;
                    testingStage = "четвертый_начало";
                    return;
                }
            }
            else
            {
                // Покажем сообщение или ошибку
                DigitSpan = "Ошибка!";
                ShowDigit = true;
                OnPropertyChanged(nameof(DigitSpan));
                _isError = true;

                await Task.Delay(3000);
                EndTitle = "Этап завершен";
                EndText = "Вы завершили третий этап.\nНажмите \"Далее\" для перехода на следующий.";
                EndButton = "Далее";
                SetStageVisibility(
                    s1: false,
                    s2: false, s3: false, s4: false, s5: false,
                    s6: false,
                    s7: false,
                    s8: true  // EndingVisible
                );
                result.Stage3Result = _currentLength;
                testingStage = "четвертый_начало";
                return;
            }

            _isChecking = true;
        }

        private void Stage4Begin()
        {
            InfoTitle = "Этап 4 из 4.\nТест Бернштейна на запоминание фигур";
            InfoText = "Эксперимент состоит из запоминания и выбора геометрических фигур.\n\nВам на короткое время будет предъявлено изображение с геометрическими фигурами. " +
                "Нужно их запомнить. Затем появляется изображение, содержащее большее количество изображений. Нужно выбрать те, которые были предъявлены ранее." +
                "\nНажмите \"Начать\", когда будете готовы.";
            SetStageVisibility(false, false, false, false, false, true, false, false);
            testingStage = "четвертый_тест";
        }

        public async void GoStage4()
        {
            SetStageVisibility(false, false, false, true, false, false, false, false);
            WarningSize = 80;
            WarningText = "ВНИМАНИЕ!";
            WarningVisible = true;
            await Task.Delay(1000);

            WarningVisible = false;
            BernsteinImage = true;
            BernsteinText = "Запомните фигуры, изображенные ниже";
            BernsteinMatrix = false;
            InputVisible = false;
            await Task.Delay(1000);

            BernsteinImage = false;
            BernsteinText = "Выберите те фигуры, что успели запомнить, и кликните на них";
            BernsteinMatrix = true;
            InputVisible = true;

            _trialCompletionSource = new TaskCompletionSource<bool>();
            await _trialCompletionSource.Task;

            
            result.Stage4Result = Check1 + Check2 + Check3 + Check4 + Check5 + Check6;

            BernsteinImage = false;
            BernsteinMatrix = false;
            InputVisible = false;
            BernsteinText = "";
            EndTitle = "Тестирование завершено";
            EndText = "Вы успешно завершили четвертый этап.\nНажмите \"Посмотреть результаты\" для просмотра результатов.";
            EndButton = "Посмотреть результаты";
            SetStageVisibility(
                s1: false,
                s2: false, s3: false, s4: false, s5: false,
                s6: false,
                s7: false,
                s8: true  // EndingVisible
            );
            testingStage = "тестирование";
        }

        private void Stage5Begin()
        {
            InfoTitle = "Этап 5 из 5.\nТест \"Запомни и расставь\"";
            InfoText = "Эксперимент состоит из запоминания и выбора геометрических фигур.\n\nВам на короткое время будет предъявлено изображение с геометрическими фигурами. " +
                "Нужно их запомнить. Затем появляется изображение, содержащее большее количество изображений. Нужно выбрать те, которые были предъявлены ранее." +
                "\nНажмите \"Начать\", когда будете готовы.";
            SetStageVisibility(false, false, false, false, false, true, false, false);
            testingStage = "пятый_тест";
        }

        public async void GoStage5()
        {

        }

        private int _check1;
        public int Check1
        {
            get => _check1;
            set { _check1 = value; OnPropertyChanged(); }
        }
        private int _check2;
        public int Check2
        {
            get => _check2;
            set { _check2 = value; OnPropertyChanged(); }
        }
        private int _check3;
        public int Check3
        {
            get => _check3;
            set { _check3 = value; OnPropertyChanged(); }
        }
        private int _check4;
        public int Check4
        {
            get => _check4;
            set { _check4 = value; OnPropertyChanged(); }
        }
        private int _check5;
        public int Check5
        {
            get => _check5;
            set { _check5 = value; OnPropertyChanged(); }
        }
        private int _check6;
        public int Check6
        {
            get => _check6;
            set { _check6 = value; OnPropertyChanged(); }
        }

        public void OpenAbout()
        {
            var about = new About();
            about.Show();
            //CloseWindowAction.Invoke();
        }
        public void OpenResults()
        {
            var resultsWindow = new Results(this)
            {
                //DataContext = this // передаём текущую ViewModel
            };
            resultsWindow.Show();
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

        private string _age;
        public string Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        //private void Do1Stage()
        //{
        //    for (int i = 0; i < 19; i++)
        //    {

        //    }
        //}

        private int _res1;
        public int Res1
        {
            get => _res1;
            set { _res1 = value; OnPropertyChanged(); }
        }

        private int _res2;
        public int Res2
        {
            get => _res2;
            set { _res2 = value; OnPropertyChanged(); }
        }

        private int _res3;
        public int Res3
        {
            get => _res3;
            set { _res3 = value; OnPropertyChanged(); }
        }

        private int _res4;
        public int Res4
        {
            get => _res4;
            set { _res4 = value; OnPropertyChanged(); }
        }

        private int _res5;
        public int Res5
        {
            get => _res5;
            set { _res5 = value; OnPropertyChanged(); }
        }

        private int _resTotal;
        public int ResTotal
        {
            get => _resTotal;
            set { _resTotal = value; OnPropertyChanged(); }
        }

        private string _resDescription;
        public string ResDescription
        {
            get => _resDescription;
            set { _resDescription = value; OnPropertyChanged(); }
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

