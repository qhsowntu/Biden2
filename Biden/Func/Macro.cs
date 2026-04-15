using Biden.View;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices; //required for dll import
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
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

        private bool stationaryCombatKeysPressed = false;
        private bool normalAttackKeyPressed = false;
        private bool pickupKeyPressed = false;

        int DO = 523;
        int RE = 587;
        int MI = 659;
        int FA = 698;
        int SOL = 784;
        int LA = 880;
        int SI = 988;

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

        private static int hell_R1 = 0;
        private static int hell_G1 = 0;
        private static int hell_B1 = 0;

        private static int hell_R2 = 0;
        private static int hell_G2 = 0;
        private static int hell_B2 = 0;

        private static int hell_R3 = 0;
        private static int hell_G3 = 0;
        private static int hell_B3 = 0;

        private static int hell_R4 = 0;
        private static int hell_G4 = 0;
        private static int hell_B4 = 0;

        private static int chawon_R1 = 0;
        private static int chawon_G1 = 0;
        private static int chawon_B1 = 0;

        private static int death_R1 = 0;
        private static int death_G1 = 0;
        private static int death_B1 = 0;

        private static int hobak_R1 = 0;
        private static int hobak_G1 = 0;
        private static int hobak_B1 = 0;

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

        public static int lastMap = 999;

        static int intervalInput = 10;
        static long interval = TimeSpan.FromSeconds(intervalInput).Ticks;
        long nextTick = System.DateTime.Now.Ticks + interval;

        public bool firstRunFlag = true;

        public bool useJipokFlag = false;
        public bool useMagiFlag = false;

        public static string curDirection;

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




        string curMove = "";
        string lastMove = "";
        string lastManualMove = "";



        public static Dictionary<string, string> directionDic;

        private readonly object moveLock = new object();
        private string moveConfigPath { get { return GetMoveConfigPathForSelectedMap(); } }
        private string downConfigPath { get { return GetDownConfigPathForSelectedMap(); } }
        private readonly string castleConfigPath = "config_관령성.json";
        private string moveConfigLoadedPath = "";
        private System.DateTime moveConfigLastWriteTime = System.DateTime.MinValue;
        private Dictionary<string, string> castleDirectionDic = new Dictionary<string, string>();
        private System.DateTime castleConfigLastWriteTime = System.DateTime.MinValue;

        private readonly object autoExploreLock = new object();
        private readonly System.Random autoExploreRandom = new System.Random();
        private Dictionary<string, AutoExploreNode> autoExploreGraph = new Dictionary<string, AutoExploreNode>();
        private Dictionary<string, string> autoExploreManualRoute = new Dictionary<string, string>();
        private Dictionary<string, string> autoExploreManualRouteExpanded = new Dictionary<string, string>();
        private bool autoExploreMode = false;
        private bool autoExploreUsingReturnRoute = false;
        private bool autoExploreReturnRouteResumeAfterEnd = false;
        private string autoExploreLastKey = "";
        private string autoExploreLastDir = "";
        private string autoExploreLastDeviationRouteKey = "";
        private string autoExploreReturnTapKey = "";
        private string autoExploreReturnTapDir = "";
        private bool autoExploreDeviationReturnPending = false;
        private System.DateTime autoExploreLastMoveTime = System.DateTime.MinValue;
        private System.DateTime autoExploreReturnTapTime = System.DateTime.MinValue;
        private int autoExploreSamePointFailCount = 0;
        private const int HauntedHouseSpecialBounceTarget = 8;
        private const int AutoExploreStuckMs = 950;
        private const int AutoExploreSoftBlockedFailCount = 1;
        private const int AutoExploreWallFailCount = 1;
        private const int AutoExploreHardWallFailCount = 12;
        private const int AutoExploreSaveIntervalMs = 1500;
        private const int AutoExploreRecentBlockDetourMs = 8000;
        private const int AutoExploreMinMapNo = 1;
        private const int AutoExploreMainRoutePercent = 88;
        private const int AutoExploreFrontierRoutePercent = 82;
        private const int AutoExploreLikelyWallFailCount = 1;
        private const int AutoExploreManualRoutePercent = 95;
        private const int AutoExploreMaxManualRouteDeviation = 1;
        private const int AutoExploreReturnTapRetryMs = 850;
        private const int GwallyeongCastleMapNo = 0;
        private const int GwallyeongCastleMaxX = 22;
        private const int GwallyeongCastleMaxY = 44;
        private const int GwallyeongHauntedHouse5EntryY = 7;
        private const int AutoExploreReturnStartMapNo = 10;
        private const int AutoExploreReturnStartX = 17;
        private const int AutoExploreReturnStartY = 13;
        private const int AutoExploreReturnEndMapNo = 5;
        private const int AutoExploreReturnEndX = 9;
        private const int AutoExploreReturnEndY = 22;
        private const int AutoExploreReturnEndStableMs = 1200;
        private const int GwallyeongHauntedEntranceMapNo = 12;
        private const int AutoExploreMaxMapNo = 12;
        private const int SpecialRouteBlockedMs = 2000;
        private const int SpecialRouteMaxDetourForwardSteps = 4;

        private string lastMoveKey = "";
        private string autoExploreReturnEndCandidateKey = "";
        private System.DateTime autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
        private string specialRouteBlockedKey = "";
        private string specialRouteBlockedDir = "";
        private System.DateTime specialRouteBlockedSince = System.DateTime.MinValue;
        private bool specialRouteDetourActive = false;
        private string[] specialRouteDetourDirs = new string[0];
        private int specialRouteDetourIndex = 0;
        private string specialRouteDetourLastKey = "";
        private string specialRouteDetourBaseDir = "";
        private System.DateTime lastDirectionInputTime = System.DateTime.MinValue;
        private int hauntedHouse5UpperRoutePhase = 0;
        private int hauntedHouse5UpperRouteBounceCount = 0;
        private bool hauntedHouse5UpperRouteReachedEast = false;
        private bool hauntedHouse5UpperRouteExitRetryPending = false;
        private int hauntedHouse5UpperRouteLoopIndex = 0;
        private bool hauntedHouse5UpperRouteCompleted = false;
        private int hauntedHouse6UpperRoutePhase = 0;
        private int hauntedHouse6UpperRouteBounceCount = 0;
        private bool hauntedHouse6UpperRouteReachedWest = false;
        private bool hauntedHouse6UpperRouteExitRetryPending = false;
        private int hauntedHouse6UpperRouteLoopIndex = 0;
        private bool hauntedHouse6UpperRouteCompleted = false;
        private int hauntedHouse7UpperRoutePhase = 0;
        private int hauntedHouse7UpperRouteBounceCount = 0;
        private bool hauntedHouse7UpperRouteReachedNorth = false;
        private bool hauntedHouse7UpperRouteExitRetryPending = false;
        private int hauntedHouse7UpperRouteLoopIndex = 0;
        private bool hauntedHouse7UpperRouteCompleted = false;
        private int hauntedHouse8UpperRoutePhase = 0;
        private int hauntedHouse8UpperRouteBounceCount = 0;
        private bool hauntedHouse8UpperRouteReachedWest = false;
        private bool hauntedHouse8UpperRouteExitRetryPending = false;
        private int hauntedHouse8UpperRouteLoopIndex = 0;
        private bool hauntedHouse8UpperRouteCompleted = false;
        private int hauntedHouse9UpperRoutePhase = 0;
        private int hauntedHouse9UpperRouteBounceCount = 0;
        private bool hauntedHouse9UpperRouteReachedWest = false;
        private bool hauntedHouse9UpperRouteExitRetryPending = false;
        private int hauntedHouse9UpperRouteLoopIndex = 0;
        private bool hauntedHouse9UpperRouteCompleted = false;
        private int hauntedHouse10UpperRoutePhase = 0;
        private int hauntedHouse10UpperRouteBounceCount = 0;
        private bool hauntedHouse10UpperRouteReachedEast = false;
        private bool hauntedHouse10UpperRouteExitRetryPending = false;
        private int hauntedHouse10UpperRouteLoopIndex = 0;
        private bool hauntedHouse10UpperRouteCompleted = false;
        private System.DateTime lastKeyChangeTime = System.DateTime.MinValue;

        // 값은 상황에 따라 조금씩 조절 가능
        private int repeatMs = 55;                   // 신규 방향 입력 전 재입력 지연시간

        private int nearSegmentMoveIntervalMs; // 턴 후 지연시간
        private int directionChangeDelayMs; // 턴 n칸전 제자리 지연시간
        private int nearTurnSlowRepeatMs; // 이동키 클릭 지속시간
        private int nearTurnStepThreshold; // 몇칸 전부터 감속 시작할지
        private int segmentedMovePressMs = 120;
        private int basicSegmentedMoveDelayMs = 120;
        private int loopSegmentedMoveDelayMs = 120;
        private bool useSegmentedMoveOnlyInLoop = false;


        private int lookAheadCount = 12;
        private int turnTapMs = 28;                  // 한 번 탭 누르는 시간

        private int stepRetryMs = 90;              // 탭 재시도 주기

        private string reservedTurnKey = "";
        private string reservedTurnDir = "";
        private string reservedApproachDir = "";
        private bool reservedUseStep = false;

        private int turnStabilizeMs = 80;   // 방향 바꾼 직후, 홀드 재입력 잠깐 금지

        private bool lastMoveWasTapMode = false;
        private string segmentedMoveLastPositionKey = "";
        private System.DateTime segmentedMovePauseUntil = System.DateTime.MinValue;

        private bool justChangedDirection = false;
        private string justChangedDirectionKey = "";
        private System.DateTime justChangedDirectionTime = System.DateTime.MinValue;

        private int nearTurnSuppressDistance = 1;    // 분기점 1칸 전에서 정지 후 1칸 이동


        private bool steppingToReservedTurn = false;
        private string stepTargetKey = "";
        private string stepDirection = "";
        private bool stepPulseSent = false;
        private System.DateTime stepPulseTime = System.DateTime.MinValue;

        private string lastPositionKey = "";
        private System.DateTime lastPositionChangedTime = System.DateTime.MinValue;
        private bool threeHellCastOnCurrentStall = false;
        private int threeHellStationaryMs;
        private int hellfireStationaryMs;
        private string lastHellfirePositionKey = "";
        private System.DateTime lastHellfirePositionChangedTime = System.DateTime.MinValue;
        private System.DateTime lastHellfireCastTime = System.DateTime.MinValue;
        private const int StationaryCombatReadyMs = 200;

        static SpeechSynthesizer synth;


        private string lastPositionKey2 = "";
        private System.DateTime lastPositionChangedTime2 = System.DateTime.MinValue;
        private bool findWall = false;
        private int findWallMs = 100000;

        private int paralysisCount = 0;
        private int poisonCount = 0;
        private int threeHellCount = 0;

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
            setMoveDictionary();
            setMagicNumber();
            synth = new SpeechSynthesizer();
            synth.Rate = SayRate;
            synth.SetOutputToDefaultAudioDevice();
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

        public static bool UseHellfire = MainWindow.getInstance.ViewModel.UseHellfire;
        public static int HellfireDirectionIndex = MainWindow.getInstance.ViewModel.HellfireDirectionIndex;
        public static int HellfireDelay = MainWindow.getInstance.ViewModel.HellfireDelay;
        public static bool UseThreeHellEvolution = MainWindow.getInstance.ViewModel.UseThreeHellEvolution;
        public static int ThreeHellEvolutionDelay = MainWindow.getInstance.ViewModel.ThreeHellEvolutionDelay;


        public static bool UseNormalAttack = MainWindow.getInstance.ViewModel.UseNormalAttack;
        public static bool UsePoison = MainWindow.getInstance.ViewModel.UsePoison;
        public static bool UsePickup = MainWindow.getInstance.ViewModel.UsePickup;
        public static bool UsePumpkinSay = MainWindow.getInstance.ViewModel.UsePumpkinSay;
        public static bool UseRouteChangeSay = MainWindow.getInstance.ViewModel.UseRouteChangeSay;
        public static bool UseCaptchaAlert = MainWindow.getInstance.ViewModel.UseCaptchaAlert;

        public static bool UseHeal = MainWindow.getInstance.ViewModel.UseHeal;
        public static string SelectedHealHpThreshold = MainWindow.getInstance.ViewModel.SelectedHealHpThreshold;
        public static bool UseProtectArmor = MainWindow.getInstance.ViewModel.UseProtectArmor;
        public static bool UseCurseOption = MainWindow.getInstance.ViewModel.UseCurseOption;
        public static bool UseBuffCombo = MainWindow.getInstance.ViewModel.UseBuffCombo;
        public static bool UseExtraCombo = MainWindow.getInstance.ViewModel.UseExtraCombo;
        public static bool UseParalysis = MainWindow.getInstance.ViewModel.UseParalysis;
        public static bool UseJipok = MainWindow.getInstance.ViewModel.UseJipok;
        public static bool UseMagi = MainWindow.getInstance.ViewModel.UseMagi;
        public static bool UseHoche = MainWindow.getInstance.ViewModel.UseHoche;
        public static bool UseNodo = MainWindow.getInstance.ViewModel.UseNodo;


        public static string SelectedMap = "관령흉가 1지역";
        public static bool UseShout = MainWindow.getInstance.ViewModel.UseShout;
        public static int ShoutCooldown = MainWindow.getInstance.ViewModel.ShoutCooldown;
        public static string ShoutMessage = MainWindow.getInstance.ViewModel.ShoutMessage;
        public static bool UseChatDetectAlt2 = MainWindow.getInstance.ViewModel.UseChatDetectAlt2;
        public static int ChatDetectAlt2Cooldown = MainWindow.getInstance.ViewModel.ChatDetectAlt2Cooldown;

        public static int magic1;
        public static int magic2;
        public static int magic3;
        public static int magic4;
        public static int magic5;
        public static int magic6;
        public static int magic7;
        public static int magic8;
        public static int magic9;
        public static int magic0;
        public static int magic11;
        public static int magic12;
        public static int magic13;
        public static int magic14;
        public static int magic15;

        public static int NearSegmentMoveIntervalMs;
        public static int DirectionChangeDelayMs;
        public static int NearTurnSlowRepeatMs;
        public static int NearTurnStepThreshold;
        public static bool UseSegmentedMove;
        public static int SegmentedMovePressMs;
        public static int SegmentedMoveDelayMs;
        public static int BasicSegmentedMoveDelayMs;
        public static int LoopSegmentedMoveDelayMs;
        public static bool UseSegmentedMoveOnlyInLoop;
        public static bool UseOneTileDeviationRoute;


        public static int movingDelayUI;


        public void setMagicNumber()
        {

            UseHellfire = MainWindow.getInstance.ViewModel.UseHellfire;
            HellfireDirectionIndex = MainWindow.getInstance.ViewModel.HellfireDirectionIndex;
            HellfireDelay = MainWindow.getInstance.ViewModel.HellfireDelay;
            UseThreeHellEvolution = MainWindow.getInstance.ViewModel.UseThreeHellEvolution;
            ThreeHellEvolutionDelay = MainWindow.getInstance.ViewModel.ThreeHellEvolutionDelay;
            UseNormalAttack = MainWindow.getInstance.ViewModel.UseNormalAttack;
            UsePoison = MainWindow.getInstance.ViewModel.UsePoison;
            UsePickup = MainWindow.getInstance.ViewModel.UsePickup;
            UsePumpkinSay = MainWindow.getInstance.ViewModel.UsePumpkinSay;
            UseRouteChangeSay = MainWindow.getInstance.ViewModel.UseRouteChangeSay;
            UseCaptchaAlert = MainWindow.getInstance.ViewModel.UseCaptchaAlert;

            UseHeal = MainWindow.getInstance.ViewModel.UseHeal;
            SelectedHealHpThreshold = MainWindow.getInstance.ViewModel.SelectedHealHpThreshold;
            UseProtectArmor = MainWindow.getInstance.ViewModel.UseProtectArmor;
            UseCurseOption = MainWindow.getInstance.ViewModel.UseCurseOption;
            UseBuffCombo = MainWindow.getInstance.ViewModel.UseBuffCombo;
            UseExtraCombo = MainWindow.getInstance.ViewModel.UseExtraCombo;
            UseParalysis = MainWindow.getInstance.ViewModel.UseParalysis;

            UseJipok = MainWindow.getInstance.ViewModel.UseJipok;
            UseMagi = MainWindow.getInstance.ViewModel.UseMagi;
            UseHoche = MainWindow.getInstance.ViewModel.UseHoche;
            UseNodo = MainWindow.getInstance.ViewModel.UseNodo;

            SelectedMap = MainWindow.getInstance.ViewModel.SelectedMap;
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
            bool ok8 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoThreeHell, out magic8);
            bool ok9 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoProtect, out magic9);
            bool ok0 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoArmor, out magic0);
            bool ok11 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoParalysis, out magic11);
            bool ok12 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoJipok, out magic12);
            bool ok13 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoMagi, out magic13);
            bool ok14 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoHoche, out magic14);
            bool ok15 = int.TryParse(MainWindow.getInstance.ViewModel.MagicNoNodo, out magic15);

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
            magic11 = GetVkNumber(magic11);
            magic12 = GetVkNumber(magic12);
            magic13 = GetVkNumber(magic13);
            magic14 = GetVkNumber(magic14);
            magic15 = GetVkNumber(magic15);


            NearSegmentMoveIntervalMs = MainWindow.getInstance.ViewModel.NearSegmentMoveIntervalMs;
            DirectionChangeDelayMs = MainWindow.getInstance.ViewModel.DirectionChangeDelayMs;
            NearTurnSlowRepeatMs = MainWindow.getInstance.ViewModel.NearTurnSlowRepeatMs;
            NearTurnStepThreshold = MainWindow.getInstance.ViewModel.NearTurnStepThreshold;
            UseSegmentedMove = MainWindow.getInstance.ViewModel.UseSegmentedMove;
            SegmentedMovePressMs = MainWindow.getInstance.ViewModel.SegmentedMovePressMs;
            SegmentedMoveDelayMs = MainWindow.getInstance.ViewModel.SegmentedMoveDelayMs;
            BasicSegmentedMoveDelayMs = MainWindow.getInstance.ViewModel.BasicSegmentedMoveDelayMs;
            LoopSegmentedMoveDelayMs = MainWindow.getInstance.ViewModel.LoopSegmentedMoveDelayMs;
            UseSegmentedMoveOnlyInLoop = MainWindow.getInstance.ViewModel.UseSegmentedMoveOnlyInLoop;
            UseOneTileDeviationRoute = MainWindow.getInstance.ViewModel.UseOneTileDeviationRoute;



            nearSegmentMoveIntervalMs = NearSegmentMoveIntervalMs;
            directionChangeDelayMs = DirectionChangeDelayMs;
            nearTurnSlowRepeatMs = NearTurnSlowRepeatMs;
            nearTurnStepThreshold = NearTurnStepThreshold;
            segmentedMovePressMs = SegmentedMovePressMs <= 0 ? 120 : SegmentedMovePressMs;
            basicSegmentedMoveDelayMs = BasicSegmentedMoveDelayMs < 0 ? 0 : BasicSegmentedMoveDelayMs;
            loopSegmentedMoveDelayMs = LoopSegmentedMoveDelayMs < 0 ? 0 : LoopSegmentedMoveDelayMs;
            useSegmentedMoveOnlyInLoop = UseSegmentedMoveOnlyInLoop;

            movingDelayUI = MainWindow.getInstance.ViewModel.MoveDelay;

            threeHellStationaryMs = ThreeHellEvolutionDelay * 1000;
            hellfireStationaryMs = (HellfireDelay <= 0 ? 5 : HellfireDelay) * 1000;
        }

        private static void SyncSelectedMapFromViewModel()
        {
            try
            {
                string selectedMap = MainWindow.getInstance.ViewModel.SelectedMap;
                if (!string.IsNullOrEmpty(selectedMap))
                {
                    SelectedMap = selectedMap == "관령흉가" ? "관령흉가 1지역" : selectedMap;
                }
            }
            catch
            {
            }
        }

        private static bool IsGwallyeongHauntedHouseMap(string selectedMap)
        {
            return selectedMap == "관령흉가" ||
                   (selectedMap != null && selectedMap.StartsWith("관령흉가 "));
        }

        private string GetMoveConfigPathForSelectedMap()
        {
            SyncSelectedMapFromViewModel();

            if (SelectedMap == "관령흉가" || SelectedMap == "관령흉가 1지역")
                return Path.Combine("관령흉가", "config_흉가_1지역.json");
            if (SelectedMap == "관령흉가 2지역")
                return Path.Combine("관령흉가", "config_흉가_2지역.json");
            if (SelectedMap == "관령흉가 3지역")
                return Path.Combine("관령흉가", "config_흉가_3지역.json");
            if (SelectedMap == "관령흉가 4지역")
                return Path.Combine("관령흉가", "config_흉가_4지역.json");
            if (SelectedMap == "관령흉가 5지역")
                return Path.Combine("관령흉가", "config_흉가_5지역.json");
            if (SelectedMap == "관령흉가 6지역")
                return Path.Combine("관령흉가", "config_흉가_6지역.json");

            return "config.json";
        }

        private string GetDownConfigPathForSelectedMap()
        {
            SyncSelectedMapFromViewModel();

            if (SelectedMap == "관령흉가" || SelectedMap == "관령흉가 1지역")
                return Path.Combine("관령흉가", "config_흉가_1지역_하행.json");
            if (SelectedMap == "관령흉가 2지역")
                return Path.Combine("관령흉가", "config_흉가_2지역_하행.json");
            if (SelectedMap == "관령흉가 3지역")
                return Path.Combine("관령흉가", "config_흉가_3지역_하행.json");
            if (SelectedMap == "관령흉가 4지역")
                return Path.Combine("관령흉가", "config_흉가_4지역_하행.json");
            if (SelectedMap == "관령흉가 5지역")
                return Path.Combine("관령흉가", "config_흉가_5지역_하행.json");
            if (SelectedMap == "관령흉가 6지역")
                return Path.Combine("관령흉가", "config_흉가_6지역_하행.json");

            return "config_하행.json";
        }

        long _lastLeftTick = 0;
        const int LeftIntervalMs = 700; // 80~200ms 사이로 취향대로

        static bool isMovingFlag = false;
        static SimpleTimer timerForBoMu;
        static SimpleTimer timerForMovingFlag;
        static SimpleTimer timerForSajahu;
        static SimpleTimer timerForAlt2;

        private readonly object actionKeyLock = new object();
        private readonly object pumpkinFullLock = new object();
        private bool pumpkinFullSequenceRunning = false;
        private bool pumpkinFullCancelRequested = false;
        private bool pumpkinFullInternalEndOff = false;
        private bool pumpkinReturnRouteResumePending = false;
        private const int PumpkinStepDelayMs = 500;
        private const int SayRate = 6;
        private const int FastSayRate = 8;

        private void send(Keys tempKey)//Keys tempKey, IntPtr wParam, IntPtr lParam
        {
            //MessageBox.Show(Control.ModifierKeys + "");
            //MessageBox.Show(tempKey.ToString().ToUpper() + "");

            if ((tempKey.ToString().ToUpper() == "LEFT" || tempKey.ToString().ToUpper() == "RIGHT" || tempKey.ToString().ToUpper() == "UP" || tempKey.ToString().ToUpper() == "DOWN"))
            {
                curDirection = tempKey.ToString().ToUpper();

                if (curDirection == "LEFT")
                    lastManualMove = "서";
                else if (curDirection == "RIGHT")
                    lastManualMove = "동";
                else if (curDirection == "UP")
                    lastManualMove = "북";
                else if (curDirection == "DOWN")
                    lastManualMove = "남";

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
                if (IsPumpkinFullSequenceRunning())
                {
                    Macro.getInstance.Flag_END = false;
                    CancelPumpkinFullSequence();
                    StopMovement();
                    ResetThreeHellState();
                    return;
                }

                if (Macro.getInstance.Flag_END)
                {
                    RememberReturnRouteBeforeEndOff();
                    Macro.getInstance.Flag_END = false;
                    CancelPumpkinFullSequence();

                    lock (actionKeyLock)
                    {
                        // 눌림 잔류 방지용 정리
                        ReleaseStationaryCombatKeys();

                        // END 토글 대상은 저주만
                        ReleaseLoopKeys();
                    }

                    StopMovement();
                    ResetThreeHellState();
                }
                else
                {
                    timerForBoMu.Start();
                    timerForMovingFlag.Start();
                    timerForSajahu.Start();
                    timerForAlt2.Start();

                    firstRunFlag = true;
                    ResetThreeHellState();

                    Macro.getInstance.Flag_END = true;
                    setMagicNumber();
                    ResumeReturnRouteAfterEndOn();

                    synth.Rate = SayRate;
                    synth.Volume = 100;

                    lock (actionKeyLock)
                    {
                        // 시작 시 저주만 활성화
                        ReleaseStationaryCombatKeys();
                        PressCurseIfRunning();
                        SyncPickupFromCurrentPositionIfRunning();
                    }
                }
            }
            if (tempKey.ToString().ToUpper() == "INSERT")
            {
                ToggleAutoExploreMode();
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


                //산적
                //16:23:33:547	XY(791,0)	RGB(18,19,17)
                //16:23:37:332    XY(807, 26)  RGB(18, 19, 17)
                //for (int i = 791; i < 807; i++)
                //{
                //    for (int j = 0; j < 26; j++)
                //    {
                //        hi3(i, j);
                //    }
                //}

                //흉가,선녀
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
                hi2();
            }
            if (tempKey.ToString().ToUpper() == "F5")
            {
                SaveDownRouteKey();
            }

            if (tempKey.ToString().ToUpper() == "F7")
            {
                StartPumpkinFullSequenceAsync();
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

        static void SayText(string text)
        {
            synth.Speak(text);
        }

        static void SayPumpkinText(string text)
        {
            if (!UsePumpkinSay)
                return;

            SayText(text);
        }

        static void SayRouteChangeText(string text)
        {
            if (!UseRouteChangeSay)
                return;

            SayText(text);
        }

        //private bool ShouldCastThreeHellOnStall(string currentKey)
        //{
        //    if (string.IsNullOrEmpty(currentKey))
        //        return false;

        //    System.DateTime now = System.DateTime.Now;

        //    if (lastPositionKey != currentKey)
        //    {
        //        lastPositionKey = currentKey;
        //        lastPositionChangedTime = now;
        //        threeHellCastOnCurrentStall = false;
        //        return false;
        //    }

        //    if (threeHellCastOnCurrentStall)
        //        return false;

        //    if ((now - lastPositionChangedTime).TotalMilliseconds >= threeHellStationaryMs)
        //    {
        //        threeHellCastOnCurrentStall = true;
        //        return true;
        //    }

        //    return false;
        //}


        private System.DateTime lastThreeHellCastTime = System.DateTime.MinValue;

        private void ResetThreeHellState()
        {
            lastPositionKey = "";
            lastPositionChangedTime = System.DateTime.MinValue;
            lastThreeHellCastTime = System.DateTime.MinValue;
            ResetHellfireStallState();
        }

        private void ResetHellfireStallState()
        {
            lastHellfirePositionKey = "";
            lastHellfirePositionChangedTime = System.DateTime.MinValue;
            lastHellfireCastTime = System.DateTime.MinValue;
        }

        private bool IsHellfireStallReady(string currentKey)
        {
            if (string.IsNullOrEmpty(currentKey))
                return false;

            System.DateTime now = System.DateTime.Now;

            if (lastHellfirePositionKey != currentKey)
            {
                lastHellfirePositionKey = currentKey;
                lastHellfirePositionChangedTime = now;
                lastHellfireCastTime = System.DateTime.MinValue;
                return false;
            }

            if (lastHellfireCastTime == System.DateTime.MinValue)
                return (now - lastHellfirePositionChangedTime).TotalMilliseconds >= hellfireStationaryMs;

            return (now - lastHellfireCastTime).TotalMilliseconds >= hellfireStationaryMs;
        }

        private void MarkHellfireStallCast()
        {
            lastHellfireCastTime = System.DateTime.Now;
        }

        private bool ShouldCastThreeHellOnStall(string currentKey)
        {
            if (string.IsNullOrEmpty(currentKey))
                return false;

            System.DateTime now = System.DateTime.Now;

            // 위치가 바뀌면 기준 시간 전부 리셋
            if (lastPositionKey != currentKey)
            {
                lastPositionKey = currentKey;
                lastPositionChangedTime = now;
                lastThreeHellCastTime = System.DateTime.MinValue;
                return false;
            }

            // 처음 1회는 key가 바뀐 뒤 1초 이상 유지되어야 발동
            if (lastThreeHellCastTime == System.DateTime.MinValue)
            {
                if ((now - lastPositionChangedTime).TotalMilliseconds >= threeHellStationaryMs) //threeHellStationaryMs
                {
                    lastThreeHellCastTime = now;
                    return true;
                }

                return false;
            }

            // 이후에는 마지막 발동 시점 기준으로 1초마다 반복 발동
            if ((now - lastThreeHellCastTime).TotalMilliseconds >= threeHellStationaryMs) //threeHellStationaryMs
            {
                lastThreeHellCastTime = now;
                return true;
            }

            return false;
        }











        //private void ResetThreeHellState()
        //{
        //    lastPositionKey = "";
        //    lastPositionChangedTime = System.DateTime.MinValue;
        //    threeHellCastOnCurrentStall = false;
        //}


        //private bool ShouldCastThreeHellOnStall(string currentKey)
        //{
        //    if (string.IsNullOrEmpty(currentKey))
        //        return false;

        //    System.DateTime now = System.DateTime.Now;

        //    if (lastPositionKey != currentKey)
        //    {
        //        lastPositionKey = currentKey;
        //        lastPositionChangedTime = now;
        //        threeHellCastOnCurrentStall = false;
        //        return false;
        //    }

        //    if ((now - lastPositionChangedTime).TotalMilliseconds < threeHellStationaryMs)
        //        return false;

        //    if (threeHellCastOnCurrentStall)
        //        return false;

        //    threeHellCastOnCurrentStall = true;
        //    return true;
        //}


        private void ResetNoMoveState()
        {
            lastPositionKey2 = "";
            lastPositionChangedTime2 = System.DateTime.MinValue;
            findWall = false;
        }

        private bool FindWall(string currentKey)
        {
            if (string.IsNullOrEmpty(currentKey))
                return false;

            System.DateTime now = System.DateTime.Now;

            if (lastPositionKey2 != currentKey)
            {
                lastPositionKey2 = currentKey;
                lastPositionChangedTime2 = now;
                findWall = false;
                return false;
            }

            if ((now - lastPositionChangedTime2).TotalMilliseconds < findWallMs)
                return false;

            if (findWall)
                return false;

            findWall = true;
            return true;
        }


        private static byte GetVkNumber(int number)
        {
            if (number < 0 || number > 9)
                throw new ArgumentOutOfRangeException(nameof(number), "0~9만 가능합니다.");

            return (byte)(0x30 + number);
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
            }
            if (Macro.getInstance.Flag_F2)
            {
            }
            if (Macro.getInstance.Flag_F3)
            {

            }
            if (Macro.getInstance.Flag_F4)
            {
            }
            if (Macro.getInstance.Flag_F5)
            {
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

            Macro.getInstance.Flag4 = false;
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
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            try
            {
                await Task.Run(() =>
                {
                    while (!ct.IsCancellationRequested)
                    {
                        sendKeyInput(tokenSource2);
                        Thread.Sleep(5);
                    }
                }, ct);
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                tokenSource2.Dispose();
            }
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

        private void attackUsingHell2()
        {
            Thread.Sleep(10);
            SK.sendkeyEsc(5);
            SK.sendkeyNumber(10, (int)magic1);

            User32.API.keybd_event((byte)magic1, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic1, 0, 2, 0);
            SK.sendkeyHome(5);

            int hellfireMode = 0;
            if (HellfireDirectionIndex == 0)
            {
                System.Random tempRandom = new System.Random((int)System.DateTime.Now.Ticks);
                int random1to10 = tempRandom.Next(1, 10);
                if(random1to10 > 4)
                {
                    hellfireMode = 1;
                }
            }

            if (hellfireMode == 0)
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
            else if (hellfireMode == 1)
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



        private void StopMovement()
        {
            lock (moveLock)
            {
                if (!string.IsNullOrEmpty(lastMove))
                {
                    ReleaseDirection(lastMove);
                }
                else
                {
                    User32.API.keybd_event(0X25, 0, 2, 0);
                    User32.API.keybd_event(0X26, 0, 2, 0);
                    User32.API.keybd_event(0X27, 0, 2, 0);
                    User32.API.keybd_event(0X28, 0, 2, 0);
                }

                lastMove = "";
                lastMoveKey = "";
                lastDirectionInputTime = System.DateTime.MinValue;
                lastKeyChangeTime = System.DateTime.MinValue;
                lastInputTime = System.DateTime.MinValue;
                segmentedMoveLastPositionKey = "";
                segmentedMovePauseUntil = System.DateTime.MinValue;
                ClearReservedTurn();
                ResetThreeHellState();
            }
        }



        private void RefreshMoveDictionaryIfNeeded()
        {
            string path = moveConfigPath;

            if (!File.Exists(path))
            {
                directionDic = new Dictionary<string, string>();
                moveConfigLoadedPath = path;
                moveConfigLastWriteTime = System.DateTime.MinValue;
                return;
            }

            System.DateTime lastWriteTime = File.GetLastWriteTime(path);

            if (directionDic != null &&
                directionDic.Count > 0 &&
                path == moveConfigLoadedPath &&
                lastWriteTime == moveConfigLastWriteTime)
            {
                return;
            }

            string json = File.ReadAllText(path);
            directionDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                           ?? new Dictionary<string, string>();

            moveConfigLoadedPath = path;
            moveConfigLastWriteTime = lastWriteTime;
        }

        private string GetDirectionFromCache(string key)
        {
            RefreshMoveDictionaryIfNeeded();

            if (TryGetRouteDirection(directionDic, key, out string value))
            {
                return value;
            }

            return null;
        }

        private void RefreshCastleMoveDictionaryIfNeeded()
        {
            if (!File.Exists(castleConfigPath))
            {
                castleDirectionDic = new Dictionary<string, string>();
                castleConfigLastWriteTime = System.DateTime.MinValue;
                return;
            }

            System.DateTime lastWriteTime = File.GetLastWriteTime(castleConfigPath);

            if (castleDirectionDic != null && castleDirectionDic.Count > 0 && lastWriteTime == castleConfigLastWriteTime)
            {
                return;
            }

            string json = File.ReadAllText(castleConfigPath);
            castleDirectionDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                                 ?? new Dictionary<string, string>();

            castleConfigLastWriteTime = lastWriteTime;
        }

        private string GetCastleDirectionFromCache(string key)
        {
            RefreshCastleMoveDictionaryIfNeeded();

            if (TryGetRouteDirection(castleDirectionDic, key, out string value))
            {
                return value;
            }

            return null;
        }

        public void setMoveDictionary()
        {
            if (!File.Exists("config.json"))
            {
                directionDic = new Dictionary<string, string>();
                return;
            }

            string json = File.ReadAllText("config.json");
            directionDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                           ?? new Dictionary<string, string>();
        }

        private void ToggleAutoExploreMode()
        {
            bool shouldPrimePickup = false;

            lock (autoExploreLock)
            {
                autoExploreMode = !autoExploreMode;

                if (autoExploreMode)
                {
                    LoadAutoExploreRoute(moveConfigPath);
                    autoExploreUsingReturnRoute = false;
                    autoExploreReturnRouteResumeAfterEnd = false;
                    pumpkinReturnRouteResumePending = false;
                    movingOpt = true;
                    autoExploreLastKey = "";
                    autoExploreLastDir = "";
                    autoExploreLastDeviationRouteKey = "";
                    autoExploreReturnTapKey = "";
                    autoExploreReturnTapDir = "";
                    autoExploreDeviationReturnPending = false;
                    autoExploreReturnTapTime = System.DateTime.MinValue;
                    autoExploreSamePointFailCount = 0;
                    ResetSpecialRouteDetour();
                    shouldPrimePickup = true;
                    SayText("자동 탐색 시작");
                }
                else
                {
                    autoExploreLastKey = "";
                    autoExploreLastDir = "";
                    autoExploreLastDeviationRouteKey = "";
                    autoExploreReturnTapKey = "";
                    autoExploreReturnTapDir = "";
                    autoExploreDeviationReturnPending = false;
                    autoExploreReturnTapTime = System.DateTime.MinValue;
                    autoExploreSamePointFailCount = 0;
                    ResetSpecialRouteDetour();
                    autoExploreManualRoute = new Dictionary<string, string>();
                    autoExploreManualRouteExpanded = new Dictionary<string, string>();
                    autoExploreUsingReturnRoute = false;
                    autoExploreReturnRouteResumeAfterEnd = false;
                    pumpkinReturnRouteResumePending = false;
                    movingOpt = false;
                    StopMovement();
                    SayText("자동 탐색 종료");
                }
            }

            if (shouldPrimePickup)
            {
                SyncPickupFromCurrentPositionIfRunning();
            }
        }

        private void RememberReturnRouteBeforeEndOff()
        {
            lock (autoExploreLock)
            {
                autoExploreReturnRouteResumeAfterEnd = autoExploreMode && autoExploreUsingReturnRoute;
            }
        }

        private void ResumeReturnRouteAfterEndOn()
        {
            lock (autoExploreLock)
            {
                if (!autoExploreMode || !autoExploreReturnRouteResumeAfterEnd)
                    return;

                LoadAutoExploreRoute(downConfigPath);
                autoExploreUsingReturnRoute = true;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                SayRouteChangeText("10굴에서 5굴 복귀 경로 유지");
            }
        }

        private string GetAutoExploreDirection(int mapNo, string xyText, string currentKey)
        {
            lock (autoExploreLock)
            {
                if (mapNo == GwallyeongCastleMapNo)
                {
                    if (autoExploreUsingReturnRoute)
                    {
                        LoadAutoExploreRoute(moveConfigPath);
                        autoExploreUsingReturnRoute = false;
                        autoExploreReturnRouteResumeAfterEnd = false;
                    }

                    string castleDir = GetCastleDirectionFromCache(currentKey);
                    if (string.IsNullOrEmpty(castleDir))
                        castleDir = GetGwallyeongCastleHouse5Direction(currentKey);

                    castleDir = GetSpecialRouteDirectionWithDetour(currentKey, castleDir);
                    TrackAutoExploreSelectedDirection(currentKey, castleDir);
                    return castleDir;
                }

                if (mapNo < 1 || mapNo > AutoExploreMaxMapNo || string.IsNullOrEmpty(xyText))
                {
                    return GetDirectionFromCache(currentKey);
                }

                if (mapNo == GwallyeongHauntedEntranceMapNo)
                {
                    if (autoExploreUsingReturnRoute)
                    {
                        LoadAutoExploreRoute(moveConfigPath);
                        autoExploreUsingReturnRoute = false;
                        autoExploreReturnRouteResumeAfterEnd = false;
                    }

                    string entranceDir = GetConfigOnlyAutoExploreDirection(currentKey);
                    entranceDir = GetSpecialRouteDirectionWithDetour(currentKey, entranceDir);
                    TrackAutoExploreSelectedDirection(currentKey, entranceDir);
                    return entranceDir;
                }

                if (UpdateAutoExploreRouteByPosition(currentKey))
                    return "";

                ObserveAutoExplorePosition(currentKey);

                if (TryGetHauntedHouse5UpperRouteDirection(mapNo, xyText, currentKey, out string specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                if (TryGetHauntedHouse6UpperRouteDirection(mapNo, xyText, currentKey, out specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                if (TryGetHauntedHouse7UpperRouteDirection(mapNo, xyText, currentKey, out specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                if (TryGetHauntedHouse8UpperRouteDirection(mapNo, xyText, currentKey, out specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                if (TryGetHauntedHouse9UpperRouteDirection(mapNo, xyText, currentKey, out specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                if (TryGetHauntedHouse10UpperRouteDirection(mapNo, xyText, currentKey, out specialDir))
                {
                    TrackAutoExploreSelectedDirection(currentKey, specialDir);
                    return specialDir;
                }

                string selectedDir = ChooseAutoExploreDirection(currentKey, null);

                TrackAutoExploreSelectedDirection(currentKey, selectedDir);
                return selectedDir;
            }
        }

        private string GetConfigOnlyAutoExploreDirection(string currentKey)
        {
            if (TryGetManualRouteDirection(currentKey, out string manualDir))
                return manualDir;

            if (TryGetDirectionTowardManualRoute(currentKey, out string returnDir))
                return returnDir;

            if (autoExploreUsingReturnRoute)
                return null;

            return GetDirectionFromCache(currentKey);
        }

        private bool TryGetHauntedHouse5UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 5)
            {
                ResetHauntedHouse5UpperRoute();
                hauntedHouse5UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse5UpperRoutePhase == 0)
            {
                if (x != 9 || y != 13)
                    return false;

                if (hauntedHouse5UpperRouteCompleted)
                    return false;

                hauntedHouse5UpperRoutePhase = 1;
                hauntedHouse5UpperRouteBounceCount = 0;
                hauntedHouse5UpperRouteReachedEast = false;
                hauntedHouse5UpperRouteLoopIndex = 0;
                SayRouteChangeText("5굴 상행 특수 경로 시작");
            }

            if (hauntedHouse5UpperRoutePhase == 1)
            {
                if (x == 9 && y == 13)
                {
                    if (hauntedHouse5UpperRouteReachedEast)
                    {
                        hauntedHouse5UpperRouteBounceCount++;
                        hauntedHouse5UpperRouteReachedEast = false;

                        if (hauntedHouse5UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse5UpperRoutePhase = 2;
                            hauntedHouse5UpperRouteExitRetryPending = true;
                            direction = "서";
                            return true;
                        }
                    }

                    direction = "동";
                    return true;
                }

                if (x == 10 && y == 13)
                {
                    hauntedHouse5UpperRouteReachedEast = true;
                    direction = "서";
                    return true;
                }

                ResetHauntedHouse5UpperRoute();
                return false;
            }

            if (hauntedHouse5UpperRoutePhase == 2)
            {
                if (x == 8 && y == 13)
                {
                    hauntedHouse5UpperRouteExitRetryPending = false;
                    hauntedHouse5UpperRoutePhase = 3;
                    hauntedHouse5UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse5ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 9 && y == 13 && hauntedHouse5UpperRouteExitRetryPending)
                {
                    hauntedHouse5UpperRouteExitRetryPending = false;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 8, 13);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse5UpperRoutePhase == 3)
            {
                if (x == 8 && y == 13 && hauntedHouse5UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse5UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                direction = GetHauntedHouse5ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 8 && y == 13 &&
                    hauntedHouse5UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse5UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse5UpperRoutePhase == 4)
            {
                if (x == 16 && y == 13)
                {
                    ResetHauntedHouse5UpperRoute();
                    hauntedHouse5UpperRouteCompleted = true;
                    SayRouteChangeText("5굴 상행 특수 경로 종료");
                    return false;
                }

                direction = GetDirectionTowardPoint(x, y, 16, 13);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse5ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 8, 13 },
                { 8, 12 },
                { 9, 12 },
                { 10, 12 },
                { 11, 12 },
                { 11, 13 },
                { 11, 14 },
                { 10, 14 },
                { 9, 14 },
                { 8, 14 },
                { 8, 13 }
            };

            if (hauntedHouse5UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse5UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse5UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse5UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse5UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse5UpperRouteLoopIndex++;
                return GetHauntedHouse5ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private string GetDirectionTowardPoint(int x, int y, int targetX, int targetY)
        {
            if (x < targetX) return "동";
            if (x > targetX) return "서";
            if (y < targetY) return "남";
            if (y > targetY) return "북";
            return "";
        }

        private int GetUpperRouteExitRetryPressMs()
        {
            return Math.Max(segmentedMovePressMs, nearTurnSlowRepeatMs);
        }

        private int GetUpperRouteStepPressMs(string key)
        {
            int pressMs = GetUpperRouteExitRetryPressMs();

            if (!IsHauntedHouse10StallPoint(key))
                return pressMs;

            pressMs = Math.Max(pressMs, 180);

            if (autoExploreSamePointFailCount > 0)
                pressMs = Math.Max(pressMs, Math.Min(320, 220 + (autoExploreSamePointFailCount * 40)));

            return pressMs;
        }

        private bool IsHauntedHouse10StallPoint(string key)
        {
            if (hauntedHouse10UpperRoutePhase != 3 && hauntedHouse10UpperRoutePhase != 4)
                return false;

            if (!TryParseMoveKey(key, out int mapNo, out int x, out int y))
                return false;

            return mapNo == 10 && y == 14 && (x == 14 || x == 15);
        }

        private void ResetHauntedHouse5UpperRoute()
        {
            hauntedHouse5UpperRoutePhase = 0;
            hauntedHouse5UpperRouteBounceCount = 0;
            hauntedHouse5UpperRouteReachedEast = false;
            hauntedHouse5UpperRouteExitRetryPending = false;
            hauntedHouse5UpperRouteLoopIndex = 0;
        }

        private bool TryGetHauntedHouse6UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 6)
            {
                ResetHauntedHouse6UpperRoute();
                hauntedHouse6UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse6UpperRoutePhase == 0)
            {
                if (y != 23 || x < 10 || x > 14)
                    return false;

                if (hauntedHouse6UpperRouteCompleted)
                    return false;

                hauntedHouse6UpperRouteBounceCount = 0;
                hauntedHouse6UpperRouteReachedWest = false;
                hauntedHouse6UpperRouteLoopIndex = 0;
                SayRouteChangeText("6굴 상행 특수 경로 시작");

                if (x == 10)
                {
                    hauntedHouse6UpperRoutePhase = 2;
                    hauntedHouse6UpperRouteReachedWest = true;
                }
                else if (x == 11)
                {
                    hauntedHouse6UpperRoutePhase = 2;
                }
                else
                {
                    hauntedHouse6UpperRoutePhase = 1;
                }
            }

            if (hauntedHouse6UpperRoutePhase == 1)
            {
                if (x == 11 && y == 23)
                {
                    hauntedHouse6UpperRoutePhase = 2;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 11, 23);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse6UpperRoutePhase == 2)
            {
                if (x == 11 && y == 23)
                {
                    if (hauntedHouse6UpperRouteReachedWest)
                    {
                        hauntedHouse6UpperRouteBounceCount++;
                        hauntedHouse6UpperRouteReachedWest = false;

                        if (hauntedHouse6UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse6UpperRoutePhase = 3;
                            hauntedHouse6UpperRouteExitRetryPending = true;
                            direction = "서";
                            return true;
                        }
                    }

                    direction = "서";
                    return true;
                }

                if (x == 10 && y == 23)
                {
                    hauntedHouse6UpperRouteReachedWest = true;
                    direction = "동";
                    return true;
                }

                ResetHauntedHouse6UpperRoute();
                return false;
            }

            if (hauntedHouse6UpperRoutePhase == 3)
            {
                if (x == 9 && y == 23)
                {
                    hauntedHouse6UpperRouteExitRetryPending = false;
                    hauntedHouse6UpperRoutePhase = 4;
                    hauntedHouse6UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse6ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 11 && y == 23 && hauntedHouse6UpperRouteExitRetryPending)
                {
                    hauntedHouse6UpperRouteExitRetryPending = false;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 9, 23);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse6UpperRoutePhase == 4)
            {
                if (x == 9 && y == 23 && hauntedHouse6UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse6UpperRoutePhase = 5;
                    direction = "동";
                    return true;
                }

                direction = GetHauntedHouse6ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 9 && y == 23 &&
                    hauntedHouse6UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse6UpperRoutePhase = 5;
                    direction = "동";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse6UpperRoutePhase == 5)
            {
                if (x == 14 && y == 23)
                {
                    ResetHauntedHouse6UpperRoute();
                    hauntedHouse6UpperRouteCompleted = true;
                    SayRouteChangeText("6굴 상행 특수 경로 종료");
                    return false;
                }

                direction = GetDirectionTowardPoint(x, y, 14, 23);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse6ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 9, 23 },
                { 9, 22 },
                { 10, 22 },
                { 11, 22 },
                { 12, 22 },
                { 12, 23 },
                { 12, 24 },
                { 11, 24 },
                { 10, 24 },
                { 9, 24 },
                { 9, 23 }
            };

            if (hauntedHouse6UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse6UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse6UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse6UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse6UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse6UpperRouteLoopIndex++;
                return GetHauntedHouse6ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private void ResetHauntedHouse6UpperRoute()
        {
            hauntedHouse6UpperRoutePhase = 0;
            hauntedHouse6UpperRouteBounceCount = 0;
            hauntedHouse6UpperRouteReachedWest = false;
            hauntedHouse6UpperRouteExitRetryPending = false;
            hauntedHouse6UpperRouteLoopIndex = 0;
        }

        private bool TryGetHauntedHouse7UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 7)
            {
                ResetHauntedHouse7UpperRoute();
                hauntedHouse7UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse7UpperRoutePhase == 0)
            {
                if (x != 14 || y != 10)
                    return false;

                if (hauntedHouse7UpperRouteCompleted)
                    return false;

                hauntedHouse7UpperRoutePhase = 1;
                hauntedHouse7UpperRouteBounceCount = 0;
                hauntedHouse7UpperRouteReachedNorth = false;
                hauntedHouse7UpperRouteLoopIndex = 0;
                SayRouteChangeText("7굴 상행 특수 경로 시작");
            }

            if (hauntedHouse7UpperRoutePhase == 1)
            {
                if (x == 14 && y == 10)
                {
                    if (hauntedHouse7UpperRouteReachedNorth)
                    {
                        hauntedHouse7UpperRouteBounceCount++;
                        hauntedHouse7UpperRouteReachedNorth = false;

                        if (hauntedHouse7UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse7UpperRoutePhase = 2;
                            hauntedHouse7UpperRouteExitRetryPending = true;
                            direction = "북";
                            return true;
                        }
                    }

                    direction = "북";
                    return true;
                }

                if (x == 14 && y == 9)
                {
                    hauntedHouse7UpperRouteReachedNorth = true;
                    direction = "남";
                    return true;
                }

                ResetHauntedHouse7UpperRoute();
                return false;
            }

            if (hauntedHouse7UpperRoutePhase == 2)
            {
                if (x == 14 && y == 8)
                {
                    hauntedHouse7UpperRouteExitRetryPending = false;
                    hauntedHouse7UpperRoutePhase = 3;
                    hauntedHouse7UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse7ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 14 && y == 10 && hauntedHouse7UpperRouteExitRetryPending)
                {
                    hauntedHouse7UpperRouteExitRetryPending = false;
                    direction = "북";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 14, 8);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse7UpperRoutePhase == 3)
            {
                if (x == 14 && y == 8 && hauntedHouse7UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse7UpperRoutePhase = 4;
                    direction = "남";
                    return true;
                }

                direction = GetHauntedHouse7ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 14 && y == 8 &&
                    hauntedHouse7UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse7UpperRoutePhase = 4;
                    direction = "남";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse7UpperRoutePhase == 4)
            {
                if (x == 14 && y == 10)
                {
                    ResetHauntedHouse7UpperRoute();
                    hauntedHouse7UpperRouteCompleted = true;
                    SayRouteChangeText("7굴 상행 특수 경로 종료");
                    return false;
                }

                direction = GetDirectionTowardPoint(x, y, 14, 10);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse7ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 14, 8 },
                { 15, 8 },
                { 15, 9 },
                { 15, 10 },
                { 15, 11 },
                { 14, 11 },
                { 13, 11 },
                { 13, 10 },
                { 13, 9 },
                { 13, 8 },
                { 14, 8 }
            };

            if (hauntedHouse7UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse7UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse7UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse7UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse7UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse7UpperRouteLoopIndex++;
                return GetHauntedHouse7ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private void ResetHauntedHouse7UpperRoute()
        {
            hauntedHouse7UpperRoutePhase = 0;
            hauntedHouse7UpperRouteBounceCount = 0;
            hauntedHouse7UpperRouteReachedNorth = false;
            hauntedHouse7UpperRouteExitRetryPending = false;
            hauntedHouse7UpperRouteLoopIndex = 0;
        }

        private bool TryGetHauntedHouse8UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 8)
            {
                ResetHauntedHouse8UpperRoute();
                hauntedHouse8UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse8UpperRoutePhase == 0)
            {
                if (x != 18 || y != 16)
                    return false;

                if (hauntedHouse8UpperRouteCompleted)
                    return false;

                hauntedHouse8UpperRoutePhase = 1;
                hauntedHouse8UpperRouteBounceCount = 0;
                hauntedHouse8UpperRouteReachedWest = false;
                hauntedHouse8UpperRouteLoopIndex = 0;
                SayRouteChangeText("8굴 상행 특수 경로 시작");
            }

            if (hauntedHouse8UpperRoutePhase == 1)
            {
                if (x == 18 && y == 16)
                {
                    if (hauntedHouse8UpperRouteReachedWest)
                    {
                        hauntedHouse8UpperRouteBounceCount++;
                        hauntedHouse8UpperRouteReachedWest = false;

                        if (hauntedHouse8UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse8UpperRoutePhase = 2;
                            hauntedHouse8UpperRouteExitRetryPending = true;
                            direction = "서";
                            return true;
                        }
                    }

                    direction = "서";
                    return true;
                }

                if (x == 17 && y == 16)
                {
                    hauntedHouse8UpperRouteReachedWest = true;
                    direction = "동";
                    return true;
                }

                ResetHauntedHouse8UpperRoute();
                return false;
            }

            if (hauntedHouse8UpperRoutePhase == 2)
            {
                if (x == 16 && y == 16)
                {
                    hauntedHouse8UpperRouteExitRetryPending = false;
                    hauntedHouse8UpperRoutePhase = 3;
                    hauntedHouse8UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse8ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 18 && y == 16 && hauntedHouse8UpperRouteExitRetryPending)
                {
                    hauntedHouse8UpperRouteExitRetryPending = false;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 16, 16);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse8UpperRoutePhase == 3)
            {
                if (x == 16 && y == 16 && hauntedHouse8UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse8UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                direction = GetHauntedHouse8ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 16 && y == 16 &&
                    hauntedHouse8UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse8UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse8UpperRoutePhase == 4)
            {
                if (x == 18 && y == 16)
                {
                    ResetHauntedHouse8UpperRoute();
                    hauntedHouse8UpperRouteCompleted = true;
                    SayRouteChangeText("8굴 상행 특수 경로 종료");
                    return false;
                }

                direction = GetDirectionTowardPoint(x, y, 18, 16);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse8ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 16, 16 },
                { 16, 15 },
                { 17, 15 },
                { 18, 15 },
                { 19, 15 },
                { 19, 16 },
                { 19, 17 },
                { 18, 17 },
                { 17, 17 },
                { 16, 17 },
                { 16, 16 }
            };

            if (hauntedHouse8UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse8UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse8UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse8UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse8UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse8UpperRouteLoopIndex++;
                return GetHauntedHouse8ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private void ResetHauntedHouse8UpperRoute()
        {
            hauntedHouse8UpperRoutePhase = 0;
            hauntedHouse8UpperRouteBounceCount = 0;
            hauntedHouse8UpperRouteReachedWest = false;
            hauntedHouse8UpperRouteExitRetryPending = false;
            hauntedHouse8UpperRouteLoopIndex = 0;
        }

        private bool TryGetHauntedHouse9UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 9)
            {
                ResetHauntedHouse9UpperRoute();
                hauntedHouse9UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse9UpperRoutePhase == 0)
            {
                if (x != 14 || y != 10)
                    return false;

                if (hauntedHouse9UpperRouteCompleted)
                    return false;

                hauntedHouse9UpperRoutePhase = 1;
                hauntedHouse9UpperRouteBounceCount = 0;
                hauntedHouse9UpperRouteReachedWest = false;
                hauntedHouse9UpperRouteLoopIndex = 0;
                SayRouteChangeText("9굴 상행 특수 경로 시작");
            }

            if (hauntedHouse9UpperRoutePhase == 1)
            {
                if (x == 14 && y == 10)
                {
                    if (hauntedHouse9UpperRouteReachedWest)
                    {
                        hauntedHouse9UpperRouteBounceCount++;
                        hauntedHouse9UpperRouteReachedWest = false;

                        if (hauntedHouse9UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse9UpperRoutePhase = 2;
                            hauntedHouse9UpperRouteExitRetryPending = true;
                            direction = "서";
                            return true;
                        }
                    }

                    direction = "서";
                    return true;
                }

                if (x == 13 && y == 10)
                {
                    hauntedHouse9UpperRouteReachedWest = true;
                    direction = "동";
                    return true;
                }

                ResetHauntedHouse9UpperRoute();
                return false;
            }

            if (hauntedHouse9UpperRoutePhase == 2)
            {
                if (x == 12 && y == 10)
                {
                    hauntedHouse9UpperRouteExitRetryPending = false;
                    hauntedHouse9UpperRoutePhase = 3;
                    hauntedHouse9UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse9ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 14 && y == 10 && hauntedHouse9UpperRouteExitRetryPending)
                {
                    hauntedHouse9UpperRouteExitRetryPending = false;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 12, 10);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse9UpperRoutePhase == 3)
            {
                if (x == 12 && y == 10 && hauntedHouse9UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse9UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                direction = GetHauntedHouse9ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 12 && y == 10 &&
                    hauntedHouse9UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse9UpperRoutePhase = 4;
                    direction = "동";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse9UpperRoutePhase == 4)
            {
                if (x == 13 && y == 10)
                {
                    ResetHauntedHouse9UpperRoute();
                    hauntedHouse9UpperRouteCompleted = true;
                    SayRouteChangeText("9굴 상행 특수 경로 종료");
                    return false;
                }

                direction = GetDirectionTowardPoint(x, y, 13, 10);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse9ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 12, 10 },
                { 12, 9 },
                { 13, 9 },
                { 14, 9 },
                { 15, 9 },
                { 15, 10 },
                { 15, 11 },
                { 14, 11 },
                { 13, 11 },
                { 12, 11 },
                { 12, 10 }
            };

            if (hauntedHouse9UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse9UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse9UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse9UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse9UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse9UpperRouteLoopIndex++;
                return GetHauntedHouse9ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private void ResetHauntedHouse9UpperRoute()
        {
            hauntedHouse9UpperRoutePhase = 0;
            hauntedHouse9UpperRouteBounceCount = 0;
            hauntedHouse9UpperRouteReachedWest = false;
            hauntedHouse9UpperRouteExitRetryPending = false;
            hauntedHouse9UpperRouteLoopIndex = 0;
        }

        private bool TryGetHauntedHouse10UpperRouteDirection(int mapNo, string xyText, string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreUsingReturnRoute || mapNo != 10)
            {
                ResetHauntedHouse10UpperRoute();
                hauntedHouse10UpperRouteCompleted = false;
                return false;
            }

            if (!TryParseMoveKey(currentKey, out _, out int x, out int y))
                return false;

            if (hauntedHouse10UpperRoutePhase == 0)
            {
                if (x != 16 || y != 9)
                    return false;

                if (hauntedHouse10UpperRouteCompleted)
                    return false;

                hauntedHouse10UpperRoutePhase = 1;
                hauntedHouse10UpperRouteBounceCount = 0;
                hauntedHouse10UpperRouteReachedEast = false;
                hauntedHouse10UpperRouteLoopIndex = 0;
                SayRouteChangeText("10굴 상행 특수 경로 시작");
            }

            if (hauntedHouse10UpperRoutePhase == 1)
            {
                if (x == 16 && y == 14)
                {
                    hauntedHouse10UpperRoutePhase = 2;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 16, 14);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse10UpperRoutePhase == 2)
            {
                if (x == 14 && y == 14)
                {
                    hauntedHouse10UpperRoutePhase = 3;
                    direction = "동";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 14, 14);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse10UpperRoutePhase == 3)
            {
                if (x == 14 && y == 14)
                {
                    if (hauntedHouse10UpperRouteReachedEast)
                    {
                        hauntedHouse10UpperRouteBounceCount++;
                        hauntedHouse10UpperRouteReachedEast = false;

                        if (hauntedHouse10UpperRouteBounceCount >= HauntedHouseSpecialBounceTarget)
                        {
                            hauntedHouse10UpperRoutePhase = 4;
                            hauntedHouse10UpperRouteExitRetryPending = true;
                            direction = "서";
                            return true;
                        }
                    }

                    direction = "동";
                    return true;
                }

                if (x == 15 && y == 14)
                {
                    hauntedHouse10UpperRouteReachedEast = true;
                    direction = "서";
                    return true;
                }

                ResetHauntedHouse10UpperRoute();
                return false;
            }

            if (hauntedHouse10UpperRoutePhase == 4)
            {
                if (x == 13 && y == 14)
                {
                    hauntedHouse10UpperRouteExitRetryPending = false;
                    hauntedHouse10UpperRoutePhase = 5;
                    hauntedHouse10UpperRouteLoopIndex = 0;
                    direction = GetHauntedHouse10ClockwiseLoopDirection(x, y);
                    return true;
                }

                if (x == 14 && y == 14 && hauntedHouse10UpperRouteExitRetryPending)
                {
                    hauntedHouse10UpperRouteExitRetryPending = false;
                    direction = "서";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 13, 14);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse10UpperRoutePhase == 5)
            {
                if (x == 13 && y == 14 && hauntedHouse10UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse10UpperRoutePhase = 6;
                    direction = "동";
                    return true;
                }

                direction = GetHauntedHouse10ClockwiseLoopDirection(x, y);
                if (string.IsNullOrEmpty(direction) &&
                    x == 13 && y == 14 &&
                    hauntedHouse10UpperRouteLoopIndex >= 10)
                {
                    hauntedHouse10UpperRoutePhase = 6;
                    direction = "동";
                    return true;
                }

                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse10UpperRoutePhase == 6)
            {
                if (x == 16 && y == 14)
                {
                    hauntedHouse10UpperRoutePhase = 7;
                    direction = "북";
                    return true;
                }

                direction = GetDirectionTowardPoint(x, y, 16, 14);
                return !string.IsNullOrEmpty(direction);
            }

            if (hauntedHouse10UpperRoutePhase == 7)
            {
                if (x == 16 && y == 9)
                {
                    StartReverseAutoExploreRouteFromUpperRoute();
                    ResetHauntedHouse10UpperRoute();
                    SayRouteChangeText("10굴 하행 시작");
                    direction = GetConfigOnlyAutoExploreDirection(currentKey);
                    return !string.IsNullOrEmpty(direction);
                }

                direction = GetDirectionTowardPoint(x, y, 16, 9);
                return !string.IsNullOrEmpty(direction);
            }

            return false;
        }

        private string GetHauntedHouse10ClockwiseLoopDirection(int x, int y)
        {
            int[,] points = new int[,]
            {
                { 13, 14 },
                { 13, 13 },
                { 14, 13 },
                { 15, 13 },
                { 16, 13 },
                { 16, 14 },
                { 16, 15 },
                { 15, 15 },
                { 14, 15 },
                { 13, 15 },
                { 13, 14 }
            };

            if (hauntedHouse10UpperRouteLoopIndex >= 10)
                return "";

            int currentX = points[hauntedHouse10UpperRouteLoopIndex, 0];
            int currentY = points[hauntedHouse10UpperRouteLoopIndex, 1];
            int nextX = points[hauntedHouse10UpperRouteLoopIndex + 1, 0];
            int nextY = points[hauntedHouse10UpperRouteLoopIndex + 1, 1];

            if (x == nextX && y == nextY)
            {
                hauntedHouse10UpperRouteLoopIndex++;
                return GetHauntedHouse10ClockwiseLoopDirection(x, y);
            }

            if (x != currentX || y != currentY)
                return GetDirectionTowardPoint(x, y, currentX, currentY);

            return GetDirectionTowardPoint(x, y, nextX, nextY);
        }

        private void StartReverseAutoExploreRouteFromUpperRoute()
        {
            Dictionary<string, string> downRoute = LoadMoveDictionary(downConfigPath);

            autoExploreManualRoute = CloneManualRouteDictionary(downRoute);
            autoExploreManualRouteExpanded = BuildExpandedManualRoute(autoExploreManualRoute);
            autoExploreUsingReturnRoute = true;
            pumpkinReturnRouteResumePending = false;
            autoExploreReturnRouteResumeAfterEnd = false;
            autoExploreReturnEndCandidateKey = "";
            autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
            autoExploreReturnTapKey = "";
            autoExploreReturnTapDir = "";
            autoExploreDeviationReturnPending = false;
            autoExploreReturnTapTime = System.DateTime.MinValue;
        }

        private Dictionary<string, string> BuildReverseRoute(Dictionary<string, string> route)
        {
            Dictionary<string, string> reverseRoute = new Dictionary<string, string>();

            if (route == null)
                return reverseRoute;

            foreach (KeyValuePair<string, string> item in route)
            {
                if (string.IsNullOrEmpty(item.Value))
                    continue;

                if (!TryGetNextMoveKey(item.Key, item.Value, out string nextKey))
                    continue;

                string oppositeDir = GetOppositeDirection(item.Value);
                if (!string.IsNullOrEmpty(oppositeDir))
                    reverseRoute[nextKey] = oppositeDir;
            }

            return reverseRoute;
        }

        private void ResetHauntedHouse10UpperRoute()
        {
            hauntedHouse10UpperRoutePhase = 0;
            hauntedHouse10UpperRouteBounceCount = 0;
            hauntedHouse10UpperRouteReachedEast = false;
            hauntedHouse10UpperRouteExitRetryPending = false;
            hauntedHouse10UpperRouteLoopIndex = 0;
        }

        private string GetSpecialRouteDirectionWithDetour(string currentKey, string baseDir)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(baseDir))
            {
                ResetSpecialRouteDetour();
                return baseDir;
            }

            if (!IsCastleSpecialRouteDetourKey(currentKey))
            {
                ResetSpecialRouteDetour();
                return baseDir;
            }

            System.DateTime now = System.DateTime.Now;

            if (specialRouteDetourActive)
            {
                if (currentKey != specialRouteDetourLastKey)
                {
                    specialRouteDetourLastKey = currentKey;
                    specialRouteDetourIndex++;
                    specialRouteBlockedSince = now;
                }
                else if ((now - specialRouteBlockedSince).TotalMilliseconds >= SpecialRouteBlockedMs &&
                         TrySwitchBlockedCastleDetourSide(currentKey, out string retryDir))
                {
                    SayText("길막힘 반대 우회");
                    return retryDir;
                }

                if (specialRouteDetourIndex < specialRouteDetourDirs.Length)
                    return specialRouteDetourDirs[specialRouteDetourIndex];

                ResetSpecialRouteDetour();
                return baseDir;
            }

            if (specialRouteBlockedKey != currentKey || specialRouteBlockedDir != baseDir)
            {
                specialRouteBlockedKey = currentKey;
                specialRouteBlockedDir = baseDir;
                specialRouteBlockedSince = now;
                return baseDir;
            }

            if ((now - specialRouteBlockedSince).TotalMilliseconds < SpecialRouteBlockedMs)
                return baseDir;

            StartSpecialRouteDetour(currentKey, baseDir);
            if (specialRouteDetourDirs.Length == 0)
                return baseDir;

            SayText("길막힘 우회");
            return specialRouteDetourDirs[0];
        }

        private void StartSpecialRouteDetour(string currentKey, string baseDir)
        {
            if (!TryBuildCastleDShapeDetour(currentKey, baseDir, out string[] detourDirs))
            {
                string sideDir = GetRightDirection(baseDir);
                string restoreDir = GetOppositeDirection(sideDir);

                detourDirs = string.IsNullOrEmpty(sideDir) || string.IsNullOrEmpty(restoreDir)
                    ? new string[0]
                    : BuildDShapeDetourDirs(sideDir, baseDir, 2);
            }

            specialRouteDetourDirs = detourDirs;
            specialRouteDetourIndex = 0;
            specialRouteDetourLastKey = currentKey;
            specialRouteDetourBaseDir = baseDir;
            specialRouteDetourActive = specialRouteDetourDirs.Length > 0;
            specialRouteBlockedSince = System.DateTime.Now;
        }

        private bool IsCastleSpecialRouteDetourKey(string currentKey)
        {
            if (!TryParseMoveKey(currentKey, out int mapNo, out _, out _))
                return false;

            return mapNo == GwallyeongCastleMapNo;
        }

        private bool TrySwitchBlockedCastleDetourSide(string currentKey, out string retryDir)
        {
            retryDir = "";

            if (specialRouteDetourIndex != 0 ||
                specialRouteDetourDirs.Length == 0 ||
                string.IsNullOrEmpty(specialRouteDetourBaseDir))
            {
                return false;
            }

            string blockedSideDir = specialRouteDetourDirs[0];
            if (!TryBuildCastleDShapeDetour(currentKey, specialRouteDetourBaseDir, out string[] retryDirs, blockedSideDir))
            {
                string alternateSideDir = GetOppositeDirection(blockedSideDir);
                retryDirs = BuildDShapeDetourDirs(alternateSideDir, specialRouteDetourBaseDir, 2);
            }

            if (retryDirs.Length == 0 || retryDirs[0] == blockedSideDir)
                return false;

            specialRouteDetourDirs = retryDirs;
            specialRouteDetourIndex = 0;
            specialRouteDetourLastKey = currentKey;
            specialRouteBlockedSince = System.DateTime.Now;

            retryDir = specialRouteDetourDirs[0];
            return true;
        }

        private bool TryBuildCastleDShapeDetour(string currentKey, string baseDir, out string[] detourDirs, string skipSideDir = "")
        {
            detourDirs = new string[0];

            string rightDir = GetRightDirection(baseDir);
            string leftDir = GetLeftDirection(baseDir);
            string[] sideDirs = new string[] { rightDir, leftDir };

            string[] bestDirs = new string[0];
            int bestScore = int.MaxValue;

            for (int sideIndex = 0; sideIndex < sideDirs.Length; sideIndex++)
            {
                string sideDir = sideDirs[sideIndex];
                if (string.IsNullOrEmpty(sideDir) || sideDir == skipSideDir)
                    continue;

                for (int forwardSteps = 2; forwardSteps <= SpecialRouteMaxDetourForwardSteps; forwardSteps++)
                {
                    string[] candidateDirs = BuildDShapeDetourDirs(sideDir, baseDir, forwardSteps);
                    if (!TryGetKeyAfterDirections(currentKey, candidateDirs, out string rejoinKey))
                        continue;

                    if (!TryGetCastleDetourRejoinScore(rejoinKey, baseDir, forwardSteps, sideIndex, out int score))
                        continue;

                    if (score >= bestScore)
                        continue;

                    bestScore = score;
                    bestDirs = candidateDirs;
                }
            }

            if (bestDirs.Length == 0)
                return false;

            detourDirs = bestDirs;
            return true;
        }

        private string[] BuildDShapeDetourDirs(string sideDir, string baseDir, int forwardSteps)
        {
            string restoreDir = GetOppositeDirection(sideDir);
            if (string.IsNullOrEmpty(sideDir) || string.IsNullOrEmpty(baseDir) || string.IsNullOrEmpty(restoreDir) || forwardSteps <= 0)
                return new string[0];

            string[] dirs = new string[forwardSteps + 2];
            dirs[0] = sideDir;

            for (int i = 0; i < forwardSteps; i++)
                dirs[i + 1] = baseDir;

            dirs[dirs.Length - 1] = restoreDir;
            return dirs;
        }

        private bool TryGetKeyAfterDirections(string currentKey, string[] dirs, out string resultKey)
        {
            resultKey = currentKey;

            if (string.IsNullOrEmpty(currentKey) || dirs == null || dirs.Length == 0)
                return false;

            foreach (string dir in dirs)
            {
                if (!TryGetNextMoveKey(resultKey, dir, out string nextKey))
                    return false;

                resultKey = nextKey;
            }

            return true;
        }

        private bool TryGetCastleDetourRejoinScore(string rejoinKey, string baseDir, int forwardSteps, int sideIndex, out int score)
        {
            score = int.MaxValue;

            if (!IsCastleSpecialRouteDetourKey(rejoinKey))
                return false;

            string rejoinDir = GetCastleRouteDirectionForDetour(rejoinKey);
            if (rejoinDir != baseDir)
                return false;

            int routeDistance = GetDistanceFromCastleRoute(rejoinKey);
            if (routeDistance < 0)
                routeDistance = 0;

            score = routeDistance * 20 + forwardSteps * 2 + sideIndex;
            return true;
        }

        private string GetCastleRouteDirectionForDetour(string currentKey)
        {
            RefreshCastleMoveDictionaryIfNeeded();

            if (TryGetRouteDirection(castleDirectionDic, currentKey, out string direction))
                return direction;

            return GetGwallyeongCastleHouse5Direction(currentKey);
        }

        private int GetDistanceFromCastleRoute(string key)
        {
            RefreshCastleMoveDictionaryIfNeeded();

            if (castleDirectionDic == null || castleDirectionDic.Count == 0)
                return -1;

            if (!TryParseMoveKey(key, out int mapNo, out int x, out int y))
                return -1;

            int best = int.MaxValue;

            foreach (KeyValuePair<string, string> item in castleDirectionDic)
            {
                if (!TryParseMoveKey(item.Key, out int savedMap, out int savedX, out int savedY))
                    continue;

                if (savedMap != mapNo)
                    continue;

                int pointDistance = Math.Abs(x - savedX) + Math.Abs(y - savedY);
                if (pointDistance < best)
                    best = pointDistance;

                if (TryGetNearestRoutePointAhead(castleDirectionDic, item.Key, item.Value, out string nextRouteKey) &&
                    TryParseMoveKey(nextRouteKey, out int nextMap, out int nextX, out int nextY) &&
                    nextMap == mapNo)
                {
                    int segmentDistance = GetManhattanDistanceToRouteSegment(x, y, savedX, savedY, nextX, nextY);
                    if (segmentDistance < best)
                        best = segmentDistance;
                }
            }

            return best == int.MaxValue ? -1 : best;
        }

        private void ResetSpecialRouteDetour()
        {
            specialRouteDetourActive = false;
            specialRouteDetourDirs = new string[0];
            specialRouteDetourIndex = 0;
            specialRouteDetourLastKey = "";
            specialRouteDetourBaseDir = "";
            specialRouteBlockedKey = "";
            specialRouteBlockedDir = "";
            specialRouteBlockedSince = System.DateTime.MinValue;
        }

        private string GetRightDirection(string dir)
        {
            if (dir == "북") return "동";
            if (dir == "동") return "남";
            if (dir == "남") return "서";
            if (dir == "서") return "북";
            return "";
        }

        private string GetLeftDirection(string dir)
        {
            if (dir == "북") return "서";
            if (dir == "서") return "남";
            if (dir == "남") return "동";
            if (dir == "동") return "북";
            return "";
        }

        private void TrackAutoExploreSelectedDirection(string currentKey, string selectedDir)
        {
            if (string.IsNullOrEmpty(selectedDir))
                return;

            bool isNewAttempt = autoExploreLastKey != currentKey || autoExploreLastDir != selectedDir;

            if (isNewAttempt)
            {
                autoExploreLastKey = currentKey;
                autoExploreLastDir = selectedDir;
                autoExploreLastMoveTime = System.DateTime.Now;
            }
        }

        private string GetGwallyeongCastleHouse5Direction(string currentKey)
        {
            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return "";

            if (mapNo != GwallyeongCastleMapNo)
                return "";

            if (x < 0 || y < 0 || y > GwallyeongCastleMaxY)
                return "";

            if (x >= GwallyeongCastleMaxX && y == GwallyeongHauntedHouse5EntryY)
                return "동";

            if (y < GwallyeongHauntedHouse5EntryY)
                return "남";

            if (y > GwallyeongHauntedHouse5EntryY)
                return "북";

            if (x < GwallyeongCastleMaxX)
                return "동";

            return "";
        }

        private bool UpdateAutoExploreRouteByPosition(string currentKey)
        {
            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return false;

            if (autoExploreUsingReturnRoute && mapNo < AutoExploreReturnEndMapNo)
            {
                LoadAutoExploreRoute(moveConfigPath);
                autoExploreUsingReturnRoute = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
                SayRouteChangeText("복귀 경로 이탈 기본 경로 복구");
                return false;
            }

            if (pumpkinReturnRouteResumePending &&
                mapNo >= AutoExploreReturnEndMapNo &&
                mapNo <= AutoExploreReturnStartMapNo)
            {
                LoadAutoExploreRoute(downConfigPath);
                autoExploreUsingReturnRoute = true;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
                SayRouteChangeText("10굴에서 5굴 복귀 경로 재개");
                return false;
            }

            if (autoExploreUsingReturnRoute &&
                mapNo == 5 &&
                x == 9 &&
                y == 13)
            {
                LoadAutoExploreRoute(moveConfigPath);
                autoExploreUsingReturnRoute = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
                SayRouteChangeText("5굴 9 13 상행 전환");
                return false;
            }

            if (!autoExploreUsingReturnRoute &&
                mapNo == AutoExploreReturnStartMapNo &&
                x == AutoExploreReturnStartX &&
                y == AutoExploreReturnStartY)
            {
                LoadAutoExploreRoute(downConfigPath);
                autoExploreUsingReturnRoute = true;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
                SayRouteChangeText("10굴에서 5굴 복귀 경로 시작");
                return false;
            }

            if (autoExploreUsingReturnRoute &&
                mapNo == AutoExploreReturnEndMapNo &&
                x == AutoExploreReturnEndX &&
                y == AutoExploreReturnEndY)
            {
                System.DateTime now = System.DateTime.Now;
                if (autoExploreReturnEndCandidateKey != currentKey)
                {
                    autoExploreReturnEndCandidateKey = currentKey;
                    autoExploreReturnEndCandidateSince = now;
                    return true;
                }

                if ((now - autoExploreReturnEndCandidateSince).TotalMilliseconds < AutoExploreReturnEndStableMs)
                    return true;

                LoadAutoExploreRoute(moveConfigPath);
                autoExploreUsingReturnRoute = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                pumpkinReturnRouteResumePending = false;
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
                SayRouteChangeText("기본 경로 복귀");
                return false;
            }

            if (autoExploreUsingReturnRoute)
            {
                autoExploreReturnEndCandidateKey = "";
                autoExploreReturnEndCandidateSince = System.DateTime.MinValue;
            }

            return false;
        }

        private void LoadAutoExploreRoute(string path)
        {
            Dictionary<string, string> route = LoadMoveDictionary(path);

            autoExploreManualRoute = CloneManualRouteDictionary(route);
            autoExploreManualRouteExpanded = BuildExpandedManualRoute(autoExploreManualRoute);
            autoExploreLastKey = "";
            autoExploreLastDir = "";
            autoExploreLastDeviationRouteKey = "";
            autoExploreReturnTapKey = "";
            autoExploreReturnTapDir = "";
            autoExploreDeviationReturnPending = false;
            autoExploreReturnTapTime = System.DateTime.MinValue;
            autoExploreSamePointFailCount = 0;
            ResetSpecialRouteDetour();
            ResetHauntedHouse5UpperRoute();
            hauntedHouse5UpperRouteCompleted = false;
            ResetHauntedHouse6UpperRoute();
            hauntedHouse6UpperRouteCompleted = false;
            ResetHauntedHouse7UpperRoute();
            hauntedHouse7UpperRouteCompleted = false;
            ResetHauntedHouse8UpperRoute();
            hauntedHouse8UpperRouteCompleted = false;
            ResetHauntedHouse9UpperRoute();
            hauntedHouse9UpperRouteCompleted = false;
            ResetHauntedHouse10UpperRoute();
            hauntedHouse10UpperRouteCompleted = false;
        }

        private void RefreshActiveManualRouteAfterF4Save()
        {
            lock (autoExploreLock)
            {
                if (!autoExploreMode || autoExploreUsingReturnRoute)
                    return;

                autoExploreManualRoute = CloneManualRouteDictionary(directionDic);
                autoExploreManualRouteExpanded = BuildExpandedManualRoute(autoExploreManualRoute);
                autoExploreReturnTapKey = "";
                autoExploreReturnTapDir = "";
                autoExploreDeviationReturnPending = false;
                autoExploreReturnTapTime = System.DateTime.MinValue;
                ResetSpecialRouteDetour();
            }
        }

        private Dictionary<string, string> LoadMoveDictionary(string path)
        {
            if (!File.Exists(path))
                return new Dictionary<string, string>();

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                   ?? new Dictionary<string, string>();
        }

        private string GetMoveDirectionForCurrentMode(int mapNo, string xyText, string currentKey)
        {
            if (autoExploreMode)
                return GetAutoExploreDirection(mapNo, xyText, currentKey);

            return GetDirectionFromCache(currentKey);
        }

        private void ObserveAutoExplorePosition(string currentKey)
        {
            if (string.IsNullOrEmpty(currentKey))
                return;

            if (string.IsNullOrEmpty(autoExploreLastKey) || string.IsNullOrEmpty(autoExploreLastDir))
                return;

            if (currentKey != autoExploreLastKey)
            {
                autoExploreSamePointFailCount = 0;
                autoExploreLastKey = currentKey;
                autoExploreLastDir = "";
                autoExploreReturnTapKey = "";
                autoExploreReturnTapDir = "";
                autoExploreReturnTapTime = System.DateTime.MinValue;
                return;
            }

            if ((System.DateTime.Now - autoExploreLastMoveTime).TotalMilliseconds < AutoExploreStuckMs)
                return;

            autoExploreSamePointFailCount++;

            autoExploreLastMoveTime = System.DateTime.Now;
            autoExploreLastDir = "";
        }

        private string ChooseAutoExploreDirection(string currentKey, AutoExploreNode node)
        {
            int routeDistance = GetDistanceFromManualRoute(currentKey);

            if (routeDistance > 0)
            {
                autoExploreDeviationReturnPending = true;

                if (TryGetDirectionTowardManualRoute(currentKey, out string returnDir))
                    return returnDir;

                return null;
            }

            bool mustResumeMainRoute = autoExploreDeviationReturnPending;

            if (TryGetManualRouteDirection(currentKey, out string manualRouteDir))
            {
                autoExploreReturnTapKey = "";
                autoExploreReturnTapDir = "";
                autoExploreReturnTapTime = System.DateTime.MinValue;

                if (mustResumeMainRoute)
                {
                    autoExploreDeviationReturnPending = false;
                    return manualRouteDir;
                }

                if (UseOneTileDeviationRoute &&
                    !autoExploreUsingReturnRoute &&
                    currentKey != autoExploreLastDeviationRouteKey &&
                    autoExploreRandom.Next(100) >= AutoExploreManualRoutePercent &&
                    TryGetManualRouteDeviationDirection(currentKey, manualRouteDir, out string deviationDir))
                {
                    autoExploreDeviationReturnPending = true;
                    autoExploreLastDeviationRouteKey = currentKey;
                    return deviationDir;
                }

                return manualRouteDir;
            }

            return null;
        }

        private Dictionary<string, string> CloneManualRouteDictionary(Dictionary<string, string> source)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (source == null)
                return result;

            foreach (KeyValuePair<string, string> item in source)
            {
                if (IsValidMoveDirectionText(item.Value))
                {
                    result[item.Key] = item.Value;
                }
            }

            return result;
        }

        private Dictionary<string, string> BuildExpandedManualRoute(Dictionary<string, string> source)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (source == null || source.Count == 0)
                return result;

            foreach (KeyValuePair<string, string> item in source)
            {
                if (!IsValidMoveDirectionText(item.Value))
                    continue;

                result[item.Key] = item.Value;
            }

            foreach (KeyValuePair<string, string> item in source)
            {
                string startKey = item.Key;
                string dir = item.Value;

                if (!IsValidMoveDirectionText(dir))
                    continue;

                if (!TryParseMoveKey(startKey, out int startMap, out int startX, out int startY))
                    continue;

                int maxDistance = lookAheadCount;

                if (TryGetNearestRoutePointAhead(source, startKey, dir, out string nextRouteKey) &&
                    TryParseMoveKey(nextRouteKey, out int nextMap, out _, out _) &&
                    nextMap == startMap)
                {
                    int distanceToNext = CalcDistanceOnDirection(startKey, nextRouteKey, dir);
                    if (distanceToNext > 0)
                        maxDistance = distanceToNext - 1;
                }

                for (int i = 1; i <= maxDistance; i++)
                {
                    int x = startX;
                    int y = startY;

                    if (dir == "동") x += i;
                    else if (dir == "서") x -= i;
                    else if (dir == "남") y += i;
                    else if (dir == "북") y -= i;

                    string key = MakeMoveKey(startMap, x, y);

                    if (source.ContainsKey(key))
                        continue;

                    if (!result.ContainsKey(key))
                        result[key] = dir;
                }
            }

            foreach (KeyValuePair<string, string> item in source)
            {
                if (IsValidMoveDirectionText(item.Value))
                    result[item.Key] = item.Value;
            }

            return result;
        }

        private void MergeManualRouteToMoveCache()
        {
            if (autoExploreManualRoute == null || autoExploreManualRoute.Count == 0)
                return;

            if (directionDic == null)
            {
                directionDic = new Dictionary<string, string>();
            }

            foreach (KeyValuePair<string, string> item in autoExploreManualRoute)
            {
                directionDic[item.Key] = item.Value;
            }
        }

        private bool TryGetManualRouteDirection(string currentKey, out string direction)
        {
            direction = "";

            if (GetDistanceFromManualRoute(currentKey) != 0)
                return false;

            if (autoExploreManualRouteExpanded != null &&
                autoExploreManualRouteExpanded.TryGetValue(currentKey, out string expandedDir) &&
                IsValidMoveDirectionText(expandedDir))
            {
                if (!IsWithinManualRouteDeviationAfterMove(currentKey, expandedDir))
                    return false;

                direction = expandedDir;
                return true;
            }

            string exactDir = "";
            bool exactRoutePoint = autoExploreManualRoute != null &&
                                   autoExploreManualRoute.TryGetValue(currentKey, out exactDir) &&
                                   IsValidMoveDirectionText(exactDir);

            string manualDir = "";

            if (exactRoutePoint)
            {
                manualDir = exactDir;
            }
            else if (!TryGetInterpolatedRouteDirection(autoExploreManualRoute, currentKey, out manualDir))
            {
                return false;
            }

            if (!exactRoutePoint &&
                !string.IsNullOrEmpty(lastMove) &&
                manualDir == GetOppositeDirection(lastMove) &&
                IsInterpolatedRouteContinuation(currentKey, lastMove))
            {
                manualDir = lastMove;
            }

            if (!IsWithinManualRouteDeviationAfterMove(currentKey, manualDir))
                return false;

            direction = manualDir;
            return true;
        }

        private bool TryPickBestDirection(List<AutoExploreDirectionScore> scores, out string direction)
        {
            direction = "";

            if (scores == null || scores.Count == 0)
                return false;

            int bestScore = int.MinValue;

            foreach (AutoExploreDirectionScore item in scores)
            {
                if (item.Score > bestScore)
                {
                    bestScore = item.Score;
                    direction = item.Direction;
                }
            }

            return !string.IsNullOrEmpty(direction);
        }

        private bool DoesDirectionLeadToKey(string currentKey, string dir, string targetKey)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(dir) || string.IsNullOrEmpty(targetKey))
                return false;

            if (!TryGetNextMoveKey(currentKey, dir, out string nextKey))
                return false;

            return nextKey == targetKey;
        }

        private bool IsInterpolatedRouteContinuation(string currentKey, string dir)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(dir))
                return false;

            if (!TryGetNextMoveKey(currentKey, dir, out string nextKey))
                return false;

            return GetDistanceFromManualRoute(nextKey) == 0;
        }

        private bool TryGetManualRouteDeviationDirection(string currentKey, string manualRouteDir, out string direction)
        {
            direction = "";

            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(manualRouteDir))
                return false;

            List<AutoExploreDirectionScore> scores = new List<AutoExploreDirectionScore>();
            string[] dirs = new string[] { "동", "서", "남", "북" };

            foreach (string dir in dirs)
            {
                if (dir == manualRouteDir)
                    continue;

                if (!IsWithinManualRouteDeviationAfterMove(currentKey, dir))
                    continue;

                if (!TryGetNextMoveKey(currentKey, dir, out string nextKey))
                    continue;

                if (GetDistanceFromManualRoute(nextKey) != 1)
                    continue;

                int score = 20;

                if (IsSideDirection(dir, manualRouteDir))
                    score += 120;

                if (!string.IsNullOrEmpty(lastMove) && dir == GetOppositeDirection(lastMove))
                    score -= 80;

                if (score > 0)
                    scores.Add(new AutoExploreDirectionScore { Direction = dir, Score = score });
            }

            if (scores.Count == 0)
                return false;

            int total = 0;
            foreach (AutoExploreDirectionScore item in scores)
            {
                total += item.Score;
            }

            int pick = autoExploreRandom.Next(1, total + 1);
            int cursor = 0;

            foreach (AutoExploreDirectionScore item in scores)
            {
                cursor += item.Score;
                if (pick <= cursor)
                {
                    direction = item.Direction;
                    return true;
                }
            }

            direction = scores[scores.Count - 1].Direction;
            return true;
        }

        private bool TryGetDirectionTowardManualRoute(string currentKey, out string direction)
        {
            direction = "";

            if (autoExploreManualRoute == null || autoExploreManualRoute.Count == 0)
                return false;

            int currentDistance = GetDistanceFromManualRoute(currentKey);
            if (currentDistance < 0)
                return false;

            List<AutoExploreDirectionScore> scores = new List<AutoExploreDirectionScore>();
            string[] dirs = new string[] { "동", "서", "남", "북" };

            foreach (string dir in dirs)
            {
                if (!TryGetNextMoveKey(currentKey, dir, out string nextKey))
                    continue;

                int nextDistance = GetDistanceFromManualRoute(nextKey);
                if (nextDistance < 0)
                    continue;

                if (currentDistance > AutoExploreMaxManualRouteDeviation)
                {
                    if (nextDistance >= currentDistance)
                        continue;
                }
                else if (nextDistance > AutoExploreMaxManualRouteDeviation)
                {
                    continue;
                }

                if (currentDistance > 0 && nextDistance >= currentDistance)
                    continue;

                int score = 100 + Math.Max(0, currentDistance - nextDistance) * 80;

                if (nextDistance == 0)
                    score += 180;

                if (!string.IsNullOrEmpty(lastMove) && dir == GetOppositeDirection(lastMove))
                    score -= 60;

                if (score > 0)
                    scores.Add(new AutoExploreDirectionScore { Direction = dir, Score = score });
            }

            if (autoExploreUsingReturnRoute)
                return TryPickBestDirection(scores, out direction);

            return TryPickWeightedDirection(scores, out direction);
        }

        private bool TryPickWeightedDirection(List<AutoExploreDirectionScore> scores, out string direction)
        {
            direction = "";

            if (scores == null || scores.Count == 0)
                return false;

            int total = 0;
            foreach (AutoExploreDirectionScore item in scores)
            {
                total += item.Score;
            }

            if (total <= 0)
                return false;

            int pick = autoExploreRandom.Next(1, total + 1);
            int cursor = 0;

            foreach (AutoExploreDirectionScore item in scores)
            {
                cursor += item.Score;
                if (pick <= cursor)
                {
                    direction = item.Direction;
                    return true;
                }
            }

            direction = scores[scores.Count - 1].Direction;
            return true;
        }

        private bool TryGetRouteDirection(Dictionary<string, string> route, string currentKey, out string direction)
        {
            direction = "";

            if (route == null || string.IsNullOrEmpty(currentKey))
                return false;

            if (route.TryGetValue(currentKey, out string exactDir) && IsValidMoveDirectionText(exactDir))
            {
                direction = exactDir;
                return true;
            }

            return TryGetInterpolatedRouteDirection(route, currentKey, out direction);
        }

        private bool TryGetInterpolatedRouteDirection(Dictionary<string, string> route, string currentKey, out string direction)
        {
            direction = "";

            if (route == null || !TryParseMoveKey(currentKey, out int currentMap, out _, out _))
                return false;

            string bestDir = "";
            int bestDistance = int.MaxValue;

            foreach (KeyValuePair<string, string> item in route)
            {
                string savedKey = item.Key;
                string savedDir = item.Value;

                if (!IsValidMoveDirectionText(savedDir))
                    continue;

                if (!TryParseMoveKey(savedKey, out int savedMap, out _, out _))
                    continue;

                if (savedMap != currentMap)
                    continue;

                int distanceFromSaved = CalcDistanceOnDirection(savedKey, currentKey, savedDir);
                if (distanceFromSaved <= 0 || distanceFromSaved >= bestDistance)
                    continue;

                bool hasRoutePointAhead = HasRoutePointAhead(route, savedKey, savedDir, distanceFromSaved);
                if (!hasRoutePointAhead && distanceFromSaved > lookAheadCount)
                    continue;

                bestDistance = distanceFromSaved;
                bestDir = savedDir;
            }

            if (string.IsNullOrEmpty(bestDir))
                return false;

            direction = bestDir;
            return true;
        }

        private bool HasRoutePointAhead(Dictionary<string, string> route, string savedKey, string savedDir, int minDistance)
        {
            if (route == null || string.IsNullOrEmpty(savedKey) || string.IsNullOrEmpty(savedDir))
                return false;

            foreach (KeyValuePair<string, string> item in route)
            {
                if (item.Key == savedKey)
                    continue;

                int distance = CalcDistanceOnDirection(savedKey, item.Key, savedDir);
                if (distance > minDistance)
                    return true;
            }

            return false;
        }

        private bool IsWithinManualRouteDeviationAfterMove(string currentKey, string dir)
        {
            if (autoExploreManualRoute == null || autoExploreManualRoute.Count == 0)
                return true;

            if (!TryGetNextMoveKey(currentKey, dir, out string nextKey))
                return false;

            int distance = GetDistanceFromManualRoute(nextKey);
            return distance >= 0 && distance <= AutoExploreMaxManualRouteDeviation;
        }

        private int GetDistanceFromManualRoute(string key)
        {
            if (autoExploreManualRoute == null || autoExploreManualRoute.Count == 0)
                return -1;

            if (!TryParseMoveKey(key, out int mapNo, out int x, out int y))
                return -1;

            if (autoExploreManualRouteExpanded != null && autoExploreManualRouteExpanded.Count > 0)
            {
                int expandedBest = int.MaxValue;

                foreach (string routeKey in autoExploreManualRouteExpanded.Keys)
                {
                    if (!TryParseMoveKey(routeKey, out int routeMap, out int routeX, out int routeY))
                        continue;

                    if (routeMap != mapNo)
                        continue;

                    int distance = Math.Abs(x - routeX) + Math.Abs(y - routeY);
                    if (distance < expandedBest)
                        expandedBest = distance;
                }

                return expandedBest == int.MaxValue ? -1 : expandedBest;
            }

            int best = int.MaxValue;

            foreach (KeyValuePair<string, string> item in autoExploreManualRoute)
            {
                if (!TryParseMoveKey(item.Key, out int savedMap, out int savedX, out int savedY))
                    continue;

                if (savedMap != mapNo)
                    continue;

                int pointDistance = Math.Abs(x - savedX) + Math.Abs(y - savedY);
                if (pointDistance < best)
                    best = pointDistance;

                if (!IsValidMoveDirectionText(item.Value))
                    continue;

                if (IsOnRouteForwardRay(item.Key, item.Value, key, lookAheadCount))
                {
                    best = 0;
                    continue;
                }

                if (TryGetNearestRoutePointAhead(autoExploreManualRoute, item.Key, item.Value, out string nextRouteKey) &&
                    TryParseMoveKey(nextRouteKey, out int nextMap, out int nextX, out int nextY) &&
                    nextMap == mapNo)
                {
                    int segmentDistance = GetManhattanDistanceToRouteSegment(x, y, savedX, savedY, nextX, nextY);
                    if (segmentDistance < best)
                        best = segmentDistance;
                }
            }

            return best == int.MaxValue ? -1 : best;
        }

        private bool IsOnManualRouteForwardRay(string currentKey, string dir)
        {
            if (autoExploreManualRoute == null || string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(dir))
                return false;

            foreach (KeyValuePair<string, string> item in autoExploreManualRoute)
            {
                if (item.Value != dir)
                    continue;

                if (IsOnRouteForwardRay(item.Key, item.Value, currentKey, lookAheadCount))
                    return true;
            }

            return false;
        }

        private bool IsOnRouteForwardRay(string routeKey, string routeDir, string targetKey, int maxDistance)
        {
            int distance = CalcDistanceOnDirection(routeKey, targetKey, routeDir);
            return distance > 0 && distance <= maxDistance;
        }

        private bool TryGetNearestRoutePointAhead(Dictionary<string, string> route, string savedKey, string savedDir, out string nextRouteKey)
        {
            nextRouteKey = "";

            if (route == null || string.IsNullOrEmpty(savedKey) || string.IsNullOrEmpty(savedDir))
                return false;

            int bestDistance = int.MaxValue;

            foreach (KeyValuePair<string, string> item in route)
            {
                if (item.Key == savedKey)
                    continue;

                int distance = CalcDistanceOnDirection(savedKey, item.Key, savedDir);
                if (distance <= 0 || distance >= bestDistance)
                    continue;

                bestDistance = distance;
                nextRouteKey = item.Key;
            }

            return !string.IsNullOrEmpty(nextRouteKey);
        }

        private int GetManhattanDistanceToRouteSegment(int x, int y, int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                int minY = Math.Min(y1, y2);
                int maxY = Math.Max(y1, y2);

                if (y >= minY && y <= maxY)
                    return Math.Abs(x - x1);

                return Math.Min(Math.Abs(x - x1) + Math.Abs(y - y1),
                                Math.Abs(x - x2) + Math.Abs(y - y2));
            }

            if (y1 == y2)
            {
                int minX = Math.Min(x1, x2);
                int maxX = Math.Max(x1, x2);

                if (x >= minX && x <= maxX)
                    return Math.Abs(y - y1);

                return Math.Min(Math.Abs(x - x1) + Math.Abs(y - y1),
                                Math.Abs(x - x2) + Math.Abs(y - y2));
            }

            return Math.Min(Math.Abs(x - x1) + Math.Abs(y - y1),
                            Math.Abs(x - x2) + Math.Abs(y - y2));
        }

        private bool TryGetNextMoveKey(string currentKey, string dir, out string nextKey)
        {
            nextKey = "";

            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return false;

            int dx = 0;
            int dy = 0;

            if (dir == "동") dx = 1;
            else if (dir == "서") dx = -1;
            else if (dir == "남") dy = 1;
            else if (dir == "북") dy = -1;
            else return false;

            nextKey = mapNo + ":(" + (x + dx).ToString("00") + "," + (y + dy).ToString("00") + ")";
            return true;
        }

        private string MakeMoveKey(int mapNo, int x, int y)
        {
            return mapNo + ":(" + x.ToString("00") + "," + y.ToString("00") + ")";
        }

        private void ApplyManualRouteEdgeConfidence(string currentKey, string selectedDir, AutoExploreEdge selectedEdge)
        {
            if (selectedEdge == null || !IsManualRouteDirection(currentKey, selectedDir))
                return;

            selectedEdge.Type = "open";
            selectedEdge.SuccessCount = Math.Max(selectedEdge.SuccessCount, 1);
            selectedEdge.ConsecutiveFailCount = 0;
            selectedEdge.Blocked = false;
        }

        private bool IsManualRouteDirection(string currentKey, string dir)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(dir) || autoExploreManualRoute == null)
                return false;

            return autoExploreManualRoute.TryGetValue(currentKey, out string manualDir) && manualDir == dir;
        }

        private bool IsValidMoveDirectionText(string dir)
        {
            return dir == "동" || dir == "서" || dir == "남" || dir == "북";
        }

        private bool TryGetFrontierRouteDirection(string currentKey, out string direction)
        {
            direction = "";

            if (!TryParseMoveKey(currentKey, out int currentMap, out _, out _))
                return false;

            Queue<string> queue = new Queue<string>();
            Dictionary<string, string> firstDirectionByKey = new Dictionary<string, string>();
            HashSet<string> visited = new HashSet<string>();
            string[] dirs = new string[] { "동", "서", "남", "북" };

            queue.Enqueue(currentKey);
            visited.Add(currentKey);
            firstDirectionByKey[currentKey] = "";

            while (queue.Count > 0)
            {
                string key = queue.Dequeue();
                string firstDir = firstDirectionByKey[key];

                foreach (string dir in dirs)
                {
                    AutoExploreEdge edge = GetAutoExploreEdge(key, dir);
                    RepairInvalidDirectionalEdge(key, dir, edge);

                    if (IsLowerMapEdge(key, edge))
                        continue;

                    if (!IsWithinManualRouteDeviationAfterMove(key, dir))
                        continue;

                    if (IsExpandableFrontierEdge(edge))
                    {
                        direction = string.IsNullOrEmpty(firstDir) ? dir : firstDir;
                        return true;
                    }

                    if (edge.Type != "open" || string.IsNullOrEmpty(edge.To))
                        continue;

                    if (IsLikelyWallEdge(edge))
                        continue;

                    if (!TryParseMoveKey(edge.To, out int nextMap, out _, out _))
                        continue;

                    if (nextMap != currentMap)
                        continue;

                    if (visited.Contains(edge.To))
                        continue;

                    visited.Add(edge.To);
                    firstDirectionByKey[edge.To] = string.IsNullOrEmpty(firstDir) ? dir : firstDir;
                    queue.Enqueue(edge.To);
                }
            }

            return false;
        }

        private bool IsExpandableFrontierEdge(AutoExploreEdge edge)
        {
            if (edge == null)
                return false;

            if (edge.Type == "wall" || edge.Type == "wall_candidate" || edge.Blocked)
                return false;

            if (edge.SuccessCount > 0)
                return false;

            if (IsLikelyWallEdge(edge))
                return false;

            return edge.Type == "unknown" || string.IsNullOrEmpty(edge.Type) || edge.Type == "blocked_candidate";
        }

        private bool IsLikelyWallEdge(AutoExploreEdge edge)
        {
            if (edge == null)
                return false;

            if (edge.SuccessCount > 0)
                return false;

            if (edge.Type == "wall" || edge.Type == "wall_candidate" || edge.Blocked)
                return true;

            return edge.ConsecutiveFailCount >= AutoExploreSoftBlockedFailCount ||
                   edge.FailCount >= AutoExploreLikelyWallFailCount;
        }

        private bool TryGetMainRouteDirection(string currentKey, out string direction)
        {
            direction = "";

            if (!TryParseMoveKey(currentKey, out int currentMap, out _, out _))
                return false;

            if (currentMap >= 10)
                return false;

            Queue<string> queue = new Queue<string>();
            Dictionary<string, string> firstDirectionByKey = new Dictionary<string, string>();
            HashSet<string> visited = new HashSet<string>();
            string[] dirs = new string[] { "동", "서", "남", "북" };

            queue.Enqueue(currentKey);
            visited.Add(currentKey);
            firstDirectionByKey[currentKey] = "";

            while (queue.Count > 0)
            {
                string key = queue.Dequeue();
                string firstDir = firstDirectionByKey[key];

                foreach (string dir in dirs)
                {
                    AutoExploreEdge edge = GetAutoExploreEdge(key, dir);
                    RepairInvalidDirectionalEdge(key, dir, edge);

                    if (edge.Type == "wall" || edge.Type == "wall_candidate" || edge.Blocked)
                        continue;

                    if (IsLowerMapEdge(key, edge))
                        continue;

                    if (!IsWithinManualRouteDeviationAfterMove(key, dir))
                        continue;

                    if (edge.Type == "portal" && edge.PortalScore > 0)
                    {
                        if (TryParseMoveKey(edge.To, out int portalMap, out _, out _) && portalMap == currentMap + 1)
                        {
                            direction = string.IsNullOrEmpty(firstDir) ? dir : firstDir;
                            return true;
                        }

                        if (string.IsNullOrEmpty(edge.To))
                        {
                            direction = string.IsNullOrEmpty(firstDir) ? dir : firstDir;
                            return true;
                        }
                    }

                    if (edge.Type != "open" || string.IsNullOrEmpty(edge.To))
                        continue;

                    if (!TryParseMoveKey(edge.To, out int nextMap, out _, out _))
                        continue;

                    if (nextMap != currentMap)
                        continue;

                    if (visited.Contains(edge.To))
                        continue;

                    visited.Add(edge.To);
                    firstDirectionByKey[edge.To] = string.IsNullOrEmpty(firstDir) ? dir : firstDir;
                    queue.Enqueue(edge.To);
                }
            }

            return false;
        }

        private string GetBlockedKnownOpenDirection(string currentKey, AutoExploreNode node)
        {
            if (string.IsNullOrEmpty(currentKey) || node == null)
                return "";

            string[] dirs = new string[] { "동", "서", "남", "북" };
            string bestDir = "";
            int bestScore = int.MinValue;

            foreach (string dir in dirs)
            {
                AutoExploreEdge edge = GetAutoExploreEdge(currentKey, dir);

                RepairInvalidDirectionalEdge(currentKey, dir, edge);

                if (edge.Type != "open" || edge.SuccessCount <= 0 || edge.ConsecutiveFailCount <= 0)
                    continue;

                if (IsLowerMapEdge(currentKey, edge))
                    continue;

                if (string.IsNullOrEmpty(edge.To))
                    continue;

                int score = edge.ConsecutiveFailCount * 100 + edge.MonsterScore * 20 + edge.SuccessCount;
                if (edge.LastFailedAt != System.DateTime.MinValue)
                    score += (int)Math.Max(0, 60 - Math.Min(60, (System.DateTime.Now - edge.LastFailedAt).TotalSeconds));

                if (score > bestScore)
                {
                    bestScore = score;
                    bestDir = dir;
                }
            }

            return bestDir;
        }

        private string GetRecentNonOpenBlockedDirection(string currentKey, AutoExploreNode node)
        {
            if (string.IsNullOrEmpty(currentKey) || node == null)
                return "";

            string[] dirs = new string[] { "동", "서", "남", "북" };
            string recentDir = "";
            System.DateTime recentAt = System.DateTime.MinValue;

            foreach (string dir in dirs)
            {
                AutoExploreEdge edge = GetAutoExploreEdge(currentKey, dir);
                if (edge.Type == "open" && edge.SuccessCount > 0)
                    continue;

                if (edge.LastFailedAt == System.DateTime.MinValue)
                    continue;

                if ((System.DateTime.Now - edge.LastFailedAt).TotalMilliseconds > AutoExploreRecentBlockDetourMs)
                    continue;

                if (edge.LastFailedAt > recentAt)
                {
                    recentAt = edge.LastFailedAt;
                    recentDir = dir;
                }
            }

            return recentDir;
        }

        private bool IsSideDirection(string dir, string blockedDir)
        {
            if (string.IsNullOrEmpty(dir) || string.IsNullOrEmpty(blockedDir))
                return false;

            if (blockedDir == "동" || blockedDir == "서")
                return dir == "남" || dir == "북";

            if (blockedDir == "남" || blockedDir == "북")
                return dir == "동" || dir == "서";

            return false;
        }

        private bool IsImmediateReturnDirection(string currentKey, string dir, AutoExploreEdge edge)
        {
            if (!string.IsNullOrEmpty(lastMove) && dir == GetOppositeDirection(lastMove))
                return true;

            return false;
        }

        private string GetDirectionBetweenKeys(string fromKey, string toKey)
        {
            if (!TryParseMoveKey(fromKey, out int fromMap, out int fromX, out int fromY))
                return "";

            if (!TryParseMoveKey(toKey, out int toMap, out int toX, out int toY))
                return "";

            if (fromMap != toMap)
                return "";

            int dx = toX - fromX;
            int dy = toY - fromY;

            if (dy == 0 && dx == 1)
                return "동";

            if (dy == 0 && dx == -1)
                return "서";

            if (dx == 0 && dy == 1)
                return "남";

            if (dx == 0 && dy == -1)
                return "북";

            return "";
        }

        private bool IsDirectionConsistentWithMove(string fromKey, string toKey, string dir)
        {
            string actualDir = GetDirectionBetweenKeys(fromKey, toKey);
            return !string.IsNullOrEmpty(actualDir) && actualDir == dir;
        }

        private void MarkMismatchedAutoExploreEdge(AutoExploreEdge edge)
        {
            if (edge == null)
                return;

            edge.To = "";
            edge.SuccessCount = 0;
            edge.FailCount = Math.Max(edge.FailCount, AutoExploreWallFailCount);
            edge.ConsecutiveFailCount = Math.Max(edge.ConsecutiveFailCount, AutoExploreSoftBlockedFailCount);
            edge.Type = "wall_candidate";
            edge.Blocked = true;
            edge.LastFailedAt = System.DateTime.Now;
        }

        private void RepairInvalidDirectionalEdge(string currentKey, string dir, AutoExploreEdge edge)
        {
            if (edge == null || string.IsNullOrEmpty(edge.To))
                return;

            if (edge.Type != "open" && edge.Type != "portal")
                return;

            if (!TryParseMoveKey(currentKey, out int currentMap, out _, out _))
                return;

            if (!TryParseMoveKey(edge.To, out int targetMap, out _, out _))
                return;

            if (currentMap != targetMap)
                return;

            if (IsDirectionConsistentWithMove(currentKey, edge.To, dir))
                return;

            MarkMismatchedAutoExploreEdge(edge);
        }

        private bool IsLowerMapEdge(string currentKey, AutoExploreEdge edge)
        {
            if (edge == null)
                return false;

            if (edge.Type == "back_portal")
                return true;

            if (!TryParseMoveKey(currentKey, out int currentMap, out _, out _))
                return false;

            if (currentMap <= AutoExploreMinMapNo && edge.Type == "portal" && edge.PortalScore < 0)
                return true;

            if (string.IsNullOrEmpty(edge.To))
                return false;

            if (!TryParseMoveKey(edge.To, out int targetMap, out _, out _))
                return false;

            return targetMap < AutoExploreMinMapNo || targetMap < currentMap;
        }

        private string ChooseLeastBadAutoExploreDirection(string currentKey, AutoExploreNode node)
        {
            string forcedKnownOpenDir = GetBlockedKnownOpenDirection(currentKey, node);
            if (!string.IsNullOrEmpty(forcedKnownOpenDir))
                return forcedKnownOpenDir;

            string[] dirs = new string[] { "동", "서", "남", "북" };
            string bestDir = "";
            int bestScore = int.MinValue;
            string recentBlockedDir = GetRecentNonOpenBlockedDirection(currentKey, node);

            foreach (string dir in dirs)
            {
                AutoExploreEdge edge = GetAutoExploreEdge(currentKey, dir);

                RepairInvalidDirectionalEdge(currentKey, dir, edge);

                if (edge.Type == "wall")
                    continue;

                if (IsLikelyWallEdge(edge))
                    continue;

                if (IsLowerMapEdge(currentKey, edge))
                    continue;

                if (!IsWithinManualRouteDeviationAfterMove(currentKey, dir))
                    continue;

                bool isImmediateReturn = IsImmediateReturnDirection(currentKey, dir, edge);
                int score = 0;

                if (edge.Type == "unknown" || string.IsNullOrEmpty(edge.Type))
                    score += 260;

                if (edge.Type == "open")
                    score += 30;

                if (edge.Type == "open" && edge.SuccessCount > 0 && edge.ConsecutiveFailCount > 0)
                {
                    score += 1800 + Math.Min(300, edge.MonsterScore * 35);
                }

                if (edge.Type == "portal" && edge.PortalScore > 0)
                    score += 1400 + (edge.PortalScore * 120);

                if (edge.Type == "portal" && edge.PortalScore < 0)
                    score -= 900;

                score += Math.Min(120, edge.MonsterScore * 20);
                if (edge.Type == "open" && edge.SuccessCount > 0)
                    score -= Math.Min(80, edge.ConsecutiveFailCount * 10);
                else
                    score -= edge.ConsecutiveFailCount * 220;

                score -= edge.FailCount * 40;
                score -= edge.ChooseCount * 22;

                if (!string.IsNullOrEmpty(edge.To) && autoExploreGraph.TryGetValue(edge.To, out AutoExploreNode targetNode))
                {
                    if (!targetNode.Visited)
                        score += 500;
                    else if (HasExpandableAutoExploreDirection(targetNode))
                        score += 320;
                    else if (targetNode.LastVisitedAt != System.DateTime.MinValue &&
                             (System.DateTime.Now - targetNode.LastVisitedAt).TotalSeconds < 90)
                        score -= 260;
                }

                if (!string.IsNullOrEmpty(lastMove) && dir == GetOppositeDirection(lastMove))
                    score -= 180;

                if (isImmediateReturn)
                    score -= string.IsNullOrEmpty(edge.To) ? 900 : 1300;

                if (!string.IsNullOrEmpty(recentBlockedDir))
                {
                    if (dir == recentBlockedDir)
                        score -= 800;
                    else if (IsSideDirection(dir, recentBlockedDir))
                        score += 220;
                    else if (dir == GetOppositeDirection(recentBlockedDir))
                        score -= 60;
                }

                if (!(edge.Type == "open" && edge.SuccessCount > 0) &&
                    edge.LastFailedAt != System.DateTime.MinValue &&
                    (System.DateTime.Now - edge.LastFailedAt).TotalSeconds < 15)
                {
                    score -= 600;
                }

                if (score > bestScore)
                {
                    bestScore = score;
                    bestDir = dir;
                }
            }

            return bestDir;
        }

        private bool HasUnknownAutoExploreDirection(AutoExploreNode node)
        {
            if (node == null)
                return false;

            string[] dirs = new string[] { "동", "서", "남", "북" };

            foreach (string dir in dirs)
            {
                if (node.Edges == null || !node.Edges.TryGetValue(dir, out AutoExploreEdge edge))
                    return true;

                if (edge.Type == "unknown" || string.IsNullOrEmpty(edge.Type))
                    return true;
            }

            return false;
        }

        private bool HasExpandableAutoExploreDirection(AutoExploreNode node)
        {
            if (node == null)
                return false;

            string[] dirs = new string[] { "동", "서", "남", "북" };

            foreach (string dir in dirs)
            {
                if (node.Edges == null || !node.Edges.TryGetValue(dir, out AutoExploreEdge edge))
                    return true;

                if (edge.Type == "unknown" || string.IsNullOrEmpty(edge.Type))
                    return true;

                if (edge.Type == "blocked_candidate" && !IsLikelyWallEdge(edge))
                    return true;
            }

            return false;
        }

        private AutoExploreNode GetAutoExploreNode(string key)
        {
            if (!autoExploreGraph.TryGetValue(key, out AutoExploreNode node))
            {
                node = new AutoExploreNode();
                node.Key = key;
                node.Edges = new Dictionary<string, AutoExploreEdge>();
                autoExploreGraph[key] = node;
            }

            if (node.Edges == null)
                node.Edges = new Dictionary<string, AutoExploreEdge>();

            return node;
        }

        private AutoExploreEdge GetAutoExploreEdge(string key, string dir)
        {
            AutoExploreNode node = GetAutoExploreNode(key);

            if (!node.Edges.TryGetValue(dir, out AutoExploreEdge edge))
            {
                edge = new AutoExploreEdge();
                edge.Direction = dir;
                edge.Type = "unknown";
                node.Edges[dir] = edge;
            }

            return edge;
        }

        private string GetOppositeDirection(string dir)
        {
            if (dir == "동") return "서";
            if (dir == "서") return "동";
            if (dir == "남") return "북";
            if (dir == "북") return "남";
            return "";
        }

        private void LoadAutoExploreGraphIfNeeded()
        {
            autoExploreGraph = new Dictionary<string, AutoExploreNode>();
        }

        private void SaveAutoExploreGraph(bool force)
        {
        }


        private System.DateTime lastInputTime = System.DateTime.MinValue;





        public void UpdateMove(string key, string curMove)
        {
            lock (moveLock)
            {
                RefreshMoveDictionaryIfNeeded();

                System.DateTime now = System.DateTime.Now;

                // 자동 탐색 중에는 방향이 없으면 반드시 멈춘다.
                string desiredDir = !string.IsNullOrEmpty(curMove) ? curMove : (autoExploreMode ? "" : lastMove);

                if (string.IsNullOrEmpty(desiredDir))
                {
                    ReleaseDirection(lastMove);
                    lastMove = "";
                    lastMoveKey = key;
                    lastMoveWasTapMode = false;
                    return;
                }

                bool useSegmentedMoveInCombat = UseSegmentedMove && ShouldUseSegmentedMoveForCurrentRoute(key);
                if (useSegmentedMoveInCombat)
                {
                    if (segmentedMoveLastPositionKey != key)
                    {
                        bool movedToNewPosition = !string.IsNullOrEmpty(segmentedMoveLastPositionKey);
                        segmentedMoveLastPositionKey = key;

                        int segmentedMoveDelayMs = GetSegmentedMoveDelayMs();
                        if (movedToNewPosition && segmentedMoveDelayMs > 0)
                        {
                            segmentedMovePauseUntil = now.AddMilliseconds(segmentedMoveDelayMs);
                            ReleaseDirection(lastMove);
                            lastMove = "";
                            lastMoveKey = key;
                            lastMoveWasTapMode = true;
                            lastInputTime = now;
                            return;
                        }
                    }

                    if (segmentedMovePauseUntil != System.DateTime.MinValue &&
                        now < segmentedMovePauseUntil)
                    {
                        ReleaseDirection(lastMove);
                        lastMove = "";
                        lastMoveKey = key;
                        lastMoveWasTapMode = true;
                        return;
                    }

                    segmentedMovePauseUntil = System.DateTime.MinValue;
                }
                else
                {
                    segmentedMoveLastPositionKey = "";
                    segmentedMovePauseUntil = System.DateTime.MinValue;
                }

                // ===== 누르기 전에 먼저 앞 탐색 =====
                bool useAutoExploreReturnTap = ShouldUseAutoExploreReturnTapMove(key, desiredDir);
                bool useHauntedHouseUpperRouteStep = IsHauntedHouseUpperRouteStepActive();
                bool useTapMode = useAutoExploreReturnTap ||
                                  useHauntedHouseUpperRouteStep ||
                                  ShouldUseTapMoveBeforePress(key, desiredDir);

                // 방향이 바뀌면 기존 방향 해제
                if (desiredDir != lastMove)
                {
                    ReleaseDirection(lastMove);
                    Thread.Sleep(directionChangeDelayMs);
                    lastInputTime = System.DateTime.MinValue;
                    lastMoveWasTapMode = false;
                }

                // ===== 짧은 구간: 홀드하지 말고 탭 이동 =====
                if (useTapMode)
                {
                    // 직전에 홀드 중이었다면 해제하고 탭 모드 전환
                    if (!lastMoveWasTapMode)
                    {
                        ReleaseDirection(desiredDir);
                        Thread.Sleep(directionChangeDelayMs);
                    }

                    if (useAutoExploreReturnTap && !CanSendAutoExploreReturnTap(key, desiredDir, now))
                    {
                        lastMove = desiredDir;
                        lastMoveKey = key;
                        lastMoveWasTapMode = true;
                        return;
                    }

                    if ((now - lastInputTime).TotalMilliseconds >= nearSegmentMoveIntervalMs)
                    {
                        int tapPressMs = useHauntedHouseUpperRouteStep
                            ? GetUpperRouteStepPressMs(key)
                            : turnTapMs;

                        TapDirection(desiredDir, tapPressMs);
                        lastInputTime = now;

                        if (useAutoExploreReturnTap)
                        {
                            autoExploreReturnTapKey = key;
                            autoExploreReturnTapDir = desiredDir;
                            autoExploreReturnTapTime = now;
                        }
                    }

                    lastMove = desiredDir;
                    lastMoveKey = key;
                    lastMoveWasTapMode = true;
                    return;
                }

                if (useSegmentedMoveInCombat)
                {
                    if (!lastMoveWasTapMode)
                    {
                        ReleaseDirection(desiredDir);
                        Thread.Sleep(directionChangeDelayMs);
                    }

                    if ((now - lastInputTime).TotalMilliseconds >= segmentedMovePressMs)
                    {
                        lastInputTime = now;
                        TapDirection(desiredDir, segmentedMovePressMs);
                    }

                    lastMove = desiredDir;
                    lastMoveKey = key;
                    lastMoveWasTapMode = true;
                    return;
                }

                // ===== 일반 구간: 기존처럼 홀드 =====
                if (desiredDir != lastMove || lastMoveWasTapMode)
                {
                    PressDirection(desiredDir);
                    lastInputTime = now;
                }
                else if ((now - lastInputTime).TotalMilliseconds >= repeatMs)
                {
                    PressDirection(desiredDir);
                    lastInputTime = now;
                }

                lastMove = desiredDir;
                lastMoveKey = key;
                lastMoveWasTapMode = false;
            }
        }





        private void ApplyDirection(string key, string dir, System.DateTime now)
        {
            if (string.IsNullOrEmpty(dir))
                return;

            if (dir != lastMove)
            {
                ReleaseDirection(lastMove);
                Thread.Sleep(directionChangeDelayMs);
                PressDirection(dir);

                lastMove = dir;
                lastMoveKey = key;
                lastInputTime = now;
                return;
            }

            if ((now - lastInputTime).TotalMilliseconds >= repeatMs)
            {
                PressDirection(dir);
                lastInputTime = now;
            }

            lastMoveKey = key;
        }

        private void BeginSingleStepToReservedTurn(string currentKey)
        {
            if (string.IsNullOrEmpty(reservedTurnKey) || string.IsNullOrEmpty(reservedApproachDir))
                return;

            ReleaseDirection(lastMove);
            lastMove = "";
            lastInputTime = System.DateTime.MinValue;
            lastMoveKey = currentKey;

            steppingToReservedTurn = true;
            stepTargetKey = reservedTurnKey;
            stepDirection = reservedApproachDir;
            stepPulseSent = false;
            stepPulseTime = System.DateTime.MinValue;
        }


        private void ClearSingleStepState()
        {
            steppingToReservedTurn = false;
            stepTargetKey = "";
            stepDirection = "";
            stepPulseSent = false;
            stepPulseTime = System.DateTime.MinValue;
        }


        private void PressDirection(string dir)
        {
            if (dir == "동")
            {
                User32.API.keybd_event(0x27, 0, 0, 0);
            }
            else if (dir == "서")
            {
                User32.API.keybd_event(0x25, 0, 0, 0);
            }
            else if (dir == "남")
            {
                User32.API.keybd_event(0x28, 0, 0, 0);
            }
            else if (dir == "북")
            {
                User32.API.keybd_event(0x26, 0, 0, 0);
            }
        }

        private void TapDirection(string dir, int pressMs)
        {
            if (string.IsNullOrEmpty(dir))
                return;

            PressDirection(dir);
            Thread.Sleep(pressMs);
            ReleaseDirection(dir);
        }

        private void ReleaseDirection(string dir)
        {
            if (dir == "동")
            {
                User32.API.keybd_event(0x27, 0, 2, 0);
            }
            else if (dir == "서")
            {
                User32.API.keybd_event(0x25, 0, 2, 0);
            }
            else if (dir == "남")
            {
                User32.API.keybd_event(0x28, 0, 2, 0);
            }
            else if (dir == "북")
            {
                User32.API.keybd_event(0x26, 0, 2, 0);
            }
        }


        private void ClearReservedTurn()
        {
            reservedTurnKey = "";
            reservedTurnDir = "";
            reservedApproachDir = "";
            reservedUseStep = false;
        }
        private bool TryParseMoveKey(string key, out int mapNo, out int x, out int y)
        {
            mapNo = 0;
            x = 0;
            y = 0;

            try
            {
                int colonIndex = key.IndexOf(':');
                int openIndex = key.IndexOf('(');
                int commaIndex = key.IndexOf(',');
                int closeIndex = key.IndexOf(')');

                if (colonIndex < 0 || openIndex < 0 || commaIndex < 0 || closeIndex < 0)
                    return false;

                string mapText = key.Substring(0, colonIndex);
                string xText = key.Substring(openIndex + 1, commaIndex - openIndex - 1);
                string yText = key.Substring(commaIndex + 1, closeIndex - commaIndex - 1);

                mapNo = int.Parse(mapText);
                x = int.Parse(xText);
                y = int.Parse(yText);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool HasTurnAhead(string currentKey, string currentDir)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(currentDir))
                return false;

            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return false;

            int dx = 0;
            int dy = 0;

            if (currentDir == "동") dx = 1;
            else if (currentDir == "서") dx = -1;
            else if (currentDir == "남") dy = 1;
            else if (currentDir == "북") dy = -1;
            else return false;

            for (int i = 1; i <= lookAheadCount; i++)
            {
                int nx = x + (dx * i);
                int ny = y + (dy * i);
                string nextKey = mapNo + ":(" + nx.ToString("00") + "," + ny.ToString("00") + ")";

                if (directionDic != null && directionDic.ContainsKey(nextKey))
                    return true;
            }

            return false;
        }

        private int CalcDistanceOnDirection(string currentKey, string targetKey, string dir)
        {
            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(targetKey) || string.IsNullOrEmpty(dir))
                return int.MinValue;

            if (!TryParseMoveKey(currentKey, out int curMap, out int curX, out int curY))
                return int.MinValue;

            if (!TryParseMoveKey(targetKey, out int tarMap, out int tarX, out int tarY))
                return int.MinValue;

            if (curMap != tarMap)
                return int.MinValue;

            if (dir == "동")
            {
                if (curY != tarY) return int.MinValue;
                return tarX - curX;
            }
            else if (dir == "서")
            {
                if (curY != tarY) return int.MinValue;
                return curX - tarX;
            }
            else if (dir == "남")
            {
                if (curX != tarX) return int.MinValue;
                return tarY - curY;
            }
            else if (dir == "북")
            {
                if (curX != tarX) return int.MinValue;
                return curY - tarY;
            }

            return int.MinValue;
        }




        private bool TryFindAheadTurn(string currentKey, string currentDir, out string foundKey, out string foundDir, out int distance)
        {
            foundKey = "";
            foundDir = "";
            distance = -1;

            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(currentDir))
                return false;

            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return false;

            int dx = 0;
            int dy = 0;

            if (currentDir == "동") dx = 1;
            else if (currentDir == "서") dx = -1;
            else if (currentDir == "남") dy = 1;
            else if (currentDir == "북") dy = -1;
            else return false;

            for (int i = 1; i <= lookAheadCount; i++)
            {
                int nx = x + (dx * i);
                int ny = y + (dy * i);
                string nextKey = mapNo + ":(" + nx.ToString("00") + "," + ny.ToString("00") + ")";

                if (directionDic != null && directionDic.TryGetValue(nextKey, out string nextDir))
                {
                    if (string.IsNullOrEmpty(nextDir))
                        continue;

                    // 같은 방향은 그냥 직선 구간
                    if (nextDir == currentDir)
                        continue;

                    foundKey = nextKey;
                    foundDir = nextDir;
                    distance = i;
                    return true;
                }
            }

            return false;
        }


        private bool ShouldUseSegmentedMoveForCurrentRoute(string key)
        {
            if (!IsCombatDungeonKey(key))
                return false;

            if (!useSegmentedMoveOnlyInLoop)
                return true;

            return IsHauntedHouseClockwiseLoopActive();
        }

        private int GetSegmentedMoveDelayMs()
        {
            return useSegmentedMoveOnlyInLoop && IsHauntedHouseClockwiseLoopActive()
                ? loopSegmentedMoveDelayMs
                : basicSegmentedMoveDelayMs;
        }

        private bool IsHauntedHouseClockwiseLoopActive()
        {
            return hauntedHouse5UpperRoutePhase == 3 ||
                   hauntedHouse6UpperRoutePhase == 4 ||
                   hauntedHouse7UpperRoutePhase == 3 ||
                   hauntedHouse8UpperRoutePhase == 3 ||
                   hauntedHouse9UpperRoutePhase == 3 ||
                   hauntedHouse10UpperRoutePhase == 5;
        }

        private bool IsHauntedHouseUpperRouteStepActive()
        {
            return hauntedHouse5UpperRoutePhase == 1 ||
                   hauntedHouse5UpperRoutePhase == 2 ||
                   hauntedHouse6UpperRoutePhase == 2 ||
                   hauntedHouse6UpperRoutePhase == 3 ||
                   hauntedHouse7UpperRoutePhase == 1 ||
                   hauntedHouse7UpperRoutePhase == 2 ||
                   hauntedHouse8UpperRoutePhase == 1 ||
                   hauntedHouse8UpperRoutePhase == 2 ||
                   hauntedHouse9UpperRoutePhase == 1 ||
                   hauntedHouse9UpperRoutePhase == 2 ||
                   hauntedHouse10UpperRoutePhase == 2 ||
                   hauntedHouse10UpperRoutePhase == 3 ||
                   hauntedHouse10UpperRoutePhase == 4;
        }


        private bool ShouldUseTapMoveBeforePress(string key, string dir)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(dir))
                return false;

            if (TryFindAheadTurn(key, dir, out _, out _, out int distance))
            {
                return distance > 0 && distance <= nearTurnStepThreshold;
            }

            return false;
        }

        private bool ShouldUseAutoExploreReturnTapMove(string key, string dir)
        {
            if (!autoExploreMode)
                return false;

            if (autoExploreManualRoute == null || autoExploreManualRoute.Count == 0)
                return false;

            int currentDistance = GetDistanceFromManualRoute(key);
            if (currentDistance <= 0)
                return false;

            if (!TryGetNextMoveKey(key, dir, out string nextKey))
                return false;

            int nextDistance = GetDistanceFromManualRoute(nextKey);
            return nextDistance >= 0 && nextDistance < currentDistance;
        }

        private bool CanSendAutoExploreReturnTap(string key, string dir, System.DateTime now)
        {
            if (key != autoExploreReturnTapKey || dir != autoExploreReturnTapDir)
                return true;

            return (now - autoExploreReturnTapTime).TotalMilliseconds >= GetAutoExploreReturnTapRetryMs();
        }

        private int GetAutoExploreReturnTapRetryMs()
        {
            if (UseSegmentedMove && !useSegmentedMoveOnlyInLoop)
                return Math.Max(basicSegmentedMoveDelayMs, nearSegmentMoveIntervalMs);

            return AutoExploreReturnTapRetryMs;
        }



        private bool TryFindDirectionChangeAhead(string currentKey, string currentDir, out string foundKey, out string foundDir, out int distance)
        {
            foundKey = "";
            foundDir = "";
            distance = -1;

            if (string.IsNullOrEmpty(currentKey) || string.IsNullOrEmpty(currentDir))
                return false;

            if (!TryParseMoveKey(currentKey, out int mapNo, out int x, out int y))
                return false;

            int dx = 0;
            int dy = 0;

            if (currentDir == "동") dx = 1;
            else if (currentDir == "서") dx = -1;
            else if (currentDir == "남") dy = 1;
            else if (currentDir == "북") dy = -1;
            else return false;

            for (int i = 1; i <= lookAheadCount; i++)
            {
                int nx = x + (dx * i);
                int ny = y + (dy * i);
                string nextKey = mapNo + ":(" + nx.ToString("00") + "," + ny.ToString("00") + ")";

                if (directionDic != null && directionDic.TryGetValue(nextKey, out string nextDir))
                {
                    if (string.IsNullOrEmpty(nextDir))
                        continue;

                    // 같은 방향은 계속 직진 구간이므로 스킵
                    if (nextDir == currentDir)
                        continue;

                    foundKey = nextKey;
                    foundDir = nextDir;
                    distance = i;
                    return true;
                }
            }

            return false;
        }

        private int GetEffectiveRepeatMs(string key, string dir)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(dir))
                return repeatMs;

            if (TryFindDirectionChangeAhead(key, dir, out _, out _, out int distance))
            {
                if (distance > 0 && distance <= nearTurnStepThreshold)
                {
                    return nearTurnSlowRepeatMs;
                }
            }

            return repeatMs;
        }






        private void activate234()
        {
            //좌표 설정
            //int x = 1750; //좀더빠르게 공증
            int x = 1801;
            int y = 950;

            int x0 = 1703;
            int y0 = 921;
            if (SelectedHealHpThreshold == "85%")
            {
                //체력 약 85% 기준
                x0 = 1703;
                y0 = 921;
            }
            else if (SelectedHealHpThreshold == "5%")
            {
                //체력 약 5% 기준
                x0 = 1843;
                y0 = 921;
            }
            else if (SelectedHealHpThreshold == "10%")
            {
                //체력 약 10% 기준
                x0 = 1823;
                y0 = 921;
            }
            else
            {
                //체력 약 85% 기준
                x0 = 1703;
                y0 = 921;
            }


            int x3 = 1843;
            int y3 = 942;

            
            //호박
            //XY(1691, 260)    RGB(6, 3, 6)
            int x1_hobak = 1691;
            int y1_hobak = 260;

            //차원의조각 채팅 확인 :: 
            //XY(60, 972)  RGB(11, 9, 172)
            //XY(58,1001)RGB(43,32,155)
            int x1_chawon = 58;
            int y1_chawon = 1001;

            //헬파이어 쿨 확인용
            int x1_hell = 32;
            int y1_hell = 8;

            int x2_hell = 26;
            int y2_hell = 37;

            int x3_hell = 26;
            int y3_hell = 66;

            int x4_hell = 26;
            int y4_hell = 95;

            //XY(26, 66)   RGB(153, 148, 148)
            //XY(26, 95)	   RGB(157,150,148)

            //캡챠 확인용 좌표
            int x5 = 1231;
            int y5 = 855;

            //일반 채팅 확인용 좌표
            int x6 = 1383;
            int y6 = 1006;

            //죽음 확인용 좌표 // XY(1863,917)	RGB(8,4,8)
            int x1_death = 1863;
            int y1_death = 917;

            //좌표 색 가져오기
            Color curColor = GetColorAt(x, y);
            Color curColor0 = GetColorAt(x0, y0);
            Color curColor3 = GetColorAt(x3, y3);
            Color curColor5 = GetColorAt(x5, y5);
            Color curColor6 = GetColorAt(x6, y6);

            Color curColorHellCool1 = GetColorAt(x1_hell, y1_hell);
            Color curColorHellCool2 = GetColorAt(x2_hell, y2_hell);
            Color curColorHellCool3 = GetColorAt(x3_hell, y3_hell);
            Color curColorHellCool4 = GetColorAt(x4_hell, y4_hell);

            Color curColorChawon = GetColorAt(x1_chawon, y1_chawon);

            Color curColorDeath = GetColorAt(x1_death, y1_death);

            Color curColorHobak = GetColorAt(x1_hobak, y1_hobak);

            R0 = curColor0.R;
            G0 = curColor0.G;
            B0 = curColor0.B;

            R2 = curColor.R;
            G2 = curColor.G;
            B2 = curColor.B;

            R3 = curColor3.R;
            G3 = curColor3.G;
            B3 = curColor3.B;

            R5 = curColor5.R;
            G5 = curColor5.G;
            B5 = curColor5.B;

            R6 = curColor6.R;
            G6 = curColor6.G;
            B6 = curColor6.B;

            hell_R1 = curColorHellCool1.R;
            hell_G1 = curColorHellCool1.G;
            hell_B1 = curColorHellCool1.B;

            hell_R2 = curColorHellCool2.R;
            hell_G2 = curColorHellCool2.G;
            hell_B2 = curColorHellCool2.B;

            hell_R3 = curColorHellCool3.R;
            hell_G3 = curColorHellCool3.G;
            hell_B3 = curColorHellCool3.B;

            hell_R4 = curColorHellCool4.R;
            hell_G4 = curColorHellCool4.G;
            hell_B4 = curColorHellCool4.B;

            chawon_R1 = curColorChawon.R;
            chawon_G1 = curColorChawon.G;
            chawon_B1 = curColorChawon.B;

            death_R1 = curColorDeath.R;
            death_G1 = curColorDeath.G;
            death_B1 = curColorDeath.B;

            hobak_R1 = curColorHobak.R;
            hobak_G1 = curColorHobak.G;
            hobak_B1 = curColorHobak.B;



            //사망 확인
            if (death_R1 == 8 && death_G1 == 4 && death_B1 == 8)
            {
                StopMovement();
                //평타 종료
                if (UseNormalAttack)
                {
                    User32.API.keybd_event(0X20, 0, 2, 0);
                }
                //줍기 종료
                if (UsePickup)
                {
                    User32.API.keybd_event(0xBC, 0, 2, 0);
                }
                //저주 종료
                if (UseCurseOption)
                {
                    User32.API.keybd_event((byte)magic4, 0, 2, 0);
                }
                //첨1,첨2 종료
                if (UseExtraCombo)
                {
                    User32.API.keybd_event((byte)magic5, 0, 2, 0);
                    User32.API.keybd_event((byte)magic6, 0, 2, 0);
                }
                Console.Beep(SOL, 150);
                Console.Beep(FA, 150);
                Console.Beep(MI, 150);
                SayText("성황령으로");
                return;
            }

            //캡챠 발생 시 동작 안함
            if (UseCaptchaAlert && !(R5 == 6 && G5 == 3 && B5 == 6) && beepCount < 30)
            {
                StopMovement();
                lock (actionKeyLock)
                {
                    ReleaseStationaryCombatKeys();
                    ReleasePickup();
                    ReleaseCurse();
                }

                beepCount++;
                Console.Beep(SOL, 150);
                Console.Beep(FA, 150);
                Console.Beep(MI, 150);
                SayText("captcha");
                return;
            }

            if (beepCount >= 30)
            {
                Macro.getInstance.Flag_END = false;
                beepCount = 0;
                StopMovement();
                lock (actionKeyLock)
                {
                    ReleaseStationaryCombatKeys();
                    ReleasePickup();
                    ReleaseCurse();
                }
                SayText("captcha chacha");
                return;
            }


            //호박 100개 참
            if (!(hobak_R1 == 6 && hobak_G1 == 3 && hobak_B1 == 6))
            {
                SayPumpkinText("호박 is full");
                StartPumpkinFullSequenceAsync();

            }

            //현재 맵 타이틀과 좌표를 통해 key 추출
            int res1 = getMapNumber();
            string res2 = getMapXY();
            string key = "" + res1 + ":" + res2;
            
            //무빙
            if (movingOpt)
            {
                curMove = GetMoveDirectionForCurrentMode(res1, res2, key);
                UpdateMove(key, curMove);
                Console.WriteLine($"KEY={key}, CUR={curMove ?? "null"}, LAST={lastMove ?? "null"}, HAS={(directionDic != null && directionDic.ContainsKey(key))}");
 
            }
            else
            {
                StopMovement();
            }

            SyncStationaryCombatKeys(key);

            if (res1 == 12 || (res1 == 0 && SelectedMap != "선비"))
            {
                //return;
            }


            bool isChawon = false;
            //차원의조각 채팅 확인 43,32,155
            if ( (chawon_R1 == 43 && chawon_G1 == 32 && chawon_B1 == 155) || (chawon_R1 == 83 && chawon_G1 == 93 && chawon_B1 == 135))      //RGB(11, 9, 172) //112,122,169
            {
                isChawon = true;
            }
            if ((chawon_R1 == 206 && chawon_G1 == 208 && chawon_B1 == 217) || (chawon_R1 == 255 && chawon_G1 == 255 && chawon_B1 == 255))
            {
                isChawon = true;
            }
            //일반 채팅 매크로 반응
            if (UseChatDetectAlt2 && !isChawon && timerForAlt2.ElapsedMs > ChatDetectAlt2Cooldown * 1000)
            {
                timerForAlt2.Start();
                Console.WriteLine($"Find!! ({chawon_R1},{chawon_G1},{chawon_B1})");
                string newString = "채팅 XY(" + x1_chawon + "," + y1_chawon + ")" + "\tRGB(" + chawon_R1 + "," + chawon_G1 + "," + chawon_B1 + ")";
                Console.WriteLine($"{newString}");
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //MainWindow.getInstance.ViewModel.StateString = newString;
                }));

                //채팅 XY(58,1001)	RGB(83,93,135)
                //채팅 XY(58,1001)	RGB(206,208,217) 
                //채팅 XY(58,1001)	RGB(178,135,95) 귓속말 거부 상태
                //채팅 XY(58,1001)	RGB(214,216,226)

                if ((R6 == 229 && G6 == 189 && B6 == 154) ||
                    (R6 == 231 && G6 == 196 && B6 == 154) ||
                    (R6 == 255 && G6 == 196 && B6 == 154) ||
                    (R6 == 230 && G6 == 187 && B6 == 139))
                {
                    Console.Beep(MI, 150);
                    Console.Beep(FA, 150);
                    Console.Beep(SOL, 150);
                    SayText("일반채팅 감지");
                }
            }

            //사자후
            if (UseShout && timerForSajahu.ElapsedMs > ShoutCooldown * 1000)
            {
                timerForSajahu.Start();

                User32.API.keybd_event(0X10, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X5A, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X5A, 0, 2, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X43, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X43, 0, 2, 0);
                User32.API.keybd_event(0X10, 0, 2, 0);

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    System.Windows.Clipboard.SetText(ShoutMessage);
                });

                Thread.Sleep(5);

                User32.API.keybd_event(0X11, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X56, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event(0X11, 0, 2, 0);
                User32.API.keybd_event(0X56, 0, 2, 0);
                Thread.Sleep(10);
                SK.sendkeyEnter(10);
            }

            //보무
            if (UseProtectArmor && (firstRunFlag || timerForBoMu.ElapsedMs >= 50000))
            {
                firstRunFlag = false;

                User32.API.keybd_event((byte)magic9, 0, 0, 0);
                User32.API.keybd_event((byte)magic0, 0, 0, 0);
                Thread.Sleep(5);
                User32.API.keybd_event((byte)magic9, 0, 2, 0);
                User32.API.keybd_event((byte)magic0, 0, 2, 0);

                timerForBoMu.Start();
            }

            //몬스터가 없는 곳을 구분
            int movingDelay = movingDelayUI;

            //동동주+공증
            if (UseBuffCombo)
            {
                if ( (R3 == 8 && G3 == 4 && B3 == 8) )
                {
                    //SayText("동");
                    SK.sendkeyCtrlAndZ(10);
                }
                Thread.Sleep(10);
                if (R2 == 8 && G2 == 4 && B2 == 8)
                {
                    //SayText("공증힐");
                    //Console.Beep(800, 500);
                    User32.API.keybd_event((byte)magic2, 0, 0, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event((byte)magic2, 0, 2, 0);
                    User32.API.keybd_event((byte)magic3, 0, 0, 0);
                    Thread.Sleep(200);
                    User32.API.keybd_event((byte)magic3, 0, 2, 0);
                }
            }


            //힐
            if (UseHeal)
            {
                System.Random random3 = new System.Random((int)System.DateTime.Now.Ticks);
                int random3to4 = random3.Next(9, 10);
                if (R0 == 8 && G0 == 4 && B0 == 8)
                {
                    User32.API.keybd_event((byte)magic3, 0, 0, 0);
                    Thread.Sleep(100 * random3to4);
                    User32.API.keybd_event((byte)magic3, 0, 2, 0);
                }
                //int tempCount = 0;
                //while (R0 == 8 && G0 == 4 && B0 == 8 && tempCount < 5)
                //{
                //    User32.API.keybd_event((byte)magic3, 0, 0, 0);
                //    Thread.Sleep(100 * random3to4);
                //    User32.API.keybd_event((byte)magic3, 0, 2, 0);
                //    curColor0 = GetColorAt(x0, y0);
                //    R0 = curColor0.R;
                //    G0 = curColor0.G;
                //    B0 = curColor0.B;
                //    tempCount++;
                //}
                //if (tempCount >= 5)
                //{
                //    User32.API.keybd_event((byte)magic3, 0, 0, 0);
                //    Thread.Sleep(1000);
                //    User32.API.keybd_event((byte)magic3, 0, 2, 0);
                //}
            }



            //산적굴 9굴 시
            
            //마기지체
            if (UseMagi && !useMagiFlag)
            {
                if (SelectedMap == "산적" && res1 == 9)
                {
                    useMagiFlag = true;
                    User32.API.keybd_event((byte)magic13, 0, 0, 0);
                    Thread.Sleep(100);
                    User32.API.keybd_event((byte)magic13, 0, 2, 0);
                }else if (SelectedMap == "선비" && res1 == 5 && (res2 == "(11,10)" || res2 == "(11,09)"))
                {
                    useMagiFlag = true;
                    User32.API.keybd_event((byte)magic13, 0, 0, 0);
                    Thread.Sleep(100);
                    User32.API.keybd_event((byte)magic13, 0, 2, 0);
                }
            }
            else
            {
                useMagiFlag = false;
            }
            //지폭
            if (UseJipok && !useJipokFlag)
            {
                //산적
                if (SelectedMap == "산적" && res1 == 9 && (res2 == "(15,17)" || res2 == "(16,17)"))
                {
                    useJipokFlag = true;
                    User32.API.keybd_event((byte)magic12, 0, 0, 0);
                    Thread.Sleep(100);
                    User32.API.keybd_event((byte)magic12, 0, 2, 0);
                    //SayText("지폭지술");
                    return;
                }
                //선비
                if (SelectedMap == "선비" && res1 == 5 && (res2 == "(11,10)" || res2 == "(11,09)"))
                {
                    useJipokFlag = true;
                    User32.API.keybd_event((byte)magic12, 0, 0, 0);
                    Thread.Sleep(100);
                    User32.API.keybd_event((byte)magic12, 0, 2, 0);
                    //SayText("지폭지술");
                    return;
                }
            }
            else
            {
                useJipokFlag = false;
            }
            
            
            //정지 시
            if (ShouldCastThreeHellOnStall(key))
            {
                //마비 사용 (강한 몹에 둘러쌓였을 경우를 대비)
                if (UseParalysis && paralysisCount % 8 == 0)
                {
                    ReleaseStationaryCombatKeys();
                    paralysis();
                }
                paralysisCount++;
                //중독 돌리기
                if (UsePoison && poisonCount % 4 == 0)
                {
                    ReleaseStationaryCombatKeys();
                    poision();
                }
                poisonCount++;
                // 삼매진화
                // 내 좌표가 3초 이상 안 움직였을 때만 1회 발동 :: threeHellStationaryMs = 3000;
                if (UseThreeHellEvolution)
                {
                    ReleaseStationaryCombatKeys();
                    User32.API.keybd_event((byte)magic8, 0, 0, 0);
                    Thread.Sleep(10);
                    User32.API.keybd_event((byte)magic8, 0, 2, 0);
                    return;
                }
            }

            // 내 좌표가 100초 이상 안 움직였을 때 발동 
            if (FindWall(key))
            {
                PauseMovementForAction();
                melody1();
                SayText("정지상태");
                return;
            }

            bool HellPosibleFlag = true;
            //is hell cool?
            if ((hell_R1 == 255 && hell_G1 == 255 && hell_B1 == 255) || (hell_R2 == 156 && hell_G2 == 148 && hell_B2 == 148)
                || (hell_R3 == 153 && hell_G3 == 148 && hell_B3 == 148) || (hell_R4 == 157 && hell_G4 == 150 && hell_B4 == 148))
            {
                HellPosibleFlag = false;
            }
            // 헬파이어: 삼매처럼 좌표가 막혀 있을 때만 발동
            if (UseHellfire && IsHellfireStallReady(key) && HellPosibleFlag && !(R0 == 8 && G0 == 4 && B0 == 8))
            {
                lock (actionKeyLock)
                {
                    // END로 이미 꺼졌으면 아무 것도 하지 않음
                    if (!Macro.getInstance.Flag_END)
                        return;

                    // 이동 상태는 살리고, 실제 방향키만 잠깐 해제
                    PauseMovementForAction();

                    // 저주 / 첨1 / 첨2 해제
                    ReleaseCurse();
                    ReleaseStationaryCombatKeys();

                    try
                    {
                        User32.API.BlockInput(true);
                        if (SelectedMap == "선비" && res1 < 5)
                        {
                            attackUsingHell2();
                        }
                        else
                        {
                            attackUsingHell();
                        }

                        MarkHellfireStallCast();
                    }
                    finally
                    {
                        User32.API.BlockInput(false);
                    }

                    // 공격 도중 END가 눌렸으면 다시 누르지 않고 끝냄
                    if (!Macro.getInstance.Flag_END)
                        return;

                    // 헬파이어 후 현재 좌표를 다시 읽어서
                    // 기존 이동 로직(UpdateMove)으로 자연스럽게 복귀
                    if (movingOpt)
                    {
                        int resumeMapNo = getMapNumber();
                        string resumeXY = getMapXY();
                        string resumeKey = resumeMapNo + ":" + resumeXY;
                        string resumeMove = GetMoveDirectionForCurrentMode(resumeMapNo, resumeXY, resumeKey);

                        UpdateMove(resumeKey, resumeMove);
                    }

                    // END가 살아 있을 때만 다시 누름
                    PressCurseIfRunning();
                }

                return;
            }

            
        }






        private void ReleaseNormalAttack()
        {
            User32.API.keybd_event(0X20, 0, 2, 0);
            normalAttackKeyPressed = false;
        }

        private void PressNormalAttackIfRunning()
        {
            if (!Macro.getInstance.Flag_END)
                return;

            if (UseNormalAttack)
            {
                User32.API.keybd_event(0X20, 0, 0, 0);
            }
        }

        private void ReleaseStationaryCombatKeys()
        {
            ReleaseNormalAttack();
            ReleaseChumChum();
            stationaryCombatKeysPressed = false;
        }

        private void PressStationaryCombatKeysIfRunning(bool allowNormalAttack)
        {
            if (!Macro.getInstance.Flag_END)
                return;

            if (stationaryCombatKeysPressed && (!allowNormalAttack || !UseNormalAttack || normalAttackKeyPressed))
                return;

            bool pressedAny = false;

            if (allowNormalAttack && UseNormalAttack && !normalAttackKeyPressed)
            {
                User32.API.keybd_event(0X20, 0, 0, 0);
                normalAttackKeyPressed = true;
                pressedAny = true;
            }

            if (UseExtraCombo)
            {
                User32.API.keybd_event((byte)magic5, 0, 0, 0);
                User32.API.keybd_event((byte)magic6, 0, 0, 0);
                pressedAny = true;
            }

            stationaryCombatKeysPressed = pressedAny;
        }

        private void SyncPickupKeyIfRunning(string currentKey)
        {
            if (!Macro.getInstance.Flag_END || !UsePickup || !IsCombatDungeonKey(currentKey))
            {
                ReleasePickup();
                return;
            }

            if (pickupKeyPressed)
                return;

            User32.API.keybd_event(0xBC, 0, 0, 0);
            pickupKeyPressed = true;
        }

        private void SyncPickupFromCurrentPositionIfRunning()
        {
            int mapNo = getMapNumber();
            string xyText = getMapXY();

            if (string.IsNullOrEmpty(xyText))
            {
                ReleasePickup();
                return;
            }

            SyncPickupKeyIfRunning(mapNo + ":" + xyText);
        }

        private bool IsCombatDungeonKey(string currentKey)
        {
            if (!TryParseMoveKey(currentKey, out int mapNo, out _, out _))
                return false;

            return mapNo >= 1 && mapNo <= 10;
        }

        private bool IsStationaryCombatReady(string currentKey)
        {
            if (string.IsNullOrEmpty(currentKey))
                return false;

            if (lastPositionKey != currentKey)
                return false;

            if (lastPositionChangedTime == System.DateTime.MinValue)
                return false;

            return (System.DateTime.Now - lastPositionChangedTime).TotalMilliseconds >= StationaryCombatReadyMs;
        }

        private void SyncStationaryCombatKeys(string currentKey)
        {
            lock (actionKeyLock)
            {
                if (!Macro.getInstance.Flag_END)
                {
                    ReleaseStationaryCombatKeys();
                    ReleasePickup();
                    return;
                }

                bool isCombatDungeon = IsCombatDungeonKey(currentKey);

                SyncPickupKeyIfRunning(currentKey);

                if (!isCombatDungeon)
                    ReleaseNormalAttack();

                if (IsStationaryCombatReady(currentKey))
                {
                    PressStationaryCombatKeysIfRunning(isCombatDungeon);
                }
                else
                {
                    ReleaseStationaryCombatKeys();
                }
            }
        }

        private void ReleaseLoopKeys()
        {
            ReleaseCurse();
            ReleasePickup();
        }

        private void StartPumpkinFullSequenceAsync()
        {
            SyncSelectedMapFromViewModel();
            if (!IsGwallyeongHauntedHouseMap(SelectedMap))
            {
                SayPumpkinText("관령흉가 아님 호박 처리 안함");
                return;
            }

            if (!Macro.getInstance.Flag_END)
            {
                SayPumpkinText("자동마법 꺼짐 호박 처리 안함");
                return;
            }

            lock (pumpkinFullLock)
            {
                if (pumpkinFullSequenceRunning)
                    return;

                pumpkinFullCancelRequested = false;
                pumpkinFullSequenceRunning = true;
            }

            Task.Run(() =>
            {
                try
                {
                    RunPumpkinFullSequence();
                }
                catch (OperationCanceledException)
                {
                    SayPumpkinText("호박 처리 중단");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pumpkin full sequence failed: " + ex.Message);
                    SayPumpkinText("호박 처리 실패");
                }
                finally
                {
                    lock (pumpkinFullLock)
                    {
                        pumpkinFullSequenceRunning = false;
                        pumpkinFullInternalEndOff = false;
                    }
                }
            });
        }

        private void CancelPumpkinFullSequence()
        {
            lock (pumpkinFullLock)
            {
                pumpkinFullCancelRequested = true;
            }

            ReleaseDirection("서");
            ReleaseDirection("동");
            ReleaseDirection("북");
            ReleaseDirection("남");
        }

        private bool IsPumpkinFullSequenceRunning()
        {
            lock (pumpkinFullLock)
            {
                return pumpkinFullSequenceRunning;
            }
        }

        private void EnsurePumpkinFullSequenceActive()
        {
            lock (pumpkinFullLock)
            {
                if (!Macro.getInstance.Flag_END && !pumpkinFullInternalEndOff)
                    pumpkinFullCancelRequested = true;

                if (pumpkinFullCancelRequested)
                    throw new OperationCanceledException();
            }
        }

        private void RunPumpkinFullSequence()
        {
            SayPumpkinText("호박 처리 시작");
            SayPumpkinText("기존 동작 종료 및 부여성 비서 사용");
            StopAllAutomationForPumpkinFull();
            UseBuyCastleScroll();
            PumpkinStepDelay();
            EnsurePumpkinFullSequenceActive();

            SayPumpkinText("비영사천문 북쪽 이동");
            UseBiyoungPortal();
            PumpkinStepDelay();
            EnsurePumpkinFullSequenceActive();

            SayPumpkinText("장터 이동 시작");
            MoveFromBuyCastleToMarket();
            PumpkinStepDelay();
            EnsurePumpkinFullSequenceActive();

            SayPumpkinText("호박 판매 시작");
            SellPumpkinsAtMarket();
            PumpkinStepDelay();
            EnsurePumpkinFullSequenceActive();

            SayPumpkinText("관령성 복귀 시작");
            ReturnToGwallyeongCastle();
            PumpkinStepDelay();
            EnsurePumpkinFullSequenceActive();

            SayPumpkinText("자동 사냥 재시작");
            StartAutomationAfterPumpkinFull();
        }

        private void StopAllAutomationForPumpkinFull()
        {
            pumpkinFullInternalEndOff = true;
            Macro.getInstance.Flag_END = false;

            lock (autoExploreLock)
            {
                pumpkinReturnRouteResumePending = autoExploreUsingReturnRoute;
                autoExploreMode = false;
                autoExploreUsingReturnRoute = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                autoExploreLastKey = "";
                autoExploreLastDir = "";
                autoExploreLastDeviationRouteKey = "";
                autoExploreReturnTapKey = "";
                autoExploreReturnTapDir = "";
                autoExploreDeviationReturnPending = false;
                autoExploreReturnTapTime = System.DateTime.MinValue;
                autoExploreSamePointFailCount = 0;
            }

            movingOpt = false;
            StopMovement();

            lock (actionKeyLock)
            {
                ReleaseStationaryCombatKeys();
                ReleaseLoopKeys();
                ReleaseChumChum();
            }
        }

        private void UseBuyCastleScroll()
        {
            PressUseItemKey();
            Thread.Sleep(180);
            PressKey(0x43, 80);
        }

        private void UseYellowScroll()
        {
            PressUseItemKey();
            Thread.Sleep(180);
            PressKey(0x42, 80);
        }

        private void PressUseItemKey()
        {
            Thread.Sleep(400);
            PressKey(0x55, 120);
        }

        private void UseBiyoungPortal()
        {
            UseBiyoungPortal(4);
        }

        private void UseBiyoungPortal(int portalNumber)
        {
            User32.API.keybd_event(0xA0, 0, 0, 0);
            Thread.Sleep(50);
            PressKey(0x5A);
            User32.API.keybd_event(0xA0, 0, 2, 0);
            Thread.Sleep(120);
            PressKey(0x5A);
            Thread.Sleep(120);
            PressKey(0x30 + portalNumber);
            Thread.Sleep(120);
            PressKey(0x0D);
        }

        private void MoveFromBuyCastleToMarket()
        {
            System.DateTime deadline = System.DateTime.Now.AddSeconds(75);
            System.DateTime forceWestUntil = System.DateTime.Now.AddSeconds(20);
            string lastMarketMoveStage = "";
            bool reachedCastleX13 = false;
            bool startedNorthToMarket = false;
            string forceWestLastPosition = "";
            System.DateTime forceWestLastPositionChanged = System.DateTime.Now;

            SayMarketMoveStage("장터 이동 서쪽 20초 유지", ref lastMarketMoveStage);
            PressDirection("서");
            try
            {
                while (System.DateTime.Now < forceWestUntil)
                {
                    EnsurePumpkinFullSequenceActive();

                    if (TryGetCurrentMapXY(out int _, out int forceWestX, out int forceWestY))
                    {
                        string currentPosition = forceWestX + "," + forceWestY;
                        if (currentPosition != forceWestLastPosition)
                        {
                            forceWestLastPosition = currentPosition;
                            forceWestLastPositionChanged = System.DateTime.Now;
                        }
                        else if ((System.DateTime.Now - forceWestLastPositionChanged).TotalSeconds >= 2)
                        {
                            SayMarketMoveStage("서쪽 길막힘 우회", ref lastMarketMoveStage);
                            ReleaseDirection("서");
                            ExecuteMarketWestDetour();
                            PressDirection("서");
                            forceWestLastPositionChanged = System.DateTime.Now;
                        }
                    }

                    Thread.Sleep(100);
                }
            }
            finally
            {
                ReleaseDirection("서");
            }

            while (System.DateTime.Now < deadline)
            {
                EnsurePumpkinFullSequenceActive();

                if (!TryGetCurrentMapXY(out int _, out int x, out int y))
                {
                    Thread.Sleep(150);
                    continue;
                }

                if (startedNorthToMarket)
                {
                    if (reachedCastleX13 && x == 22)
                    {
                        SayPumpkinText("장터 도착");
                        return;
                    }

                    SayMarketMoveStage("장터 진입 북쪽", ref lastMarketMoveStage);
                    TapDirection("북", 120);
                    Thread.Sleep(120);
                    continue;
                }

                if (y < 13)
                {
                    SayMarketMoveStage("장터 이동 y 보정 남쪽", ref lastMarketMoveStage);
                    TapDirection("남", 80);
                }
                else if (y > 13)
                {
                    SayMarketMoveStage("장터 이동 y 보정 북쪽", ref lastMarketMoveStage);
                    TapDirection("북", 80);
                }
                else if (x > 13)
                {
                    SayMarketMoveStage("장터 이동 서쪽", ref lastMarketMoveStage);
                    TapDirection("서", 80);
                }
                else if (x < 13)
                {
                    SayMarketMoveStage("장터 이동 동쪽 복귀", ref lastMarketMoveStage);
                    TapDirection("동", 80);
                }
                else
                {
                    reachedCastleX13 = true;
                    startedNorthToMarket = true;
                    SayMarketMoveStage("장터 진입 북쪽", ref lastMarketMoveStage);
                    TapDirection("북", 120);
                }

                Thread.Sleep(120);
            }
        }

        private void ExecuteMarketWestDetour()
        {
            TapDirection("남", 120);
            PumpkinStepDelay();
            TapDirection("서", 120);
            PumpkinStepDelay();
            TapDirection("서", 120);
            PumpkinStepDelay();
            TapDirection("북", 120);
            PumpkinStepDelay();
        }

        private void SellPumpkinsAtMarket()
        {
            EnsurePumpkinFullSequenceActive();
            SayPumpkinText("알트 육 판매");
            PressAltNumber(6);
            PumpkinStepDelay();

            EnsurePumpkinFullSequenceActive();
            SayPumpkinText("알트 칠 판매");
            PressAltNumber(7);
            PumpkinStepDelay();

            EnsurePumpkinFullSequenceActive();
            SayPumpkinText("알트 팔 판매");
            PressAltNumber(8);
            PumpkinStepDelay();
        }

        private void ReturnToGwallyeongCastle()
        {
            System.DateTime deadline = System.DateTime.Now.AddMinutes(3);

            while (System.DateTime.Now < deadline)
            {
                EnsurePumpkinFullSequenceActive();

                SayPumpkinText("노란 비서 사용");
                UseYellowScroll();
                PumpkinStepDelay();

                EnsurePumpkinFullSequenceActive();
                SayPumpkinText("비영사천문 남쪽 이동");
                UseBiyoungPortal(3);
                PumpkinStepDelay();

                if (IsGwallyeongCastleByPixels())
                {
                    SayPumpkinText("관령성 도착");
                    return;
                }
            }

            SayPumpkinText("관령성 확인 실패");
        }

        private void StartAutomationAfterPumpkinFull()
        {
            timerForBoMu.Start();
            timerForMovingFlag.Start();
            timerForSajahu.Start();
            timerForAlt2.Start();

            firstRunFlag = true;
            ResetThreeHellState();
            setMagicNumber();

            Macro.getInstance.Flag_END = true;

            lock (actionKeyLock)
            {
                ReleaseStationaryCombatKeys();
                PressCurseIfRunning();
            }

            lock (autoExploreLock)
            {
                LoadAutoExploreRoute(moveConfigPath);
                autoExploreUsingReturnRoute = false;
                autoExploreReturnRouteResumeAfterEnd = false;
                autoExploreMode = true;
                movingOpt = true;
            }

            pumpkinFullInternalEndOff = false;
        }

        private bool TryGetCurrentMapXY(out int mapNo, out int x, out int y)
        {
            mapNo = getMapNumber();
            x = 0;
            y = 0;

            string xyText = getMapXY();
            string key = mapNo + ":" + xyText;

            return TryParseMoveKey(key, out _, out x, out y);
        }

        private bool IsGwallyeongCastleByPixels()
        {
            return IsPixel(720, 3, 255, 255, 255) &&
                   IsPixel(730, 4, 198, 192, 192) &&
                   IsPixel(734, 25, 255, 255, 255) &&
                   IsPixel(736, 26, 244, 244, 244) &&
                   IsPixel(748, 12, 136, 137, 136) &&
                   IsPixel(756, 7, 255, 255, 255) &&
                   IsPixel(764, 19, 75, 75, 74) &&
                   IsPixel(764, 25, 81, 63, 64);
        }

        private void SayMarketMoveStage(string text, ref string lastStage)
        {
            if (lastStage == text)
                return;

            lastStage = text;
            SayPumpkinText(text);
        }

        private void PumpkinStepDelay()
        {
            int waitedMs = 0;
            while (waitedMs < PumpkinStepDelayMs)
            {
                EnsurePumpkinFullSequenceActive();
                int sleepMs = Math.Min(100, PumpkinStepDelayMs - waitedMs);
                Thread.Sleep(sleepMs);
                waitedMs += sleepMs;
            }
        }

        private bool IsPixel(int x, int y, int r, int g, int b)
        {
            Color color = GetColorAt(x, y);
            return color.R == r && color.G == g && color.B == b;
        }

        private void PressAltNumber(int number)
        {
            User32.API.keybd_event(0xA4, 0, 0, 0);
            Thread.Sleep(60);
            PressKey(0x30 + number);
            Thread.Sleep(60);
            User32.API.keybd_event(0xA4, 0, 2, 0);
        }

        private void PressKey(int keyCode)
        {
            PressKey(keyCode, 50);
        }

        private void PressKey(int keyCode, int pressMs)
        {
            User32.API.keybd_event((byte)keyCode, 0, 0, 0);
            Thread.Sleep(pressMs);
            User32.API.keybd_event((byte)keyCode, 0, 2, 0);
        }






        private void PauseMovementForAction()
        {
            lock (moveLock)
            {
                // 실제 눌린 방향키만 해제
                if (!string.IsNullOrEmpty(lastMove))
                {
                    ReleaseDirection(lastMove);
                }
                else
                {
                    User32.API.keybd_event(0X25, 0, 2, 0);
                    User32.API.keybd_event(0X26, 0, 2, 0);
                    User32.API.keybd_event(0X27, 0, 2, 0);
                    User32.API.keybd_event(0X28, 0, 2, 0);
                }

                // 이동 경로 상태는 유지
                // 다만 헬파이어 후에는 즉시 다시 눌릴 수 있게 타이머만 초기화
                lastInputTime = System.DateTime.MinValue;
                lastDirectionInputTime = System.DateTime.MinValue;
            }
        }


        public static bool isGwanryung()
        {
            bool res = false;


            return res;
        }


        public static int getMapNumber()
        {   
            SyncSelectedMapFromViewModel();

            int res = 0;
            int mapType = 0;

            int mapTitleX1 = 0;
            int mapTitleY1 = 0;
            int mapTitleX2 = 0;
            int mapTitleY2 = 0;

            int mapTitleX3 = 0;
            int mapTitleY3 = 0;

            //관령흉가, 선녀, 산적
            if (IsGwallyeongHauntedHouseMap(SelectedMap) || SelectedMap == "선녀")
            {
                mapType = 0;
                mapTitleX1 = 802;
                mapTitleY1 = 17;
                mapTitleX2 = 809;
                mapTitleY2 = 11;
                //선녀의방 10번 이후를 위한 좌표 추가
                mapTitleX3 = 645;
                mapTitleY3 = 23;
            }
            else if (SelectedMap == "산적")
            {
                mapType = 1;
                mapTitleX1 = 795;
                mapTitleY1 = 17;
                mapTitleX2 = 802;
                mapTitleY2 = 11;
            }
            else if (SelectedMap == "선비")
            {
                mapType = 2;
                mapTitleX1 = 795-7;
                mapTitleY1 = 17;
                mapTitleX2 = 802-7;
                mapTitleY2 = 11;
                mapTitleX3 = 686;
                mapTitleY3 = 17; //RGB(243, 243, 243)
            }
            else if (SelectedMap == "관령세작")
            {
                mapType = 3;
                mapTitleX1 = 802 + 54;
                mapTitleY1 = 17;
                mapTitleX2 = 809 + 54;
                mapTitleY2 = 11;
            }

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


            if (mapType == 1)
            {
                //Console.WriteLine($"if (titleR1 == {titleR1} && titleG1 == {titleG1} && titleB1 == {titleB1} && titleR2 == {titleR2} && titleG2 == {titleG2} && titleB2 == {titleB2})");
                if (titleR1 == 245 && titleG1 == 244 && titleB1 == 244 && titleR2 == 108 && titleG2 == 109 && titleB2 == 108)
                {
                    res = 0;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                {
                    res = 1;
                }
                else if (titleR1 == 85 && titleG1 == 67 && titleB1 == 69 && titleR2 == 136 && titleG2 == 137 && titleB2 == 136)
                    {
                    res = 2;
                }
                else if (titleR1 == 33 && titleG1 == 10 && titleB1 == 12 && titleR2 == 136 && titleG2 == 137 && titleB2 == 136)
                {
                    res = 3;
                }
                else if (titleR1 == 203 && titleG1 == 197 && titleB1 == 198 && titleR2 == 136 && titleG2 == 137 && titleB2 == 136)
                {
                    res = 4;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 108 && titleG2 == 109 && titleB2 == 108)
                {
                    res = 5;
                }
                else if (titleR1 == 200 && titleG1 == 194 && titleB1 == 195 && titleR2 == 108 && titleG2 == 109 && titleB2 == 108)
                {
                    res = 6;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 136 && titleG2 == 137 && titleB2 == 136)
                {
                    res = 7;
                }
                else if (titleR1 == 200 && titleG1 == 194 && titleB1 == 195 && titleR2 == 136 && titleG2 == 137 && titleB2 == 136)
                {
                    res = 8;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 137 && titleG2 == 137 && titleB2 == 136)
                {
                    res = 9;
                }
                else
                {
                    res = 99;
                }
            }
            else if (mapType == 2)
            {
                //if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                //if (titleR1 == 27 && titleG1 == 25 && titleB1 == 27 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)
                //if (titleR1 == 17 && titleG1 == 14 && titleB1 == 17 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)
                //if (titleR1 == 244 && titleG1 == 244 && titleB1 == 244 && titleR2 == 198 && titleG2 == 199 && titleB2 == 198)
                //if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 156 && titleG2 == 157 && titleB2 == 156)
                //if (titleR1 == 243 && titleG1 == 243 && titleB1 == 243 && titleR2 == 156 && titleG2 == 157 && titleB2 == 156)
                //if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)

                //Console.WriteLine($"if (titleR1 == {titleR1} && titleG1 == {titleG1} && titleB1 == {titleB1} && titleR2 == {titleR2} && titleG2 == {titleG2} && titleB2 == {titleB2})");
                if (titleR3 == 243 && titleG3 == 243 && titleB3 == 243 && titleR1 == 6 && titleG1 == 3 && titleB1 == 6)
                {
                    res = 0;
                }
                //else if(titleR1 == 16 && titleG1 == 13 && titleB1 == 16 && titleR2 == 20 && titleG2 == 21 && titleB2 == 19)
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
                {
                    res = 1;
                }
                else if (titleR1 == 27 && titleG1 == 25 && titleB1 == 27 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)
                {
                    res = 2;
                }
                else if (titleR1 == 17 && titleG1 == 14 && titleB1 == 17 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)
                {
                    res = 3;
                }
                else if (titleR1 == 244 && titleG1 == 244 && titleB1 == 244 && titleR2 == 198 && titleG2 == 199 && titleB2 == 198)
                {
                    res = 4;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 156 && titleG2 == 157 && titleB2 == 156)
                {
                    res = 5;
                }
                else if (titleR1 == 243 && titleG1 == 243 && titleB1 == 243 && titleR2 == 156 && titleG2 == 157 && titleB2 == 156)
                {
                    res = 6;
                }
                else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 171 && titleG2 == 172 && titleB2 == 171)
                {
                    res = 7;
                }
                else
                {
                    res = 99;
                }
            }
            else if (mapType == 3)
            {
                //Console.WriteLine($"if (titleR1 == {titleR1} && titleG1 == {titleG1} && titleB1 == {titleB1} && titleR2 == {titleR2} && titleG2 == {titleG2} && titleB2 == {titleB2})");
                if (titleR1 == 244 && titleG1 == 243 && titleB1 == 243 && titleR2 == 26 && titleG2 == 2 && titleB2 == 4)
                {
                    res = 0;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 26 && titleG2 == 2 && titleB2 == 4)
                {
                    res = 1;
                }
                else if (titleR1 == 141 && titleG1 == 129 && titleB1 == 130 && titleR2 == 107 && titleG2 == 91 && titleB2 == 93)
                {
                    res = 2;
                }
                else if(titleR1 == 31 && titleG1 == 8 && titleB1 == 10 && titleR2 == 107 && titleG2 == 91 && titleB2 == 93)
                {
                    res = 3;
                }
                else if (titleR1 == 145 && titleG1 == 133 && titleB1 == 134 && titleR2 == 81 && titleG2 == 63 && titleB2 == 64)
                {
                    res = 4;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 66 && titleG2 == 47 && titleB2 == 48)
                {
                    res = 5;
                }
                else if (titleR1 == 140 && titleG1 == 128 && titleB1 == 129 && titleR2 == 66 && titleG2 == 47 && titleB2 == 48)
                {
                    res = 6;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 107 && titleG2 == 91 && titleB2 == 93)
                {
                    res = 7;
                }
                else if (titleR1 == 140 && titleG1 == 128 && titleB1 == 129 && titleR2 == 107 && titleG2 == 91 && titleB2 == 93)
                {
                    res = 8;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 200 && titleG2 == 194 && titleB2 == 195)
                {
                    res = 9;
                }
                else if (titleR1 == 26 && titleG1 == 2 && titleB1 == 4 && titleR2 == 81 && titleG2 == 63 && titleB2 == 64)
                {
                    res = 10;
                }
                else
                {
                    res = 99;
                }
            }
            else if (mapType == 0)
            {
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
                    //Console.WriteLine($"if (titleR3 == {titleR3} && titleG3 == {titleG3} && titleB3 == {titleB3})");
                    //Console.WriteLine($"if (titleR4 == {titleR4} && titleG4 == {titleG4} && titleB4 == {titleB4})");
                    //Console.WriteLine($"if (titleR5 == {titleR5} && titleG5 == {titleG5} && titleB5 == {titleB5})");

                    //XY(1185,329)	RGB(139,147,71)
                    if (titleR3 == 18 && titleG3 == 19 && titleB3 == 17 &&
                        titleR4 == 6 && titleG4 == 3 && titleB4 == 6 &&
                        titleR5 == 18 && titleG5 == 19 && titleB5 == 17)
                    {
                        res = 0;
                    }
                    else if (titleR1 == 6 && titleG1 == 3 && titleB1 == 6 && titleR2 == 18 && titleG2 == 19 && titleB2 == 17)
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
            }
            else
            {

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

            //66,129
            //91,53,12,102,55,13,0,0,0,255,255,183,195,155,79,102,55,13,223,207,127,0,0,0,102,55,13,91,53,12

            //res = "" + titleR1 + "," + titleG1 + "," + titleB1 + "," +
            //    titleR2 + "," + titleG2 + "," + titleB2 + "," +
            //    titleR3 + "," + titleG3 + "," + titleB3 + "," +
            //    titleR4 + "," + titleG4 + "," + titleB4 + "," +
            //    titleR5 + "," + titleG5 + "," + titleB5 + "," +
            //    titleR6 + "," + titleG6 + "," + titleB6 + "," +
            //    titleR7 + "," + titleG7 + "," + titleB7 + "," +
            //    titleR8 + "," + titleG8 + "," + titleB8 + "," +
            //    titleR9 + "," + titleG9 + "," + titleB9 + "," +
            //    titleR10 + "," + titleG10 + "," + titleB10;

            string NumOfX1 = titleR6 + "," + titleG6 + "," + titleB6 + "\n" +
                titleR7 + "," + titleG7 + "," + titleB7 + "\n" +
                titleR8 + "," + titleG8 + "," + titleB8;
            string NumOfX10 = titleR9 + "," + titleG9 + "," + titleB9 + "\n" +
                titleR10 + "," + titleG10 + "," + titleB10;


            string x_10 = ""; //x의 십의자리 좌표, 0~4까지 구분가능
            string x_1 = ""; //x의 일의자리 좌표, 0~9까지 구분가능

            string y_10 = ""; //y의 십의자리 좌표, 0~4까지 구분가능
            string y_1 = ""; //y의 일의자리 좌표, 0~9까지 구분가능


            if (titleR6 == 0 && titleG6 == 0 && titleB6 == 0 && titleR7 == 223 && titleG7 == 207 && titleB7 == 127 && titleR8 == 243 && titleG8 == 239 && titleB8 == 163) 
            { x_1 = "0"; }
            if (titleR6 == 0 && titleG6 == 0 && titleB6 == 0 && titleR7 == 107 && titleG7 == 58 && titleB7 == 43 && titleR8 == 117 && titleG8 == 70 && titleB8 == 21) 
            { x_1 = "1"; }
            if (titleR6 == 235 && titleG6 == 223 && titleB6 == 143 && titleR7 == 107 && titleG7 == 58 && titleB7 == 43 && titleR8 == 0 && titleG8 == 0 && titleB8 == 0) 
            { x_1 = "2"; }
            if (titleR6 == 235 && titleG6 == 223 && titleB6 == 143 && titleR7 == 107 && titleG7 == 58 && titleB7 == 43 && titleR8 == 117 && titleG8 == 70 && titleB8 == 21) 
            { x_1 = "3"; }
            if (titleR6 == 102 && titleG6 == 55 && titleB6 == 13 && titleR7 == 223 && titleG7 == 207 && titleB7 == 127 && titleR8 == 243 && titleG8 == 239 && titleB8 == 163) 
            { x_1 = "4"; }
            if (titleR6 == 0 && titleG6 == 0 && titleB6 == 0 && titleR7 == 0 && titleG7 == 0 && titleB7 == 0 && titleR8 == 117 && titleG8 == 70 && titleB8 == 21) 
            { x_1 = "5"; }
            if (titleR6 == 102 && titleG6 == 55 && titleB6 == 13 && titleR7 == 223 && titleG7 == 207 && titleB7 == 127 && titleR8 == 0 && titleG8 == 0 && titleB8 == 0) 
            { x_1 = "6"; } 
            if (titleR6 == 235 && titleG6 == 223 && titleB6 == 143 && titleR7 == 0 && titleG7 == 0 && titleB7 == 0 && titleR8 == 0 && titleG8 == 0 && titleB8 == 0) 
            { x_1 = "7"; }
            if (titleR6 == 235 && titleG6 == 223 && titleB6 == 143 && titleR7 == 223 && titleG7 == 207 && titleB7 == 127 && titleR8 == 0 && titleG8 == 0 && titleB8 == 0) 
            { x_1 = "8"; }
            if (titleR6 == 0 && titleG6 == 0 && titleB6 == 0 && titleR7 == 0 && titleG7 == 0 && titleB7 == 0 && titleR8 == 243 && titleG8 == 239 && titleB8 == 163) 
            { x_1 = "9"; }

            if (titleR9 == 0 && titleG9 == 0 && titleB9 == 0 && titleR10 == 91 && titleG10 == 53 && titleB10 == 12) 
            { x_10 = "0"; }
            if (titleR9 == 102 && titleG9 == 55 && titleB9 == 13 && titleR10 == 0 && titleG10 == 0 && titleB10 == 0) 
            { x_10 = "1"; }
            if (titleR9 == 0 && titleG9 == 0 && titleB9 == 0 && titleR10 == 195 && titleG10 == 155 && titleB10 == 79) 
            { x_10 = "2"; }
            if (titleR9 == 102 && titleG9 == 55 && titleB9 == 13 && titleR10 == 195 && titleG10 == 155 && titleB10 == 79) 
            { x_10 = "3"; }
            if (titleR9 == 102 && titleG9 == 55 && titleB9 == 13 && titleR10 == 91 && titleG10 == 53 && titleB10 == 12) 
            { x_10 = "4"; }


            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 0 && titleG2 == 0 && titleB2 == 0 && titleR3 == 117 && titleG3 == 70 && titleB3 == 21)
            { y_1 = "0"; }
            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 195 && titleG2 == 155 && titleB2 == 79 && titleR3 == 117 && titleG3 == 70 && titleB3 == 21)
            { y_1 = "1"; }
            if (titleR1 == 0 && titleG1 == 0 && titleB1 == 0 && titleR2 == 195 && titleG2 == 155 && titleB2 == 79 && titleR3 == 117 && titleG3 == 70 && titleB3 == 21)
            { y_1 = "2"; }
            if (titleR1 == 0 && titleG1 == 0 && titleB1 == 0 && titleR2 == 0 && titleG2 == 0 && titleB2 == 0 && titleR3 == 215 && titleG3 == 191 && titleB3 == 111)
            { y_1 = "3"; }
            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 195 && titleG2 == 155 && titleB2 == 79 && titleR3 == 215 && titleG3 == 191 && titleB3 == 111)
            { y_1 = "4"; }
            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 102 && titleG2 == 55 && titleB2 == 13 && titleR3 == 215 && titleG3 == 191 && titleB3 == 111)
            { y_1 = "5"; }
            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 0 && titleG2 == 0 && titleB2 == 0 && titleR3 == 215 && titleG3 == 191 && titleB3 == 111)
            { y_1 = "6"; }
            if (titleR1 == 0 && titleG1 == 0 && titleB1 == 0 && titleR2 == 102 && titleG2 == 55 && titleB2 == 13 && titleR3 == 215 && titleG3 == 191 && titleB3 == 111)
            { y_1 = "7"; }
            if (titleR1 == 0 && titleG1 == 0 && titleB1 == 0 && titleR2 == 0 && titleG2 == 0 && titleB2 == 0 && titleR3 == 0 && titleG3 == 0 && titleB3 == 0)
            { y_1 = "8"; }
            if (titleR1 == 91 && titleG1 == 53 && titleB1 == 12 && titleR2 == 102 && titleG2 == 55 && titleB2 == 13 && titleR3 == 0 && titleG3 == 0 && titleB3 == 0)
            { y_1 = "9"; }

            if (titleR4 == 0 && titleG4 == 0 && titleB4 == 0 && titleR5 == 102 && titleG5 == 55 && titleB5 == 13) { y_10 = "0"; }
            if (titleR4 == 0 && titleG4 == 0 && titleB4 == 0 && titleR5 == 0 && titleG5 == 0 && titleB5 == 0) { y_10 = "1"; }
            if (titleR4 == 255 && titleG4 == 255 && titleB4 == 183 && titleR5 == 195 && titleG5 == 155 && titleB5 == 79) { y_10 = "2"; }
            if (titleR4 == 102 && titleG4 == 55 && titleB4 == 13 && titleR5 == 195 && titleG5 == 155 && titleB5 == 79) { y_10 = "3"; }
            if (titleR4 == 102 && titleG4 == 55 && titleB4 == 13 && titleR5 == 102 && titleG5 == 55 && titleB5 == 13) { y_10 = "4"; }
            //Console.WriteLine($"{x_10}{x_1}");
            //Console.WriteLine($"{y_10}{y_1}");


            //Console.WriteLine($"if (titleR1 == {titleR1} && titleG1 == {titleG1} && titleB1 == {titleB1} && titleR2 == {titleR2} && titleG2 == {titleG2} && titleB2 == {titleB2} && titleR3 == {titleR3} && titleG3 == {titleG3} && titleB3 == {titleB3})");
            //Console.WriteLine($"if (titleR4 == {titleR4} && titleG4 == {titleG4} && titleB4 == {titleB4} && titleR5 == {titleR5} && titleG5 == {titleG5} && titleB5 == {titleB5}");

            res = "(" + x_10 + x_1 + "," + y_10 + y_1 + ")";

            return res;
        }


        public void attackPoison()
        {
            Thread.Sleep(50);

            SK.sendkeyEsc(5);

            //돌려
            for(int i = 0; i< 4; i++)
            {
                User32.API.keybd_event((byte)magic7, 0, 0, 0);
                Thread.Sleep(50);
                User32.API.keybd_event((byte)magic7, 0, 2, 0);
                SK.sendkeyRight(5);
                Thread.Sleep(10);
                SK.sendkeyEnter(5);
                SK.sendkeyEsc(5);
            }

            ////우
            //User32.API.keybd_event((byte)magic7, 0, 0, 0);
            //Thread.Sleep(50);
            //User32.API.keybd_event((byte)magic7, 0, 2, 0);
            //SK.sendkeyHome(5);
            //SK.sendkeyRight(5);
            //Thread.Sleep(10);
            //SK.sendkeyEnter(5);
            //SK.sendkeyEsc(5);

            ////좌
            //User32.API.keybd_event((byte)magic7, 0, 0, 0);
            //Thread.Sleep(50);
            //User32.API.keybd_event((byte)magic7, 0, 2, 0);
            //SK.sendkeyHome(5);
            //SK.sendkeyLeft(5);
            //Thread.Sleep(10);
            //SK.sendkeyEnter(5);
            //SK.sendkeyEsc(5);

            ////위
            //User32.API.keybd_event((byte)magic7, 0, 0, 0);
            //Thread.Sleep(50);
            //User32.API.keybd_event((byte)magic7, 0, 2, 0);
            //SK.sendkeyHome(5);
            //SK.sendkeyUp(5);
            //Thread.Sleep(10);
            //SK.sendkeyEnter(5);
            //SK.sendkeyEsc(5);

            ////아래
            //User32.API.keybd_event((byte)magic7, 0, 0, 0);
            //Thread.Sleep(50);
            //User32.API.keybd_event((byte)magic7, 0, 2, 0);
            //SK.sendkeyHome(5);
            //SK.sendkeyDown(5);
            //Thread.Sleep(10);
            //SK.sendkeyEnter(5);
            //SK.sendkeyEsc(5);

            Thread.Sleep(10);
            SK.sendkeyTab(10);
            SK.sendkeyHome(10);
            SK.sendkeyTab(10);
        }

        public void attackParalysis()
        {
            Thread.Sleep(100);

            SK.sendkeyEsc(5);

            //우
            User32.API.keybd_event((byte)magic11, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic11, 0, 2, 0);
            SK.sendkeyHome(5);
            SK.sendkeyRight(5);
            Thread.Sleep(10);
            SK.sendkeyEnter(5);
            SK.sendkeyEsc(5);

            //좌
            User32.API.keybd_event((byte)magic11, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic11, 0, 2, 0);
            SK.sendkeyHome(5);
            SK.sendkeyLeft(5);
            Thread.Sleep(10);
            SK.sendkeyEnter(5);
            SK.sendkeyEsc(5);

            //위
            User32.API.keybd_event((byte)magic11, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic11, 0, 2, 0);
            SK.sendkeyHome(5);
            SK.sendkeyUp(5);
            Thread.Sleep(10);
            SK.sendkeyEnter(5);
            SK.sendkeyEsc(5);

            //아래
            User32.API.keybd_event((byte)magic11, 0, 0, 0);
            Thread.Sleep(50);
            User32.API.keybd_event((byte)magic11, 0, 2, 0);
            SK.sendkeyHome(5);
            SK.sendkeyDown(5);
            Thread.Sleep(10);
            SK.sendkeyEnter(5);
            SK.sendkeyEsc(5);

            Thread.Sleep(10);
            SK.sendkeyTab(10);
            SK.sendkeyHome(10);
            SK.sendkeyTab(10);

        }

        public void paralysis()
        {
            lock (actionKeyLock)
            {
                if (!Macro.getInstance.Flag_END)
                    return;

                PauseMovementForAction();

                ReleaseCurse();
                ReleaseStationaryCombatKeys();

                try
                {
                    User32.API.BlockInput(true);
                    attackParalysis();
                }
                finally
                {
                    User32.API.BlockInput(false);
                }

                if (!Macro.getInstance.Flag_END)
                    return;

                int resumeMapNo = getMapNumber();
                string resumeXY = getMapXY();
                string resumeKey = resumeMapNo + ":" + resumeXY;

                if (movingOpt)
                {
                    string resumeMove = GetMoveDirectionForCurrentMode(resumeMapNo, resumeXY, resumeKey);
                    UpdateMove(resumeKey, resumeMove);
                }

                PressCurseIfRunning();
                SyncStationaryCombatKeys(resumeKey);
            }
        }

        public void poision()
        {
            lock (actionKeyLock)
            {
                if (!Macro.getInstance.Flag_END)
                    return;

                PauseMovementForAction();

                ReleaseCurse();
                ReleaseStationaryCombatKeys();

                try
                {
                    User32.API.BlockInput(true);
                    attackPoison();
                }
                finally
                {
                    User32.API.BlockInput(false);
                }

                if (!Macro.getInstance.Flag_END)
                    return;

                int resumeMapNo = getMapNumber();
                string resumeXY = getMapXY();
                string resumeKey = resumeMapNo + ":" + resumeXY;

                if (movingOpt)
                {
                    string resumeMove = GetMoveDirectionForCurrentMode(resumeMapNo, resumeXY, resumeKey);
                    UpdateMove(resumeKey, resumeMove);
                }

                PressCurseIfRunning();
                SyncStationaryCombatKeys(resumeKey);
            }
        }




        private class AutoExploreNode
        {
            public string Key { get; set; }
            public bool Visited { get; set; }
            public System.DateTime LastVisitedAt { get; set; }
            public Dictionary<string, AutoExploreEdge> Edges { get; set; }
        }

        private class AutoExploreEdge
        {
            public string Direction { get; set; }
            public string To { get; set; }
            public string Type { get; set; }
            public int SuccessCount { get; set; }
            public int FailCount { get; set; }
            public int ConsecutiveFailCount { get; set; }
            public int MonsterScore { get; set; }
            public int PortalScore { get; set; }
            public int ChooseCount { get; set; }
            public bool Blocked { get; set; }
            public System.DateTime LastFailedAt { get; set; }
            public System.DateTime LastSucceededAt { get; set; }
            public System.DateTime LastChosenAt { get; set; }
        }

        private class AutoExploreDirectionScore
        {
            public string Direction { get; set; }
            public int Score { get; set; }
        }


        public partial class YourClass
    {
        private enum MoveDir
        {
            None,
            East,   // 동 = 오른쪽
            West,   // 서 = 왼쪽
            South,  // 남 = 아래
            North   // 북 = 위
        }

        // 예: "1:(28,12)" -> "동"
        private readonly Dictionary<string, string> moveDictionary = new Dictionary<string, string>();

        // 몇 칸 앞을 미리 볼지
        private const int LOOK_AHEAD_COUNT = 3;

        // 미리 발견한 다음 정지/회전 지점
        private string reservedStopKey = null;
        private MoveDir reservedTurnDir = MoveDir.None;

        // 현재 진행 방향 (네 기존 코드의 방향 변수와 연결해서 쓰면 됨)
        private MoveDir currentMoveDir = MoveDir.None;

        // =========================================================
        // 메인 호출부
        // =========================================================
        // 좌표가 갱신될 때마다 이 함수를 호출
        private void AutoMoveStep(int mapNo, int x, int y)
        {
            string currentKey = MakeKey(mapNo, x, y);

            // 1) 이미 앞쪽 저장 좌표를 발견해서 정지 예약한 상태라면
            if (!string.IsNullOrEmpty(reservedStopKey))
            {
                // 정확히 예약 좌표에 도착하면
                if (currentKey == reservedStopKey)
                {
                    StopMove();

                    MoveDir nextDir = reservedTurnDir;

                    reservedStopKey = null;
                    reservedTurnDir = MoveDir.None;

                    if (nextDir != MoveDir.None)
                    {
                        currentMoveDir = nextDir;
                        StartMove(nextDir);
                    }

                    return;
                }

                // 예약 좌표를 이미 지나쳤다면 비상 정지
                if (HasPassedTarget(mapNo, x, y, currentMoveDir, reservedStopKey))
                {
                    StopMove();
                    reservedStopKey = null;
                    reservedTurnDir = MoveDir.None;
                    currentMoveDir = MoveDir.None;
                    return;
                }

                // 예약 정지 상태에서는 추가 입력하지 않음
                return;
            }

            // 2) 현재 진행 방향 앞쪽 1~3칸 미리 탐색
            if (TryFindSavedPointAhead(mapNo, x, y, currentMoveDir, out string foundKey, out MoveDir foundTurnDir))
            {
                // 앞쪽에 저장 좌표가 보이면 미리 멈춤
                reservedStopKey = foundKey;
                reservedTurnDir = foundTurnDir;

                StopMove();
                return;
            }

            // 3) 앞쪽에 예약 좌표가 없으면 계속 진행
            if (currentMoveDir != MoveDir.None)
            {
                KeepMoving(currentMoveDir);
            }
        }

        // =========================================================
        // 앞쪽 좌표 미리 탐색
        // =========================================================
        private bool TryFindSavedPointAhead(int mapNo, int x, int y, MoveDir dir, out string foundKey, out MoveDir foundTurnDir)
        {
            foundKey = null;
            foundTurnDir = MoveDir.None;

            if (dir == MoveDir.None)
                return false;

            int dx = 0;
            int dy = 0;

            switch (dir)
            {
                case MoveDir.East: dx = 1; break;
                case MoveDir.West: dx = -1; break;
                case MoveDir.South: dy = 1; break;
                case MoveDir.North: dy = -1; break;
            }

            for (int i = 1; i <= LOOK_AHEAD_COUNT; i++)
            {
                int nx = x + (dx * i);
                int ny = y + (dy * i);

                string checkKey = MakeKey(mapNo, nx, ny);

                if (moveDictionary.TryGetValue(checkKey, out string savedDirText))
                {
                    foundKey = checkKey;
                    foundTurnDir = ParseDirection(savedDirText);
                    return true;
                }
            }

            return false;
        }

        // =========================================================
        // 예약 좌표를 지나쳤는지 검사
        // =========================================================
        private bool HasPassedTarget(int currentMapNo, int currentX, int currentY, MoveDir dir, string targetKey)
        {
            if (!TryParseKey(targetKey, out int targetMapNo, out int targetX, out int targetY))
                return false;

            if (currentMapNo != targetMapNo)
                return false;

            switch (dir)
            {
                case MoveDir.East:
                    return currentY == targetY && currentX > targetX;

                case MoveDir.West:
                    return currentY == targetY && currentX < targetX;

                case MoveDir.South:
                    return currentX == targetX && currentY > targetY;

                case MoveDir.North:
                    return currentX == targetX && currentY < targetY;

                default:
                    return false;
            }
        }

        // =========================================================
        // 저장 키 생성 / 파싱
        // =========================================================
        // 예시 형식: 1:(25,09)
        private string MakeKey(int mapNo, int x, int y)
        {
            return $"{mapNo}:({x:00},{y:00})";
        }

        private bool TryParseKey(string key, out int mapNo, out int x, out int y)
        {
            mapNo = 0;
            x = 0;
            y = 0;

            try
            {
                int colonIndex = key.IndexOf(':');
                int openIndex = key.IndexOf('(');
                int commaIndex = key.IndexOf(',');
                int closeIndex = key.IndexOf(')');

                if (colonIndex < 0 || openIndex < 0 || commaIndex < 0 || closeIndex < 0)
                    return false;

                string mapText = key.Substring(0, colonIndex);
                string xText = key.Substring(openIndex + 1, commaIndex - openIndex - 1);
                string yText = key.Substring(commaIndex + 1, closeIndex - commaIndex - 1);

                mapNo = int.Parse(mapText);
                x = int.Parse(xText);
                y = int.Parse(yText);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // =========================================================
        // 방향 문자열 변환
        // =========================================================
        private MoveDir ParseDirection(string dirText)
        {
            if (string.IsNullOrWhiteSpace(dirText))
                return MoveDir.None;

            string v = dirText.Trim().ToUpper();

            switch (v)
            {
                case "동":
                case "E":
                case "EAST":
                case "RIGHT":
                    return MoveDir.East;

                case "서":
                case "W":
                case "WEST":
                case "LEFT":
                    return MoveDir.West;

                case "남":
                case "S":
                case "SOUTH":
                case "DOWN":
                    return MoveDir.South;

                case "북":
                case "N":
                case "NORTH":
                case "UP":
                    return MoveDir.North;

                default:
                    return MoveDir.None;
            }
        }

        // =========================================================
        // F4 저장 시 사용
        // last pressed 방향을 value로 저장
        // =========================================================
        private void SaveCurrentPoint(int mapNo, int x, int y, MoveDir lastPressedDir)
        {
            string key = MakeKey(mapNo, x, y);
            moveDictionary[key] = DirectionToText(lastPressedDir);
        }

        private string DirectionToText(MoveDir dir)
        {
            switch (dir)
            {
                case MoveDir.East: return "동";
                case MoveDir.West: return "서";
                case MoveDir.South: return "남";
                case MoveDir.North: return "북";
                default: return "";
            }
        }

        // =========================================================
        // 실제 키 입력 부분
        // 아래 4개는 네 기존 키 입력 함수로 바꿔서 쓰면 됨
        // =========================================================
        private void StartMove(MoveDir dir)
        {
            StopMove();

            switch (dir)
            {
                case MoveDir.East:
                    KeyDownRight();
                    break;
                case MoveDir.West:
                    KeyDownLeft();
                    break;
                case MoveDir.South:
                    KeyDownDown();
                    break;
                case MoveDir.North:
                    KeyDownUp();
                    break;
            }
        }

        private void KeepMoving(MoveDir dir)
        {
            // 이미 누르고 있는 구조면 비워둬도 됨
            // 필요하면 방향 유지 입력 로직 넣기
        }

        private void StopMove()
        {
            KeyUpLeft();
            KeyUpRight();
            KeyUpUp();
            KeyUpDown();
        }

        // =========================================================
        // 네 기존 키 입력 함수로 교체
        // =========================================================
        private void KeyDownLeft() { /* 기존 왼쪽 KeyDown */ }
        private void KeyDownRight() { /* 기존 오른쪽 KeyDown */ }
        private void KeyDownUp() { /* 기존 위 KeyDown */ }
        private void KeyDownDown() { /* 기존 아래 KeyDown */ }

        private void KeyUpLeft() { /* 기존 왼쪽 KeyUp */ }
        private void KeyUpRight() { /* 기존 오른쪽 KeyUp */ }
        private void KeyUpUp() { /* 기존 위 KeyUp */ }
        private void KeyUpDown() { /* 기존 아래 KeyUp */ }
    }

    public void melody1()
        {
            // tempo base
            int q = 200;   // quarter
            int e = q / 2; // eighth
            int h = q * 2; // half

            // Super Mario-style main theme intro
            Console.Beep(659, e); Thread.Sleep(e / 2);
            Console.Beep(659, e); Thread.Sleep(q);
            Console.Beep(659, e); Thread.Sleep(q);
            Console.Beep(523, e); Thread.Sleep(e / 2);
            Console.Beep(659, e); Thread.Sleep(q);
            Console.Beep(784, q); Thread.Sleep(h);
            Console.Beep(392, q); Thread.Sleep(h);

            Console.Beep(523, q); Thread.Sleep(q);
            Console.Beep(392, q); Thread.Sleep(q / 2);
            Console.Beep(330, q); Thread.Sleep(q);
           
            Console.Beep(440, e); Thread.Sleep(q);
            Console.Beep(494, e); Thread.Sleep(e);
            Console.Beep(466, e); Thread.Sleep(e / 2);
            Console.Beep(440, e); Thread.Sleep(q);
            Console.Beep(392, e); Thread.Sleep(e / 2);
            Console.Beep(659, e); Thread.Sleep(e / 2);
            Console.Beep(784, e); Thread.Sleep(e);
            Console.Beep(880, q); Thread.Sleep(q / 2);
            Console.Beep(698, e); Thread.Sleep(e);
            Console.Beep(784, e); Thread.Sleep(e);
            Console.Beep(659, e); Thread.Sleep(e / 2);
            Console.Beep(523, e); Thread.Sleep(q);
            Console.Beep(587, e); Thread.Sleep(e / 4);
            Console.Beep(494, q); Thread.Sleep(q);

            Console.Beep(523, q); Thread.Sleep(q);
            Console.Beep(392, q); Thread.Sleep(q / 2);
            Console.Beep(330, q); Thread.Sleep(q);

            Console.Beep(440, e); Thread.Sleep(q);
            Console.Beep(494, e); Thread.Sleep(e);
            Console.Beep(466, e); Thread.Sleep(e / 2);
            Console.Beep(440, e); Thread.Sleep(q);
            Console.Beep(392, e); Thread.Sleep(e / 2);
            Console.Beep(659, e); Thread.Sleep(e / 2);
            Console.Beep(784, e); Thread.Sleep(e);
            Console.Beep(880, q); Thread.Sleep(q / 2);
            Console.Beep(698, e); Thread.Sleep(e);
            Console.Beep(784, e); Thread.Sleep(e);
            Console.Beep(659, e); Thread.Sleep(e / 2);
            Console.Beep(523, e); Thread.Sleep(q);
            Console.Beep(587, e); Thread.Sleep(e / 4);
            Console.Beep(494, q); Thread.Sleep(h);
        }


        public void deleteKey()
        {
            int res1 = getMapNumber();
            string res2 = getMapXY();
            string key = "" + res1 + ":" + res2;
            string routePath = GetRouteConfigPathForMapNo(res1);

            bool result1 = DeleteStringKeyValue(routePath, key);


            synth.Rate = FastSayRate;
            SayText(res1 + "굴  " + res2.Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", " ") + " 삭제");
            synth.Rate = SayRate;

        }

        public void SaveDownRouteKey()
        {
            int res1 = getMapNumber();
            string res2 = getMapXY();
            string key = "" + res1 + ":" + res2;

            string tempDirection = "";

            if (!string.IsNullOrEmpty(lastManualMove))
            {
                tempDirection = lastManualMove;
            }
            else if (!string.IsNullOrEmpty(lastMove))
            {
                tempDirection = lastMove;
            }
            else
            {
                return;
            }

            string routePath = res1 == GwallyeongCastleMapNo
                ? castleConfigPath
                : GetDownConfigPathForSelectedMap();

            SaveStringKeyValue(routePath, key, tempDirection);

            synth.Rate = FastSayRate;
            SayText(res1 + "굴 " + tempDirection + "쪽 하행 Save");
            synth.Rate = SayRate;
        }




        public void hi()
        {
            int res1 = getMapNumber();
            string res2 = getMapXY();
            string key = "" + res1 + ":" + res2;

            //Console.WriteLine("SAVE TRY : " + key);

            string tempDirection = "";

            // 1순위: 최근 수동 방향키
            if (!string.IsNullOrEmpty(lastManualMove))
            {
                tempDirection = lastManualMove;
            }
            // 2순위: 자동이동 중 마지막 방향
            else if (!string.IsNullOrEmpty(lastMove))
            {
                tempDirection = lastMove;
            }
            else
            {
                //Console.WriteLine("SAVE FAIL : direction empty");
                return;
            }

            if (directionDic == null)
            {
                directionDic = new Dictionary<string, string>();
            }

            string routePath = GetRouteConfigPathForMapNo(res1);

            if (res1 == GwallyeongCastleMapNo)
            {
                if (castleDirectionDic == null)
                    castleDirectionDic = new Dictionary<string, string>();

                castleDirectionDic[key] = tempDirection;
            }
            else
            {
                directionDic[key] = tempDirection;
            }

            SaveStringKeyValue(routePath, key, tempDirection);


            if (res1 == GwallyeongCastleMapNo && File.Exists(routePath))
            {
                castleConfigLastWriteTime = File.GetLastWriteTime(routePath);
            }
            else if (File.Exists(routePath))
            {
                moveConfigLoadedPath = routePath;
                moveConfigLastWriteTime = File.GetLastWriteTime(routePath);
            }

            RefreshActiveManualRouteAfterF4Save();

            //Console.WriteLine("SAVE OK : " + key + " -> " + tempDirection);

            //string tempStr = res1 + "굴  " + res2.Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", " ") + " " + tempDirection + "쪽" + " Save";
            string tempStr = res1 + "굴 " + tempDirection + "쪽" + " Save";

            //Console.WriteLine(tempStr);

            synth.Rate = FastSayRate;
            SayText(tempStr);
            synth.Rate = SayRate;
            // 저장 즉시 현재 메모리 기준으로도 반영
            curMove = tempDirection;
        }

        private string GetRouteConfigPathForMapNo(int mapNo)
        {
            if (mapNo == GwallyeongCastleMapNo)
                return castleConfigPath;

            if (mapNo == GwallyeongHauntedEntranceMapNo)
                return moveConfigPath;

            return moveConfigPath;
        }


        static void SaveDirectionDic(string filePath, Dictionary<string, string> directionDic)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string json = JsonConvert.SerializeObject(directionDic, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


        private static void hi2()
        {
            Point p = getMousePosAndColor();
            Color curColor = GetColorAt(p.X, p.Y);

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

            string newString = "XY(" + p.X + "," + p.Y + ")" + "\tRGB(" + curColor.R + "," + curColor.G + "," + curColor.B + ")";
            Console.WriteLine($"{newString}");
            
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                //MainWindow.getInstance.ViewModel.StateString = newString;
            }));

            int x1_hell = 32;
            int y1_hell = 8;

            int x2_hell = 26;
            int y2_hell = 37;

            //좌표 설정
            int x0 = 1703;
            int y0 = 921;


            int x4 = 32;
            int y4 = 8;

            x0 = 32;
            y0 = 8;


            x4 = 26;
            y4 = 37;

            //좌표 색 가져오기
            Color curColor0 = GetColorAt(x0, y0);
            Color curColor4 = GetColorAt(x4, y4);

            R0 = curColor0.R;
            G0 = curColor0.G;
            B0 = curColor0.B;

            R4 = curColor4.R;
            G4 = curColor4.G;
            B4 = curColor4.B;

            //(UseHellfire && !(R4 == 255 && G4 == 255 && B4 == 255) && !(R0 == 8 && G0 == 4 && B0 == 8))
            bool rgb4 = !(R4 == 255 && G4 == 255 && B4 == 255);
            bool rgb0 = !(R0 == 255 && G0 == 255 && B0 == 255);


            //Console.WriteLine($"UseHellfire : {Macro.UseHellfire}");
            //Console.WriteLine($"RGB4({R4},{G4},{B4}) : {rgb4}");
            //Console.WriteLine($"RGB0({R0},{G0},{B0}) : {rgb0}");
        }

        private static void hi3(int x, int y)
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
        public static void SaveStringKeyValue(string filePath, string key, string value)
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

            string directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string updatedJson = JsonConvert.SerializeObject(dict, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
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
        public static bool DeleteStringKeyValue(string filePath, string key)
        {
            if (!File.Exists(filePath))
                return false;

            string json = File.ReadAllText(filePath);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();

            bool removed = dict.Remove(key);

            if (!removed)
                return false;

            string updatedJson = JsonConvert.SerializeObject(dict, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);

            return true;
        }

        static string GetValue(string filePath, string key)
        {
            if (directionDic == null)
                return null;

            return directionDic.TryGetValue(key, out var value) ? value : null;
        }

        private void AltAndDelete()
        {
            SK.sendkeyAlt(15);
            SK.sendkeyDelete(10);
        }


        private void ReleaseCurse()
        {
            if (UseCurseOption)
            {
                User32.API.keybd_event((byte)magic4, 0, 2, 0);
            }
        }

        private void ReleasePickup()
        {
            User32.API.keybd_event(0xBC, 0, 2, 0);
            pickupKeyPressed = false;
        }
        private void ReleaseChumChum()
        {
            if (UseExtraCombo)
            {
                User32.API.keybd_event((byte)magic5, 0, 2, 0);
                User32.API.keybd_event((byte)magic6, 0, 2, 0);
            }
        }

        private void PressCurseIfRunning()
        {
            if (!Macro.getInstance.Flag_END)
                return;

            if (UseCurseOption)
            {
                User32.API.keybd_event((byte)magic4, 0, 0, 0);
            }
        }
        private void PressChumChumIfRunning()
        {
            if (!Macro.getInstance.Flag_END)
                return;

            if (UseExtraCombo)
            {
                User32.API.keybd_event((byte)magic5, 0, 0, 0);
                User32.API.keybd_event((byte)magic6, 0, 0, 0);
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
