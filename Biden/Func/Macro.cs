using Biden.Func.Clone;
using Biden.Model;
using Biden.View;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices; //required for dll import
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WindowsInput;
using static IronPython.Modules.PythonIterTools;
using static IronPython.Modules.PythonRandom;

namespace Biden.Func
{

    [DefaultEvent("ClipboardChanged")]
    class Macro
    {

        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;
        private bool flag5 = false;

        private bool Flag_F1 = false;
        private bool Flag_F2 = false;
        private bool Flag_F3 = false;
        private bool Flag_F4 = false;

        private bool Flag_F5 = false;
        private bool Flag_F6 = false;
        private bool Flag_F7 = false;
        private bool Flag_F8 = false;

        private bool Flag_F9 = false;
        private bool Flag_F10 = false;
        private bool Flag_F11 = false;
        private bool Flag_F12 = false;



        private static bool isRunning = false;
        private static bool movingFlag = false;
        private bool isInit = false;

        private bool modeOn1 = false;
        private bool modeOn2 = false;
        private bool modeOn3 = false;
        private bool modeOn4 = false;

        public bool removeBlankFlag = false;
        public static bool doublePasteFlag = true;
        public static bool clipBoardMonitorFlag = false; // Ctrl+V 중 클립보드모니터 중지

        private static bool keyOnFlag = false;
        private static string key1 = "";
        private static string key2 = "";
        private static string clipboardChangedResult = "";

        private static string LR = "L";
        private static int global_LR = 0;
        private static int x1 = 0;
        private static int y1 = 0;
        private static int R1 = 0;
        private static int G1 = 0;
        private static int B1 = 0;
        private static int x2 = 0;
        private static int y2 = 0;
        private static int R2 = 0;
        private static int G2 = 0;
        private static int B2 = 0;

        private static int sleepCount = 0;
        private static int intervalCount = 0;
        private static int sleepCountMax = 0;

        private static string last_LR = "R";

        private static int lastPosX = 0;
        private static System.Random randomNum = new System.Random((int)DateTime.Now.Ticks);

        private static List<Task> allTasks = new List<Task>();
        private static ManualResetEvent pauseEvent = new ManualResetEvent(true);



        private static List<String> list;
        private static List<String> parameterList;
        private static List<stopPoint> stopPointList;

        private CorrectString correctString;
        private FindAndAlert findAndAlert;
        private MultiClipboard multiClipboard;
        private PasteAlert pasteAlert;
        private ClipboardMonitor clipboardMonitor;
        private SendKeyInput SK;

        public static object pasteSelectedObject = "";

        public Image ggg = null;


        //public static Bitmap screenPixel = new Bitmap(500, 200, PixelFormat.Format32bppArgb);
        public static Bitmap screenPixel = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        public static Color color;

        private static Macro instance = null;

        private Macro()
        {
            list = new List<String>();
            parameterList = new List<String>();
            correctString = new CorrectString();
            findAndAlert = new FindAndAlert();
            multiClipboard = new MultiClipboard();
            pasteAlert = new PasteAlert();
            clipboardMonitor = new ClipboardMonitor(this);

            stopPointList = new List<stopPoint>();

            SK = new SendKeyInput();

        }

