using Biden.Func;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media;

namespace Biden.ViewModel
{
    public class ViewModelMain : INotifyPropertyChanged
    {
        private const string SettingsFileName = "settings.json";

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLoadingSettings;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public ViewModelMain()
        {
            CmdBtn01 = new RelayCommand(OnBtn01);
            CmdOnOffBtn01 = new RelayCommand(OnMainToggle);
            CmdSaveSettings = new RelayCommand(OnSaveSettings);
            CmdLoadSettings = new RelayCommand(OnLoadSettings);

            Text01 = "실행";
            ColorBtn01 = Brushes.LightSkyBlue;
            Num = "0";
            Num2 = "0";
            StateString = "초기화 중";

            LoadSettingsOrDefault();
        }


        private bool _isMainEnabled;
        //public bool IsMainEnabled
        //{
        //    get => _isMainEnabled;
        //    set => SetProperty(ref _isMainEnabled, value);
        //}

        public bool IsMainEnabled
        {
            get => _isMainEnabled;
            set
            {
                if (SetProperty(ref _isMainEnabled, value))
                {
                    StateString = value ? "메인 기능 ON" : "메인 기능 OFF";

                    if (_isLoadingSettings)
                        return;

                    if (value)
                    {
                        var macro = Macro.getInstance;
                        macro.create();
                        macro.start();
                    }
                    else
                    {
                        Macro.destroy();
                    }
                }
            }
        }

        private void SayHello()
        {
            StateString = "sayhello() 실행됨";
            // 실제 실행할 함수 내용
        }



        // =========================
        // 기존 UI용
        // =========================

        private string _text01;
        public string Text01
        {
            get => _text01;
            set => SetProperty(ref _text01, value);
        }

        private Brush _colorBtn01;
        public Brush ColorBtn01
        {
            get => _colorBtn01;
            set => SetProperty(ref _colorBtn01, value);
        }

        private string _num;
        public string Num
        {
            get => _num;
            set => SetProperty(ref _num, value);
        }

        private string _num2;
        public string Num2
        {
            get => _num2;
            set => SetProperty(ref _num2, value);
        }

        private string _stateString;
        public string StateString
        {
            get => _stateString;
            set => SetProperty(ref _stateString, value);
        }

        private bool _isChecked01;
        public bool IsChecked01
        {
            get => _isChecked01;
            set => SetProperty(ref _isChecked01, value);
        }


        // =========================
        // 전투 옵션
        // =========================

        private bool _useHellfire;
        public bool UseHellfire
        {
            get => _useHellfire;
            set => SetProperty(ref _useHellfire, value);
        }

        private int _hellfireDirectionIndex;
        public int HellfireDirectionIndex
        {
            get => _hellfireDirectionIndex;
            set => SetProperty(ref _hellfireDirectionIndex, value);
        }

        private bool _useNormalAttack;
        public bool UseNormalAttack
        {
            get => _useNormalAttack;
            set => SetProperty(ref _useNormalAttack, value);
        }

        private bool _usePoison;
        public bool UsePoison
        {
            get => _usePoison;
            set => SetProperty(ref _usePoison, value);
        }

        private bool _usePickup;
        public bool UsePickup
        {
            get => _usePickup;
            set => SetProperty(ref _usePickup, value);
        }

        private bool _useCaptchaAlert;
        public bool UseCaptchaAlert
        {
            get => _useCaptchaAlert;
            set => SetProperty(ref _useCaptchaAlert, value);
        }
        private bool _useProtectArmor;
        public bool UseProtectArmor
        {
            get => _useProtectArmor;
            set => SetProperty(ref _useProtectArmor, value);
        }

        // =========================
        // 마법 번호
        // =========================

        private string _magicNoHellfire;
        public string MagicNoHellfire
        {
            get => _magicNoHellfire;
            set => SetProperty(ref _magicNoHellfire, value);
        }

        private string _magicNoBuff;
        public string MagicNoBuff
        {
            get => _magicNoBuff;
            set => SetProperty(ref _magicNoBuff, value);
        }

        private string _magicNoHeal;
        public string MagicNoHeal
        {
            get => _magicNoHeal;
            set => SetProperty(ref _magicNoHeal, value);
        }

        private string _magicNoCurse;
        public string MagicNoCurse
        {
            get => _magicNoCurse;
            set => SetProperty(ref _magicNoCurse, value);
        }

        private string _magicNoExtra1;
        public string MagicNoExtra1
        {
            get => _magicNoExtra1;
            set => SetProperty(ref _magicNoExtra1, value);
        }

