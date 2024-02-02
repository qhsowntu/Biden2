using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Biden.Func.PasteMonitor
{
    class TextCompositionManager : System.Windows.Threading.DispatcherObject
    {
        public TextCompositionManager()
        {

        }


        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                //var composition = new TextComposition(InputManager.Current, this, text);
                //TextCompositionManager.StartComposition(composition);
            }

            e.CancelCommand();
        }

    }
}
