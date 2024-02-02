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

        private static int sleepCount = 100;
        private static int sleepCountMax = 0;

        private static int lastPosX = 0;
        private static System.Random randomNum = new System.Random((int)DateTime.Now.Ticks);

        private static List<String> list;
        private static List<String> parameterList;
        private static List<stopPoint> stopPointList;

        private CorrectString correctString;
        private FindAndAlert findAndAlert;
        private MultiClipboard multiClipboard;
        private PasteAlert pasteAlert;
        private ClipboardMonitor clipboardMonitor;


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

        const int KEYEVENTF_KEYDOWN = 0x0000;
        const int KEYEVENTF_KEYUP = 0x0002;

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
            VK_MENU = 0X12, //ALT
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

            WM_PASTE = 0x302
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


        private static void sendkeyLeft(int time)
        {
            User32.API.keybd_event(0X25, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X25, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyRight(int time)
        {
            User32.API.keybd_event(0X27, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X27, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyUp(int time)
        {
            User32.API.keybd_event(0X26, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X26, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyDown(int time)
        {
            User32.API.keybd_event(0X28, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X28, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyPageUp(int time)
        {
            User32.API.keybd_event(0X21, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X21, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyPageDown(int time)
        {
            User32.API.keybd_event(0X22, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X22, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyInsert(int time)
        {
            User32.API.keybd_event(0X2D, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X2D, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyHome(int time)
        {
            User32.API.keybd_event(0X24, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X24, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyDelete(int time)
        {
            User32.API.keybd_event(0X2E, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X2E, 0, KEYEVENTF_KEYUP, 0);
        }
        private static void sendkeyEnd(int time)
        {
            User32.API.keybd_event(0X23, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X23, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyShift(int time)
        {
            User32.API.keybd_event(0X22, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X22, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyControl(int time)
        {
            User32.API.keybd_event(0XA2, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0XA2, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void sendkeyZ(int time)
        {
            User32.API.keybd_event(0X5A, 0, 0, 0);
            Thread.Sleep(time * 5);
            User32.API.keybd_event(0X5A, 0, KEYEVENTF_KEYUP, 0);
        }



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
            /*
            if (form.recordFlag == true)
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
            //MessageBox.Show(tempKey.ToString().ToUpper() + "");
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

            else if (tempKey.ToString().ToUpper() == "F8")
            {
                if (Macro.getInstance.Flag_F8)
                {
                    Macro.getInstance.Flag_F8 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F8 = true;
                    Macro_F8();
                }
            }
            if (tempKey.ToString().ToUpper() == "F9")
            {
                if (Macro.getInstance.Flag_F9)
                {
                    Macro.getInstance.Flag_F9 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F9 = true;
                    Macro_F9();
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
                    Macro_F10();
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
                    Macro.getInstance.Flag_F11 = true;
                    Macro_F11();
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
                    Macro_F12();
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

        }



        private static void getWindow()
        {
            IntPtr zero = IntPtr.Zero;
            IntPtr curWindow = User32.API.GetForegroundWindow();


            /*
            for (int i = 0; (i < 60) && (zero == IntPtr.Zero); i++)
            {
                Thread.Sleep(500);
                zero = API.FindWindow(null, "YourWindowName");
            }
            */


            if (curWindow != null)
            {
                User32.API.SetForegroundWindow(curWindow);
                sendkey("{A 10}");
                //SendKeys.Flush();
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

        public static void create()
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
                    try
                    {
                        //동바산 중앙 레이건
                        //getCurPos(tokenSource2);

                        //동바산 좌측 옐런
                        //getCurPos2(tokenSource2);

                        //동바산 우측 레이건
                        if (Macro.getInstance.Flag_F12)
                        {
                            getCurPos3(tokenSource2);
                            sendkeyZ(5);
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
                Macro_F8();
            }
            if (Macro.getInstance.Flag_F9)
            {
                Macro_F9();
            }
            if (Macro.getInstance.Flag_F10)
            {
                Macro_F10();
            }
            if (Macro.getInstance.Flag_F11)
            {
                Macro_F11();
            }
            if (Macro.getInstance.Flag_F12)
            {
                Macro_F12();
            }
            else
            {

            }

            Macro.getInstance.Flag1 = false;
            Macro.getInstance.Flag2 = false;
            Macro.getInstance.Flag3 = false;
            Macro.getInstance.Flag4 = false;
            Macro.getInstance.Flag5 = false;

            Macro.getInstance.Flag_F13 = false;
            Macro.getInstance.Flag_F21 = false;
            Macro.getInstance.Flag_F31 = false;
            Macro.getInstance.Flag_F41 = false;

            Macro.getInstance.Flag_F5 = false;
            Macro.getInstance.Flag_F6 = false;
            Macro.getInstance.Flag_F7 = false;
            Macro.getInstance.Flag_F8 = false;

            //Macro.getInstance.Flag_F9 = false;
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

        private static void Macro_F9()
        {

            //개미굴 거실 표도 고정
            for (int i = 0; i < 3; i++)
            {
                Color curLeftColor = GetColorAt(375 + i, 283);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    //sendkey("{RIGHT 1}");
                    movingFlag = true;
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curRightColor = GetColorAt(394 - i, 283);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    //sendkey("{LEFT 1}");
                    movingFlag = true;
                    LR = "L";
                    break;
                }
            }

            Color curCenterColor = GetColorAt(382, 285);

            if (curCenterColor.R == 255 && curCenterColor.G == 255 && movingFlag)
            {
                sendkey("{LEFT 1}");
                movingFlag = false;
            }

            if (movingFlag)
            {
                if (LR == "L")
                {
                    sendkey("{LEFT 10}");
                }
                else if (LR == "R")
                {
                    sendkey("{RIGHT 20}");
                }
            }

            sendkey("z");
        }


        //개미굴 전사 중간 넓은자리 고정
        private static void Macro_F10()
        {


            rejoin();



            /*
            //이동 여부 판단 
            for (int i = 0; i < 35; i++)
            {
                Color curLeftColor = GetColorAt(310 + i, 296);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    //sendkey("{RIGHT 1}");
                    LR = "R";
                    break;
                }
            }

            for (int i = 0; i < 1; i++)
            {
                System.Random random = new System.Random();
                int randomNum = random.Next(1, 3);
                if(randomNum != 3)
                {
                    break;
                }
                Color curRightColor = GetColorAt(370 - i, 293);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            for (int i = 0; i < 20; i++)
            {
                Color curRightColor = GetColorAt(402 - i, 294);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curRightColor = GetColorAt(415 - i, 294);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }

            // 스킬 확인
            // 1801 147  // 208 208 191
            // 1873 147  // 208 208 191
            Color curColor1 = GetColorAt(1801, 147);
            Color curColor2 = GetColorAt(1873, 147);
            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191)))
            {
                sendkey("{INSERT}");
            }
            // 물약확인
            // 510 1054 // 238,0,0
            // 725 1054 // 0,143,238
            Color curColor3 = GetColorAt(510, 1054);
            Color curColor4 = GetColorAt(725, 1054);
            if (!(curColor3.R == 238 && curColor3.G == 0 && curColor3.B == 0))
            {
                sendkey("{PGUP}");
            }
            if (!(curColor4.R == 0 && curColor4.G == 143 && curColor4.B == 238))
            {
                sendkey("{PGDN}");
            }

            //이동
            if (LR == "L")
            {
                sendkey("{LEFT 55}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 55}");
            }

            sendkey("z");
        
            }
            */
        }


        private static void Macro_F11()
        {

            //개미굴 전사 아랫층 넓은자리 고정(노트북)
            //gg1();

            //개미굴 전사 아랫층 넓은자리 고정(방안 메인컴)

            //gg2();

            //와보땅1 전사 아랫층 넓은자리 고정(방안 메인컴)
            //gg3();

            //와보땅1 전사 아랫층 넓은자리 고정(거실컴)
            //gg4();


        }

        private static void Macro_F12()
        {
            //동바산6 옐런 아랫층(거실컴)
            //gg7();

            //동바산6 아래층 레이건
            //rr1();

            //동바산6 아래층 레이건
            try
            {
                //동바산6 아래층 레이건
                //rr3();
                //동바산6 아래층 좌측 옐런
                //rr4();
                //동바산6 아래층 우측 레이건
                rr5();
            }
            catch
            {

            }

        }


        private static Color[] getColor()
        {
            Point p = getMousePosAndColor();
            //Color curColor = GetColorAt(p.X, p.Y);
            Color curColor1 = GetColorAt(x1, y1);
            Color curColor2 = GetColorAt(x2, y2);

            if (!(curColor1.R == R1 || curColor1.G == G1 || curColor1.B == B1))
            {
                global_LR = 1;
            }
            else if (!(curColor2.R == R2 || curColor2.G == G2 || curColor2.B == B2))
            {
                global_LR = 2;
            }
            else
            {
                global_LR = 3;
            }
            Color[] arr = new Color[2];
            arr[0] = curColor1;
            arr[1] = curColor2;

            return arr;
        }

        private static void getColor2()
        {


            for (int i = 0; i <= 30; i++)
            {
                Color curColor1 = GetColorAt(x1 + i, y1);
                Color curColor2 = GetColorAt(x1 - i, y1);
                //I Found Same Color
                if ((curColor1.R == R1 && curColor1.G == G1 && curColor1.B == B1))
                {
                    global_LR = 1;
                    return;
                }
                if ((curColor2.R == R1 && curColor2.G == G1 && curColor2.B == B1))
                {
                    global_LR = 2;
                    return;
                }
            }
            global_LR = 3;

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

        private static void gg1()
        {
            for (int i = 0; i < 3; i++)
            {
                Color curLeftColor = GetColorAt(394 + i, 427);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curRightColor = GetColorAt(543 - i, 424);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curRightColor2 = GetColorAt(554 - i, 418);
                if (curRightColor2.R == 255 && curRightColor2.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            if (LR == "L")
            {
                sendkey("{LEFT 55}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 55}");
            }
        }
        private static void gg2()
        {
            for (int i = 0; i < 3; i++)
            {
                Color curLeftColor = GetColorAt(294 + i, 319);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curLeftColor = GetColorAt(316 + i, 319);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Color curLeftColor = GetColorAt(336 + i, 319);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Color curRightColor = GetColorAt(407 - i, 319);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Color curRightColor2 = GetColorAt(416 - i, 314);
                if (curRightColor2.R == 255 && curRightColor2.G == 255)
                {
                    LR = "L";
                    break;
                }
            }
            if (LR == "L")
            {
                sendkey("{LEFT 55}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 55}");
            }

            //
            // 스킬 확인
            // 1801 147  // 208 208 191
            // 1873 147  // 208 208 191
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);
            Color curColor4 = GetColorAt(1758, 147);
            Color curColor5 = GetColorAt(1826, 147);
            Color curColor6 = GetColorAt(1894, 147);
            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                sendkey("{INSERT}");
            }
            if (!((curColor4.R == 85 && curColor4.G == 85 && curColor4.B == 85) || (curColor5.R == 85 && curColor5.G == 85 && curColor5.B == 85) || (curColor6.R == 85 && curColor6.G == 85 && curColor6.B == 85)))
            {
                sendkey("{HOME}");
            }
            // 물약확인
            // 510 1054 // 238,0,0
            // 725 1054 // 0,143,238
            Color curColorRed = GetColorAt(510, 1054);
            Color curColorBlue = GetColorAt(725, 1054);
            if (!(curColorRed.R == 238 && curColorRed.G == 0 && curColorRed.B == 0))
            {
                sendkey("{PGUP}");
            }
            if (!(curColorBlue.R == 0 && curColorBlue.G == 143 && curColorBlue.B == 238))
            {
                sendkey("{PGDN}");
            }
        }

        private static void gg3()
        {
            for (int i = 0; i < 3; i++)
            {
                Color curLeftColor = GetColorAt(43 + i, 284);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Color curRightColor = GetColorAt(327 - i, 284);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }

            if (LR == "L")
            {
                sendkey("{LEFT 120}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 120}");
            }

            //
            // 스킬 확인
            // 1801 147  // 208 208 191
            // 1873 147  // 208 208 191
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);
            Color curColor4 = GetColorAt(1758, 147);
            Color curColor5 = GetColorAt(1826, 147);
            Color curColor6 = GetColorAt(1894, 147);
            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                sendkey("{INSERT}");
            }
            if (!((curColor4.R == 85 && curColor4.G == 85 && curColor4.B == 85) || (curColor5.R == 85 && curColor5.G == 85 && curColor5.B == 85) || (curColor6.R == 85 && curColor6.G == 85 && curColor6.B == 85)))
            {
                sendkey("{HOME}");
            }
            // 물약확인
            // 510 1054 // 238,0,0
            // 725 1054 // 0,143,238
            Color curColorRed = GetColorAt(510, 1054);
            Color curColorBlue = GetColorAt(725, 1054);
            if (!(curColorRed.R == 238 && curColorRed.G == 0 && curColorRed.B == 0))
            {
                sendkey("{PGUP}");
            }
            if (!(curColorBlue.R == 0 && curColorBlue.G == 143 && curColorBlue.B == 238))
            {
                sendkey("{PGDN}");
            }
        }

        private static void gg4()
        {
            //
            sleepCount++;


            for (int i = 0; i < 40 && sleepCount > sleepCountMax; i = i + 5)
            {
                if (LR == "L")
                {
                    Color curLeftColor = GetColorAt(192 - i, 223);
                    if (curLeftColor.R == 255 && curLeftColor.G == 255)
                    {
                        sleepCount = 0;
                        LR = "R";
                        break;
                    }
                }
                else if (LR == "R")
                {
                    Color curRightColor2 = GetColorAt(207 + i, 223);
                    if (curRightColor2.R == 255 && curRightColor2.G == 255)
                    {
                        sleepCount = 0;
                        LR = "L";
                        break;
                    }
                }

                if (i < 10)
                {
                    sleepCountMax = 0;
                }
                else if (i >= 10 && i < 20)
                {
                    sleepCountMax = 1;
                }
                else if (i >= 20 && i < 30)
                {
                    sleepCountMax = 3;
                }
                else if (i >= 30 && i < 40)
                {
                    sleepCountMax = 5;
                }

            }



            if (LR == "L")
            {
                sendkey("{LEFT 100}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 100}");
            }
            //
            /*
            for (int i = 0; i < 45; i = i+1)
            {
                Color curLeftColor = GetColorAt(139 + i, 221);
                if (curLeftColor.R == 255 && curLeftColor.G == 255)
                {
                    LR = "R";
                    break;
                }
            }
            for (int i = 0; i < 45; i = i +1)
            {
                Color curRightColor = GetColorAt(254 - i, 221);
                if (curRightColor.R == 255 && curRightColor.G == 255)
                {
                    LR = "L";
                    break;
                }
            }

            if (LR == "L")
            {
                sendkey("{LEFT 100}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 100}");
            }
            */
            //
            //
            // 스킬 확인
            // 1801 147  // 208 208 191
            // 1873 147  // 208 208 191
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);
            Color curColor5 = GetColorAt(1826, 147);
            Color curColor4 = GetColorAt(1758, 147);
            Color curColor6 = GetColorAt(1894, 147);
            Color curColor7 = GetColorAt(1888, 145);//181,102,84
            Color curColor8 = GetColorAt(1820, 145);//181,102,84
            Color curColor9 = GetColorAt(1752, 145);//181,102,84
            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                sendkey("{INSERT}");
            }
            if (!((curColor4.R == 153 && curColor4.G == 153 && curColor4.B == 153) || (curColor5.R == 153 && curColor5.G == 153 && curColor5.B == 153) || (curColor6.R == 153 && curColor6.G == 153 && curColor6.B == 153)))
            {
                sendkey("{HOME}");
            }
            if (!((curColor7.R == 181 && curColor7.G == 102 && curColor7.B == 84) || (curColor8.R == 181 && curColor8.G == 102 && curColor8.B == 84) || (curColor9.R == 181 && curColor9.G == 102 && curColor9.B == 84)))
            {
                sendkey("+");
            }
            // 물약확인
            // 510 1054 // 238,0,0zzzzzzzzzzzzzzzzzzzzz
            // 725 1054 // 0,143,238
            Color curColorRed = GetColorAt(510, 1054);
            Color curColorBlue = GetColorAt(725, 1054);
            if (!(curColorRed.R == 238 && curColorRed.G == 0 && curColorRed.B == 0))
            {
                sendkey("{PGUP}");
            }
            if (!(curColorBlue.R == 0 && curColorBlue.G == 143 && curColorBlue.B == 238))
            {
                sendkey("{PGDN}");
            }


        }

        private static void gg7()
        {
            //
            sleepCount++;


            for (int i = 0; i < 5 && sleepCount > sleepCountMax; i = i + 4)
            {
                if (LR == "L")
                {
                    Color curLeftColor = GetColorAt(28 - i, 263);
                    if (curLeftColor.R == 255 && curLeftColor.G == 255)
                    {
                        sleepCount = 0;
                        LR = "R";
                        sendkey("{RIGHT 10}");
                        sendkey("{END}");
                        break;
                    }

                    Color curLeftColor2 = GetColorAt(93 - i, 269);
                    if (curLeftColor2.R == 255 && curLeftColor2.G == 255)
                    {
                        sleepCount = 0;
                        LR = "R";
                        sendkey("{END}");
                        sendkey("{RIGHT 10}");
                        break;
                    }
                }
                else if (LR == "R")
                {
                    Color curRightColor2 = GetColorAt(244 + i, 271);
                    if (curRightColor2.R == 255 && curRightColor2.G == 255)
                    {
                        sleepCount = 0;
                        LR = "L";
                        sendkey("{LEFT 10}");
                        sendkey("{END}");
                        break;
                    }
                }

                if (i < 10)
                {
                    sleepCountMax = 2;
                }

            }



            if (LR == "L")
            {
                sendkey("{LEFT 100}");
            }
            else if (LR == "R")
            {
                sendkey("{RIGHT 100}");
            }

            System.Random random = new System.Random((int)DateTime.Now.Ticks);
            int randomNum = random.Next(1, 100);
            if (randomNum >= 98)
            {
                sendkey("{END}");
            }
            if (randomNum < 20)
            {
                if (LR == "L")
                {
                    sendkey("{RIGHT 80}");
                    sendkey("^");
                }
                else if (LR == "R")
                {
                    sendkey("{LEFT 80}");
                    sendkey("^");
                }
            }
            if (randomNum == 11 || randomNum == 12 || randomNum == 13 || randomNum == 14)
            {
                random_F1toF5();
            }
            // 스킬 확인
            // 1801 147  // 208 208 191
            // 1873 147  // 208 208 191
            Color curColor1 = GetColorAt(1738, 147);
            Color curColor2 = GetColorAt(1801, 147);
            Color curColor3 = GetColorAt(1873, 147);

            Color curColor4 = GetColorAt(1826, 147); //
            Color curColor5 = GetColorAt(1758, 147);
            Color curColor6 = GetColorAt(1894, 147);

            Color curColor7 = GetColorAt(1826, 147);//233
            Color curColor8 = GetColorAt(1758, 147);//181,102,84
            Color curColor9 = GetColorAt(1894, 147);//


            if (!((curColor7.R == 233 && curColor7.G == 233 && curColor7.B == 233) || (curColor8.R == 233 && curColor8.G == 233 && curColor8.B == 233) || (curColor9.R == 233 && curColor9.G == 233 && curColor9.B == 233)))
            {
                sendkey("+");
            }
            if (!((curColor1.R == 208 && curColor1.G == 208 && curColor1.B == 191) || (curColor2.R == 208 && curColor2.G == 208 && curColor2.B == 191) || (curColor3.R == 208 && curColor3.G == 208 && curColor3.B == 191)))
            {
                sendkey("{INSERT}");
            }
            if (!((curColor4.R == 153 && curColor4.G == 153 && curColor4.B == 153) || (curColor5.R == 153 && curColor5.G == 153 && curColor5.B == 153) || (curColor6.R == 153 && curColor6.G == 153 && curColor6.B == 153)))
            {
                sendkey("{HOME}");
            }
            // 물약확인
            // 510 1054 // 238,0,0zzzzzzzzzzzzzzzzzzzzz
            // 725 1054 // 0,143,238
            Color curColorRed = GetColorAt(510, 1054);
            Color curColorBlue = GetColorAt(725, 1054);
            if (!(curColorRed.R == 238 && curColorRed.G == 0 && curColorRed.B == 0))
            {
                sendkey("{PGUP}");
            }
            if (!(curColorBlue.R == 0 && curColorBlue.G == 143 && curColorBlue.B == 238))
            {
                sendkey("{PGDN}");
            }


        }

        private static void rejoin()
        {

            Thread.Sleep(1000);
            User32.API.SetCursorPos(1718, 246);
            MouseClick();
            Thread.Sleep(1000);
            User32.API.SetCursorPos(1724, 326);
            MouseClick();
            Thread.Sleep(1000);
            User32.API.SetCursorPos(791, 615);
            MouseClick();
            Thread.Sleep(20000);
            User32.API.SetCursorPos(960, 633);
            MouseClick();
            Thread.Sleep(6000);
        }

        private static void MouseClick()
        {
            // 마우스 이벤트 발생 (왼쪽 버튼 클릭)
            User32.API.mouse_event(0x0002, 0, 0, 0, 0);
            User32.API.mouse_event(0x0004, 0, 0, 0, 0);
        }


        private static void getCurPos3(CancellationTokenSource ct)
        {
            Color curColorWall = new Color();
            Color curColorLeft = new Color();
            Color curColorRight = new Color();

            for (int i = 0; i <= 250; i = i + 2)
            {
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
                        sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 248)
                    {
                        sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorRight.R == 255 && curColorRight.G == 255)
                {
                    lastPosX = leftX;
                    if (lastPosX <= 170)
                    {
                        sendkeyRight(50);
                        LR = "R";
                    }
                    else if (lastPosX >= 248)
                    {
                        sendkeyLeft(50);
                        LR = "L";
                    }
                    break;
                }
                else if (curColorWall.R == 255 && curColorWall.G == 255)
                {
                    sendkeyRight(3050);
                    LR = "R";
                    break;
                }
            }
        }

        private static void rr5()
        {
            try
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
                    if ((tempColor.R == 101 && tempColor.G == 51 && tempColor.B == 35) ||
                        (tempColor.R == 99 && tempColor.G == 84 && tempColor.B == 68) ||
                        (tempColor.R == 104 && tempColor.G == 89 && tempColor.B == 66) ||
                        (tempColor.R == 27 && tempColor.G == 20 && tempColor.B == 16) ||
                        (tempColor.R == 99 && tempColor.G == 88 && tempColor.B == 68) ||
                        (tempColor.R == 86 && tempColor.G == 76 && tempColor.B == 58) ||
                        (tempColor.R == 140 && tempColor.G == 121 && tempColor.B == 90) ||
                        (tempColor.R == 100 && tempColor.G == 86 && tempColor.B == 67) ||
                        (tempColor.R == 137 && tempColor.G == 122 && tempColor.B == 88) ||
                        (tempColor.R == 57 && tempColor.G == 52 && tempColor.B == 33) ||
                        (tempColor.R == 15 && tempColor.G == 7 && tempColor.B == 6))
                    {
                        //MainWindow.getInstance.SetStateString(-i, 271, tempColor);
                        LR = "L";
                        sendkeyLeft(i / 3 * 2);
                        attackFlag = true;
                        break;
                    }
                    if ((tempColor2.R == 101 && tempColor2.G == 51 && tempColor2.B == 35) ||
                        (tempColor2.R == 99 && tempColor2.G == 84 && tempColor2.B == 68) ||
                        (tempColor2.R == 104 && tempColor2.G == 89 && tempColor2.B == 66) ||
                        (tempColor2.R == 27 && tempColor2.G == 20 && tempColor2.B == 16) ||
                        (tempColor2.R == 99 && tempColor2.G == 88 && tempColor2.B == 68) ||
                        (tempColor2.R == 86 && tempColor2.G == 76 && tempColor2.B == 58) ||
                        (tempColor2.R == 140 && tempColor2.G == 121 && tempColor2.B == 90) ||
                        (tempColor2.R == 100 && tempColor2.G == 86 && tempColor2.B == 67) ||
                        (tempColor2.R == 137 && tempColor2.G == 122 && tempColor2.B == 88) ||
                        (tempColor2.R == 57 && tempColor2.G == 52 && tempColor2.B == 33) ||
                        (tempColor2.R == 15 && tempColor2.G == 7 && tempColor2.B == 6))
                    {
                        //MainWindow.getInstance.SetStateString(i, 271, tempColor);
                        LR = "R";
                        sendkeyRight(i / 3 * 2);
                        attackFlag = true;
                        break;
                    }
                }



                int randomNum1to100 = randomNum.Next(1, 100);

                if (attackFlag)
                {
                    if (randomNum1to100 < 3)
                    {
                        sendkeyEnd(20);
                    }

                    sendkeyControl(20);
                    if (randomNum1to100 < 88)
                    {
                        Thread.Sleep(600);
                        sendkeyControl(20);
                    }
                }


                if (randomNum1to100 == 50 || randomNum1to100 == 51)
                {
                    //random_F1toF5();
                }

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
                    sendkeyInsert(20);
                }

                Color curColorRed = GetColorAt(510, 1054);
                Color curColorBlue = GetColorAt(725, 1054);
                if (!(curColorRed.R == 238 && curColorRed.G == 0 && curColorRed.B == 0))
                {
                    sendkeyPageUp(20);
                }
                if (!(curColorBlue.R == 0 && curColorBlue.G == 143 && curColorBlue.B == 238))
                {
                    sendkeyPageDown(20);
                }
            }
            catch
            {

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
