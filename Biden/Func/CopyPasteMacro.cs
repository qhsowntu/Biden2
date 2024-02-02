using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices; //required for dll import
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class CopyPasteMacro
    {

        private static bool flag1 = false;
        private static bool flag2 = false;
        private static bool flag3 = false;
        private static bool flag4 = false;
        private static bool flag5 = false;
        private static bool flag6 = false;

        private static bool isRunning = false;

        public static bool pasteModeOn = false;

        public bool removeBlankFlag = false;

        private static string key1 = "";
        private static string key2 = "";


        private static List<String> list;
        private static List<String> parameterList;

        //public static Bitmap screenPixel = new Bitmap(500, 200, PixelFormat.Format32bppArgb);
        public static Bitmap screenPixel = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        public static Color color;

        public CopyPasteMacro()
        {
            list = new List<String>();
            parameterList = new List<String>();
        }

        private static class API
        {

            //dll imports for hooking and unhooking and sending events trough hook hierarchy

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(
                int idHook,
                HookDel lpfn,
                IntPtr hMod,
                uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(
                IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr CallNextHookEx(
                IntPtr hhk,
                int nCode,
                IntPtr
                wParam,
                IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(
                string lpModuleName);

            // read the Color of a Screen Pixel
            [DllImport("user32.dll")]
            static extern bool GetCursorPos(ref Point lpPoint);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetWindowDC(IntPtr window);
            [DllImport("gdi32.dll", SetLastError = true)]
            public static extern uint GetPixel(IntPtr dc, int x, int y);
            [DllImport("user32.dll", SetLastError = true)]
            public static extern int ReleaseDC(IntPtr window, IntPtr dc);

            // get the mouse position
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int X;
                public int Y;

                public static implicit operator Point(POINT point)
                {
                    return new Point(point.X, point.Y);
                }
            }

            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(out POINT lpPoint);
            //[DllImport("user32.dll")]
            //public static extern bool GetCursorPos2(ref Point lpPoint);


        }

        public enum VK
        {
            //Keycodes may be found on many internet sites.
            //Some keys are commented feel free to uncomment them, explanations are provided for uncommon ones ;)

            VK_LBUTTON = 0X01, //Left mouse
            VK_RBUTTON = 0X02, //Right mouse
            //VK_CANCEL       = 0X03,
            VK_MBUTTON = 0X04,
            VK_BACK = 0X08, //Backspace
            VK_TAB = 0X09,
            //VK_CLEAR        = 0X0C,
            VK_RETURN = 0X0D, //Enter
            VK_SHIFT = 0X10,
            VK_CONTROL = 0X11, //CTRL
            //VK_MENU         = 0X12,
            VK_PAUSE = 0X13,
            VK_CAPITAL = 0X14, //Caps-Lock
            VK_ESCAPE = 0X1B,
            VK_SPACE = 0X20,
            VK_PRIOR = 0X21, //Page-Up
            VK_NEXT = 0X22, //Page-Down
            VK_END = 0X23,
            VK_HOME = 0X24,
            VK_LEFT = 0X25,
            VK_UP = 0X26,
            VK_RIGHT = 0X27,
            VK_DOWN = 0X28,
            //VK_SELECT       = 0X29,
            //VK_PRINT        = 0X2A,
            //VK_EXECUTE      = 0X2B,
            VK_SNAPSHOT = 0X2C, //Print Screen
            VK_INSERT = 0X2D,
            VK_DELETE = 0X2E,
            //VK_HELP         = 0X2F,

            VK_0 = 0X30,
            VK_1 = 0X31,
            VK_2 = 0X32,
            VK_3 = 0X33,
            VK_4 = 0X34,
            VK_5 = 0X35,
            VK_6 = 0X36,
            VK_7 = 0X37,
            VK_8 = 0X38,
            VK_9 = 0X39,

            VK_A = 0X41,
            VK_B = 0X42,
            VK_C = 0X43,
            VK_D = 0X44,
            VK_E = 0X45,
            VK_F = 0X46,
            VK_G = 0X47,
            VK_H = 0X48,
            VK_I = 0X49,
            VK_J = 0X4A,
            VK_K = 0X4B,
            VK_L = 0X4C,
            VK_M = 0X4D,
            VK_N = 0X4E,
            VK_O = 0X4F,
            VK_P = 0X50,
            VK_Q = 0X51,
            VK_R = 0X52,
            VK_S = 0X53,
            VK_T = 0X54,
            VK_U = 0X55,
            VK_V = 0X56,
            VK_W = 0X57,
            VK_X = 0X58,
            VK_Y = 0X59,
            VK_Z = 0X5A,

            VK_NUMPAD0 = 0X60,
            VK_NUMPAD1 = 0X61,
            VK_NUMPAD2 = 0X62,
            VK_NUMPAD3 = 0X63,
            VK_NUMPAD4 = 0X64,
            VK_NUMPAD5 = 0X65,
            VK_NUMPAD6 = 0X66,
            VK_NUMPAD7 = 0X67,
            VK_NUMPAD8 = 0X68,
            VK_NUMPAD9 = 0X69,

            VK_SEPERATOR = 0X6C, // | (shift + backslash)
            VK_SUBTRACT = 0X6D, // -
            VK_DECIMAL = 0X6E, // .
            VK_DIVIDE = 0X6F, // /

            VK_F1 = 0X70,
            VK_F2 = 0X71,
            VK_F3 = 0X72,
            VK_F4 = 0X73,
            VK_F5 = 0X74,
            VK_F6 = 0X75,
            VK_F7 = 0X76,
            VK_F8 = 0X77,
            VK_F9 = 0X78,
            VK_F10 = 0X79,
            VK_F11 = 0X7A,
            VK_F12 = 0X7B,
            //and for the 8 people in the world who do, I think they can live without using them

            VK_NUMLOCK = 0X90,
            VK_SCROLL = 0X91, //Scroll-Lock
            VK_LSHIFT = 0XA0,
            VK_RSHIFT = 0XA1,
            VK_LCONTROL = 0XA2,
            VK_RCONTROL = 0XA3,
            //VK_LMENU        = 0XA4,
            //VK_RMENU        = 0XA5,
            //VK_PLAY         = 0XFA,
            //VK_ZOOM         = 0XFB
        } //keycodes

        //There are detailed explanations for these functions on MSDNAA and implementations.
        public delegate IntPtr HookDel(
            int nCode,
            IntPtr wParam,
            IntPtr lParam);

        public delegate void KeyHandler(
            IntPtr wParam,
            IntPtr lParam);

        private static IntPtr hhk = IntPtr.Zero;
        private static HookDel hd;
        private static KeyHandler kh;

        //Creation of the hook
        public static void CreateHook(KeyHandler _kh)
        {
            Process _this = Process.GetCurrentProcess();
            ProcessModule mod = _this.MainModule;

            hd = HookFunc;
            kh = _kh;

            //13 is the parameter specifying that we're gonna do a low-level keyboard hook
            hhk = API.SetWindowsHookEx(13, hd, API.GetModuleHandle(mod.ModuleName), 0);

            //MessageBox.Show(Marshal.GetLastWin32Error().ToString()); //for debugging
            //Note that this could be a Console.WriteLine(), as well. I just happened
            //to be debugging this in a Windows Application
        }

        public static bool DestroyHook()
        {
            //to be called when we're done with the hook

            return API.UnhookWindowsHookEx(hhk);
        }

        //called when key is active
        private static IntPtr HookFunc(
            int nCode,
            IntPtr wParam,
            IntPtr lParam)
        {
            int iwParam = wParam.ToInt32();
            //depending on what you want to detect you can either detect keypressed or keyrealased also with  a bit tweaking keyclicked.
            if (nCode >= 0 &&
                (iwParam == 0x100 ||
                iwParam == 0x104)) //0x100 = WM_KEYDOWN, 0x104 = WM_SYSKEYDOWN
                kh(wParam, lParam);
            return API.CallNextHookEx(hhk, nCode, wParam, lParam);
        }

        private static void KeyReaderr(IntPtr wParam, IntPtr lParam)
        {
            int key = Marshal.ReadInt32(lParam);

            Macro.VK vk = (Macro.VK)key;


            String temp = "";

            #region

            switch (vk)
            {
                case Macro.VK.VK_F1:
                    //temp = "&lt;-F1-&gt;";
                    temp = "{F1}";
                    break;
                case Macro.VK.VK_F2:
                    //temp = "&lt;-F2-&gt;";
                    temp = "{F2}";
                    break;
                case Macro.VK.VK_F3:
                    //temp = "&lt;-F3-&gt;";
                    temp = "{F3}";
                    break;
                case Macro.VK.VK_F4:
                    //temp = "&lt;-F4-&gt;";
                    temp = "{F4}";
                    break;
                case Macro.VK.VK_F5:
                    //temp = "&lt;-F5-&gt;";
                    temp = "{F5}";
                    break;
                case Macro.VK.VK_F6:
                    //temp = "&lt;-F6-&gt;";
                    temp = "{F6}";
                    break;
                case Macro.VK.VK_F7:
                    //temp = "&lt;-F7-&gt;";
                    temp = "{F7}";
                    break;
                case Macro.VK.VK_F8:
                    //temp = "&lt;-F8-&gt;";
                    temp = "{F8}";
                    break;
                case Macro.VK.VK_F9:
                    //temp = "&lt;-F9-&gt;";
                    temp = "{F9}";
                    break;
                case Macro.VK.VK_F10:
                    //temp = "&lt;-F10-&gt;";
                    temp = "{F10}";
                    break;
                case Macro.VK.VK_F11:
                    //temp = "&lt;-F11-&gt;";
                    temp = "{F11}";
                    break;
                case Macro.VK.VK_F12:
                    //temp = "&lt;-F12-&gt;";
                    temp = "{F12}";
                    break;
                case Macro.VK.VK_NUMLOCK:
                    //temp = "&lt;-numlock-&gt;";
                    temp = "{NUMLOCK}";
                    break;
                case Macro.VK.VK_SCROLL:
                    //temp = "&lt;-scroll&gt;";
                    temp = "{SCROLLLOCK}";
                    break;
                case Macro.VK.VK_LSHIFT:
                    //temp = "&lt;-left shift-&gt;";
                    temp = "{+}";
                    break;
                case Macro.VK.VK_RSHIFT:
                    //temp = "&lt;-right shift-&gt;";
                    temp = "{+}";
                    break;
                case Macro.VK.VK_LCONTROL:
                    //temp = "&lt;-left control-&gt;";
                    temp = "{CTRL}";
                    break;
                case Macro.VK.VK_RCONTROL:
                    //temp = "&lt;-right control-&gt;";
                    temp = "{CTRL}";
                    break;
                case Macro.VK.VK_SEPERATOR:
                    temp = "|";
                    break;
                case Macro.VK.VK_SUBTRACT:
                    temp = "-";
                    break;
                case Macro.VK.VK_DECIMAL:
                    temp = ".";
                    break;
                case Macro.VK.VK_DIVIDE:
                    temp = "/";
                    break;
                case Macro.VK.VK_NUMPAD0:
                    temp = "0";
                    break;
                case Macro.VK.VK_NUMPAD1:
                    temp = "1";
                    break;
                case Macro.VK.VK_NUMPAD2:
                    temp = "2";
                    break;
                case Macro.VK.VK_NUMPAD3:
                    temp = "3";
                    break;
                case Macro.VK.VK_NUMPAD4:
                    temp = "4";
                    break;
                case Macro.VK.VK_NUMPAD5:
                    temp = "5";
                    break;
                case Macro.VK.VK_NUMPAD6:
                    temp = "6";
                    break;
                case Macro.VK.VK_NUMPAD7:
                    temp = "7";
                    break;
                case Macro.VK.VK_NUMPAD8:
                    temp = "8";
                    break;
                case Macro.VK.VK_NUMPAD9:
                    temp = "9";
                    break;
                case Macro.VK.VK_Q:
                    temp = "q";
                    break;
                case Macro.VK.VK_W:
                    temp = "w";
                    break;
                case Macro.VK.VK_E:
                    temp = "e";
                    break;
                case Macro.VK.VK_R:
                    temp = "r";
                    break;
                case Macro.VK.VK_T:
                    temp = "t";
                    break;
                case Macro.VK.VK_Y:
                    temp = "y";
                    break;
                case Macro.VK.VK_U:
                    temp = "u";
                    break;
                case Macro.VK.VK_I:
                    temp = "i";
                    break;
                case Macro.VK.VK_O:
                    temp = "o";
                    break;
                case Macro.VK.VK_P:
                    temp = "p";
                    break;
                case Macro.VK.VK_A:
                    temp = "a";
                    break;
                case Macro.VK.VK_S:
                    temp = "s";
                    break;
                case Macro.VK.VK_D:
                    temp = "d";
                    break;
                case Macro.VK.VK_F:
                    temp = "f";
                    break;
                case Macro.VK.VK_G:
                    temp = "g";
                    break;
                case Macro.VK.VK_H:
                    temp = "h";
                    break;
                case Macro.VK.VK_J:
                    temp = "j";
                    break;
                case Macro.VK.VK_K:
                    temp = "k";
                    break;
                case Macro.VK.VK_L:
                    temp = "l";
                    break;
                case Macro.VK.VK_Z:
                    temp = "z";
                    break;
                case Macro.VK.VK_X:
                    temp = "x";
                    break;
                case Macro.VK.VK_C:
                    temp = "c";
                    break;
                case Macro.VK.VK_V:
                    temp = "v";
                    break;
                case Macro.VK.VK_B:
                    temp = "b";
                    break;
                case Macro.VK.VK_N:
                    temp = "n";
                    break;
                case Macro.VK.VK_M:
                    temp = "m";
                    break;
                case Macro.VK.VK_0:
                    temp = "0";
                    break;
                case Macro.VK.VK_1:
                    temp = "1";
                    break;
                case Macro.VK.VK_2:
                    temp = "2";
                    break;
                case Macro.VK.VK_3:
                    temp = "3";
                    break;
                case Macro.VK.VK_4:
                    temp = "4";
                    break;
                case Macro.VK.VK_5:
                    temp = "5";
                    break;
                case Macro.VK.VK_6:
                    temp = "6";
                    break;
                case Macro.VK.VK_7:
                    temp = "7";
                    break;
                case Macro.VK.VK_8:
                    temp = "8";
                    break;
                case Macro.VK.VK_9:
                    temp = "9";
                    break;
                case Macro.VK.VK_SNAPSHOT:
                    //temp = "&lt;-print screen-&gt;";
                    temp = "{PRTSC}";
                    break;
                case Macro.VK.VK_INSERT:
                    //temp = "&lt;-insert-&gt;";
                    temp = "{INSERT}";
                    break;
                case Macro.VK.VK_DELETE:
                    //temp = "&lt;-delete-&gt;";
                    temp = "{DELETE}";
                    break;
                case Macro.VK.VK_BACK:
                    //temp = "&lt;-backspace-&gt;";
                    temp = "{BACKSPACE}";
                    break;
                case Macro.VK.VK_TAB:
                    //temp = "&lt;-tab-&gt;";
                    temp = "{TAB}";
                    break;
                case Macro.VK.VK_RETURN:
                    //temp = "&lt;-enter-&gt;" + Environment.NewLine;
                    temp = "{ENTER}";
                    break;
                case Macro.VK.VK_PAUSE:
                    //temp = "&lt;-pause-&gt;";
                    temp = "{PAUSE}";
                    break;
                case Macro.VK.VK_CAPITAL:
                    //temp = "&lt;-caps lock-&gt;";
                    temp = "{CAPSLOCK}";
                    break;
                case Macro.VK.VK_ESCAPE:
                    //temp = "&lt;-esc-&gt;";
                    temp = "{ESC}";
                    break;
                case Macro.VK.VK_SPACE:
                    //temp = "&lt;-space-&gt;";
                    temp = "{SPACE}";
                    break;
                case Macro.VK.VK_PRIOR:
                    //temp = "&lt;-page up-&gt;";
                    temp = "{PGUP}";
                    break;
                case Macro.VK.VK_NEXT:
                    //temp = "&lt;-page down-&gt;";
                    temp = "{PGDN}";
                    break;
                case Macro.VK.VK_END:
                    //temp = "&lt;-end-&gt;";
                    temp = "{END}";
                    break;
                case Macro.VK.VK_HOME:
                    //temp = "&lt;-home-&gt;";
                    temp = "{HOME}";
                    break;
                case Macro.VK.VK_LEFT:
                    //temp = "&lt;-arrow left-&gt;";
                    temp = "{LEFT}";
                    break;
                case Macro.VK.VK_UP:
                    //temp = "&lt;-arrow up-&gt;";
                    temp = "{UP}";
                    break;
                case Macro.VK.VK_RIGHT:
                    //temp = "&lt;-arrow right-&gt;";
                    temp = "{RIGHT}";
                    break;
                case Macro.VK.VK_DOWN:
                    //temp = "&lt;-arrow down-&gt;";
                    temp = "{DOWN}";
                    break;
                default: break;
            }

            #endregion

            key1 = vk + "";
            key2 = temp + "";


            counter();

            /*if (form.recordFlag == true)
            {
                list.Add(key2);
            }*/
            send((Keys)key);

        }

        private static void counter()
        {
        }

        private static void send(Keys tempKey)//Keys tempKey, IntPtr wParam, IntPtr lParam
        {
            //MessageBox.Show(Control.ModifierKeys + "");

            if ((Control.ModifierKeys + "").Contains("Control"))
            {
                //form.textBox17.Text = tempKey.ToString().ToUpper();
                if (tempKey.ToString().ToUpper() == "C") { flag1 = true; }
                if (tempKey.ToString().ToUpper() == "D2") { flag2 = true; }
                if (tempKey.ToString().ToUpper() == "D3") { flag3 = true; }
                if (tempKey.ToString().ToUpper() == "D4") { flag4 = true; }
                if (tempKey.ToString().ToUpper() == "D5") { flag5 = true; }
                if (tempKey.ToString().ToUpper() == "D6") { flag6 = true; }

            }

            key1 = "";
            key2 = "";

        }

        public void sendKeyInput()
        {
            //form.textBox1.Text = key1;
            //form.textBox2.Text = key2;

            //form.textBox6.Text = (Int32.Parse(form.textBox6.Text) + 1) + "";

            if (key1 == "" && key2 == "" && (Control.ModifierKeys + "").Contains("None"))
            {
                if (flag1 && pasteModeOn == true && isRunning == false)
                {
                    isRunning = true;
                    removeBlankFlag = false; // 차트 형식 내 변환 확인. flag가 false상태로 유지 될 경우 빈칸을 "_"으로 변환함.
                    string clipboardText = "";
                    clipboardText = "" + getClipBoardText();
                    String modifiedText = "";
                    String[] split = SplitStrByDoubleEnter(clipboardText);
                    try
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            //MessageBox.Show(split[i]);
                            if (i > 0)
                            {
                                modifiedText += "\r\n\r\n";
                            }
                            modifiedText += getModifiedText(split[i]);
                        }
                        if (clipboardText != "")
                        {
                            setClipBoardText(modifiedText);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        //form.textBox1.Text = clipboardText;
                        //form.textBox2.Text = modifiedText;
                        //SendKeys.SendWait(form.textBox1.Text);
                        isRunning = false;
                        flag1 = false;
                    }
                }
                else if (flag2)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Thread.Sleep(8);
                        SendKeys.SendWait(list[i]);
                    }
                    flag2 = false;
                }
                else if (flag3)
                {
                    flag3 = false;
                }
                else if (flag4)
                {
                    flag4 = false;
                }
                else if (flag5)
                {
                    flag5 = false;
                }
            }
        }


        public String getClipBoardText()
        {

            String res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        //idat = Clipboard.GetDataObject();
                        if (Clipboard.ContainsText(TextDataFormat.Text))
                        {
                            res = Clipboard.GetText(TextDataFormat.Text);
                        }
                        else
                        {
                            res = "";
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        public void setClipBoardText(String str)
        {
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        Clipboard.SetText(str + "");
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }






        public String[] SplitStrByDoubleEnter(String str)
        {
            return str.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
        }

        public String getModifiedText(String str)
        {
            String res = "";

            bool onlyCondition = false;

            //가끔 Text 공백이 "?"로 처리되는 오류가 발생. "?" 부분을 공백 처리
            str = str.Replace("?", "");
            //"km/h" 공백처리
            str = str.Replace("km/h", "");
            //"km" 공백처리
            str = str.Replace("km", "");




            try
            {
                // 조건문만 포함된 경우 실행
                if (str.Contains(" = ") == false && (str.Contains("==") || str.Contains(">") || str.Contains("<") || str.Contains(">=") || str.Contains("=>") || str.Contains("<=") || str.Contains("=<") || str.Contains("&&") || str.Contains("||") || str.Contains("hasChangedTo(") || str.Contains("!=")))
                {
                    onlyCondition = true;
                    str = str.Replace("[", "(").Replace("]", ")").Replace("{", "(").Replace("}", ")");
                }


                //주석 삭제
                int index = str.IndexOf(". ");
                if (index != -1)
                {
                    str = str.Substring(index - 1, str.Length - (index - 1));
                    removeBlankFlag = true;
                }


                //번호에 "%"붙이기, "."지우기, "["괄호시작붙이기   ex) 1.Input_xxx   ->  %1\n[Input_XXX
                int index2 = str.IndexOf("{");
                if (index2 > 6)
                {
                    removeBlankFlag = true;
                    for (int i = 1; i < 25; i++)
                    {
                        str = str.Replace(i + ". ", "%" + i + "\r\n[");
                        str = str.Replace(i + ".", "%" + i + "\r\n[");
                    }
                }
                else if (index2 == -1 && (str.Contains("==") || str.Contains(">") || str.Contains("<") || str.Contains("=>") || str.Contains(">=") || str.Contains("<=") || str.Contains("=<") || str.Contains("&&") || str.Contains("||") || str.Contains("@") || str.Contains("!=")))
                {
                    for (int i = 1; i < 25; i++)
                    {
                        str = str.Replace(i + ". ", "%" + i + "\r\n[");
                        str = str.Replace(i + ".", "%" + i + "\r\n[");
                    }
                }
                else
                {
                    for (int i = 1; i < 25; i++)
                    {
                        str = str.Replace(i + ". ", "%" + i + "\r\n{");
                        str = str.Replace(i + ".", "%" + i + "\r\n{");
                        str = str.Replace(i + "\r\n\r\n{", i + "\r\n{");
                    }
                }



                //줄바꿈에 ...추가
                if (str.Contains("||") || str.Contains("&&") || str.Contains(">=") || str.Contains("=>") || str.Contains("<=") || str.Contains("=<") || str.Contains("==") || str.Contains(">") || str.Contains("<"))
                {
                    removeBlankFlag = true;
                    str = str.Replace("||\r\n", "||...\r\n");
                    str = str.Replace("|| \r\n", "||...\r\n");
                    str = str.Replace("||  \r\n", "||...\r\n");
                    str = str.Replace("&&\r\n", "&&...\r\n");
                    str = str.Replace("&& \r\n", "&&...\r\n");
                    str = str.Replace("&&  \r\n", "&&...\r\n");
                    str = str.Replace("==\r\n", "==...\r\n");
                    str = str.Replace("== \r\n", "==...\r\n");
                    str = str.Replace("==  \r\n", "==...\r\n");
                    str = str.Replace(">=\r\n", ">=...\r\n");
                    str = str.Replace(">= \r\n", ">=...\r\n");
                    str = str.Replace(">=  \r\n", ">=...\r\n");
                    str = str.Replace("=>\r\n", "=>...\r\n");
                    str = str.Replace("=> \r\n", "=>...\r\n");
                    str = str.Replace("=>  \r\n", "=>...\r\n");
                    str = str.Replace("=<\r\n", "=<...\r\n");
                    str = str.Replace("=< \r\n", "=<...\r\n");
                    str = str.Replace("=<  \r\n", "=<...\r\n");
                    str = str.Replace("<=\r\n", "<=...\r\n");
                    str = str.Replace("<= \r\n", "<=...\r\n");
                    str = str.Replace("<=  \r\n", "<=...\r\n");
                    str = str.Replace(",\r\n", ",...\r\n");
                    str = str.Replace(", \r\n", ",...\r\n");
                    str = str.Replace(",  \r\n", ",...\r\n");
                    str = str.Replace("<\r\n", "<...\r\n");
                    str = str.Replace("< \r\n", "<...\r\n");
                    str = str.Replace("<  \r\n", "<...\r\n");
                    str = str.Replace(">\r\n", ">...\r\n");
                    str = str.Replace("> \r\n", ">...\r\n");
                    str = str.Replace(">  \r\n", ">...\r\n");
                }

                //MessageBox.Show(str);
                int num = 0;

                //조건문 괄호 추가
                if (index2 > 6)
                {
                    str = str.Replace("\r\n{", "]\r\n{");
                    num = 1;
                }
                else if (index2 == -1 && str.Contains("\r\n/") && str.Contains("[") && (str.IndexOf("\r\n/") < str.IndexOf("/")))
                {
                    int tempIndex = str.IndexOf("\r\n/");
                    str = str.Substring(0, tempIndex) + "]\r\n{" + str.Substring(tempIndex + 2, str.Length - (tempIndex + 2));
                    str = str.Replace("/ ", "").Replace("/", "");
                    num = 2;
                }
                else if (index2 == -1 && (str.IndexOf("/") == 0) || str.Contains("\r\n/"))
                {
                    if (str.IndexOf("/") != 0)
                    {
                        int tempIndex = str.IndexOf("\r\n/");
                        if (str.Contains("{") == false)
                        {
                            str = str.Substring(0, tempIndex) + "{" + str.Substring(tempIndex + 1, str.Length - (tempIndex + 1));
                        }
                    }
                    else
                    {
                        str = str.Substring(1, str.Length - 1);
                        if (str.IndexOf(" ") == 0)
                        {
                            str = str.Substring(1, str.Length - 1);
                        }
                        str = "{" + str;
                    }
                    str = str.Replace("\r\n/ ", "\r\n").Replace("\r\n/", "\r\n");
                    num = 3;//////
                }
                else if (index2 == -1 && str.Contains("["))
                {
                    str = str + "]";
                    num = 4;
                }

                //MessageBox.Show(num+"\n"+str);

                // !@ 포함
                while (str.IndexOf("!@") != -1)
                {
                    removeBlankFlag = true;
                    int index3 = str.IndexOf("!@");
                    bool indexSwitch = false;
                    string cutStr = "";
                    int s = 0;
                    int e = 0;
                    for (int i = index3; i < str.Length; i++)
                    {
                        Char tempStr = str[i];
                        if ((tempStr + "" == "(")) { indexSwitch = true; s = i; }
                        if ((tempStr + "" == ")")) { indexSwitch = false; e = i; break; }
                        if (indexSwitch == true) // 괄호 내부의 char 검사
                        {
                            cutStr += tempStr + "";
                        }
                    }
                    for (int i = index3; i < str.Length; i++)
                    {
                        Char tempStr = str[i];

                        if ((tempStr + "" == "("))
                        {
                            //!@Input_XX(*) -> !hasChanged(Input_XX)
                            if (cutStr.Contains("*") == true)
                            {
                                string inputStr = str.Substring(index3 + 2, s - (index3 + 2));
                                str = str.Substring(0, index3) + "!hasChanged(" + inputStr + ")" + str.Substring(e + 1, str.Length - (e + 1));
                                break;
                            }

                            //!@Input_XX(A) -> !hasChangedTo(Input_XX,A)
                            else if (cutStr.Contains("(!") == false && cutStr.Contains(":!") == false && cutStr.Contains(":") == false && cutStr.Contains("(*") == false)
                            {
                                string inputStr = str.Substring(index3 + 2, s - (index3 + 2));
                                string strA = str.Substring(s + 1, e - (s + 1));
                                str = str.Substring(0, index3) + "!hasChangedTo(" + inputStr + "," + strA + ")" + str.Substring(e + 1, str.Length - (e + 1));
                                break;
                            }

                            else
                            {
                                str = str.Substring(0, i) + "," + str.Substring(i + 1, str.Length - (i + 1));
                                str = str.Substring(0, index3 - 1) + "Error1(" + str.Substring(index3 + 2, str.Length - (index3 + 2));
                                break;
                            }
                        }
                    }
                }


                // @ 포함
                int golbangCount = 0;
                while (str.IndexOf("@") != -1)
                {
                    golbangCount++;
                    if (golbangCount >= 100)
                    {
                        break;
                    }
                    removeBlankFlag = true;
                    int index3 = str.IndexOf("@");
                    bool indexSwitch = false;
                    string cutStr = "";
                    int s = 0;
                    int e = 0;
                    for (int i = index3; i < str.Length; i++)
                    {
                        Char tempStr = str[i];
                        if ((tempStr + "" == "(")) { indexSwitch = true; s = i; }
                        if ((tempStr + "" == ")")) { indexSwitch = false; e = i; break; }
                        if (indexSwitch == true) // 괄호 내부의 char 검사
                        {
                            cutStr += tempStr + "";
                        }
                    }



                    for (int i = index3; i < str.Length; i++)
                    {
                        Char tempStr = str[i];

                        if ((tempStr + "" == "("))
                        {
                            //@Input_XX(A) -> hasChangedTo(Input_XX,A)
                            if (cutStr.Contains("(!") == false && cutStr.Contains(":!") == false && cutStr.Contains(":") == false && cutStr.Contains("(*") == false)

                            {
                                str = str.Substring(0, i) + "," + str.Substring(i + 1, str.Length - (i + 1));
                                str = str.Substring(0, index3) + "hasChangedTo(" + str.Substring(index3 + 1, str.Length - (index3 + 1));
                                break;
                            }

                            //@Input_XX(*) -> hasChanged(Input_XX)
                            else if (cutStr.Contains("*") == true)
                            {
                                str = str.Substring(0, i) + ")" + str.Substring(i + 3, str.Length - (i + 3));
                                str = str.Substring(0, index3) + "hasChanged(" + str.Substring(index3 + 1, str.Length - (index3 + 1));
                                break;
                            }

                            //@Input_XX(!A) -> Input_XX != A && hasChanged(Input_XX)
                            else if (cutStr.Contains("!") == true && cutStr.Contains(":") == false)
                            {
                                str = str.Substring(0, index3) + "(" + str.Substring(index3 + 1, str.Length - (index3 + 1));
                                //MessageBox.Show(str+"\n22");
                                str = str.Substring(0, i) + " != " + str.Substring(s + 2, e - (s + 2)) + " && hasChanged(" + str.Substring(index3 + 1, s - (index3 + 1)) + "))"
                                    + str.Substring(e + 1, str.Length - (e + 1));
                                //MessageBox.Show(str + "\n33");
                                break;
                            }

                            //@Input_XX(A:B) -> hasChangedFrom(Input_XX,A) && hasChangedTo(Input_XX,B)
                            else if (cutStr.Contains(":") == true && cutStr.Contains("!") == false)
                            {
                                int colonIndex = str.IndexOf(":");
                                string strA = str.Substring(s + 1, colonIndex - (s + 1));
                                string strB = str.Substring(colonIndex + 1, e - (colonIndex + 1));
                                string inputStr = str.Substring(index3 + 1, s - (index3 + 1));
                                str = str.Substring(0, index3) + "(hasChangedFrom(" + inputStr + "," + strA + ") && hasChangedTo(" + inputStr + "," + strB + "))" + str.Substring(e + 1, str.Length - (e + 1));
                                break;
                            }

                            //@Input_XX(!A:B) -> !hasChangedFrom(Input_XX,A) && hasChangedTo(Input_XX,B)
                            else if (cutStr.Contains(":") == true && cutStr.Contains("(!") == true)
                            {
                                int colonIndex = str.IndexOf(":");
                                string strA = str.Substring(s + 1, colonIndex - (s + 1));
                                string strB = str.Substring(colonIndex + 1, e - (colonIndex + 1));
                                string inputStr = str.Substring(index3 + 1, s - (index3 + 1));
                                str = str.Substring(0, index3) + "(!hasChangedFrom(" + inputStr + "," + strA.Replace("!", "") + ") && hasChangedTo(" + inputStr + "," + strB + "))" + str.Substring(e + 1, str.Length - (e + 1));
                                break;
                            }

                            //@Input_XX(A:!A) -> hasChangedFrom(Input_XX,A)
                            else if (cutStr.Contains(":!") == true)
                            {
                                int colonIndex = str.IndexOf(":");
                                string strA = str.Substring(s + 1, colonIndex - (s + 1));
                                string strB = str.Substring(colonIndex + 2, e - (colonIndex + 2));
                                string inputStr = str.Substring(index3 + 1, s - (index3 + 1));
                                //MessageBox.Show("!" + strA + "!" + "\n" + "!" + strB + "!");
                                if (strA == strB)
                                {
                                    //MessageBox.Show("equal");
                                    str = str.Substring(0, index3) + "hasChangedFrom(" + inputStr + "," + strA + ")" + str.Substring(e + 1, str.Length - (e + 1));
                                }
                                else
                                {
                                    str = str.Substring(0, index3) + "(hasChangedFrom(" + inputStr + "," + strA + ") && !hasChangedTo(" + inputStr + "," + strB + "))" + str.Substring(e + 1, str.Length - (e + 1));
                                }

                                break;
                            }

                            else
                            {
                                str = str.Substring(0, i) + "," + str.Substring(i + 1, str.Length - (i + 1));
                                str = str.Substring(0, index3) + "Error2(" + str.Substring(index3 + 1, str.Length - (index3 + 1));
                                break;
                            }




                        }
                    }
                }

                //MessageBox.Show(str);


                // Function_ 포함시 변환
                str = str.Replace("\r\nfunction_", "\r\nFunction_");
                while (str.IndexOf("\r\nFunction_") != -1)
                {
                    Regex regex = new Regex("Function_");
                    int index5 = str.IndexOf("Function_");
                    str = regex.Replace(str, "", 1);
                    for (int i = index5; i < str.Length; i++)
                    {
                        Char tempStr = str[i];
                        if (tempStr + "" == "\n" || tempStr + "" == "\r") // 타이머 포함 줄인지 확인
                        {
                            str = str.Substring(0, i) + "();" + str.Substring(i + 1, str.Length - (i + 1));
                            break;
                        }
                        else if (i == str.Length - 1)
                        {
                            str = str.Substring(0, i + 1) + "()";
                            break;
                        }
                    }
                }


                //MessageBox.Show(str+"\n"+str.IndexOf("function_")+"");
                // Function 포함시 변환2 (Function Title인 경우)
                if ((str.IndexOf("Function ") == 0 || str.IndexOf("function ") == 0))
                {
                    str = str.Substring(9, str.Length - 9);
                    int tempIndex = str.IndexOf("()");
                    if (tempIndex != -1)
                    {
                        str = str.Substring(0, tempIndex);
                    }
                }
                // Function 포함시 변환3 (Function Title인 경우)
                if ((str.IndexOf("Function_") == 0 || str.IndexOf("function_") == 0))
                {
                    str = str.Substring(9, str.Length - 9);
                    int tempIndex = str.IndexOf("()");
                    if (tempIndex != -1)
                    {
                        str = str.Substring(0, tempIndex);
                    }
                }

                // Start Timer,Cancel Timer 포함시 변환    ex) Cancel Timer_DoorLock -> send(Cancel_Timer_DoorLock,DoorLock)
                int count = 0;
                while (str.IndexOf("Start Timer") != -1)
                {
                    count++;
                    removeBlankFlag = true;
                    Regex regex = new Regex("Start Timer");
                    str = regex.Replace(str, "send(Start_Timer", 1);
                    int index4 = NthIndexOf(str, "send(Start_Timer", count);
                    for (int i = index4; i < str.Length; i++)
                    {
                        Char tempStr = str[i];

                        if (tempStr + "" == "\n" || tempStr + "" == "\r") // 타이머 포함 줄인지 확인
                        {
                            //MessageBox.Show(tempStr + "@");
                            str = str.Substring(0, i) + ",TimerChart_" + str.Substring(index4 + 17, i - (index4 + 17)) + ")" + str.Substring(i, str.Length - i);
                            break;
                        }
                        else if (i == str.Length - 1) //타이머가 마지막 줄에 포함된 경우
                        {
                            str = str.Substring(0, (i + 1)) + ",TimerChart_" + str.Substring(index4 + 17, (i + 1) - (index4 + 17)) + ")" + str.Substring((i + 1), str.Length - (i + 1));
                            break;
                        }
                    }
                }
                count = 0;
                while (str.IndexOf("Cancel Timer") != -1)
                {
                    count++;
                    removeBlankFlag = true;
                    Regex regex = new Regex("Cancel Timer");
                    str = regex.Replace(str, "send(Cancel_Timer", 1);
                    int index4 = NthIndexOf(str, "send(Cancel_Timer", count);
                    //int index4 = str.IndexOf("send(Cancel_Timer");
                    for (int i = index4; i < str.Length; i++)
                    {
                        Char tempStr = str[i];

                        if (tempStr + "" == "\n" || tempStr + "" == "\r") // 타이머 포함 줄인지 확인
                        {
                            //MessageBox.Show(tempStr + "@");
                            str = str.Substring(0, i) + ",TimerChart_" + str.Substring(index4 + 18, i - (index4 + 18)) + ")" + str.Substring(i, str.Length - i);
                            break;
                        }
                        else if (i == str.Length - 1) //타이머가 마지막 줄에 포함된 경우
                        {
                            str = str.Substring(0, (i + 1)) + ",TimerChart_" + str.Substring(index4 + 18, (i + 1) - (index4 + 18)) + ")" + str.Substring((i + 1), str.Length - (i + 1));
                            break;
                        }
                    }
                }



                // Timer_aaa is Running -> b_Timer_aaa_is_Running == On 변환
                if (str.Contains(" is Running") || (str.Contains(" is running")))
                {
                    str = str.Replace(" is running", " is Running");
                    //str = str.Replace(" is Running", "_is_Running == On").Replace(" is running", "_is_Running == On");
                    int s = 0;
                    int e = str.IndexOf(" is Running");

                    bool indexSwitch = false;

                    for (int i = e; i > 0; i--)
                    {
                        Char tempChar = str[i];
                        if ((tempChar + "" == "_")) { indexSwitch = true; s = i; }
                        if (indexSwitch == true)
                        {
                            break;
                        }
                    }
                    String tempStr = str.Substring(s + 1, e - (s + 1));
                    str = str.Substring(0, s - 5) + "b_Timer_" + tempStr + "_is_Running == On" + str.Substring(e + 11, str.Length - (e + 11));
                }
                // Timer_aaa is Not Running -> b_Timer_aaa_is_Running == Off 변환
                if (str.Contains(" is Not Running") || (str.Contains(" is not running")) || (str.Contains(" is Not running")) || (str.Contains(" is not Running")))
                {
                    str = str.Replace(" is not running", " is Not Running").Replace(" is Not running", " is Not Running").Replace(" is not Running", " is Not Running");
                    //str = str.Replace(" is Running", "_is_Running == On").Replace(" is running", "_is_Running == On");
                    int s = 0;
                    int e = str.IndexOf(" is Not Running");

                    bool indexSwitch = false;

                    for (int i = e; i > 0; i--)
                    {
                        Char tempChar = str[i];
                        if ((tempChar + "" == "_")) { indexSwitch = true; s = i; }
                        if (indexSwitch == true)
                        {
                            break;
                        }
                    }
                    String tempStr = str.Substring(s + 1, e - (s + 1));
                    str = str.Substring(0, s - 5) + "b_Timer_" + tempStr + "_is_Running == Off" + str.Substring(e + 15, str.Length - (e + 15));
                }


                //실행문 괄호 추가
                if (index2 == -1 && str.Contains("{") && str.Contains("}") == false)
                {
                    str = str + "}";
                }
                else
                {
                    str = str.Replace("\r\n{", "\r\n{");
                }

                // 조건없는 천이문 변환
                if (str.Contains("=") && str.Contains("==") == false && str.Contains(">=") == false && str.Contains("=>") == false
                    && str.Contains("<=") == false && str.Contains("<=") == false && str.Contains(">") == false && str.Contains("<") == false
                    && str.Contains("@") == false && str.Contains(".") == false && str.Contains(":") == false && str.Contains("[") == false && str.Contains("]") == false
                    && str.Contains("{") == false && str.Contains("}") == false)
                {
                    str = str.Replace("=\r\n", "=...\r\n");
                    str = str.Replace("= \r\n", "=...\r\n");
                    str = str.Replace("=  \r\n", "=...\r\n");
                    str = "{" + str + "}";
                }


                //공백처리 예외 경우 추가
                if (str.Contains("%") || str.Contains("en:") || str.Contains("du:") || str.Contains("ex:") || str.Contains("||") || str.Contains("&&") || str.Contains("{") ||
                     str.Contains("}") || str.Contains("[") || str.Contains("]") || str.Contains("=") || str.Contains(">") || str.Contains("<") || str.Contains("\r\n"))
                {
                    removeBlankFlag = true;
                }

                //빈칸을 공백으로 처리
                if (removeBlankFlag == false)
                {
                    str = str.Replace("  ", "_").Replace(" ", "_");
                }

                //공통처리
                //str = str.Replace("/", "");
                str = str.Replace("{\r\n", "{");
                str = str.Replace("{ ", "{").Replace(" }", "}");
                str = str.Replace(" ]", "]");
                str = str.Replace("]]", "]");
                str = str.Replace("[[", "[");
                str = str.Replace("[\r\n", "[");
                str = str.Replace("==", " == ");
                str = str.Replace(">=", " >= ");
                str = str.Replace("<=", " <= ");
                str = str.Replace("!=", " != ");
                str = str.Replace("=", " = ");
                str = str.Replace("! = ", "!=");
                str = str.Replace("> = ", ">=");
                str = str.Replace("< = ", "<=");
                str = str.Replace(" =  = ", "==");
                str = str.Replace("  ", " ");



                //옵션 사용 시 처리
                /*
                //플랫폼 형식 사용
                if (form.checkBox1.Checked == true)
                {
                    str = platformOption(str);
                }
                //2번 옵션 사용
                if (form.checkBox2.Checked == true)
                {
                }
                //3번 옵션 사용
                if (form.checkBox3.Checked == true)
                {
                }*/


                res = str;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error1022\n" + e);
                return "Error1022 : " + e;
            }
            return res;
        }

        public string platformOption(string str)
        {
            string res = "";
            int countInput = 0;
            int countOutput = 0;
            string tempStr = str;

            List<string> prefixList = new List<string>();
            prefixList.Add("Input_");
            prefixList.Add("Output_");
            prefixList.Add("Var_");
            prefixList.Add("m_");



            for (int i = 0; i < prefixList.Count; i++)
            {
                tempStr = findPrefix(tempStr, prefixList[i]);
            }


            tempStr = removePrefix(tempStr, "__Par");
            tempStr = removePrefix(tempStr, "__Calib");
            tempStr = removePrefix(tempStr, "__TimerChart");
            tempStr = removePrefix(tempStr, "__IXnXpXuXtX_");
            tempStr = removePrefix(tempStr, "__OXuXtXpXuXtX_");
            tempStr = removePrefix(tempStr, "__VXaXrX_");
            tempStr = removePrefix(tempStr, "__mX_");




            tempStr = tempStr.Replace("IXnXpXuXtX_", "Input_");
            tempStr = tempStr.Replace("OXuXtXpXuXtX_", "Output_");
            tempStr = tempStr.Replace("VXaXrX_", "Var_");
            tempStr = tempStr.Replace("mX_", "m_");

            res = tempStr;


            string parameterStr = "";
            parameterList.Sort();
            for (int i = 0; i < parameterList.Count; i++)
            {
                parameterStr += parameterList[i] + "\r\n";
            }
            //form.textBox3.Text = parameterStr;

            return res;
        }

        public static string findPrefix(string tempStr, string prefix) // 문장을 넣고 변환해야 할 prefix를 포함했는지 검사
        {
            string res = "";
            string prefixSign = "";
            for (int i = 0; i < prefix.Length - 1; i++)
            {
                prefixSign = prefixSign + prefix[i] + "X";
            }
            try
            {
                while (tempStr.IndexOf(prefix) != -1)
                {
                    if (tempStr[tempStr.IndexOf(prefix) - 1] + "" != " " && tempStr[tempStr.IndexOf(prefix) - 1] + "" != "\n" && tempStr[tempStr.IndexOf(prefix) - 1] + "" != "\r" &&
                    tempStr[tempStr.IndexOf(prefix) - 1] + "" != "[" && tempStr[tempStr.IndexOf(prefix) - 1] + "" != "(" && tempStr[tempStr.IndexOf(prefix) - 1] + "" != "{")
                    {
                        int index2 = NthIndexOf(tempStr, prefix, 1);
                        tempStr = tempStr.Substring(0, index2) + prefixSign + tempStr.Substring(index2 + prefix.Length - 1, tempStr.Length - (index2 + prefix.Length - 1));
                        continue;
                    }
                    int index = NthIndexOf(tempStr, prefix, 1);
                    tempStr = tempStr.Substring(0, index) + prefixSign + tempStr.Substring(index + prefix.Length - 1, tempStr.Length - (index + prefix.Length - 1));


                    //countOutput++;
                    int indexOfUnderbar = 0;
                    for (int i = index; i < tempStr.Length; i++)
                    {
                        if (tempStr[i] + "" == "_")
                        {
                            indexOfUnderbar = i;
                        }
                        if (tempStr[i] + "" == "=" && tempStr[i + 1] + "" == "=") //  "==" 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 2));
                            tempStr = tempStr.Substring(0, i + 3) + signalName + "__" + tempStr.Substring(i + 3, tempStr.Length - (i + 3));
                            saveParameter(tempStr.Substring(i + 3, tempStr.Length - (i + 3)));
                            //MessageBox.Show(signalName);
                            break;
                        }
                        else if (tempStr[i] + "" == "!" && tempStr[i + 1] + "" == "=") //  "!=" 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 2));
                            tempStr = tempStr.Substring(0, i + 3) + signalName + "__" + tempStr.Substring(i + 3, tempStr.Length - (i + 3));
                            saveParameter(tempStr.Substring(i + 3, tempStr.Length - (i + 3)));
                            break;
                        }
                        else if (tempStr[i] + "" == ">" && tempStr[i + 1] + "" == "=") //  ">=" 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 2));
                            tempStr = tempStr.Substring(0, i + 3) + signalName + "__" + tempStr.Substring(i + 3, tempStr.Length - (i + 3));
                            saveParameter(tempStr.Substring(i + 3, tempStr.Length - (i + 3)));
                            break;
                        }
                        else if (tempStr[i] + "" == "<" && tempStr[i + 1] + "" == "=") //  "<=" 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 2));
                            tempStr = tempStr.Substring(0, i + 3) + signalName + "__" + tempStr.Substring(i + 3, tempStr.Length - (i + 3));
                            saveParameter(tempStr.Substring(i + 3, tempStr.Length - (i + 3)));
                            break;
                        }
                        else if (tempStr[i] + "" == "=" && tempStr[i + 1] + "" != "=") //  "=" 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 2));
                            tempStr = tempStr.Substring(0, i + 2) + signalName + "__" + tempStr.Substring(i + 2, tempStr.Length - (i + 2));
                            saveParameter(tempStr.Substring(i + 2, tempStr.Length - (i + 2)));
                            //MessageBox.Show(tempStr + "\n\n" + signalName);
                            break;
                        }
                        else if (tempStr[i] + "" == ",") //  "," 발견
                        {
                            string signalName = tempStr.Substring(indexOfUnderbar + 1, i - (indexOfUnderbar + 1));
                            tempStr = tempStr.Substring(0, i + 1) + signalName + "__" + tempStr.Substring(i + 1, tempStr.Length - (i + 1));
                            saveParameter(tempStr.Substring(i + 1, tempStr.Length - (i + 1)));
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(tempStr.IndexOf(prefix) + "\n\n" + e);
            }

            res = tempStr;

            return res;
        }

        public static void saveParameter(string str)
        {
            str = str.Trim();
            int indexOfBlank = 0;
            string tempStr = "";


            int startIndex = 0;
            if (str.Contains("Par_"))
            {
                startIndex = str.IndexOf("Par_");
            }
            else if (str.Contains("Calib_"))
            {
                startIndex = str.IndexOf("Calib_");
            }
            else if (str.Contains("Var_"))
            {
                startIndex = str.IndexOf("Var_");
            }
            else if (str.Contains("m_"))
            {
                startIndex = str.IndexOf("m_");
            }

            for (int i = startIndex; i < str.Length; i++)
            {
                if (str[i] + "" == " " || str[i] + "" == ")" || str[i] + "" == "]" || str[i] + "" == "}" || str[i] + "" == "\n" || str[i] + "" == "&" || str[i] + "" == "|")
                {
                    indexOfBlank = i;
                    break;
                }
                else if (i == str.Length - 1)
                {
                    indexOfBlank = i + 1;
                    break;
                }
            }

            try
            {
                tempStr = str.Substring(startIndex, indexOfBlank - startIndex);
            }
            catch (Exception e)
            {
                //MessageBox.Show(startIndex + ":" + (indexOfBlank - startIndex) + "\n\n" + e);
            }

            tempStr = tempStr.Trim();
            if (parameterList.Contains(tempStr) != true)
            {
                parameterList.Add(tempStr);
                //MessageBox.Show(tempStr + ":" + indexOfBlank);
            }
        }

        public static string removePrefix(string tempStr, string word)
        {
            string res = "";

            while (tempStr.Contains(word))
            {
                int indexOfPar = tempStr.IndexOf(word);
                int indexOfStart = 0;
                for (int i = indexOfPar; i > 0; i--)
                {
                    if (tempStr[i] + "" == " " || tempStr[i] + "" == "=" || tempStr[i] + "" == ",")
                    {
                        indexOfStart = i;
                        break;
                    }
                }
                tempStr = tempStr.Substring(0, indexOfStart + 1) + tempStr.Substring(indexOfPar + 2, tempStr.Length - (indexOfPar + 2));
            }

            res = tempStr;

            return res;
        }
        public static int NthIndexOf(string input, String charToFind, int n)
        {
            int position;

            switch (Math.Sign(n))
            {
                case 1:
                    position = 0;
                    while (((position = input.IndexOf(charToFind, position)) != -1) && ((--n) > 0)) { position++; }
                    break;
                case -1:
                    position = input.Length - 1;
                    while (((position = input.LastIndexOf(charToFind, position)) != -1) && ((++n) < 0)) { position--; }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(message: "param cannot be equal to 0", paramName: nameof(n));
            }

            return position;
        }


        public static void getMousePosAndColor()
        {
            Point p = GetCursorPosition();
            //form.textBox8.Text = p.X+"";
            //form.textBox9.Text = p.Y+"";
            GetColorAt(p.X, p.Y);

        }
        public static Point GetCursorPosition()
        {
            API.POINT lpPoint;
            API.GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }



        public static void create()
        {
            Macro.CreateHook(KeyReaderr);
        }

        public static void destroy()
        {
            Macro.DestroyHook();
        }

        public async void start()
        {
            //await System.Threading.Tasks.Task.Run(() => run());
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            await Task.Run(() =>
            {
                // Were we already canceled?
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {

                    sendKeyInput();
                    //getMousePosAndColor();
                    Task.Delay(5);

                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }, tokenSource2.Token); // Pass same token to Task.Run.

            tokenSource2.Cancel();

            tokenSource2.Dispose();

        }

        public void reset()
        {
            //form.recordFlag = false;
            list = new List<string>();
        }

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = API.GetDesktopWindow();
            IntPtr dc = API.GetWindowDC(desk);
            int a = (int)API.GetPixel(dc, x, y);
            API.ReleaseDC(desk, dc);

            String r = ((a >> 0) & 0xff) + "";
            String g = ((a >> 8) & 0xff) + "";
            String b = ((a >> 16) & 0xff) + "";

            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public static Color GetColorAt2(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = API.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }
        public static void isRGB()
        {
            //Point cursor = new Point();
            //API.GetCursorPos(ref cursor);

            API.POINT cursor;
            API.GetCursorPos(out cursor);

            var c = GetColorAt2(cursor);
            //form.BackColor = c;
        }
    }
}
