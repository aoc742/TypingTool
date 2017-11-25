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
                OnPropertyChanged(nameof(this.Quote));
            }
        }

        private string _text = String.Empty;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(_text) && !String.IsNullOrWhiteSpace(value))
                {
                    StatisticsViewModel.StartTimer.Execute(null);
                }
                if (value == _quote && !String.IsNullOrWhiteSpace(value))
                {
                    StatisticsViewModel.StopTimer.Execute(null);
                    StatisticsViewModel.WordsPerMinute = (Quote.Length / 5) * (60 / StatisticsViewModel.GetTimeInSeconds());
                    Times.Add(new TimeResults(StatisticsViewModel.GetTimeInSeconds(), StatisticsViewModel.WordsPerMinute));
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
}
