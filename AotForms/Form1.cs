using System.Drawing;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using AimBotConquer.Guna;
using Guna.UI2.WinForms;
using LuciferMem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AotForms
{
    public partial class Form1 : Form
    {
        private int targetWidth1, targetWidth2, targetWidth3;  // Target width for each slider
        private const int minWidth = 5;  // Minimum width for the slider panel
        private const int maxValueFov = 360; // Maximum value for the sliders
        private const int maxValueSmooth = 100; // Maximum value for the sliders
        private const int maxValueRange = 500; // Maximum value for the sliders
        private Mem Lucifer = new Mem();
        public static Label StatusLbl;
        IntPtr mainHandle;
        private Image logoImage;
        public Form1(IntPtr handle)
        {

            mainHandle = handle;
            InitializeComponent();


            // Initialize the timer for animation for Slider 1


            label11.BackColor = Color.Transparent;
            label12.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label14.BackColor = Color.Transparent;
            label15.BackColor = Color.Transparent;
            label16.BackColor = Color.Transparent;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            label11.BackColor = Color.Transparent;
            label12.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label14.BackColor = Color.Transparent;
            label15.BackColor = Color.Transparent;
            label16.BackColor = Color.Transparent;
            // Update Config.linePosition to reflect the default selection



        }
        public void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                process.StandardInput.WriteLine(command);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
            }
        }




        static IntPtr FindRenderWindow(IntPtr parent)
        {
            IntPtr renderWindow = IntPtr.Zero;
            WinAPI.EnumChildWindows(parent, (hWnd, lParam) =>
            {
                StringBuilder sb = new StringBuilder(256);
                WinAPI.GetWindowText(hWnd, sb, sb.Capacity);
                string windowName = sb.ToString();
                if (!string.IsNullOrEmpty(windowName))
                {
                    if (windowName != "HD-Player")
                    {
                        renderWindow = hWnd;
                    }
                }
                return true;
            }, IntPtr.Zero);

            return renderWindow;
        }

        private void checkAimBot_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkNoRecoil_CheckedChanged(object sender, EventArgs e)
        {
        }



        private void checkESPBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEspName_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void checkESPHealth_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void picColorLine_Click(object sender, EventArgs e)
        {
            //var picker = new ColorDialog();
            //var result = picker.ShowDialog();

            //if (result == DialogResult.OK)
            //{
            //    picColorLine.BackColor = picker.Color;
            //    Config.ESPLineColor = picker.Color;
            //}
        }

        private void picColorBox_Click(object sender, EventArgs e)
        {
            //var picker = new ColorDialog();
            //var result = picker.ShowDialog();

            //if (result == DialogResult.OK)
            //{
            //    picColorBox.BackColor = picker.Color;
            //    Config.ESPBoxColor = picker.Color;
            //}
        }

        private void picColorName_Click(object sender, EventArgs e)
        {
            //var picker = new ColorDialog();
            //var result = picker.ShowDialog();

            //if (result == DialogResult.OK)
            //{
            //    picColorName.BackColor = picker.Color;
            //    Config.ESPNameColor = picker.Color;
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void comboKeys_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackAimBotDistance_Scroll(object sender, EventArgs e)
        {
            //var distance = trackAimBotDistance.Value;

            //lblDistance.Text = $"Distance ({distance})";

            //Config.AimBotMaxDistance = distance;
        }

        private void checkIgnoreKnocked_CheckedChanged(object sender, EventArgs e)
        {
            Config.IgnoreKnocked = checkIgnoreKnockedd.Checked;
        }



        //private async void btnAntiCheat_Click(object sender, EventArgs e) {
        //    string search = "01 00 00 E2 0B D0 A0 E1 00 88 BD E8 50 CF 19 00 00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 08 C0 9B E5 48 E3 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 2C 00 0B E5 30 10 0B E5 34 20 0B E5 38 30 0B E5 2C 00 1B E5 00 10 00 E3 3C 10 0B E5 0C 10 0B E5 40 10 0B E5 10 10 4B E2 30 00 8D E5 01 00 A0 E1 2C C0 8D E5 A4 0C 00 EB 30 10 1B E5 00 20 00 E3 02 00 51 E1 04 00 00 1A 00 00 00 E3 28 00 0B E5 01 00 00 E3 44 00 8D E5 A5 00 00 EA 00 00 A0 E3 06 00 4B E5 6A 0A 07 E3 B8 00 4B E1 08 00 4B E2 02 10 A0 E3 18 20 A0 E3 34 0E FD EB FF FF FF EA 30 00 1B E5 08 10 4B E2 1A AE FA EB 28 00 8D E5 FF FF FF EA 28 00 9D E5 38 00 8D E5 38 10 9D E5 00 20 00 E3 02 00 51 E1 2F 00 00 1A 20 00 4B E2 06 10 A0 E3 38 8D FE EB FF FF FF EA 30 10 1B E5 20 00 4B E2 BF 8D FE EB FF FF FF EA 20 00 4B E2 08 10 4B E2 BB 8D FE EB FF FF FF EA EE AC FA EB 00 00 90 E5 EF AC FA EB 24 00 8D E5 FF FF FF EA 20 00 4B E2 24 10 9D E5 B2 8D FE EB FF FF FF EA 10 91 FE EB 20 00 8D E5 FF FF FF EA 20 00 9D E5 00 10 90 E5 08 10 91 E5 20 20 4B E2 1C 10 8D E5 02 10 A0 E1 1C 20 9D E5 32 FF 2F E1 FF FF FF EA 00 00 00 E3 28 00 0B E5 01 00 00 E3 44 00 8D E5";
        //    string replace = "00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1 00 00 A0 E3 1E FF 2F E1";

        //    var memory = new Mem();
        //    memory.OpenProcess("HD-Player");

        //    lblStatus.Text = "Aplicando";

        //    bool k = false;
        //    int i2 = 22000000;
        //    IEnumerable<long> wl = await memory.AoBScan(search, writable: true);
        //    string u = "0x" + wl.FirstOrDefault().ToString("X");
        //    if (wl.Count() != 0) {
        //        for (int i = 0; i < wl.Count(); i++) {
        //            i2++;
        //            memory.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
        //        }
        //        k = true;
        //    }

        //    if (k) {
        //        lblStatus.Text = "Aplicado AC.";
        //    } else {
        //        lblStatus.Text = "Error";
        //    }
        //}

        private async void btnAntiCheat_Click(object sender, EventArgs e)
        {

        }


        private void checkMagicBullet_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkSpeed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //Offsets.Speed = Convert.ToUInt32(textSpeedOffset.Text, 16);
        }

        private async void guna2CustomCheckBox11_Click(object sender, EventArgs e)
        {

        }

        private void checkAimBot_Click(object sender, EventArgs e)
        {
            //Config.AimBot = checkAimBot.Checked;
        }

        private void checkNoRecoil_Click(object sender, EventArgs e)
        {
            //Config.NoRecoil = checkNoRecoil.Checked;
        }

        private void checkIgnoreKnocked_Click(object sender, EventArgs e)
        {
            //Config.IgnoreKnocked = checkIgnoreKnocked.Checked;
        }

        private void checkESPLines_Click(object sender, EventArgs e)
        {
            //Config.ESPLine = checkESPLines.Checked;
        }

        private void checkESPBox_Click(object sender, EventArgs e)
        {
            //Config.ESPBox = checkESPBox.Checked;
        }

        private void checkEspName_Click(object sender, EventArgs e)
        {
            //Config.ESPName = checkEspName.Checked;
        }

        private void checkESPHealth_Click(object sender, EventArgs e)
        {
            //Config.ESPHealth = checkESPHealth.Checked;
        }

        private void guna2CustomCheckBox4_Click(object sender, EventArgs e)
        {
            InternalMemory.Cache = new();
            Core.Entities = new();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;

                TreeNode entityNode = new TreeNode(entity.Name);

                entityNode.Nodes.Add(new TreeNode($"IsKnown: {entity.IsKnown}"));
                entityNode.Nodes.Add(new TreeNode($"IsTeam: {entity.IsTeam}"));
                entityNode.Nodes.Add(new TreeNode($"Head: {entity.Head}"));
                entityNode.Nodes.Add(new TreeNode($"Root: {entity.Root}"));
                entityNode.Nodes.Add(new TreeNode($"Health: {entity.Health}"));
                entityNode.Nodes.Add(new TreeNode($"IsDead: {entity.IsDead}"));
                entityNode.Nodes.Add(new TreeNode($"IsKnocked: {entity.IsKnocked}"));


            }
        }

        private void paneltwo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private async void checkSpeed_Click(object sender, EventArgs e)
        {


        }


        public Mem MemLib = new Mem();


        private async Task ApplyHack(string processName, string searchPattern, string replacePattern, string successMessage, string failureMessage, string injectingMessage)
        {

        }


        public static string GetPattern(string value)
        {
            string pattern = Form2.KeyAuthApp.var(value);
            return string.IsNullOrEmpty(pattern) ? string.Empty : pattern;
        }

        private async void guna2CustomCheckBox10_Click(object sender, EventArgs e)
        {
            string search1 = "D4 49 1C 00 9C FA 17 00 98 F8 17 00 30 48 2D E9 08 B0 8D E2 42 DF 4D E2 DC 02 9F E5";
            string replace1 = "00 20 70 47 9C FA 17 00 98 F8 17 00 30 48 2D E9 08 B0 8D E2 42 DF 4D E2 DC 02 9F E5";
            string search2 = "0D F5 94 6D 01 B0 BD E8 00 0F F0 BD 03 E0 02 E0 01 E0 00 E0 FF E7 04 46 A7 F1 30 00";
            string replace2 = "00 00 00 E1 01 B0 BD E8 00 0F F0 BD 03 E0 02 E0 01 E0 00 E0 FF E7 04 46 A7 F1 30 00";
            string search5 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 08 C0 9B";
            string replace5 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 08 C0 9B E5";
            string search6 = "F0 B5 03 AF 4D F8 04 BD AD F5 C9 6D";
            string replace6 = "00 00 00 00 4D F8 04 BD AD F5 C9 6D";
            string processName = "HD-Player";
            string injectingMessage = "Security Injetando...";
            string successMessage = "Security Injetado!";
            string failureMessage = "Falha ao aplicar.";

            await ApplyHack(processName, search6, replace6, successMessage, failureMessage, injectingMessage);
            await ApplyHack(processName, search5, replace5, successMessage, failureMessage, injectingMessage);
            await ApplyHack(processName, search1, replace1, successMessage, failureMessage, injectingMessage);
            await ApplyHack(processName, search2, replace2, successMessage, failureMessage, injectingMessage);

        }









        private async void guna2CustomCheckBox5_Click(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox4_Click_1(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox3_Click(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox2_Click(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox6_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox1_Click_1(object sender, EventArgs e)
        {

        }





        private async void guna2CustomCheckBox2_Click_1(object sender, EventArgs e)
        {



        }

        private async void guna2CustomCheckBox3_Click_1(object sender, EventArgs e)
        {

        }

        private async void guna2CustomCheckBox4_Click_2(object sender, EventArgs e)
        {

        }



        private void RageMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //var picker = new ColorDialog();
            //var result = picker.ShowDialog();

            //if (result == DialogResult.OK)
            //{
            //    picColorLine.BackColor = picker.Color;
            //    Config.ESPLineColor = picker.Color;
            //}
        }

        private async void guna2CustomCheckBox5_Click_1(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox2_Click_2(object sender, EventArgs e)
        {

        }

        private void aimbothexcheckbox_Click(object sender, EventArgs e)
        {

            aimbotragecheckbox.Checked = false;
            aimbotlegitcheckbox.Checked = false;
            Config.AimBot = aimbothexcheckbox.Checked;
        }

        private void aimbotragecheckbox_Click(object sender, EventArgs e)
        {

            aimbothexcheckbox.Checked = false;
            aimbotlegitcheckbox.Checked = false;
            Config.AimBot = aimbotragecheckbox.Checked;
        }

        private void aimbotlegitcheckbox_Click(object sender, EventArgs e)
        {

            aimbothexcheckbox.Checked = false;
            aimbotragecheckbox.Checked = false;
            Config.AimBot = aimbotlegitcheckbox.Checked;
        }

        private void drawcirclecheckbox_Click(object sender, EventArgs e)
        {

            Config.Aimfovc = drawcirclecheckbox.Checked;
        }

        private void ingoreknockcheckbox_Click(object sender, EventArgs e)
        {

            Config.IgnoreKnocked = ingoreknockcheckbox.Checked;
        }

        private void norecoilcheckbox_Click(object sender, EventArgs e)
        {

            Config.NoRecoil = norecoilcheckbox.Checked;
        }

        private void esplinecheckbox_Click(object sender, EventArgs e)
        {

            Config.ESPLine = esplinecheckbox.Checked;
        }

        private void espboxcheckbox_Click(object sender, EventArgs e)
        {

            Config.ESPBox2 = espboxcheckbox.Checked;
           
        }

        private void espinfocheckbox_Click(object sender, EventArgs e)
        {
        
            Config.ESPName = espinfocheckbox.Checked;
            Config.ESPHealth = espinfocheckbox.Checked;
          
        }

        private void espskeletoncheckbox_Click(object sender, EventArgs e)
        {

            Config.ESPSkeleton = espskeletoncheckbox.Checked;
        }

        private void espfillboxcheckbox_Click(object sender, EventArgs e)
        {

            Config.ESPFillBox = espfillboxcheckbox.Checked;
        }

        private void espboxcornercheckbox_Click(object sender, EventArgs e)
        {
            Config.ESPBox = espboxcornercheckbox.Checked;
        }

        [DllImport("user32.dll")]
        static extern uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

        const uint WDA_NONE = 0x00000000;
        const uint WDA_MONITOR = 0x00000001;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
        private void streammodecheckbox_Click(object sender, EventArgs e)
        {
            if (streammodecheckbox.Checked) ;
            else { streammodecheckbox.Checked = false; }

            SetStreamMode(streammodecheckbox.Checked);
            void SetStreamMode(bool state)
            {
                foreach (var obj in Application.OpenForms)
                {
                    var form = obj as Form;

                    if (state)
                    {
                        SetWindowDisplayAffinity(form.Handle, WDA_EXCLUDEFROMCAPTURE);

                    }
                    else
                    {

                        SetWindowDisplayAffinity(form.Handle, WDA_NONE);

                    }
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guna2ComboBox comboBox = (Guna2ComboBox)sender;
            string selectedItem = (string)comboBox.SelectedItem;

            if (selectedItem == "Up")
            {
                Config.linePosition = "Up";
            }
            else if (selectedItem == "Bottom")
            {
                Config.linePosition = "Bottom";
            }
            else if (selectedItem == "Left")
            {
                Config.linePosition = "Left";
            }
            else if (selectedItem == "Right")
            {
                Config.linePosition = "Right";
            }
        }

        private void pinontop_Click(object sender, EventArgs e)
        {
            if (pinontop.Checked)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void esplinecolorpicker_Click(object sender, EventArgs e)
        {

            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                esplinecolorpicker.FillColor = picker.Color;
                Config.ESPLineColor = picker.Color;
            }
        }

        private void espboxcolorpicker_Click(object sender, EventArgs e)
        {

            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                espboxcolorpicker.FillColor = picker.Color;
                Config.ESPBoxColor = picker.Color;
            }
        }

        private void guna2CustomCheckBox1_Click_2(object sender, EventArgs e)
        {

        }



        private async void hookscheckbox_Click(object sender, EventArgs e)
        {
            var processes = Process.GetProcessesByName("HD-Player");

            if (processes.Length != 1)
            {
                MessageBox.Show("Emulator not Running");
                return;
            }

            var process = processes[0];
            var mainModulePath = Path.GetDirectoryName(process.MainModule.FileName);
            var adbPath = Path.Combine(mainModulePath, "HD-Adb.exe");

            if (!File.Exists(adbPath))
            {
                MessageBox.Show("Reinstall Emulator.");
                return;
            }


            var adb = new Adb(adbPath);
            await adb.Kill();

            var started = await adb.Start();
            if (!started)
            {
                MessageBox.Show("Adb Error");
                Application.Exit();
                return;
            }

            var moduleAddr = await adb.FindModule("com.dts.freefireth", "libil2cpp.so");

            Offsets.Il2Cpp = moduleAddr;
            Core.Handle = FindRenderWindow(mainHandle);

            var esp = new ESP();
            await esp.Start();

            new Thread(Data.Work) { IsBackground = true }.Start();
            new Thread(Aimbot.Work) { IsBackground = true }.Start();

            statuslabel.Text = "Status : OK! - " + moduleAddr.ToString("X");
        }

        private void Slider1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Slider1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Slider1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void animationTimer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void espskeletoncolorpicker_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                espskeletoncolorpicker.FillColor = picker.Color;
                Config.ESPSkeletonColor = picker.Color;
            }
        }

        private void guna2ControlBox1_Click_1(object sender, EventArgs e)
        {

        }

        private async void guna2ControlBox1_Click_2(object sender, EventArgs e)
        {
            KillProcess("HD-Adb");
            await Task.Delay(2000);
            KillProcess("HD-Player");
            await Task.Delay(1000);
            Environment.Exit(0);
            Application.Exit();
        }
        private void KillProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
        private async void autorefresh_Tick(object sender, EventArgs e)
        {
            InternalMemory.Cache = new();
            Core.Entities = new();
            await Task.Delay(1000);

        }

        private void autorefreshcheckbox_Click(object sender, EventArgs e)
        {
            autorefresh.Interval = 1000; // Intervalo em milissegundos
            autorefresh.Tick += autorefresh_Tick;
            autorefresh.Start();
        }

        private void aimfovtrackvalue_Scroll(object sender, ScrollEventArgs e)
        {
            var Fov = aimfovtrackvalue.Value;

            label12.Text = $"({Fov})";

            Config.Aimfov = Fov;
        }

        private void smoothtrackvalue_Scroll(object sender, ScrollEventArgs e)
        {
            var value2 = smoothtrackvalue.Value;

            label13.Text = $"({value2})";

            Config.Aimbotype = maxValueSmooth - value2;
        }

        private void rangetrackvalue_Scroll(object sender, ScrollEventArgs e)
        {
            var value3 = rangetrackvalue.Value;

            label15.Text = $"({value3})";

            Config.AimBotMaxDistance = value3;
        }

        private void espboxcornercolorpicker_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                espboxcornercolorpicker.FillColor = picker.Color;
                Config.ESPBoxColor = picker.Color;
            }
        }

        private void espfillboxcolorpicker_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                espfillboxcolorpicker.FillColor = picker.Color;
                Config.ESPFillBoxColor = picker.Color;
            }
        }

        private void renderinglagfixcheckbox_Click(object sender, EventArgs e)
        {

        }

        private void renderinglinefixcheckbox_Click(object sender, EventArgs e)
        {

        }
    }
}