        public static Macro getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Macro();
                }
                return instance;
            }
        }

        public bool IsInit { get => isInit; set => isInit = value; }
        public bool ModeOn1 { get => modeOn1; set => modeOn1 = value; }
        public bool ModeOn2 { get => modeOn2; set => modeOn2 = value; }
        public bool ModeOn3 { get => modeOn3; set => modeOn3 = value; }
        public bool ModeOn4 { get => modeOn4; set => modeOn4 = value; }
        public static bool IsRunning { get => isRunning; set => isRunning = value; }
        public bool Flag1 { get => flag1; set => flag1 = value; }
        public bool Flag2 { get => flag2; set => flag2 = value; }
        public bool Flag3 { get => flag3; set => flag3 = value; }
        public bool Flag4 { get => flag4; set => flag4 = value; }
        public bool Flag5 { get => flag5; set => flag5 = value; }
        public bool Flag_F91 { get => Flag_F9; set => Flag_F9 = value; }
        public bool Flag_F101 { get => Flag_F10; set => Flag_F10 = value; }
        public bool Flag_F111 { get => Flag_F11; set => Flag_F11 = value; }
        public bool Flag_F121 { get => Flag_F12; set => Flag_F12 = value; }
        public bool Flag_F51 { get => Flag_F5; set => Flag_F5 = value; }
        public bool Flag_F61 { get => Flag_F6; set => Flag_F6 = value; }
        public bool Flag_F71 { get => Flag_F7; set => Flag_F7 = value; }
        public bool Flag_F81 { get => Flag_F8; set => Flag_F8 = value; }
        public bool Flag_F13 { get => Flag_F1; set => Flag_F1 = value; }
        public bool Flag_F21 { get => Flag_F2; set => Flag_F2 = value; }
        public bool Flag_F31 { get => Flag_F3; set => Flag_F3 = value; }
        public bool Flag_F41 { get => Flag_F4; set => Flag_F4 = value; }
        public bool MovingFlag { get => movingFlag; set => movingFlag = value; }


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
            hhk = User32.API.SetWindowsHookEx(13, hd, User32.API.GetModuleHandle(mod.ModuleName), 0);

            //MessageBox.Show(Marshal.GetLastWin32Error().ToString()); //for debugging
            //Note that this could be a Console.WriteLine(), as well. I just happened
            //to be debugging this in a Windows Application
        }

        public static bool DestroyHook()
        {
            //to be called when we're done with the hook

            return User32.API.UnhookWindowsHookEx(hhk);
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
            return User32.API.CallNextHookEx(hhk, nCode, wParam, lParam);
        }

        private void KeyReaderr(IntPtr wParam, IntPtr lParam)
        {
            int key = Marshal.ReadInt32(lParam);

            SendKeyInput.VK vk = (SendKeyInput.VK)key;


            String temp = "";

            #region

            switch (vk)
            {
                case SendKeyInput.VK.VK_F1:
                    //temp = "&lt;-F1-&gt;";
                    temp = "{F1}";
                    break;
                case SendKeyInput.VK.VK_F2:
                    //temp = "&lt;-F2-&gt;";
                    temp = "{F2}";
                    break;
                case SendKeyInput.VK.VK_F3:
                    //temp = "&lt;-F3-&gt;";
                    temp = "{F3}";
                    break;
                case SendKeyInput.VK.VK_F4:
                    //temp = "&lt;-F4-&gt;";
                    temp = "{F4}";
                    break;
                case SendKeyInput.VK.VK_F5:
                    //temp = "&lt;-F5-&gt;";
                    temp = "{F5}";
                    break;
                case SendKeyInput.VK.VK_F6:
                    //temp = "&lt;-F6-&gt;";
                    temp = "{F6}";
                    break;
                case SendKeyInput.VK.VK_F7:
                    //temp = "&lt;-F7-&gt;";
                    temp = "{F7}";
                    break;
                case SendKeyInput.VK.VK_F8:
                    //temp = "&lt;-F8-&gt;";
                    temp = "{F8}";
                    break;
                case SendKeyInput.VK.VK_F9:
                    //temp = "&lt;-F9-&gt;";
                    temp = "{F9}";
                    break;
                case SendKeyInput.VK.VK_F10:
                    //temp = "&lt;-F10-&gt;";
                    temp = "{F10}";
                    break;
                case SendKeyInput.VK.VK_F11:
                    //temp = "&lt;-F11-&gt;";
                    temp = "{F11}";
                    break;
                case SendKeyInput.VK.VK_F12:
                    //temp = "&lt;-F12-&gt;";
                    temp = "{F12}";
                    break;
                case SendKeyInput.VK.VK_NUMLOCK:
                    //temp = "&lt;-numlock-&gt;";
                    temp = "{NUMLOCK}";
                    break;
                case SendKeyInput.VK.VK_SCROLL:
                    //temp = "&lt;-scroll&gt;";
                    temp = "{SCROLLLOCK}";
                    break;
                case SendKeyInput.VK.VK_LSHIFT:
                    //temp = "&lt;-left shift-&gt;";
                    temp = "{+}";
                    break;
                case SendKeyInput.VK.VK_RSHIFT:
                    //temp = "&lt;-right shift-&gt;";
                    temp = "{+}";
                    break;
                case SendKeyInput.VK.VK_LCONTROL:
                    //temp = "&lt;-left control-&gt;";
                    temp = "{CTRL}";
                    break;
                case SendKeyInput.VK.VK_RCONTROL:
                    //temp = "&lt;-right control-&gt;";
                    temp = "{CTRL}";
                    break;
                case SendKeyInput.VK.VK_SEPERATOR:
                    temp = "|";
                    break;
                case SendKeyInput.VK.VK_SUBTRACT:
                    temp = "-";
                    break;
                case SendKeyInput.VK.VK_DECIMAL:
                    temp = ".";
                    break;
                case SendKeyInput.VK.VK_DIVIDE:
                    temp = "/";
                    break;
                case SendKeyInput.VK.VK_NUMPAD0:
                    temp = "0";
                    break;
                case SendKeyInput.VK.VK_NUMPAD1:
                    temp = "1";
                    break;
                case SendKeyInput.VK.VK_NUMPAD2:
                    temp = "2";
                    break;
                case SendKeyInput.VK.VK_NUMPAD3:
                    temp = "3";
                    break;
                case SendKeyInput.VK.VK_NUMPAD4:
                    temp = "4";
                    break;
                case SendKeyInput.VK.VK_NUMPAD5:
                    temp = "5";
                    break;
                case SendKeyInput.VK.VK_NUMPAD6:
                    temp = "6";
                    break;
                case SendKeyInput.VK.VK_NUMPAD7:
                    temp = "7";
                    break;
                case SendKeyInput.VK.VK_NUMPAD8:
                    temp = "8";
                    break;
                case SendKeyInput.VK.VK_NUMPAD9:
                    temp = "9";
                    break;
                case SendKeyInput.VK.VK_Q:
                    temp = "q";
                    break;
                case SendKeyInput.VK.VK_W:
                    temp = "w";
                    break;
                case SendKeyInput.VK.VK_E:
                    temp = "e";
                    break;
                case SendKeyInput.VK.VK_R:
                    temp = "r";
                    break;
                case SendKeyInput.VK.VK_T:
                    temp = "t";
                    break;
                case SendKeyInput.VK.VK_Y:
                    temp = "y";
                    break;
                case SendKeyInput.VK.VK_U:
                    temp = "u";
                    break;
                case SendKeyInput.VK.VK_I:
                    temp = "i";
                    break;
                case SendKeyInput.VK.VK_O:
                    temp = "o";
                    break;
                case SendKeyInput.VK.VK_P:
                    temp = "p";
                    break;
                case SendKeyInput.VK.VK_A:
                    temp = "a";
                    break;
                case SendKeyInput.VK.VK_S:
                    temp = "s";
                    break;
                case SendKeyInput.VK.VK_D:
                    temp = "d";
                    break;
                case SendKeyInput.VK.VK_F:
                    temp = "f";
                    break;
                case SendKeyInput.VK.VK_G:
                    temp = "g";
                    break;
                case SendKeyInput.VK.VK_H:
                    temp = "h";
                    break;
                case SendKeyInput.VK.VK_J:
                    temp = "j";
                    break;
                case SendKeyInput.VK.VK_K:
                    temp = "k";
                    break;
                case SendKeyInput.VK.VK_L:
                    temp = "l";
                    break;
                case SendKeyInput.VK.VK_Z:
                    temp = "z";
                    break;
                case SendKeyInput.VK.VK_X:
                    temp = "x";
                    break;
                case SendKeyInput.VK.VK_C:
                    temp = "c";
                    break;
                case SendKeyInput.VK.VK_V:
                    temp = "v";
                    break;
                case SendKeyInput.VK.VK_B:
                    temp = "b";
                    break;
                case SendKeyInput.VK.VK_N:
                    temp = "n";
                    break;
                case SendKeyInput.VK.VK_M:
                    temp = "m";
                    break;
                case SendKeyInput.VK.VK_0:
                    temp = "0";
                    break;
                case SendKeyInput.VK.VK_1:
                    temp = "1";
                    break;
                case SendKeyInput.VK.VK_2:
                    temp = "2";
                    break;
                case SendKeyInput.VK.VK_3:
                    temp = "3";
                    break;
                case SendKeyInput.VK.VK_4:
                    temp = "4";
                    break;
                case SendKeyInput.VK.VK_5:
                    temp = "5";
                    break;
                case SendKeyInput.VK.VK_6:
                    temp = "6";
                    break;
                case SendKeyInput.VK.VK_7:
                    temp = "7";
                    break;
                case SendKeyInput.VK.VK_8:
                    temp = "8";
                    break;
                case SendKeyInput.VK.VK_9:
                    temp = "9";
                    break;
                case SendKeyInput.VK.VK_SNAPSHOT:
                    //temp = "&lt;-print screen-&gt;";
                    temp = "{PRTSC}";
                    break;
                case SendKeyInput.VK.VK_INSERT:
                    //temp = "&lt;-insert-&gt;";
                    temp = "{INSERT}";
                    break;
                case SendKeyInput.VK.VK_DELETE:
                    //temp = "&lt;-delete-&gt;";
                    temp = "{DELETE}";
                    break;
                case SendKeyInput.VK.VK_BACK:
                    //temp = "&lt;-backspace-&gt;";
                    temp = "{BACKSPACE}";
                    break;
                case SendKeyInput.VK.VK_TAB:
                    //temp = "&lt;-tab-&gt;";
                    temp = "{TAB}";
                    break;
                case SendKeyInput.VK.VK_RETURN:
                    //temp = "&lt;-enter-&gt;" + Environment.NewLine;
                    temp = "{ENTER}";
                    break;
                case SendKeyInput.VK.VK_PAUSE:
                    //temp = "&lt;-pause-&gt;";
                    temp = "{PAUSE}";
                    break;
                case SendKeyInput.VK.VK_CAPITAL:
                    //temp = "&lt;-caps lock-&gt;";
                    temp = "{CAPSLOCK}";
                    break;
                case SendKeyInput.VK.VK_ESCAPE:
                    //temp = "&lt;-esc-&gt;";
                    temp = "{ESC}";
                    break;
                case SendKeyInput.VK.VK_SPACE:
                    //temp = "&lt;-space-&gt;";
                    temp = "{SPACE}";
                    break;
                case SendKeyInput.VK.VK_PRIOR:
                    //temp = "&lt;-page up-&gt;";
                    temp = "{PGUP}";
                    break;
                case SendKeyInput.VK.VK_NEXT:
                    //temp = "&lt;-page down-&gt;";
                    temp = "{PGDN}";
                    break;
                case SendKeyInput.VK.VK_END:
                    //temp = "&lt;-end-&gt;";
                    temp = "{END}";
                    break;
                case SendKeyInput.VK.VK_HOME:
                    //temp = "&lt;-home-&gt;";
                    temp = "{HOME}";
                    break;
                case SendKeyInput.VK.VK_LEFT:
                    //temp = "&lt;-arrow left-&gt;";
                    temp = "{LEFT}";
                    break;
                case SendKeyInput.VK.VK_UP:
                    //temp = "&lt;-arrow up-&gt;";
                    temp = "{UP}";
                    break;
                case SendKeyInput.VK.VK_RIGHT:
                    //temp = "&lt;-arrow right-&gt;";
                    temp = "{RIGHT}";
                    break;
                case SendKeyInput.VK.VK_DOWN:
                    //temp = "&lt;-arrow down-&gt;";
                    temp = "{DOWN}";
                    break;
                default: break;
            }

            #endregion

            key1 = vk + "";
            key2 = temp + "";


            send((Keys)key);

        }


        private static void rejoin()
        {

            Thread.Sleep(1500);
            User32.API.SetCursorPos(1718, 246);
            MouseClick();
            Thread.Sleep(1500);
            User32.API.SetCursorPos(1724, 326);
            MouseClick();
            Thread.Sleep(1500);
            User32.API.SetCursorPos(791, 615);
            MouseClick();
            Thread.Sleep(46000);
            User32.API.SetCursorPos(960, 633);
            MouseClick();
            Thread.Sleep(7000);
        }

        private static void MouseClick()
        {
            // 마우스 이벤트 발생 (왼쪽 버튼 클릭)
            User32.API.mouse_event(0x0002, 0, 0, 0, 0);
            User32.API.mouse_event(0x0004, 0, 0, 0, 0);
        }




        private void send(Keys tempKey)//Keys tempKey, IntPtr wParam, IntPtr lParam
        {
            //MessageBox.Show(Control.ModifierKeys + "");
            //MessageBox.Show(tempKey.ToString().ToUpper() + "");

            if (tempKey.ToString().ToUpper() == "RSHIFTKEY" && keyOnFlag == false)
            {
                keyOnFlag = true;
                AltAndDelete();
            }

            // 1회 실행
            if (tempKey.ToString().ToUpper() == "F1")
            {
                pushLeftStopPoint();
            }
            if (tempKey.ToString().ToUpper() == "F2")
            {
                pushRightStopPoint();
            }
            if (tempKey.ToString().ToUpper() == "F3")
            {
                deleteStopPoint();
            }
            if (tempKey.ToString().ToUpper() == "F4")
            {
                hi();
            }

            // 토글
            else if (tempKey.ToString().ToUpper() == "F8")
            {
                if (Macro.getInstance.Flag_F8)
                {
                    Macro.getInstance.Flag_F8 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F8 = true;
                }
            }
            else if (tempKey.ToString().ToUpper() == "F9")
            {
                if (Macro.getInstance.Flag_F9)
                {
                    Macro.getInstance.Flag_F9 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F9 = true;
                }
            }
            else if (tempKey.ToString().ToUpper() == "F10")
            {
                if (Macro.getInstance.Flag_F10)
                {
                    Macro.getInstance.Flag_F10 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F10 = true;
                }
            }
            else if (tempKey.ToString().ToUpper() == "F11")
            {
                if (Macro.getInstance.Flag_F11)
                {
                    Macro.getInstance.Flag_F11 = false;
                }
                else
                {
                    Thread.Sleep(15000);
                    Macro.getInstance.Flag_F11 = true;
                }
            }
            else if (tempKey.ToString().ToUpper() == "F12")
            {
                if (Macro.getInstance.Flag_F12)
                {
                    Macro.getInstance.Flag_F12 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F12 = true;
                }
            }
            else
            {

            }
            /*
            else if ((Control.ModifierKeys + "").Contains("Control"))
            {

            }

            else if ((tempKey.ToString().ToUpper() + "").Contains("LWIN"))
            {
                if ((tempKey.ToString().ToUpper() + "").Contains("LSHIFTKEY"))
                {
                    if (tempKey.ToString().ToUpper() == "S") 
                    {
                        //Macro.getInstance.Flag1 = true; 
                    }
                }
            }*/

            key1 = "";
            key2 = "";
            keyOnFlag = false;
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
                        IDataObject idat = Clipboard.GetDataObject();
                        //MessageBox.Show(idat.GetFormats(). + "");
                        if (Clipboard.ContainsText()) //Clipboard.ContainsText(TextDataFormat.Text)
                        {
                            res = Clipboard.GetText();
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }




        //fff
        public DataObjectClass getClipBoardIData()
        {

            Exception threadEx = null;
            DataObject dataObject = null;
            IDataObject idataObject;
            string curFormat = "";
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (getClipBoardDataType() == "Text") { curFormat = DataFormats.Text; }
                        else if (getClipBoardDataType() == "Image") { curFormat = DataFormats.Bitmap; }
                        else { }

                        dataObject = new DataObject();
                        idataObject = Clipboard.GetDataObject();
                        object data = idataObject.GetData(curFormat);
                        if (data != null) { dataObject.SetData(curFormat, data); }

                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            try
            {
                //MessageBox.Show(((IDataObject)dataObject).GetData(DataFormats.Text) + "!!\n" + dataObject);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error \n\n\n" + e);
            }
            DataObjectClass tempDataObjectClass = new DataObjectClass();
            tempDataObjectClass.dataObject = dataObject;
            tempDataObjectClass.type = curFormat;
            return tempDataObjectClass;
        }



        public Bitmap getClipBoardImage()
        {

            Bitmap res = null;
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        IDataObject idat = Clipboard.GetDataObject();
                        string tempStr = idat.GetFormats() + "";
                        //MessageBox.Show(tempStr, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        if (Clipboard.ContainsImage())
                        {
                            res = (Bitmap)Clipboard.GetImage();
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        public Bitmap getClipBoardObject()
        {

            Bitmap res = null;
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        IDataObject idat = Clipboard.GetDataObject();
                        string tempStr = idat.GetFormats() + "";
                        string tempStr2 = idat.GetFormats().GetType() + "";
                        if (idat.GetDataPresent(DataFormats.Bitmap))
                        {

                        }
                        //MessageBox.Show(tempStr1, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        //MessageBox.Show(tempStr2, "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        if (Clipboard.ContainsImage())
                        {
                            Bitmap x;
                            x = (Bitmap)idat.GetData(DataFormats.Bitmap, true);
                            x.SetResolution(x.HorizontalResolution, x.VerticalResolution);
                            x = cropAtRect(x, new Rectangle(0, 0, x.Width, x.Height));
                            //img.MakeTransparent();
                            //img = GetResizeImage(img, 1000, 1000);
                            res = x.Clone(new Rectangle(0, 0, x.Width, x.Height), x.PixelFormat);
                            //res = img;
                            string path = System.IO.Path.GetFullPath(@"clipimage\ccc.png");
                            x.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                            setClipBoardImage(res);
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }


        public Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                return nb;
            }
        }


        public void saveFile(Image img)
        {
            //string path = 
        }


        public String getClipBoardDataType()
        {

            String res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if (Clipboard.ContainsText()) //Clipboard.ContainsText(TextDataFormat.Text)
                        {
                            res = "Text";
                        }
                        else if (Clipboard.ContainsImage() == true)
                        {
                            res = "Image";
                        }
                        else
                        {
                            res = "None";
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }

        public static void setClipBoardText(object obj)
        {
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        if ((string)obj == "")
                        {
                            Clipboard.Clear();
                        }
                        else
                        {
                            Clipboard.SetText(obj + "");
                        }
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        public static void setClipBoardImage(Bitmap obj)
        {
            Exception threadEx = null;
            //MessageBox.Show(obj.GetFormats()+"@");
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        Clipboard.Clear();
                        if (obj == null)
                        {
                        }
                        else
                        {
                            //Clipboard.SetImage((Bitmap)obj.GetData(DataFormats., true));
                            Clipboard.SetImage((Bitmap)obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }


        public String[] SplitStrByDoubleEnter(String str)
        {
            return str.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
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

        public static Point getMousePosAndColor()
        {
            Point p = GetCursorPosition();
            //form.textBox8.Text = p.X+"";
            //form.textBox9.Text = p.Y+"";

            return p;

        }
        public static Point GetCursorPosition()
        {
            User32.API.POINT lpPoint;
            User32.API.GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        public void create()
        {
            Macro.CreateHook(KeyReaderr);
        }

        public static void destroy()
        {
            Macro.DestroyHook();
        }

        //sss
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
                    //ClipboardDetect();
                    //getMousePosAndColor();
                    sendKeyInput(tokenSource2);
                    if (Macro.getInstance.Flag_F12)
                    {
                        //dongbasan_left_getPos(tokenSource2);
                        //dongbasan_mid_getPos(tokenSource2);
                        //dongbasan_right_getPos(tokenSource2);

                        dongbasan_bernanke_getPos(tokenSource2);

                    }
                    if (Macro.getInstance.Flag_F11)
                    {
                        //dongbasan_rest_getPos(tokenSource2);
                    }
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


        public async void start2()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            await Task.Run(() =>
            {
                // Were we already canceled?
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    //ClipboardDetect();
                    //getMousePosAndColor();
                    try
                    {
                        if (Macro.getInstance.Flag_F12)
                        {
                            //dongbasan_left_attack();
                            //dongbasan_mid_attack();
                            //dongbasan_right_attack();
                            Bernanke_mid_attack();

                            //yellen_buff();
                            //volker_buff();
                            //reagan_buff();
                            Bernanke_buff();
                        }
                        if (Macro.getInstance.Flag_F11)
                        {
                            //reagan_buff_fury();
                        }
                    }
                    catch
                    {

                    }

                    Task.Delay(1);

                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }, tokenSource2.Token); // Pass same token to Task.Run.370

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
            IntPtr desk = User32.API.GetDesktopWindow();
            IntPtr dc = User32.API.GetWindowDC(desk);
            int a = (int)User32.API.GetPixel(dc, x, y);
            User32.API.ReleaseDC(desk, dc);

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
                    int retval = User32.API.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
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

            User32.API.POINT cursor;
            User32.API.GetCursorPos(out cursor);

            var c = GetColorAt2(cursor);
            //form.BackColor = c;
        }


        //ddd
        public void sendKeyInput(CancellationTokenSource ct)
        {
            if (Macro.getInstance.Flag_F1)
            {
                pushLeftStopPoint();
            }
            if (Macro.getInstance.Flag_F2)
            {
                pushRightStopPoint();
            }
            if (Macro.getInstance.Flag_F3)
            {
                deleteStopPoint();
            }
            if (Macro.getInstance.Flag_F4)
            {
                hi();
            }

            if (Macro.getInstance.Flag_F8)
            {
            }
            if (Macro.getInstance.Flag_F9)
            {
            }
            if (Macro.getInstance.Flag_F10)
            {
                rejoin();
            }
            if (Macro.getInstance.Flag_F11)
            {
            }
            if (Macro.getInstance.Flag_F12)
            {
            }
            else
            {

            }

            Macro.getInstance.Flag1 = false;
            Macro.getInstance.Flag2 = false;
            Macro.getInstance.Flag3 = false;
            Macro.getInstance.Flag4 = false;
            Macro.getInstance.Flag5 = false;

            Macro.getInstance.Flag_F5 = false;
            Macro.getInstance.Flag_F6 = false;
            Macro.getInstance.Flag_F7 = false;
            Macro.getInstance.Flag_F8 = false;

            Macro.getInstance.Flag_F9 = false;
            //Macro.getInstance.Flag_F10 = false;
            //Macro.getInstance.Flag_F11 = false;
            //Macro.getInstance.Flag_F12 = false;

        }



        private static void Macro_F8()
        {
            Point p = getMousePosAndColor();
            Color curColor = GetColorAt(p.X, p.Y);
            x1 = p.X;
            y1 = p.Y;
            R1 = curColor.R;
            G1 = curColor.G;
            B1 = curColor.B;
        }


        private static void random_F1toF5()
        {
            System.Random random = new System.Random((int)DateTime.Now.Ticks);
            int randomF1toF5 = random.Next(1, 7);
            if (randomF1toF5 == 1)
            {
                Thread.Sleep(20);
                sendkey("{F1}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 2)
            {
                Thread.Sleep(20);
                sendkey("{F2}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 13)
            {
                Thread.Sleep(20);
                sendkey("{F3}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 4)
            {
                Thread.Sleep(20);
                sendkey("{F4}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 5)
            {
                Thread.Sleep(20);
                sendkey("{F5}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 6)
            {
                Thread.Sleep(20);
                sendkey("{F6}");
                Thread.Sleep(20);
            }
            else if (randomF1toF5 == 7)
            {
                Thread.Sleep(20);
                sendkey("{F7}");
                Thread.Sleep(20);
            }
            else
            {

            }
        }

        private static void pushLeftStopPoint()
        {
            Point p = GetCursorPosition();
            int x = p.X;
            int y = p.Y;
            Color curColor = GetColorAt(x, y);
            stopPoint tempStopPoint = new stopPoint();
            tempStopPoint.x = x;
            tempStopPoint.y = y;
            tempStopPoint.color = curColor;
            tempStopPoint.LR = "L";
            stopPointList.Add(tempStopPoint);
        }

        private static void pushRightStopPoint()
        {
            Point p = GetCursorPosition();
            int x = p.X;
            int y = p.Y;
            Color curColor = GetColorAt(x, y);
            stopPoint tempStopPoint = new stopPoint();
            tempStopPoint.x = x;
            tempStopPoint.y = y;
            tempStopPoint.color = curColor;
            tempStopPoint.LR = "R";
            stopPointList.Add(tempStopPoint);
        }

        private static void deleteStopPoint()
        {
            stopPointList = new List<stopPoint>();
        }

        private static void hi()
        {
            Point p = getMousePosAndColor();
            Color curColor = GetColorAt(p.X, p.Y);

            x2 = p.X;
            y2 = p.Y;
            R2 = curColor.R;
            G2 = curColor.G;
            B2 = curColor.B;

            MainWindow.getInstance.SetStateString(x2, y2, curColor);
        }



        private void dongbasan_left_getPos(CancellationTokenSource ct)
        {
            Color curColorLeft2 = new Color();
            Color curColorRight2 = new Color();
            Color curColorLeft = new Color();
            Color curColorRight = new Color();


            int curIndex = 0;


            for (int i = 0; i <= 250; i = i + 2)
            {
                SK.sendkeyZ(5);
                int leftX = lastPosX - i;
                int rightX = lastPosX + i;
                if (leftX < 25)
                {
                    leftX = 25;
                }
                else if (leftX > 249)
                {
                    leftX = 249;
                }
                if (rightX < 25)
                {
                    rightX = 25;
                }
                else if (rightX > 249)
                {
                    rightX = 249;
                }
                curColorLeft = GetColorAt(rightX, 258);
                curColorRight = GetColorAt(leftX, 258);
                curColorLeft2 = GetColorAt(rightX, 269);
                curColorRight2 = GetColorAt(leftX, 269);
                if (curColorLeft.R == 255 && curColorLeft.G == 255)
                {
                    lastPosX = rightX;
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorLeft);
                    if (lastPosX <= 25)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 80)
                    {
                        SK.sendkeyLeft(350);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorRight);
                    if (lastPosX <= 25)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 80)
                    {
                        SK.sendkeyLeft(350);
                        LR = "L";
                    }
                    break;
                }
                else if ((curColorRight2.R == 255 && curColorRight2.G == 255) || (curColorLeft2.R == 255 && curColorLeft2.G == 255))
                {
                    if (rightX >= 79)
                    {
                        SK.sendkeyLeft(850);
                    }
                }
            }
        }
        private void dongbasan_mid_getPos(CancellationTokenSource ct)
        {
            Color curColorWall = new Color();
            Color curColorLeft = new Color();
            Color curColorRight = new Color();

            int curIndex = 0;

            for (int i = 0; i <= 250; i = i + 2)
            {
                SK.sendkeyZ(5);
                int leftX = lastPosX - i;
                int rightX = lastPosX + i;
                if (leftX < 92)
                {
                    leftX = 92;
                }
                else if (leftX > 250)
                {
                    leftX = 250;
                }
                if (rightX < 92)
                {
                    rightX = 92;
                }
                else if (rightX > 250)
                {
                    rightX = 250;
                }
                curColorWall = GetColorAt(26, 263);
                curColorLeft = GetColorAt(rightX, 266);
                curColorRight = GetColorAt(leftX, 266);
                if (curColorLeft.R == 255 && curColorLeft.G == 255)
                {
                    lastPosX = rightX;
                    curIndex = (int)(lastPosX * 6.53);
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorLeft);
                    if (lastPosX <= 115)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 175)
                    {
                        SK.sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    curIndex = (int)(lastPosX * 6.53);
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorRight);
                    if (lastPosX <= 115)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 175)
                    {
                        SK.sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorWall.R == 255 && curColorWall.G == 255)
                {
                    SK.sendkeyRight(2050);
                    LR = "R";
                }
            }
        }
        private void dongbasan_right_getPos(CancellationTokenSource ct)
        {
            Color curColorWall = new Color();
            Color curColorLeft = new Color();
            Color curColorRight = new Color();

            for (int i = 0; i <= 250; i = i + 2)
            {
                SK.sendkeyZ(5);
                int leftX = lastPosX - i;
                int rightX = lastPosX + i;
                if (leftX < 25)
                {
                    leftX = 25;
                }
                else if (leftX > 249)
                {
                    leftX = 249;
                }
                if (rightX < 25)
                {
                    rightX = 25;
                }
                else if (rightX > 249)
                {
                    rightX = 249;
                }

                curColorLeft = GetColorAt(rightX, 266);
                curColorRight = GetColorAt(leftX, 266);
                curColorWall = GetColorAt(26, 263);

                if (curColorLeft.R == 255 && curColorLeft.G == 255)
                {
                    lastPosX = rightX;
                    if (lastPosX <= 170)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 248)
                    {
                        SK.sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    if (lastPosX <= 170)
                    {
                        SK.sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 248)
                    {
                        SK.sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorWall.R == 255 && curColorWall.G == 255)
                {
                    SK.sendkeyRight(3050);
                    LR = "R";
                    break;
                }
            }
        }

        private void dongbasan_rest_getPos(CancellationTokenSource ct)
        {
            Thread.Sleep(500);
            Color curColorLeft = new Color();
            Color curColorRight = new Color();


            for (int i = 0; i <= 250; i = i + 1)
            {
                int leftX = lastPosX - i;
                int rightX = lastPosX + i;
                if (leftX <= 150)
                {
                    leftX = 150;
                    lastPosX = 150;
                }
                if (rightX >= 158)
                {
                    rightX = 158;
                    lastPosX = 158;
                }
                curColorLeft = GetColorAt(rightX, 235);
                curColorRight = GetColorAt(leftX, 235);

                //MainWindow.getInstance.SetStateString(lastPosX, 235, curColorLeft);

                if (curColorLeft.R == 255 && curColorLeft.G == 255)
                {
                    lastPosX = rightX;
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorLeft);
                    if (lastPosX <= 152)
                    {
                        LR = "R";
                    }
                    else if (lastPosX >= 156)
                    {
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    //MainWindow.getInstance.SetStateString(lastPosX, 271, curColorRight);
                    if (lastPosX <= 152)
                    {
                        LR = "R";
                    }
                    else if (lastPosX >= 156)
                    {
                        LR = "L";
                    }
                    break;
                }
            }

            if (LR == "L")
            {
                SK.sendkeyLeft(0.2);
            }else if (LR == "R")
            {
                SK.sendkeyRight(0.2);
            }
            else
            {

            }
        }



        private void dongbasan_bernanke_getPos(CancellationTokenSource ct)
        {
            /*
            Color curColorLeft = new Color();
            Color curColorRight = new Color();

            for (int i = 0; i <= 250; i = i + 4)
            {
                SK.sendkeyZ(5);
                int leftX = lastPosX - i;
                int rightX = lastPosX + i;
                if (leftX < 25)
                {
                    leftX = 25;
                }
                else if (leftX > 249)
                {
                    leftX = 249;
                }
                if (rightX < 25)
                {
                    rightX = 25;
                }
                else if (rightX > 249)
                {
                    rightX = 249;
                }

                curColorLeft = GetColorAt(rightX, 269);
                curColorRight = GetColorAt(leftX, 269);

                if (curColorLeft.R == 255 && curColorLeft.G == 255)
                {
                    lastPosX = rightX;
                    if (lastPosX <= 120)
                    {
                        LR = "R";
                    }
                    else if (lastPosX >= 230)
                    {
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    if (lastPosX <= 120)
                    {
                        LR = "R";
                    }
                    else if (lastPosX >= 230)
                    {
                        LR = "L";
                    }
                    break;
                }
                if (i == 250)
                {
                    lastPosX = 180;
                }
            }
            */
            
 
            for (int i = 0; i < 8; i++)
            {
                Color tempColor = GetColorAt(90 + (i * 5), 269);
                if (tempColor.R == 255 && tempColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Color tempColor = GetColorAt(250 - (i * 5), 269);
                if (tempColor.R == 255 && tempColor.G == 255)
                {
                    LR = "L";
                    break;
            }
        }

            




        private void dongbasan_left_attack()
        {
            int curIndex = (int)((lastPosX - 25) * 13);
            int randomNum1to100 = randomNum.Next(1, 100);

            bool attackFlag = false;

            int mobDistMin = 10;
            int mobDistMax = 400;
            for (int i = mobDistMin; i < mobDistMax; i = i + (i / 100 + 3))
            {
                int leftX = curIndex - i;
                int rightX = curIndex + i;

                if (rightX > 730)
                {
                    rightX = 730;
                }
                if (leftX < 0)
                {
                    leftX = 1;
                }

                Color tempColor = GetColorAt(leftX, 675);
                Color tempColor2 = GetColorAt(rightX, 675);
                // 154, 137,121
                if ((tempColor.R >= 99 && tempColor.R <= 115 && tempColor.G >= 48 && tempColor.G <= 53 && tempColor.B >= 29 && tempColor.B <= 38) ||
                    (tempColor.R == 99 && tempColor.G == 84 && tempColor.B == 68) ||
                    (tempColor.R == 104 && tempColor.G == 89 && tempColor.B == 66) ||
                    (tempColor.R == 27 && tempColor.G == 20 && tempColor.B == 16) ||
                    (tempColor.R == 99 && tempColor.G == 88 && tempColor.B == 68) ||
                    (tempColor.R == 86 && tempColor.G == 76 && tempColor.B == 58) ||
                    (tempColor.R == 140 && tempColor.G == 121 && tempColor.B == 90) ||
                    (tempColor.R == 100 && tempColor.G == 86 && tempColor.B == 67) ||
                    (tempColor.R == 137 && tempColor.G == 122 && tempColor.B == 88) ||
                    (tempColor.R == 57 && tempColor.G == 52 && tempColor.B == 33) ||
                    (tempColor.R == 33 && tempColor.G == 16 && tempColor.B == 16) ||
                    (tempColor.R == 8 && tempColor.G == 0 && tempColor.B == 0) ||
                    (tempColor.R == 75 && tempColor.G == 66 && tempColor.B == 51) ||
                    (tempColor.R == 0 && tempColor.G == 4 && tempColor.B == 0) ||
                    (tempColor.R == 4 && tempColor.G == 3 && tempColor.B == 3) ||
                    (tempColor.R == 15 && tempColor.G == 7 && tempColor.B == 6))
                {
                    MainWindow.getInstance.SetStateString(curIndex, -i, tempColor);
                    LR = "L";
                    SK.sendkeyLeft(i * 2 / 3);
                    attackFlag = true;
                    break;
                }
                if ((tempColor2.R >= 99 && tempColor2.R <= 115 && tempColor2.G >= 48 && tempColor2.G <= 53 && tempColor2.B >= 29 && tempColor2.B <= 38) ||
                    (tempColor2.R == 99 && tempColor2.G == 84 && tempColor2.B == 68) ||
                    (tempColor2.R == 104 && tempColor2.G == 89 && tempColor2.B == 66) ||
                    (tempColor2.R == 27 && tempColor2.G == 20 && tempColor2.B == 16) ||
                    (tempColor2.R == 99 && tempColor2.G == 88 && tempColor2.B == 68) ||
                    (tempColor2.R == 86 && tempColor2.G == 76 && tempColor2.B == 58) ||
                    (tempColor2.R == 140 && tempColor2.G == 121 && tempColor2.B == 90) ||
                    (tempColor2.R == 100 && tempColor2.G == 86 && tempColor2.B == 67) ||
                    (tempColor2.R == 137 && tempColor2.G == 122 && tempColor2.B == 88) ||
                    (tempColor2.R == 57 && tempColor2.G == 52 && tempColor2.B == 33) ||
                    (tempColor2.R == 33 && tempColor2.G == 16 && tempColor2.B == 16) ||
                    (tempColor2.R == 8 && tempColor2.G == 0 && tempColor2.B == 0) ||
                    (tempColor2.R == 75 && tempColor2.G == 66 && tempColor2.B == 51) ||
                    (tempColor2.R == 0 && tempColor2.G == 4 && tempColor2.B == 0) ||
                    (tempColor2.R == 4 && tempColor2.G == 3 && tempColor2.B == 3) ||
                    (tempColor2.R == 15 && tempColor2.G == 7 && tempColor2.B == 6))
                {
                    MainWindow.getInstance.SetStateString(curIndex, -i, tempColor);
                    LR = "R";
                    SK.sendkeyRight(i * 2 / 3);
                    attackFlag = true;
                    break;
                }
                if (i > mobDistMax - 10)
                {
                    if (lastPosX > 50)
                    {
                        SK.sendkeyLeft(100);
                    }
                    else
                    {
                        SK.sendkeyRight(100);
                    }
                }
            }


            if (attackFlag)
            {
                SK.sendkeyControl(20);
                if (randomNum1to100 < 90)
                {
                    Thread.Sleep(600);
                    SK.sendkeyControl(20);
                    Thread.Sleep(100);
                }
            }

            if (randomNum1to100 == 50 || randomNum1to100 == 51)
            {
                //random_F1toF5();
            }

        }
        private void dongbasan_mid_attack()
        {
            int randomNum1to100 = randomNum.Next(1, 100);
            bool attackFlag = false;

            if (lastPosX < 170)
            {
                int mobDistMin = 10;
                int mobDistMax = 400;
                for (int i = mobDistMin; i < mobDistMax; i = i + (i / 100 + 3))
                {
                    Color tempColor = GetColorAt(970 - i, 747);
                    Color tempColor2 = GetColorAt(970 + i, 747);
                    // 154, 137,121
                    if ((tempColor.R >= 99 && tempColor.R <= 115 && tempColor.G >= 48 && tempColor.G <= 53 && tempColor.B >= 29 && tempColor.B <= 38) ||
                    (tempColor.R == 99 && tempColor.G == 84 && tempColor.B == 68) ||
                    (tempColor.R == 104 && tempColor.G == 89 && tempColor.B == 66) ||
                    (tempColor.R == 27 && tempColor.G == 20 && tempColor.B == 16) ||
                    (tempColor.R == 99 && tempColor.G == 88 && tempColor.B == 68) ||
                    (tempColor.R == 86 && tempColor.G == 76 && tempColor.B == 58) ||
                    (tempColor.R == 140 && tempColor.G == 121 && tempColor.B == 90) ||
                    (tempColor.R == 100 && tempColor.G == 86 && tempColor.B == 67) ||
                    (tempColor.R == 137 && tempColor.G == 122 && tempColor.B == 88) ||
                    (tempColor.R == 57 && tempColor.G == 52 && tempColor.B == 33) ||
                    (tempColor.R == 33 && tempColor.G == 16 && tempColor.B == 16) ||
                    (tempColor.R == 8 && tempColor.G == 0 && tempColor.B == 0) ||
                    (tempColor.R == 75 && tempColor.G == 66 && tempColor.B == 51) ||
                    (tempColor.R == 0 && tempColor.G == 4 && tempColor.B == 0) ||
                    (tempColor.R == 4 && tempColor.G == 3 && tempColor.B == 3) ||
                    (tempColor.R == 15 && tempColor.G == 7 && tempColor.B == 6))
                    {
                        //MainWindow.getInstance.SetStateString(-i, 271, tempColor);
                        LR = "L";
                        SK.sendkeyLeft(i * 2 / 3);
                        attackFlag = true;
                        break;
                    }
                    if ((tempColor2.R >= 99 && tempColor2.R <= 115 && tempColor2.G >= 48 && tempColor2.G <= 53 && tempColor2.B >= 29 && tempColor2.B <= 38) ||
                    (tempColor2.R == 99 && tempColor2.G == 84 && tempColor2.B == 68) ||
                    (tempColor2.R == 104 && tempColor2.G == 89 && tempColor2.B == 66) ||
                    (tempColor2.R == 27 && tempColor2.G == 20 && tempColor2.B == 16) ||
                    (tempColor2.R == 99 && tempColor2.G == 88 && tempColor2.B == 68) ||
                    (tempColor2.R == 86 && tempColor2.G == 76 && tempColor2.B == 58) ||
                    (tempColor2.R == 140 && tempColor2.G == 121 && tempColor2.B == 90) ||
                    (tempColor2.R == 100 && tempColor2.G == 86 && tempColor2.B == 67) ||
                    (tempColor2.R == 137 && tempColor2.G == 122 && tempColor2.B == 88) ||
                    (tempColor2.R == 57 && tempColor2.G == 52 && tempColor2.B == 33) ||
                    (tempColor2.R == 33 && tempColor2.G == 16 && tempColor2.B == 16) ||
                    (tempColor2.R == 8 && tempColor2.G == 0 && tempColor2.B == 0) ||
                    (tempColor2.R == 75 && tempColor2.G == 66 && tempColor2.B == 51) ||
                    (tempColor2.R == 0 && tempColor2.G == 4 && tempColor2.B == 0) ||
                    (tempColor2.R == 4 && tempColor2.G == 3 && tempColor2.B == 3) ||
                    (tempColor2.R == 15 && tempColor2.G == 7 && tempColor2.B == 6))
                    {
                        //MainWindow.getInstance.SetStateString(i, 271, tempColor);
                        LR = "R";
                        SK.sendkeyRight(i * 2 / 3);
                        attackFlag = true;
                        break;
                    }
                    if (i > mobDistMax - 10)
                    {
                        if (lastPosX < 145)
                        {
                            SK.sendkeyRight(230);
                        }
                        else
                        {
                            SK.sendkeyLeft(200);
                        }
                    }
                }

            }


            if (attackFlag)
            {
                if (randomNum1to100 < 10)
                {
                    Thread.Sleep(100);
                    SK.sendkeyDelete(20);
                    Thread.Sleep(600);
                    SK.sendkeyControl(20);
                }
                else
                {
                    Thread.Sleep(100);
                    SK.sendkeyControl(20);
                    Thread.Sleep(600);
                    SK.sendkeyControl(20);
                    Thread.Sleep(100);
                }
            }


            if (randomNum1to100 == 50 || randomNum1to100 == 51)
            {
                //random_F1toF5();
            }


        }
        private void dongbasan_right_attack()
        {

            int curIndex = 0;
            if (lastPosX < 170)
            {
                curIndex = 960;
            }
            else
            {
                curIndex = (int)((lastPosX - 170) * 12) + 960;
            }

            bool attackFlag = false;

            //1150

            int mobDistMin = 10;
            int mobDistMax = 400;
            for (int i = mobDistMin; i < mobDistMax; i = i + (i / 100 + 3))
            {
                int leftX = curIndex - i;
                int rightX = curIndex + i;

                if (rightX > 1919)
                {
                    rightX = 1919;
                }
                if (leftX < 950)
                {
                    leftX = 950;
                }

                Color tempColor = GetColorAt(leftX, 747);
                Color tempColor2 = GetColorAt(rightX, 747);
                // 154, 137,121
                if ((tempColor.R >= 99 && tempColor.R <= 115 && tempColor.G >= 48 && tempColor.G <= 53 && tempColor.B >= 29 && tempColor.B <= 38) ||
                    (tempColor.R == 99 && tempColor.G == 84 && tempColor.B == 68) ||
                    (tempColor.R == 104 && tempColor.G == 89 && tempColor.B == 66) ||
                    (tempColor.R == 27 && tempColor.G == 20 && tempColor.B == 16) ||
                    (tempColor.R == 99 && tempColor.G == 88 && tempColor.B == 68) ||
                    (tempColor.R == 86 && tempColor.G == 76 && tempColor.B == 58) ||
                    (tempColor.R == 140 && tempColor.G == 121 && tempColor.B == 90) ||
                    (tempColor.R == 100 && tempColor.G == 86 && tempColor.B == 67) ||
                    (tempColor.R == 137 && tempColor.G == 122 && tempColor.B == 88) ||
                    (tempColor.R == 57 && tempColor.G == 52 && tempColor.B == 33) ||
                    (tempColor.R == 33 && tempColor.G == 16 && tempColor.B == 16) ||
                    (tempColor.R == 8 && tempColor.G == 0 && tempColor.B == 0) ||
                    (tempColor.R == 75 && tempColor.G == 66 && tempColor.B == 51) ||
                    (tempColor.R == 0 && tempColor.G == 4 && tempColor.B == 0) ||
                    (tempColor.R == 4 && tempColor.G == 3 && tempColor.B == 3) ||
                    (tempColor.R == 15 && tempColor.G == 7 && tempColor.B == 6))
                {
                    //MainWindow.getInstance.SetStateString(-i, 271, tempColor);
                    LR = "L";
                    SK.sendkeyLeft(i / 3 * 2);
                    attackFlag = true;
                    break;
                }
                if ((tempColor2.R >= 99 && tempColor2.R <= 115 && tempColor2.G >= 48 && tempColor2.G <= 53 && tempColor2.B >= 29 && tempColor2.B <= 38) ||
                    (tempColor2.R == 99 && tempColor2.G == 84 && tempColor2.B == 68) ||
                    (tempColor2.R == 104 && tempColor2.G == 89 && tempColor2.B == 66) ||
                    (tempColor2.R == 27 && tempColor2.G == 20 && tempColor2.B == 16) ||
                    (tempColor2.R == 99 && tempColor2.G == 88 && tempColor2.B == 68) ||
                    (tempColor2.R == 86 && tempColor2.G == 76 && tempColor2.B == 58) ||
                    (tempColor2.R == 140 && tempColor2.G == 121 && tempColor2.B == 90) ||
                    (tempColor2.R == 100 && tempColor2.G == 86 && tempColor2.B == 67) ||
                    (tempColor2.R == 137 && tempColor2.G == 122 && tempColor2.B == 88) ||
                    (tempColor2.R == 57 && tempColor2.G == 52 && tempColor2.B == 33) ||
                    (tempColor2.R == 33 && tempColor2.G == 16 && tempColor2.B == 16) ||
                    (tempColor2.R == 8 && tempColor2.G == 0 && tempColor2.B == 0) ||
                    (tempColor2.R == 75 && tempColor2.G == 66 && tempColor2.B == 51) ||
                    (tempColor2.R == 0 && tempColor2.G == 4 && tempColor2.B == 0) ||
                    (tempColor2.R == 4 && tempColor2.G == 3 && tempColor2.B == 3) ||
                    (tempColor2.R == 15 && tempColor2.G == 7 && tempColor2.B == 6))
                {
                    //MainWindow.getInstance.SetStateString(i, 271, tempColor);
                    LR = "R";
                    SK.sendkeyRight(i / 3 * 2);
                    attackFlag = true;
                    break;
                }

                if (i > mobDistMax - 10)
                {
                    if (lastPosX > 210)
                    {
                        SK.sendkeyLeft(100);
                    }
                    else
                    {
                        SK.sendkeyRight(100);
                    }
                }
            }



            int randomNum1to100 = randomNum.Next(1, 100);

            if (attackFlag)
            {
                if (randomNum1to100 < 3)
                {
                    SK.sendkeyEnd(20);
                }

                SK.sendkeyControl(20);
                if (randomNum1to100 < 88)
                {
                    Thread.Sleep(600);
                    SK.sendkeyControl(20);
                }
            }


            if (randomNum1to100 == 50 || randomNum1to100 == 51)
            {
                //random_F1toF5();
            }
        }




        private void yellen_buff()
        {
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);

            Color curColor7 = GetColorAt(1888, 145);//181,102,84
            Color curColor8 = GetColorAt(1820, 145);//181,102,84
            Color curColor9 = GetColorAt(1752, 145);//181,102,84

            if (!((curColor7.R == 64 && curColor7.G == 64 && curColor7.B == 64) || (curColor8.R == 64 && curColor8.G == 64 && curColor8.B == 64) || (curColor9.R == 64 && curColor9.G == 64 && curColor9.B == 64)))
            {
                SK.sendkeyHome(20);
            }

            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                SK.sendkeyInsert(20);
            }

            Color curColorRed = GetColorAt(550, 1054);
            Color curColorBlue = GetColorAt(706, 1054);
            if ((curColorRed.R == 190 && curColorRed.G == 190 && curColorRed.B == 190))
            {
                SK.sendkeyPageUp(20);
            }
            if ((curColorBlue.R == 190 && curColorBlue.G == 190 && curColorBlue.B == 190))
            {
                SK.sendkeyPageDown(20);
            }
        }
        private void reagan_buff()
        {
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);
            /*
            Color curColor7 = GetColorAt(1888, 145);//181,102,84
            Color curColor8 = GetColorAt(1820, 145);//181,102,84
            Color curColor9 = GetColorAt(1752, 145);//181,102,84

            if (!((curColor7.R == 64 && curColor7.G == 64 && curColor7.B == 64) || (curColor8.R == 64 && curColor8.G == 64 && curColor8.B == 64) || (curColor9.R == 64 && curColor9.G == 64 && curColor9.B == 64)))
            {
                Thread.Sleep(200);
                User32.API.keybd_event(0X24, 0, 0, 0); // home
                Thread.Sleep(200);
                User32.API.keybd_event(0X24, 0, KEYEVENTF_KEYUP, 0); // home
                Thread.Sleep(50);
            }*/

            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                SK.sendkeyInsert(20);
            }

            Color curColorRed = GetColorAt(550, 1054);
            Color curColorBlue = GetColorAt(706, 1054);
            if ((curColorRed.R == 190 && curColorRed.G == 190 && curColorRed.B == 190))
            {
                SK.sendkeyPageUp(20);
            }
            if ((curColorBlue.R == 190 && curColorBlue.G == 190 && curColorBlue.B == 190))
            {
                SK.sendkeyPageDown(20);

            }
        }
        private void volker_buff()
        {
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);

            Color curColor4 = GetColorAt(1826, 147); //
            Color curColor5 = GetColorAt(1758, 147);
            Color curColor6 = GetColorAt(1894, 147);


            /*
            Color curColor7 = GetColorAt(1888, 145);//181,102,84
            Color curColor8 = GetColorAt(1820, 145);//181,102,84
            Color curColor9 = GetColorAt(1752, 145);//181,102,84

            if (!((curColor7.R == 181 && curColor7.G == 102 && curColor7.B == 84) || (curColor8.R == 181 && curColor8.G == 102 && curColor8.B == 84) || (curColor9.R == 181 && curColor9.G == 102 && curColor9.B == 84)))
            {
                Thread.Sleep(200);
                User32.API.keybd_event(0X24, 0, 0, 0);
                Thread.Sleep(200);
                User32.API.keybd_event(0X24, 0, KEYEVENTF_KEYUP, 0);
                Thread.Sleep(50);
            }*/

            if (!((curColor4.R == 85 && curColor4.G == 85 && curColor4.B == 85) || (curColor5.R == 85 && curColor5.G == 85 && curColor5.B == 85) || (curColor6.R == 85 && curColor6.G == 85 && curColor6.B == 85)))
            {
                SK.sendkeyHome(40);
            }

            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                SK.sendkeyInsert(40);
            }

            Color curColorRed = GetColorAt(550, 1054);
            Color curColorBlue = GetColorAt(706, 1054);
            if ((curColorRed.R == 190 && curColorRed.G == 190 && curColorRed.B == 190))
            {
                SK.sendkeyPageUp(10);
            }
            if ((curColorBlue.R == 190 && curColorBlue.G == 190 && curColorBlue.B == 190))
            {
                SK.sendkeyPageDown(10);
            }

        }

        private void Bernanke_buff()
        {
            int buffStackCount = 5;
            Color curColor1 = GetColorAt(1876, 145);
            List<Color> colorList1 = new List<Color>();
            List<Color> colorList2 = new List<Color>();

            for (int i = 0; i < buffStackCount; i++)
            {
                colorList1.Add(GetColorAt(1876 - (68 * i), 145));
            }
            for (int i = 0; i < buffStackCount; i++)
            {
                colorList2.Add(GetColorAt(1872 - (68 * i), 145));
            }

            for (int i = 0; i < buffStackCount; i++)
            {
                if ((colorList1[i].R == 182 && colorList1[i].G == 195 && colorList1[i].B == 203))
                {
                    break;
                }
                if (i == buffStackCount - 1)
                {
                    SK.sendkeyHome(40);
                }
            }
            for (int i = 0; i < buffStackCount; i++)
            {
                if ((colorList2[i].R == 38 && colorList2[i].G == 38 && colorList2[i].B == 38))
                {
                    break;
                }
                if (i == buffStackCount - 1)
                {
                    SK.sendkeyInsert(40);
                }
            }


            Color curColorRed = GetColorAt(550, 1054);
            Color curColorBlue = GetColorAt(706, 1054);
            if ((curColorRed.R == 190 && curColorRed.G == 190 && curColorRed.B == 190))
            {
                SK.sendkeyPageUp(10);
            }
            if ((curColorBlue.R == 190 && curColorBlue.G == 190 && curColorBlue.B == 190))
            {
                SK.sendkeyPageDown(10);
            }

            Color curColorRedWarning = GetColorAt(462, 1054);
            if ((curColorRedWarning.R == 190 && curColorRedWarning.G == 190 && curColorRedWarning.B == 190))
            {
                Thread.Sleep(100);
                User32.API.keybd_event(0XA4, 0, 0, 0);
                Thread.Sleep(100);
                User32.API.keybd_event(0X73, 0, 0, 0);
                Thread.Sleep(100);
                User32.API.keybd_event(0X73, 0, 0x0002, 0);
                Thread.Sleep(100);
                User32.API.keybd_event(0XA4, 0, 0x0002, 0);
                Thread.Sleep(100);
            }

        }







        private void reagan_buff_fury()
        {
            Color curColor1 = GetColorAt(70, 925);
            Color curColor2 = GetColorAt(48, 925);
            string tempLR = LR;

            if ((curColor1.R == 0 && curColor1.G == 255 && curColor1.B == 0) || (curColor2.R == 0 && curColor2.G == 255 && curColor2.B == 0))
            {
                User32.API.keybd_event(0X25, 0, 0x0002, 0);
                User32.API.keybd_event(0X27, 0, 0x0002, 0);
                
                tempLR = LR;
                LR = "STOP";
                Thread.Sleep(100);
                SK.sendkeyEnter(100);
                Thread.Sleep(100);
                sendkey("/vkxlcheo qjsodzl");
                Thread.Sleep(100);
                SK.sendkeyEnter(100);
                Thread.Sleep(500);
                SK.sendkeyEsc(50);
                SK.sendkeyEsc(50);
                Thread.Sleep(100);
                SK.sendkeyShift(100);
                Thread.Sleep(800);
                SK.sendkeyEnter(100);
                Thread.Sleep(100);
                sendkey("/vkxlrkdxhl qjsodzl");
                Thread.Sleep(100);
                SK.sendkeyEnter(100);
                Thread.Sleep(100);
                SK.sendkeyEnter(100);
                Thread.Sleep(100);
                sendkey("/vkxlrkdxhl qjsodzl");
                Thread.Sleep(100);
                SK.sendkeyEnter(100);
                Thread.Sleep(4000);
                
            }


            SK.sendkeyEsc(10);
            SK.sendkeyEsc(10);

            LR = tempLR;

            Color curColorRed = GetColorAt(550, 1054);
            Color curColorBlue = GetColorAt(706, 1054);
            if ((curColorRed.R == 190 && curColorRed.G == 190 && curColorRed.B == 190))
            {
                SK.sendkeyPageUp(20);
            }
            if ((curColorBlue.R == 190 && curColorBlue.G == 190 && curColorBlue.B == 190))
            {
                SK.sendkeyPageDown(20);

            }
        }

        private static void sendkey(string str)
        {
            //IntPtr targetWindowHandle = GetTargetWindowHandle();

            try
            {
                SendKeys.SendWait(str);
            }
            catch
            {

            }

        }




        private void AltAndDelete()
        {
            SK.sendkeyAlt(15);
            SK.sendkeyDelete(10);
        }

        private void Bernanke_mid_attack()
        {
            int changeNum = 90;
            sleepCount++;
            SK.sendkeyZ(1);
            /*
            if(sleepCount < changeNum && L_Flag == false)
            {
                L_Flag = true;
                User32.API.keybd_event(0X27, 0, 0x0002, 0);
                User32.API.keybd_event(0X25, 0, 0, 0);
                R_Flag = false;
            }
            else if (sleepCount >= changeNum && R_Flag == false)
            {
                L_Flag = false;
                R_Flag = true;
                User32.API.keybd_event(0X25, 0, 0x0002, 0);
                User32.API.keybd_event(0X27, 0, 0, 0);
            }*/

            if (sleepCount % 6 == 0 && intervalCount == 0)
            {
                AltAndDelete();
            }

            if (LR == "L")
            {
                if (last_LR == "R")
                {
                    sleepCount = 0;
                    intervalCount++;
                }
                if (intervalCount > 13)
                {
                    intervalCount = 0;
                    User32.API.keybd_event(0X27, 0, 0x0002, 0);
                    User32.API.keybd_event(0X25, 0, 0, 0);
                    last_LR = "L";
                }
                
            }
            else if (LR == "R")
            {
                
                if (last_LR == "L")
                {
                    sleepCount = 0;
                    intervalCount++;
                }
                if (intervalCount > 13)
                {
                    intervalCount = 0;
                    User32.API.keybd_event(0X25, 0, 0x0002, 0);
                    User32.API.keybd_event(0X27, 0, 0, 0);
                    last_LR = "R";
                }
            }
            else
            {
                User32.API.keybd_event(0X25, 0, 0, 0);
                User32.API.keybd_event(0X27, 0, 0, 0);
            }



            

            if (sleepCount > changeNum)
            {
                sleepCount = 0;
                if (LR == "L")
                {
                    LR = "R";
                }
                else if (LR == "L")
                {
                    LR = "L";
                }
            }
        }


    }

    public class stopPoint
    {
        public int x;
        public int y;
        public Color color;
        public string LR;
    }
    public class DataObjectClass
    {
        public DataObject dataObject;
        public string type;
    }



}
