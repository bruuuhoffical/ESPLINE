using System.Media;
using System.Reflection;

namespace AotForms
{
    internal static class Config
    {
        internal static bool AimBot = false;
        internal static bool AimBotLeft = false;
        internal static float aimlegit = 0.05f;
        internal static int Aimbotype = 0;
        internal static int smooth = 0;
        internal static int LagFix = 0;

        internal static Keys AimbotKey = Keys.LButton;
        internal static Keys AimbotKey2 = Keys.None;

        internal static int AimBotMaxDistance = 100;
        internal static int expsize = 8;
        internal static int Aimfov = 50;
        internal static int espran = 100;
        internal static int aimdelay = 0;

        internal static bool IgnoreKnocked = false;
        internal static bool Speed = false;
        internal static bool NoRecoil = false;
        internal static bool MagicBullet = false;
        internal static bool NoCache = false;
        internal static bool aimIsVisible = true;
        internal static bool StreamMode = false;
        internal static bool esptotalplyer = false;
        internal static bool FixEsp = false;

        internal static bool ESPLine = false;
        internal static Color ESPLineColor = Color.White;

        internal static bool ESPBox = false;
        internal static Color ESPBoxColor = Color.White;
        internal static bool ESPDistance = false;
        internal static bool ESPBox2 = false;
        internal static Color ESPFillBoxColor = Color.White;
        internal static bool ESPName = false;
        internal static Color ESPNameColor = Color.White;

        internal static bool ESPHealth = false;
        internal static Color ESPHeath = Color.White;

        internal static bool ESPSkeleton = false;
        internal static Color ESPSkeletonColor = Color.White;
        internal static Color NameCheat = Color.FromArgb(192,0,192);
        internal static bool ESPFillBox = false;
        public static Keys AimBotKeys = Keys.None;
        internal static bool ESPCorner = false;
        internal static bool ESPCornerColor = false;

        internal static bool ESPInfo = false;

        internal static bool ESPFove = false;
        internal static bool espbg = false;
        internal static bool Aimfovc = false;
        internal static Color Aimfovcolor = Color.White;

        internal static bool espcfx = false;
        internal static bool sound = false;

        internal static string linePosition = "Bottom";

        public static void Notif()
        {
            if (!sound)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Client.clicksound.wav");

                if (stream != null)
                {
                    using (SoundPlayer player = new SoundPlayer(stream))
                    {
                        player.Play();
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
        }
    }
}
