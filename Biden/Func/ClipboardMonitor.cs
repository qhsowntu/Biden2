using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class ClipboardMonitor
    {

        private string lastFormat;
        private object lastData;

        private NotificationForm notiClip;

        public ClipboardMonitor()
        {
            notiClip = new NotificationForm();
        }
        public ClipboardMonitor(Macro macro)
        {
            notiClip = new NotificationForm();
        }

        public string detectChange()
        {
            return "";
            string res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {


                        IDataObject iData = Clipboard.GetDataObject();


                        if (iData == null)
                        {
                            return;
                        }

                        string format = null;

                        if (Clipboard.ContainsText())
                        {
                            format = DataFormats.Text;
                        }
                        else if (Clipboard.ContainsImage())
                        {
                            format = DataFormats.Bitmap;
                        }

                        object data = iData.GetData(format.ToString());

                        if (data == null || format == null)
                            return;


                        if (format + "" != lastFormat + "" || data != lastData)
                        {
                            string tempStr = "Changed!\n\n1." + format + "\n2." + lastFormat + "\n\n3." + data + "\n4." + lastData;
                            MessageBox.Show(tempStr, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                            lastFormat = format;
                            lastData = data;
                            if ((format + "").Contains("Text") == true)
                            {
                                res = "Text";
                            }
                            else if ((format + "").Contains("Image") == true)
                            {
                                res = "Image";
                            }
                            else
                            {
                                res = "None";
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                        MessageBox.Show(ex + "");
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }


        public string LastFormat { get => lastFormat; set => lastFormat = value; }
        public object LastData { get => lastData; set => lastData = value; }



        /// <summary>
        /// Occurs when the contents of the clipboard is updated.
        /// </summary>
        public static event EventHandler ClipboardUpdate;

        private static NotificationForm _form = new NotificationForm();

        /// <summary>
        /// Raises the <see cref="ClipboardUpdate"/> event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        private static void OnClipboardUpdate(EventArgs e)
        {
            var handler = ClipboardUpdate;
            if (handler != null)
            {
                handler(null, e);
            }
        }
        private class NotificationForm : Form
        {
            public NotificationForm()
            {
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE && Macro.IsRunning == false && Macro.getInstance.Flag1 == false
                    && Macro.getInstance.ModeOn3 == true && ModelFunc3.getInstance.TheSelectedRule == ModelFunc3.getInstance.Source.ElementAt(2) && Macro.clipBoardMonitorFlag)
                {
                    if (Clipboard.ContainsText())
                    {
                        //MessageBox.Show("1", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        Macro.getInstance.Flag1 = true;
                    }
                    else if (Clipboard.ContainsImage())
                    {
                        //MessageBox.Show("2", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        Macro.getInstance.Flag1 = true;
                    }
                    OnClipboardUpdate(null);
                    //string tempStr = "Updated!";
                    //MessageBox.Show(tempStr, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                }
                if (Macro.clipBoardMonitorFlag == false)
                {
                    Macro.clipBoardMonitorFlag = true;
                }
                base.WndProc(ref m);
            }
        }

    }
    internal static class NativeMethods
    {
        // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        // See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
        // See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
