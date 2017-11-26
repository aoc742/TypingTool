using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TypingTool.Commands;

namespace TypingTool.ViewModels
{
    public class InputViewModel : BaseViewModel
    {
        private ParseText _fullText;
         
        private string _quote = String.Empty;
        public string Quote
        {
            get
            {
                return _quote;
            }
            set
            {
                this._quote = value;
                _fullText = new ParseText(value);
                OnPropertyChanged(nameof(this.Quote));
            }
        }

        private bool _typingEnabled = true;
        public bool TypingEnabled
        {
            get
            {
                return _typingEnabled;
            }
            set
            {
                this._typingEnabled = value;
                OnPropertyChanged(nameof(TypingEnabled));
            }
        }

        private string _progressText = String.Empty;
        private string _text = String.Empty; // current text field text (current word)
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                // Start timer
                if (String.IsNullOrWhiteSpace(_text) && !String.IsNullOrWhiteSpace(value))
                {
                    StatisticsViewModel.StartTimer.Execute(null);
                }
                // Stop timer
                if ((_progressText + value) == _quote && !String.IsNullOrWhiteSpace(value))
                {
                    StatisticsViewModel.StopTimer.Execute(null);
                    StatisticsViewModel.WordsPerMinute = ((double)Quote.Length / 5.0) * (60 / StatisticsViewModel.GetTimeInSeconds());
                    Times.Add(new TimeResults(StatisticsViewModel.GetTimeInSeconds(), StatisticsViewModel.WordsPerMinute));
                    _progressText = "";
                    TypingEnabled = false;
                    _fullText.GetNextWord(); // resets ParseText
                }

                // Clear Text when value reaches current ParseText Iteration
                if (value == (_fullText.GetCurrent() + " "))
                {
                    _progressText += value;
                    value = "";
                    _fullText.GetNextWord();
                }

                this._text = value;
                OnPropertyChanged(nameof(this.Text));
            }
        }

        private ObservableCollection<TimeResults> _times = new ObservableCollection<TimeResults>();
        public ObservableCollection<TimeResults> Times
        {
            get
            {
                return _times;
            }
            set
            {
                this._times = value;
                OnPropertyChanged(nameof(this.Times));
            }
        }

        public StatisticsViewModel StatisticsViewModel { get; set; } = new StatisticsViewModel();

        private ICommand _reset;
        public ICommand Reset
        {
            get
            {
                return _reset ?? (_reset = new DelegateCommand((obj) =>
                {
                    Text = "";
                    _progressText = "";
                    TypingEnabled = true;
                    _fullText.Reset();
                    StatisticsViewModel.WordsPerMinute = 0;
                    StatisticsViewModel.ResetTimer.Execute(null);
                }));
            }
        }

        public InputViewModel(string typingQuote)
        {
            this.Quote = typingQuote ?? String.Empty;
        }
        public InputViewModel()
        {
        }
    }

    public class TimeResults
    {
        public double WordsPerMinute { get; set; }
        public double Time { get; set; }

        public TimeResults(double time, double wordsPerMinute)
        {
            Time = time;
            WordsPerMinute = wordsPerMinute;
        }
    }

    public class ParseText
    {
        private string _fullText = String.Empty;
        private string _currentIteration;
        private string[] _parsedTextArray;
        private int _index = 0;


        public ParseText(string fullText)
        {
            _fullText = fullText ?? String.Empty;

            _parsedTextArray = _fullText.Split();
            GetNextWord(); // Do once in constructor to initialize _currentIteration
        }

        public void Reset()
        {
            _index = 0;
            GetNextWord();
        }

        public void GetNextWord()
        {
            if (_index >= _parsedTextArray.Length)
            {
                _index = 0; // reset index
                _currentIteration = "";
            }
            int count = _index;
            _index++;
            _currentIteration = _parsedTextArray[count];
        }

        public string GetCurrent()
        {
            return _currentIteration;
        }

        public string GetFullText()
        {
            return _fullText;
        }
    }
}
