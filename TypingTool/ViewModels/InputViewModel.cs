using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingTool.ViewModels
{
    public class InputViewModel : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                this._text = value;
                OnPropertyChanged(nameof(this._text));
            }
        }

        public InputViewModel()
        {
            Text = "Sample text";
        }
    }
}
