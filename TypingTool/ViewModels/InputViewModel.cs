using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                this._text = value;
                OnPropertyChanged(nameof(this._text));
            }
        }

        public InputViewModel(string typingQuote)
        {
            this.Quote = typingQuote ?? String.Empty;
        }
        public InputViewModel()
        {
            Quote = "Paste a quote to type here...";
            Text = "Begin typing here...";
        }
    }
}