        private string _magicNoExtra2;
        public string MagicNoExtra2
        {
            get => _magicNoExtra2;
            set => SetProperty(ref _magicNoExtra2, value);
        }

        private string _magicNoPoison;
        public string MagicNoPoison
        {
            get => _magicNoPoison;
            set => SetProperty(ref _magicNoPoison, value);
        }

        private string _magicNoParalyze;
        public string MagicNoParalyze
        {
            get => _magicNoParalyze;
            set => SetProperty(ref _magicNoParalyze, value);
        }

        private string _magicNoProtect;
        public string MagicNoProtect
        {
            get => _magicNoProtect;
            set => SetProperty(ref _magicNoProtect, value);
        }

        private string _magicNoArmor;
        public string MagicNoArmor
        {
            get => _magicNoArmor;
            set => SetProperty(ref _magicNoArmor, value);
        }




        private bool _useHeal;
        public bool UseHeal
        {
            get => _useHeal;
            set => SetProperty(ref _useHeal, value);
        }

        private bool _useBuffCombo;
        public bool UseBuffCombo
        {
            get => _useBuffCombo;
            set => SetProperty(ref _useBuffCombo, value);
        }

        private bool _useExtraCombo;
        public bool UseExtraCombo
        {
            get => _useExtraCombo;
            set => SetProperty(ref _useExtraCombo, value);
        }

        private bool _useShout;
        public bool UseShout
        {
            get => _useShout;
            set => SetProperty(ref _useShout, value);
        }

        private int _shoutCooldown;
        public int ShoutCooldown
        {
            get => _shoutCooldown;
            set
            {
                int newValue = value;
                if (newValue < 0) newValue = 0;
                SetProperty(ref _shoutCooldown, newValue);
            }
        }

        private string _shoutMessage;
        public string ShoutMessage
        {
            get => _shoutMessage;
            set => SetProperty(ref _shoutMessage, value);
        }

        private bool _useChatDetectAlt2;
        public bool UseChatDetectAlt2
        {
            get => _useChatDetectAlt2;
            set => SetProperty(ref _useChatDetectAlt2, value);
        }

        private int _chatDetectAlt2Cooldown;
        public int ChatDetectAlt2Cooldown
        {
            get => _chatDetectAlt2Cooldown;
            set
            {
                int newValue = value;
                if (newValue < 0) newValue = 0;
                SetProperty(ref _chatDetectAlt2Cooldown, newValue);
            }
        }

        private bool _useCurseOption;
        public bool UseCurseOption
        {
            get => _useCurseOption;
            set => SetProperty(ref _useCurseOption, value);
        }


        // =========================
        // 기타 설정
        // =========================

        private int _moveDelay;
        public int MoveDelay
        {
            get => _moveDelay;
            set
            {
                int newValue = value;
                if (newValue < 0) newValue = 0;
                if (newValue > 2000) newValue = 2000;

                SetProperty(ref _moveDelay, newValue);
            }
        }

        // =========================
        // Commands
        // =========================

        public ICommand CmdBtn01 { get; }
        public ICommand CmdOnOffBtn01 { get; }
        public ICommand CmdSaveSettings { get; }
        public ICommand CmdLoadSettings { get; }

        private void OnBtn01(object obj)
        {
            StateString = "실행 버튼 클릭";
        }

        private void OnMainToggle(object obj)
        {
            IsChecked01 = !IsChecked01;
            StateString = IsChecked01 ? "스위치 ON" : "스위치 OFF";
        }

        private void OnSaveSettings(object obj)
        {
            SaveSettings();
        }

        private void OnLoadSettings(object obj)
        {
            LoadSettingsOrDefault();
        }

        // =========================
        // 설정 로드/저장
        // =========================

