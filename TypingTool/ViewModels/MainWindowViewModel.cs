using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingTool.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public InputViewModel InputViewModel { get; set; } = new InputViewModel();
    }
}
