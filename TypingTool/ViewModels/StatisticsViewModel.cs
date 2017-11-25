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

        private ICommand _stopTimer;
        public ICommand StopTimer
        {
            get
            {
                return _stopTimer ?? (_stopTimer = new DelegateCommand((obj) =>
                {
                    _dispatcherTimer.Stop();
                }));
            }
        }

        public StatisticsViewModel()
        {
            _stopwatch.Start();
            Timer = _stopwatch.Elapsed.ToString();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _dispatcherTimer.Start();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer = _stopwatch.Elapsed.TotalSeconds.ToString(); // Format as you wish
        }
    }
}
