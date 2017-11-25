using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TypingTool.Commands;

namespace TypingTool.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private Stopwatch _stopwatch = new Stopwatch();
        private string _timer = "00:00.00";
        public string Timer
        {
            get
            {
                return _timer;
            }
            set
            {
                this._timer = value;
                OnPropertyChanged(nameof(this.Timer));
            }
        }

        #region Commands
        private ICommand _startTimer;
        public ICommand StartTimer
        {
            get
            {
                return _startTimer ?? (_startTimer = new DelegateCommand((obj) =>
                {
                    _stopwatch.Start();
                    _dispatcherTimer.Start();
                }));
            }
        }

        private ICommand _stopTimer;
        public ICommand StopTimer
        {
            get
            {
                return _stopTimer ?? (_stopTimer = new DelegateCommand((obj) =>
                {
                    _stopwatch.Stop();
                    _dispatcherTimer.Stop();
                }));
            }
        }

        private ICommand _resetTimer;
        public ICommand ResetTimer
        {
            get
            {
                return _resetTimer ?? (_resetTimer = new DelegateCommand((obj) =>
                {
                    _dispatcherTimer.Tick -= _dispatcherTimer_Tick;
                    initializeTimer();
                    Timer = _stopwatch.Elapsed.TotalSeconds.ToString();
                }));
            }
        }
        #endregion Commands

        public StatisticsViewModel()
        {
            initializeTimer();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer = _stopwatch.Elapsed.TotalSeconds.ToString();
        }

        private void initializeTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _stopwatch = new Stopwatch();
        }
    }
}
