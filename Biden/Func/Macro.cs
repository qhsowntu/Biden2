using Biden.Func.Clone;
using Biden.Model;
using Biden.View;
using Biden.ViewModel;
using System;
using System.Collections;
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

using Newtonsoft.Json;
using static Community.CsharpSqlite.Sqlite3;
using System.Windows.Shapes;
using static IronPython.Modules._ast;
using System.Media;


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

        private bool Flag_DEL = false;
        private bool Flag_END = false;
        private bool Flag_PGDN = false;
        private bool Flag_PGUP = false;
        private bool Flag_INS = false;
        private bool Flag_HOME = false;

        private bool movingOpt = false;
        


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

        private static int R0 = 0;
        private static int G0 = 0;
        private static int B0 = 0;

        private static int R3 = 0;
        private static int G3 = 0;
        private static int B3 = 0;

        private static int R4 = 0;
        private static int G4 = 0;
        private static int B4 = 0;

        private static int R5 = 0;
        private static int G5 = 0;
        private static int B5 = 0;

        private static int R6 = 0;
        private static int G6 = 0;
        private static int B6 = 0;

        private static int titleR1 = 0;
        private static int titleG1 = 0;
        private static int titleB1 = 0;
        private static int titleR2 = 0;
        private static int titleG2 = 0;
        private static int titleB2 = 0;
        private static int titleR3 = 0;
        private static int titleG3 = 0;
        private static int titleB3 = 0;
        private static int titleR4 = 0;
        private static int titleG4 = 0;
        private static int titleB4 = 0;
        private static int titleR5 = 0;
        private static int titleG5 = 0;
        private static int titleB5 = 0;
        private static int titleR6 = 0;
        private static int titleG6 = 0;
        private static int titleB6 = 0;
        private static int titleR7 = 0;
        private static int titleG7 = 0;
        private static int titleB7 = 0;
        private static int titleR8 = 0;
        private static int titleG8 = 0;
        private static int titleB8 = 0;
        private static int titleR9 = 0;
        private static int titleG9 = 0;
        private static int titleB9 = 0;
        private static int titleR10 = 0;
        private static int titleG10 = 0;
        private static int titleB10 = 0;

        public static int beepCount = 0;

        string curMove = "";
        string lastMove = "";

        public static Dictionary<string, string> directionDic;


        static int intervalInput = 10;
        static long interval = TimeSpan.FromSeconds(intervalInput).Ticks;
        long nextTick = System.DateTime.Now.Ticks + interval;

        public bool firstRunFlag = true;

        public static string curDirection;

        private static int sleepCount = 0;
        private static int intervalCount = 0;
        private static int intervalNoCount_L = 0;
        private static int intervalNoCount_R = 0;
        private static int sleepCountMax = 0;
        private static int changeDirectionCount = 0;

        private static int gongjeungCount = 0;

        private static string last_LR = "R";

        private static int lastPosX = 0;
        private static System.Random randomNum = new System.Random((int)System.DateTime.Now.Ticks);

        private static List<Task> allTasks = new List<Task>();
        private static ManualResetEvent pauseEvent = new ManualResetEvent(true);



        private static List<String> list;
        private static List<String> parameterList;
        private static List<stopPoint> stopPointList;

        private CorrectString correctString;
        private FindAndAlert findAndAlert;
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
            pasteAlert = new PasteAlert();
            clipboardMonitor = new ClipboardMonitor(this);

            stopPointList = new List<stopPoint>();

            SK = new SendKeyInput();
            
            timerForBoMu = new SimpleTimer();
            timerForMovingFlag = new SimpleTimer();
            timerForSajahu = new SimpleTimer();
            timerForAlt2 = new SimpleTimer();

            directionDic = new Dictionary<string, string>();
            //setMoveDictionary();
            setMagicNumber();
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


        public static void initValue()
        {
            LR = "L";
            global_LR = 0;
            x1 = 0;
            y1 = 0;
            R1 = 0;
            G1 = 0;
            B1 = 0;
            x2 = 0;
            y2 = 0;
            R2 = 0;
            G2 = 0;
            B2 = 0;

            sleepCount = 0;
            intervalCount = 0;
            sleepCountMax = 0;

            last_LR = "R";

            lastPosX = 0;

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

        bool UseHellfire = MainWindow.getInstance.ViewModel.UseHellfire;
        int HellfireDirectionIndex = MainWindow.getInstance.ViewModel.HellfireDirectionIndex;
        bool UseNormalAttack = MainWindow.getInstance.ViewModel.UseNormalAttack;
        bool UsePoison = MainWindow.getInstance.ViewModel.UsePoison;
        bool UsePickup = MainWindow.getInstance.ViewModel.UsePickup;
        bool UseCaptchaAlert = MainWindow.getInstance.ViewModel.UseCaptchaAlert;

        bool UseHeal = MainWindow.getInstance.ViewModel.UseHeal;
        bool UseProtectArmor = MainWindow.getInstance.ViewModel.UseProtectArmor;
        bool UseCurseOption = MainWindow.getInstance.ViewModel.UseCurseOption;
        bool UseBuffCombo = MainWindow.getInstance.ViewModel.UseBuffCombo;
        bool UseExtraCombo = MainWindow.getInstance.ViewModel.UseExtraCombo;

        bool UseShout = MainWindow.getInstance.ViewModel.UseShout;
        int ShoutCooldown = MainWindow.getInstance.ViewModel.ShoutCooldown;
        string ShoutMessage = MainWindow.getInstance.ViewModel.ShoutMessage;
        bool UseChatDetectAlt2 = MainWindow.getInstance.ViewModel.UseChatDetectAlt2;
        int ChatDetectAlt2Cooldown = MainWindow.getInstance.ViewModel.ChatDetectAlt2Cooldown;

        int magic1;
        int magic2;
        int magic3;
        int magic4;
        int magic5;
        int magic6;
        int magic7;
        int magic8;
        int magic9;
        int magic0;

        int movingDelayUI;

        public void setMagicNumber()
        {

            UseHellfire = MainWindow.getInstance.ViewModel.UseHellfire;
            HellfireDirectionIndex = MainWindow.getInstance.ViewModel.HellfireDirectionIndex;
            UseNormalAttack = MainWindow.getInstance.ViewModel.UseNormalAttack;
            UsePoison = MainWindow.getInstance.ViewModel.UsePoison;
            UseHeal = MainWindow.getInstance.ViewModel.UseHeal;
            UseCurseOption = MainWindow.getInstance.ViewModel.UseCurseOption;
            UseBuffCombo = MainWindow.getInstance.ViewModel.UseBuffCombo;
            UseExtraCombo = MainWindow.getInstance.ViewModel.UseExtraCombo;
            UseShout = MainWindow.getInstance.ViewModel.UseShout;
            ShoutCooldown = MainWindow.getInstance.ViewModel.ShoutCooldown;
            ShoutMessage = MainWindow.getInstance.ViewModel.ShoutMessage;
            UseChatDetectAlt2 = MainWindow.getInstance.ViewModel.UseChatDetectAlt2;
            ChatDetectAlt2Cooldown = MainWindow.getInstance.ViewModel.ChatDetectAlt2Cooldown;
       
            bool ok1 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoHellfire, out magic1);
            bool ok2 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoBuff, out magic2);
            bool ok3 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoHeal, out magic3);
            bool ok4 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoCurse, out magic4);
            bool ok5 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoExtra1, out magic5);
            bool ok6 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoExtra2, out magic6);
            bool ok7 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoPoison, out magic7);
            bool ok8 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoParalyze, out magic8);
            bool ok9 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoProtect, out magic9);
            bool ok0 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoArmor, out magic0);

            magic1 = GetVkNumber(magic1);
            magic2 = GetVkNumber(magic2);
           
                magic3 = GetVkNumber(magic3);
            magic4 = GetVkNumber(magic4);
            magic5 = GetVkNumber(magic5);
            magic6 = GetVkNumber(magic6);
            magic7 = GetVkNumber(magic7);
            magic8 = GetVkNumber(magic8);
            magic9 = GetVkNumber(magic9);
            magic0 = GetVkNumber(magic0);

            movingDelayUI = MainWindow.getInstance.ViewModel.MoveDelay;
        }

        long _lastLeftTick = 0;
        const int LeftIntervalMs = 700; // 80~200ms 사이로 취향대로

        static bool isMovingFlag = false;
        static SimpleTimer timerForBoMu;
        static SimpleTimer timerForMovingFlag;
        static SimpleTimer timerForSajahu;
        static SimpleTimer timerForAlt2;

        private void send(Keys tempKey)//Keys tempKey, IntPtr wParam, IntPtr lParam
        {
            //MessageBox.Show(Control.ModifierKeys + "");
            //MessageBox.Show(tempKey.ToString().ToUpper() + "");

            if ((tempKey.ToString().ToUpper() == "LEFT" || tempKey.ToString().ToUpper() == "RIGHT" || tempKey.ToString().ToUpper() == "UP" || tempKey.ToString().ToUpper() == "DOWN"))
            {
                curDirection = tempKey.ToString().ToUpper();
                isMovingFlag = true;
                timerForMovingFlag.Start();
            }
            else
            {
                if(timerForMovingFlag.ElapsedMs >= 700)
                {
                    isMovingFlag = false;
                }
            }



            // 1회 실행
            if (tempKey.ToString().ToUpper() == "DELETE")
            {
                Macro.getInstance.Flag_DEL = true;
            }
            if (tempKey.ToString().ToUpper() == "END")
            {
                if (Macro.getInstance.Flag_END)
                {
                    keyUp();
                    Macro.getInstance.Flag_END = false;
                }
                else
                {
                    timerForBoMu.Start();
                    timerForMovingFlag.Start();
                    timerForSajahu.Start();
                    timerForAlt2.Start();
                    firstRunFlag = true;
                    Macro.getInstance.Flag_END = true;
                    setMagicNumber();
                }
            }
            if (tempKey.ToString().ToUpper() == "INSERT")
            {
                if (!movingOpt)
                {
                    movingOpt = true;
                }
                else
                {
                    movingOpt = false;
                }
            }
            if (tempKey.ToString().ToUpper() == "HOME")
            {
                Macro.getInstance.Flag_HOME = true;
            }
            if (tempKey.ToString().ToUpper() == "Flag_PGUP")
            {
                Macro.getInstance.Flag_PGUP = true;
            }
            if (tempKey.ToString().ToUpper() == "Flag_PGDN")
            {
                Macro.getInstance.Flag_PGDN = true;
            }
            


            if (tempKey.ToString().ToUpper() == "F1")
            {
                //pushLeftStopPoint();
                Macro.getInstance.Flag_F1 = true;
            }
            if (tempKey.ToString().ToUpper() == "F2")
            {
                //pushRightStopPoint();
                if (Macro.getInstance.Flag_F2)
                {
                    Macro.getInstance.Flag_F2 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F2 = true;
                }
            }
            if (tempKey.ToString().ToUpper() == "F3")
            {
                
            }
            if (tempKey.ToString().ToUpper() == "F4")
            {
                //for (int i = 800; i < 825; i++)
                //{
                //    for (int j = 3; j < 30; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                //y일의자리
                //for (int i = 1824; i < 1839 ; i++)
                //{
                //    for (int j = 1026; j < 1046; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                //y십의자리
                //for (int i = 1805; i < 1820; i++)
                //{
                //    for (int j = 1026; j < 1046; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                //x일의자리
                //for (int i = 1724; i < 1738; i++) //1724,1027 ~ 1738,1044
                //{
                //    for (int j = 1027; j < 1044; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                //x십의자리
                //for (int i = 1704; i < 1718; i++) //1704,1027 ~ 1718,1046
                //{
                //    for (int j = 1027; j < 1046; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                //선녀의방10굴 이후
                //for (int i = 805; i < 819; i++) 
                //{
                //    for (int j = 3; j < 24; j++)
                //    {
                //        hi2(i, j);
                //    }
                //}

                hi();
            }
            if (tempKey.ToString().ToUpper() == "F5")
            {
                if (Macro.getInstance.Flag_F5)
                {
                    Macro.getInstance.Flag_F5 = false;
                }
                else
                {
                    Macro.getInstance.Flag_F5 = true;
                }
            }

            if (tempKey.ToString().ToUpper() == "F7")
            {

            }

            if (tempKey.ToString().ToUpper() == "F8")
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
            if (tempKey.ToString().ToUpper() == "F11")
            {
                //buyRing();
                //sellItemAndBuy();

            }
            if (tempKey.ToString().ToUpper() == "F12")
            {
                //sellAllItem();
            }
            // 토글
            //else if (tempKey.ToString().ToUpper() == "F8")
            //{
            //    if (Macro.getInstance.Flag_F8)
            //    {
            //        Macro.getInstance.Flag_F8 = false;
            //        User32.API.keybd_event(0X57, 0, 0x0002, 0);
            //    }
            //    else
            //    {
            //        Macro.getInstance.Flag_F8 = true;
            //        User32.API.keybd_event(0X57, 0, 0, 0);
            //    }
            //}
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

        private static byte GetVkNumber(int number)
        {
            if (number < 0 || number > 9)
                throw new ArgumentOutOfRangeException(nameof(number), "0~9만 가능합니다.");

            return (byte)(0x30 + number);
        }

        public void keyUp()
        {
            //평타
            if (UseNormalAttack)
            {
                User32.API.keybd_event(0X20, 0, 2, 0);
            }
            //줍기
            if (UsePickup)
            { 
                User32.API.keybd_event(0xBC, 0, 2, 0);
            }
            //저주
            if (UseCurseOption)
            {
                User32.API.keybd_event((byte)magic4, 0, 2, 0);
            }
            //첨1,첨2
            if (UseExtraCombo)
            {
                User32.API.keybd_event((byte)magic5, 0, 2, 0);
                User32.API.keybd_event((byte)magic6, 0, 2, 0);
            }
        }

        public void keyDown()
        {
            //평타
            if (UseNormalAttack)
            {
                User32.API.keybd_event(0X20, 0, 0, 0);
            }
            //줍기
            if (UsePickup)
            {
                User32.API.keybd_event(0xBC, 0, 0, 0);
            }
            if (UseCurseOption)
            {
                User32.API.keybd_event(0xBC, 0, 0, 0);
            }
            //저주
            if (UseCurseOption)
            {
                User32.API.keybd_event((byte)magic4, 0, 0, 0);
            }
            //첨1,첨2
            if (UseExtraCombo)
            {
                User32.API.keybd_event((byte)magic5, 0, 0, 0);
                User32.API.keybd_event((byte)magic6, 0, 0, 0);
            }
        }

        public void sendKeyInput(CancellationTokenSource ct)
        {
            if (Macro.getInstance.Flag_DEL)
            {
                attackUsingHell();
                Macro.getInstance.Flag_DEL = false;
                 
            }
            if (Macro.getInstance.Flag_END)
            {
                activate234();
            }
            if (Macro.getInstance.Flag_INS)
            {
                
            }
            if (Macro.getInstance.Flag_HOME)
            {
                
            }
            if (Macro.getInstance.Flag_PGDN)
            {
                
            }
            if (Macro.getInstance.Flag_PGUP)
            {
                
            }

            if (Macro.getInstance.Flag_F1)
            {
                Macro.getInstance.Flag_F1 = false;

            }
            if (Macro.getInstance.Flag_F2)
            {
                //pushRightStopPoint();

            }
            if (Macro.getInstance.Flag_F3)
            {

            }
            if (Macro.getInstance.Flag_F4)
            {
                //hi();
            }
            if (Macro.getInstance.Flag_F5)
            {
                //activateGem();
            }
            if (Macro.getInstance.Flag_F6)
            {
            }
            if (Macro.getInstance.Flag_F7)
            {
                

            }

            if (Macro.getInstance.Flag_F8)
            {
            }
            if (Macro.getInstance.Flag_F9)
            {
            }
            if (Macro.getInstance.Flag_F10)
            {
                //rejoin();
            }
            if (Macro.getInstance.Flag_F11)
            {
            }
            if (Macro.getInstance.Flag_F12)
            {
                //sellAllItem();
            }
            else
            {

            }

            //Macro.getInstance.Flag1 = false;
            //Macro.getInstance.Flag2 = false;
            Macro.getInstance.Flag3 = false;
            Macro.getInstance.Flag4 = false;
            Macro.getInstance.Flag5 = false;

            //Macro.getInstance.Flag_F5 = false;
            Macro.getInstance.Flag_F6 = false;
            //Macro.getInstance.Flag_F7 = false;
            //Macro.getInstance.Flag_F8 = false;

            //Macro.getInstance.Flag_F9 = false;
            //Macro.getInstance.Flag_F10 = false;
            //Macro.getInstance.Flag_F11 = false;
            Macro.getInstance.Flag_F12 = false;

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


        static int now2 = Environment.TickCount;
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

                    Task.Delay(50);
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        //int abc = 0;
                        //ct.ThrowIfCancellationRequested();
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
        

        public static string lastDirection = "LEFT";

    

        private void attackUsingHell()
        {
            Thread.Sleep(10);
            SK.sendkeyEsc(5);
            SK.sendkeyNumber(10, (int)magic1);

            User32.API.keybd_event((byte)magic1, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic1, 0, 2, 0);
            SK.sendkeyHome(5);
            if (HellfireDirectionIndex == 0)
            {
                if (curDirection == null)
                {
                    curDirection = lastDirection;
                }   
                if (curDirection == "LEFT")
                {
                    SK.sendkeyLeft(5);
                }
                else if (curDirection == "RIGHT")
                {
                    SK.sendkeyRight(5);
                }
                else if (curDirection == "UP")
                {
                    SK.sendkeyUp(5);
                }
                else if (curDirection == "DOWN")
                {
                    SK.sendkeyDown(5);
                }
                else
                {
                }
                lastDirection = curDirection;
            }
            else if (HellfireDirectionIndex == 1)
            {
                if (curDirection == null)
                {
                    curDirection = lastDirection;
                }
                if (curDirection == "LEFT")
                {
                    SK.sendkeyRight(5);
                }
                else if (curDirection == "RIGHT")
                {
                    SK.sendkeyLeft(5);
                }
                else if (curDirection == "UP")
                {
                    SK.sendkeyDown(5);
                }
                else if (curDirection == "DOWN")
                {
                    SK.sendkeyUp(5);
                }
                else
                {
                }
                lastDirection = curDirection;
            }
            else if (HellfireDirectionIndex == 2)
            {

            }
            else
            {

            }
            
            Thread.Sleep(10);
            SK.sendkeyEnter(5);
            SK.sendkeyEsc(5);
            Thread.Sleep(10);
            SK.sendkeyTab(5);
            SK.sendkeyHome(5);
            SK.sendkeyTab(5);

        }


        private void activate234()
        {

            //좌표 설정  
            int x = 1801;
            int y = 950;

            int x0 = 1703;
            int y0 = 921;

            int x3 = 1843;
            int y3 = 942;

            int x4 = 32;
            int y4 = 8;

            //캡챠 확인용 좌표
            int x5 = 1231;
            int y5 = 855;

            //일반 채팅 확인용 좌표
            int x6 = 1383;
            int y6 = 1006;

            //Console.WriteLine($"User32.API.SetCursorPos({R2},{G2},{B2}");
            //MainWindow.getInstance.SetStateString(x, y, curColor);



            //////////////////////////////////////////////////////////////////////////////////////
            //좌표 색 가져오기
            Color curColor = GetColorAt(x, y);
            Color curColor0 = GetColorAt(x0, y0);
            Color curColor3 = GetColorAt(x3, y3);
            Color curColor4 = GetColorAt(x4, y4);
            Color curColor5 = GetColorAt(x5, y5);
            Color curColor6 = GetColorAt(x6, y6);
            R0 = curColor0.R;
            G0 = curColor0.G;
            B0 = curColor0.B;
            R2 = curColor.R;
            G2 = curColor.G;
            B2 = curColor.B;
            R3 = curColor3.R;
            G3 = curColor3.G;
            B3 = curColor3.B;
            R4 = curColor4.R;
            G4 = curColor4.G;
            B4 = curColor4.B;
            R5 = curColor5.R;
            G5 = curColor5.G;
            B5 = curColor5.B;
            R6 = curColor6.R;
            G6 = curColor6.G;
            B6 = curColor6.B;

            //캡챠 발생 시 동작 안함
            if (UseCaptchaAlert && !(R5 == 6 && G5 == 3 && B5 == 6) && beepCount < 30)
            {
                keyUp();
                User32.API.keybd_event(0X25, 0, 2, 0);
                User32.API.keybd_event(0X26, 0, 2, 0);
                User32.API.keybd_event(0X27, 0, 2, 0);
                User32.API.keybd_event(0X28, 0, 2, 0);
                beepCount++;
                //SystemSounds.Beep.Play();
                //SystemSounds.Asterisk.Play();
                //SystemSounds.Exclamation.Play();
                //SystemSounds.Hand.Play();
                Console.Beep(500, 500);
                //MessageBox.Show("캡챠 발생!!");
                return;
            }
            else
            {
                beepCount = 0;
            }

            
            //일반 채팅 매크로 반응
            if (UseChatDetectAlt2 && timerForAlt2.ElapsedMs > ChatDetectAlt2Cooldown * 1000)
            {
                timerForAlt2.Start();
                if ((R6 == 229 && G6 == 189 && B6 == 1541 ||
                    (R6 == 231 && G6 == 196 && B6 == 154) ||
                    (R6 == 255 && G6 == 196 && B6 == 154) ||
                    R6 == 230 && G6 == 187 && B6 == 139))
                {
                    User32.API.keybd_event(0X12, 0, 0, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event(0X32, 0, 0, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event(0X12, 0, 2, 0);
                    User32.API.keybd_event(0X32, 0, 2, 0);
                }
            }

            //사자후
            if (UseShout && timerForSajahu.ElapsedMs > ShoutCooldown * 1000)
            {
                timerForSajahu.Start();
                User32.API.keybd_event(0X10, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X5A, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X5A, 0, 2, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X43, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X43, 0, 2, 0);
                User32.API.keybd_event(0X10, 0, 2, 0);
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    System.Windows.Clipboard.SetText(ShoutMessage);
                });
                Thread.Sleep(10);

                User32.API.keybd_event(0X11, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X56, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event(0X11, 0, 2, 0);
                User32.API.keybd_event(0X56, 0, 2, 0);
                Thread.Sleep(10);
                SK.sendkeyEnter(10);
            }


            //현재 맵 타이틀과 좌표를 통해 key 추출
            int movingTime = 10;
            int res1 = getMapNumber();
            string res2 = getMapXY();
            string key = "" + res1 + ":" + res2;

            //몬스터가 없는 곳을 구분
            bool noMonsterFlag = false;
            int movingDelay = movingDelayUI;
            if (key.Contains("122:"))
            {
                noMonsterFlag = true;
            }

            //몬스터가 없는 곳에서는 빠르게 이동
            if (noMonsterFlag)
            {
                movingDelay = 10;
            }
            //몬스터가 있는 곳에서 스킬 사용
            else
            {
                //힐, 저주, 첨첨
                keyDown();
            }


            //동동주+공증
            if (UseBuffCombo)
            {
                if (R3 == 8 && G3 == 4 && B3 == 8)
                {
                    SK.sendkeyCtrlAndZ(10);
                }
                if (R2 == 8 && G2 == 4 && B2 == 8)
                {
                    User32.API.keybd_event((byte)magic2, 0, 0, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event((byte)magic2, 0, 2, 0);
                }
            }

            //보무
            if (UseProtectArmor && (firstRunFlag || timerForBoMu.ElapsedMs >= 90000))
            {
                firstRunFlag = false;

                User32.API.keybd_event((byte)magic9, 0, 0, 0);
                User32.API.keybd_event((byte)magic0, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event((byte)magic9, 0, 2, 0);
                User32.API.keybd_event((byte)magic0, 0, 2, 0);
                timerForBoMu.Start();
            }
                        
            //힐
            if (UseHeal)
            {
                if (R0 == 8 && G0 == 4 && B0 == 8)
                {
                    System.Random random3 = new System.Random((int)System.DateTime.Now.Ticks);
                    int random3to4 = random3.Next(4, 5);
                    User32.API.keybd_event((byte)magic3, 0, 0, 0);
                    Thread.Sleep(100* random3to4);
                    User32.API.keybd_event((byte)magic3, 0, 2, 0);
                }
            }

            //무빙 //여기
            if (movingOpt)
            {
                Console.WriteLine($"{key})");
                curMove = GetValue("config.json", key);
                //흉가 대기실에서 출 외치고 들어가기
                if(key == "12:0,0,0,102,55,13,215,191,111,0,0,0,0,0,0,0,0,0,91,53,12,203,171,95,102,55,13,0,0,0" || key == "12:0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,91,53,12,203,171,95,102,55,13,0,0,0")
                {
                        
                    User32.API.keybd_event(0X25, 0, 2, 0);
                    User32.API.keybd_event(0X26, 0, 2, 0);
                    User32.API.keybd_event(0X27, 0, 2, 0);
                    User32.API.keybd_event(0X28, 0, 2, 0);
                    keyUp();
                    Thread.Sleep(2000);
                    SK.sendkeyEsc(5);

                    User32.API.keybd_event(0XA4, 0, 0, 0);
                    Thread.Sleep(5);
                    User32.API.keybd_event(0X31, 0, 0, 0);
                    Thread.Sleep(5);
                    User32.API.keybd_event(0X31, 0, 2, 0);
                    User32.API.keybd_event(0XA4, 0, 2, 0);
                    Thread.Sleep(2000);
                    SK.sendkeyTab(5);
                    Thread.Sleep(10);
                    SK.sendkeyTab(5);
                }
                //입장 전 노란비서 떨구기
                if(key == "12:0,0,0,102,55,13,215,191,111,0,0,0,102,55,13,0,0,0,91,53,12,203,171,95,0,0,0,195,155,79" || 
                    key == "12:91,53,12,0,0,0,215,191,111,0,0,0,102,55,13,0,0,0,91,53,12,203,171,95,0,0,0,195,155,79")
                    //key == "12:0,0,0,102,55,13,215,191,111,0,0,0,102,55,13,255,255,183,0,0,0,203,171,95,0,0,0,195,155,79" || 
                    //key == "12:91,53,12,0,0,0,215,191,111,0,0,0,102,55,13,255,255,183,0,0,0,203,171,95,0,0,0,195,155,79" || 
                {
                    Thread.Sleep(1000);
                    User32.API.keybd_event(0X44, 0, 0, 0);
                    User32.API.keybd_event(0X44, 0, 2, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event(0X42, 0, 0, 0);
                    User32.API.keybd_event(0X42, 0, 2, 0);
                    Thread.Sleep(500);
                    keyDown();
                }

                // key가 없는 좌표에서는 이전 이동을 그대로
                if (curMove == null)
                {
                    curMove = lastMove;
                }
                if (curMove == "동")
                {
                    User32.API.keybd_event(0X27, 0, 0, 0);
                    Thread.Sleep(movingTime);
                    User32.API.keybd_event(0X27, 0, 2, 0);
                }
                else if (curMove == "서")
                {
                    User32.API.keybd_event(0X25, 0, 0, 0);
                    Thread.Sleep(movingTime);
                    User32.API.keybd_event(0X25, 0, 2, 0);
                }
                else if (curMove == "남")
                {
                    User32.API.keybd_event(0X28, 0, 0, 0);
                    Thread.Sleep(movingTime);
                    User32.API.keybd_event(0X28, 0, 2, 0);
                }
                else if (curMove == "북")
                {
                    User32.API.keybd_event(0X26, 0, 0, 0);
                    Thread.Sleep(movingTime);
                    User32.API.keybd_event(0X26, 0, 2, 0);
                }
                else
                {
                }
                lastMove = curMove;
                User32.API.keybd_event(0X25, 0, 2, 0);
                User32.API.keybd_event(0X26, 0, 2, 0);
                User32.API.keybd_event(0X27, 0, 2, 0);
                User32.API.keybd_event(0X28, 0, 2, 0);
                Thread.Sleep(movingDelay);
            }


            
            keyUp();

            //중독
            if (UsePoison)
            {
                SK.sendkeyEsc(10);
                User32.API.keybd_event((byte)magic7, 0, 0, 0);
                Thread.Sleep(10);
                User32.API.keybd_event((byte)magic7, 0, 2, 0);
                SK.sendkeyRight(5);
                SK.sendkeyEnter(5);
                SK.sendkeyEsc(10);
                Thread.Sleep(10);
                SK.sendkeyTab(5);
                SK.sendkeyHome(5);
                SK.sendkeyTab(5);
            }

            // 정지상태인 경우 헬파이어
            if (UseHellfire && !noMonsterFlag && !(R4 == 255 && G4 == 255 && B4 == 255) && !(R0 == 8 && G0 == 4 && B0 == 8)) //isMovingFlag == false && 
            {
                try
                {
                    User32.API.BlockInput(true);   // 사용자 입력 차단
                    attackUsingHell();

                }
                finally
                {
                    User32.API.BlockInput(false);        // 반드시 해제
                }
                return;
            }

        }



        public static int getMapNumber()


        {   //XY(805,21)  RGB(255,255,255)
            //XY(805,22)  RGB(255,255,255)
            //XY(805,23)  RGB(255,255,255)
            //XY(805,24)  RGB(203,200,196)


            //XY(804,23)  RGB(75,75,74)
            //XY(804,24)  RGB(60,61,59)



            int res = 0;
            //(804, 13) + (809, 6)
            //
            //(802,17) + (809,11) 이
            int mapTitleX1 = 802;
            int mapTitleY1 = 17;
            int mapTitleX2 = 809;
            int mapTitleY2 = 11;


            //선녀의방 10번 이후를 위한 좌표 추가
            int mapTitleX3 = 645;
            int mapTitleY3 = 23;
            //XY(806, 3)과 XY(811,13)
            int mapTitleX4 = 806;
            int mapTitleY4 = 3;
            int mapTitleX5 = 811;
            int mapTitleY5 = 13;

            //좌표 색 가져오기
            Color curTitleColor1 = GetColorAt(mapTitleX1, mapTitleY1);
            Color curTitleColor2 = GetColorAt(mapTitleX2, mapTitleY2);
            Color curTitleColor3 = GetColorAt(mapTitleX3, mapTitleY3);
            Color curTitleColor4 = GetColorAt(mapTitleX4, mapTitleY4);
            Color curTitleColor5 = GetColorAt(mapTitleX5, mapTitleY5);
            titleR1 = curTitleColor1.R;
            titleG1 = curTitleColor1.G;
            titleB1 = curTitleColor1.B;
            titleR2 = curTitleColor2.R;
            titleG2 = curTitleColor2.G;
            titleB2 = curTitleColor2.B;
            titleR3 = curTitleColor3.R;
            titleG3 = curTitleColor3.G;
            titleB3 = curTitleColor3.B;
            titleR4 = curTitleColor4.R;
            titleG4 = curTitleColor4.G;
            titleB4 = curTitleColor4.B;
            titleR5 = curTitleColor5.R;
            titleG5 = curTitleColor5.G;
            titleB5 = curTitleColor5.B;


            if (titleR3 == 244 && titleG3 == 244 && titleB3 == 244)
            {
                //Console.WriteLine($"if (titleR4 == {titleR4} && titleG4 == {titleG4} && titleB4 == {titleB4} && titleR5 == {titleR5} && titleG5 == {titleG5} && titleB5 == {titleB5} && titleR6 == {titleR6} && titleG6 == {titleG6} && titleB6 == {titleB6})" );
                if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 18 && titleG5 == 19 && titleB5 == 17)
                {
                    res = 10;
                }
                else if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 137 && titleG5 == 137 && titleB5 == 136)
                {
                    res = 11;
                }
                else if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 77 && titleG5 == 78 && titleB5 == 77)
                {
                    res = 12;
                }
                else if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 78 && titleG5 == 79 && titleB5 == 78)
                {
                    res = 13;
                }
                else if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 77 && titleG5 == 77 && titleB5 == 76)
                {
                    res = 14;
                }
                else if (titleR4 == 18 && titleG4 == 15 && titleB4 == 18 && titleR5 == 136 && titleG5 == 137 && titleB5 == 136)
                {
                    res = 15;
                }
                else if (titleR4 == 6 && titleG4 == 3 && titleB4 == 6 && titleR5 == 136 && titleG5 == 137 && titleB5 == 136)
                {
                    res = 16;
                }
                else if (titleR4 == 18 && titleG4 == 15 && titleB4 == 18 && titleR5 == 18 && titleG5 == 19 && titleB5 == 17)
                {
                    res = 17;
                }
                else
                {
                    res = 999;
                }

            }
            else
            {
                if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                {
                    res = 1;
                }
                else if (titleR1 == 131 && titleG1 == 129 && titleB1 == 131 && titleR2 == 102 && titleG2 == 102 && titleB2 == 101)
                {
                    res = 2;
                }
                else if (titleR1 == 12 && titleG1 == 9 && titleB1 == 12 && titleR2 == 102 && titleG2 == 102 && titleB2 == 101)
                {
                    res = 3;
                }
                else if (titleR1 == 135 && titleG1 == 133 && titleB1 == 135 && titleR2 == 75 && titleG2 == 75 && titleB2 == 74)
                {
                    res = 4;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 60 && titleG2 == 61 && titleB2 == 59)
                {
                    res = 5;
                }
                else if (titleR1 == 130 && titleG1 == 129 && titleB1 == 130 && titleR2 == 60 && titleG2 == 61 && titleB2 == 59)
                {
                    res = 6;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 102 && titleG2 == 102 && titleB2 == 101)
                {
                    res = 7;
                }
                else if (titleR1 == 130 && titleG1 == 129 && titleB1 == 130 && titleR2 == 102 && titleG2 == 102 && titleB2 == 101)
                {
                    res = 8;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 198 && titleG2 == 199 && titleB2 == 198)
                {
                    res = 9;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 75 && titleG2 == 75 && titleB2 == 74)
                {
                    res = 10;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                {
                    res = 11;
                }
                else if (titleR1 == 243 && titleG1 == 243 && titleB1 == 243 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                {
                    res = 12;
                }
                else
                {
                    res = 0;
                }
            }


            return res;
        }

        public static string getMapXY()
        {
            string res = "";


            //1824,1028
            //1835, 1044
            //1838, 1026
            int mapTitleX1 = 1824;
            int mapTitleY1 = 1028;
            int mapTitleX2 = 1835;
            int mapTitleY2 = 1044;
            int mapTitleX3 = 1838;
            int mapTitleY3 = 1026;

            int mapTitleX4 = 1805;
            int mapTitleY4 = 1030;
            int mapTitleX5 = 1805;
            int mapTitleY5 = 1044;

            int mapTitleX6 = 1725;
            int mapTitleY6 = 1028;
            int mapTitleX7 = 1725;
            int mapTitleY7 = 1037;
            int mapTitleX8 = 1736;
            int mapTitleY8 = 1033;

            int mapTitleX9 = 1704;
            int mapTitleY9 = 1033;
            int mapTitleX10 = 1704;
            int mapTitleY10 = 1044;

            //좌표 색 가져오기
            Color curTitleColor1 = GetColorAt(mapTitleX1, mapTitleY1);
            Color curTitleColor2 = GetColorAt(mapTitleX2, mapTitleY2);
            Color curTitleColor3 = GetColorAt(mapTitleX3, mapTitleY3);
            Color curTitleColor4 = GetColorAt(mapTitleX4, mapTitleY4);
            Color curTitleColor5 = GetColorAt(mapTitleX5, mapTitleY5);
            Color curTitleColor6 = GetColorAt(mapTitleX6, mapTitleY6);
            Color curTitleColor7 = GetColorAt(mapTitleX7, mapTitleY7);
            Color curTitleColor8 = GetColorAt(mapTitleX8, mapTitleY8);
            Color curTitleColor9 = GetColorAt(mapTitleX9, mapTitleY9);
            Color curTitleColor10 = GetColorAt(mapTitleX10, mapTitleY10);

            titleR1 = curTitleColor1.R;
            titleG1 = curTitleColor1.G;
            titleB1 = curTitleColor1.B;
            titleR2 = curTitleColor2.R;
            titleG2 = curTitleColor2.G;
            titleB2 = curTitleColor2.B;
            titleR3 = curTitleColor3.R;
            titleG3 = curTitleColor3.G;
            titleB3 = curTitleColor3.B;
            titleR4 = curTitleColor4.R;
            titleG4 = curTitleColor4.G;
            titleB4 = curTitleColor4.B;
            titleR5 = curTitleColor5.R;
            titleG5 = curTitleColor5.G;
            titleB5 = curTitleColor5.B;
            titleR6 = curTitleColor6.R;
            titleG6 = curTitleColor6.G;
            titleB6 = curTitleColor6.B;
            titleR7 = curTitleColor7.R;
            titleG7 = curTitleColor7.G;
            titleB7 = curTitleColor7.B;
            titleR8 = curTitleColor8.R;
            titleG8 = curTitleColor8.G;
            titleB8 = curTitleColor8.B;
            titleR9 = curTitleColor9.R;
            titleG9 = curTitleColor9.G;
            titleB9 = curTitleColor9.B;
            titleR10 = curTitleColor10.R;
            titleG10 = curTitleColor10.G;
            titleB10 = curTitleColor10.B;

            res = "" + titleR1 + "," + titleG1 + "," + titleB1 + "," +
                titleR2 + "," + titleG2 + "," + titleB2 + "," +
                titleR3 + "," + titleG3 + "," + titleB3 + "," +
                titleR4 + "," + titleG4 + "," + titleB4 + "," +
                titleR5 + "," + titleG5 + "," + titleB5 + "," +
                titleR6 + "," + titleG6 + "," + titleB6 + "," +
                titleR7 + "," + titleG7 + "," + titleB7 + "," +
                titleR8 + "," + titleG8 + "," + titleB8 + "," +
                titleR9 + "," + titleG9 + "," + titleB9 + "," +
                titleR10 + "," + titleG10 + "," + titleB10;

            return res;
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
            //Console.WriteLine($"현재 마우스 커서의 위치: X = {p.X}, Y = {p.Y}");
            //Console.WriteLine($"User32.API.SetCursorPos({p.X},{p.Y})");

            int res1 = getMapNumber();
            string res2 = getMapXY();
            //MainWindow.getInstance.SetStateString(res1, res1, curColor);
            //MainWindow.getInstance.SetStateString(res2, res2, curColor);
            //MainWindow.getInstance.SetStateString(x2, y2, curColor);
            Console.WriteLine($"{x2}, {y2}, {curColor}");


            string key = "" + res1 + ":" + res2;

            string tempDirection = "";

            if (curDirection == "LEFT")
            {
                tempDirection = "서";
            }
            else if (curDirection == "RIGHT")
            {
                tempDirection = "동";
            }
            else if (curDirection == "UP")
            {
                tempDirection = "북";
            }
            else if (curDirection == "DOWN")
            {
                tempDirection = "남";
            }
            Console.WriteLine($"directionDic.Add(\"{key}\", \"{tempDirection}\");");



            string path = "config.json";
            
            //SaveDirectionDic(path, directionDic);

            SaveStringKeyValue(path, key, tempDirection);


            //if (TryLoadByArrayListKey(path, new ArrayList { 1, 2, 3 }, out string v))
            //    Console.WriteLine(v); // numbers


            //Console.WriteLine($"curColor = GetColorAt({p.X}, {p.Y})");
            //Console.WriteLine($"{R2},{G2},{B2}");

            //Console.WriteLine($"curColor = GetColorAt({p.X}, {p.Y});");
            //Console.WriteLine($"colorList.Add(curColor);");


        }
        static void SaveDirectionDic(string filePath, Dictionary<string, string> directionDic)
        {
            string json = JsonConvert.SerializeObject(directionDic, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void setMoveDictionary()
        {
        }

        private static void hi2(int x,int y)
        {
            Point p = getMousePosAndColor();
            Color curColor = GetColorAt(x, y);

            R2 = curColor.R;
            G2 = curColor.G;
            B2 = curColor.B;
            //Console.WriteLine($"현재 마우스 커서의 위치: X = {p.X}, Y = {p.Y}");
            //Console.WriteLine($"User32.API.SetCursorPos({p.X},{p.Y})");

            //MainWindow.getInstance.SetStateString(x, y, curColor);


            //Console.WriteLine($"curColor = GetColorAt({p.X}, {p.Y})");
            //Console.WriteLine($"{R2},{G2},{B2}");


            //Console.WriteLine($"curColor = GetColorAt({x}, {y});");
            //Console.WriteLine($"colorList.Add(curColor);");

            string newString = "XY(" + x + "," + y + ")" + "\tRGB(" + curColor.R + "," + curColor.G + "," + curColor.B + ")";
            Console.WriteLine($"{newString}");

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

        static void SaveStringKeyValue(string filePath, string key, string value)
        {
            Dictionary<string, string> dict;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();
            }
            else
            {
                dict = new Dictionary<string, string>();
            }

            dict[key] = value;

            File.WriteAllText(filePath, JsonConvert.SerializeObject(dict, Formatting.Indented));
        }


        public static bool TryLoadByArrayListKey(string filePath, ArrayList keyList, out string value)
        {
            value = null;

            if (!File.Exists(filePath))
                return false;

            var json = File.ReadAllText(filePath);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (dict == null) return false;

            // 동일한 ArrayList를 동일한 JSON 문자열로 만들어서 키로 조회
            string key = JsonConvert.SerializeObject(keyList);

            return dict.TryGetValue(key, out value);
        }

        
        static string GetValue(string filePath, string key)
        {
            if (!File.Exists(filePath))
                return null;

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText(filePath)
            );

            if (dict == null)
                return null;

            return dict.TryGetValue(key, out var value) ? value : null;
        }

        private void AltAndDelete()
        {
            SK.sendkeyAlt(15);
            SK.sendkeyDelete(10);
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

    public class SimpleTimer
    {
        private long _startTick;
        private bool _isRunning;

        public void Start()
        {
            _startTick = Environment.TickCount;
            _isRunning = true;
        }

        public void Reset()
        {
            _startTick = 0;
            _isRunning = false;
        }

        public long ElapsedMs
        {
            get
            {
                if (!_isRunning)
                    return 0;

                return Environment.TickCount - _startTick;
            }
        }

        public bool IsRunning => _isRunning;
    }







}