        private void LoadSettingsOrDefault()
        {
            try
            {
                if (File.Exists(SettingsFileName))
                {
                    string json = File.ReadAllText(SettingsFileName);
                    MainSettings loaded = System.Text.Json.JsonSerializer.Deserialize<MainSettings>(json);

                    if (loaded != null)
                    {
                        ApplySettings(loaded);
                        StateString = "settings.json 불러오기 완료";
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                // 실패 시 기본값 사용
            }

            ApplyDefaultSettings();
            StateString = "기본값 적용";
        }

        private void SaveSettings()
        {
            try
            {
                MainSettings settings = GetSettings();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = System.Text.Json.JsonSerializer.Serialize(settings, options);
                File.WriteAllText(SettingsFileName, json);

                StateString = "settings.json 저장 완료";
            }
            catch (Exception ex)
            {
                StateString = $"저장 실패: {ex.Message}";
            }
        }

        private void ApplyDefaultSettings()
        {
            _isLoadingSettings = true;
            try
            {
                IsMainEnabled = false;
                IsChecked01 = false;

                UseHellfire = false;
                HellfireDirectionIndex = 0;
                UseNormalAttack = false;
                UsePoison = false;
                UsePickup = true;
                UseCaptchaAlert = true;

                UseHeal = true;
                UseProtectArmor = true;
                UseCurseOption = true;
                UseBuffCombo = true;
                UseExtraCombo = true;

                UseShout = true;
                ShoutCooldown = 60;
                ShoutMessage = "9선녀중";

                UseChatDetectAlt2 = true;
                ChatDetectAlt2Cooldown = 60;


                MagicNoHellfire = "1";
                MagicNoBuff = "2";
                MagicNoHeal = "3";
                MagicNoCurse = "4";
                MagicNoExtra1 = "5";
                MagicNoExtra2 = "6";
                MagicNoPoison = "7";
                MagicNoParalyze = "8";
                MagicNoProtect = "9";
                MagicNoArmor = "0";

                MoveDelay = 200;
            }
            finally
            {
                _isLoadingSettings = false;
            }
        }

        public MainSettings GetSettings()
        {
            return new MainSettings
            {
                IsMainEnabled = false,
                UseHellfire = UseHellfire,
                HellfireDirectionIndex = HellfireDirectionIndex,
                UseNormalAttack = UseNormalAttack,
                UsePoison = UsePoison,
                UsePickup = UsePickup,
                UseCaptchaAlert = UseCaptchaAlert,

                UseHeal = UseHeal,
                UseProtectArmor = UseProtectArmor,
                UseCurseOption = UseCurseOption,
                UseBuffCombo = UseBuffCombo,
                UseExtraCombo = UseExtraCombo,

                UseShout = UseShout,
                ShoutCooldown = ShoutCooldown,
                ShoutMessage = ShoutMessage,

                UseChatDetectAlt2 = UseChatDetectAlt2,
                ChatDetectAlt2Cooldown = ChatDetectAlt2Cooldown,


                MagicNoHellfire = MagicNoHellfire,
                MagicNoBuff = MagicNoBuff,
                MagicNoHeal = MagicNoHeal,
                MagicNoCurse = MagicNoCurse,
                MagicNoExtra1 = MagicNoExtra1,
                MagicNoExtra2 = MagicNoExtra2,
                MagicNoPoison = MagicNoPoison,
                MagicNoParalyze = MagicNoParalyze,
                MagicNoProtect = MagicNoProtect,
                MagicNoArmor = MagicNoArmor,

                MoveDelay = MoveDelay
            };
        }

        public void ApplySettings(MainSettings settings)
        {
            if (settings == null)
                return;

            IsMainEnabled = settings.IsMainEnabled;

            UseHellfire = settings.UseHellfire;
            HellfireDirectionIndex = settings.HellfireDirectionIndex;
            UseNormalAttack = settings.UseNormalAttack;
            UsePickup = settings.UsePickup;
            UseCaptchaAlert = settings.UseCaptchaAlert;

            UseHeal = settings.UseHeal;
            UseProtectArmor = settings.UseProtectArmor;
            UseBuffCombo = settings.UseBuffCombo;
            UseCurseOption = settings.UseCurseOption;
            UseExtraCombo = settings.UseExtraCombo;
            UsePoison = settings.UsePoison;

            UseShout = settings.UseShout;
            ShoutCooldown = settings.ShoutCooldown;
            ShoutMessage = settings.ShoutMessage;

            UseChatDetectAlt2 = settings.UseChatDetectAlt2;
            ChatDetectAlt2Cooldown = settings.ChatDetectAlt2Cooldown;

            MagicNoHellfire = settings.MagicNoHellfire;
            MagicNoBuff = settings.MagicNoBuff;
            MagicNoHeal = settings.MagicNoHeal;
            MagicNoCurse = settings.MagicNoCurse;
            MagicNoExtra1 = settings.MagicNoExtra1;
            MagicNoExtra2 = settings.MagicNoExtra2;
            MagicNoPoison = settings.MagicNoPoison;
            MagicNoParalyze = settings.MagicNoParalyze;
            MagicNoProtect = settings.MagicNoProtect;
            MagicNoArmor = settings.MagicNoArmor;

            MoveDelay = settings.MoveDelay;
        }
    }
}