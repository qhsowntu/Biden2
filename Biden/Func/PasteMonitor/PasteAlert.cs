using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class PasteAlert : TextBox
    {
        private const int WM_PASTE = 0x0302;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != WM_PASTE)
            {
                // Handle all other messages normally
                base.WndProc(ref m);
            }
            else
            {
                // Some simplified example code that complete replaces the
                // text box content only if the clipboard contains a valid double.
                // I'll leave improvement of this behavior as an exercise :)
                double value;
                if (double.TryParse(Clipboard.GetText(), out value))
                {
                    Text = value.ToString();
                }
            }
        }
    }
}
