namespace Biden.ViewModel
{
    public class MainSettings
    {
        public bool IsMainEnabled { get; set; }
        public bool UseHellfire { get; set; }
        public int HellfireDirectionIndex { get; set; }
        public bool UseNormalAttack { get; set; }
        public bool UsePoison { get; set; }
        public bool UsePickup { get; set; }
        public bool UseCaptchaAlert { get; set; }
        public bool UseHeal { get; set; }
        public bool UseProtectArmor { get; set; }
        
        public bool UseCurseOption { get; set; }
        public bool UseBuffCombo { get; set; }
        public bool UseExtraCombo { get; set; }
        public bool UseShout { get; set; }
        public int ShoutCooldown { get; set; }
        public string ShoutMessage { get; set; }
        public bool UseChatDetectAlt2 { get; set; }
        public int ChatDetectAlt2Cooldown { get; set; }
        


        public string MagicNoHellfire { get; set; }
        public string MagicNoBuff { get; set; }
        public string MagicNoHeal { get; set; }
        public string MagicNoCurse { get; set; }
        public string MagicNoExtra1 { get; set; }
        public string MagicNoExtra2 { get; set; }
        public string MagicNoPoison { get; set; }
        public string MagicNoParalyze { get; set; }
        public string MagicNoProtect { get; set; }
        public string MagicNoArmor { get; set; }



        public int MoveDelay { get; set; }
    }
}