using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Reborn;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AotForms
{
    public partial class Form2 : Form
    {

        public static api KeyAuthApp = new api(

             name: ("CV_PREMIUM"),
             ownerid: ("JOzxAPywrc"),
             secret: ("7eb2e3fbeea45f892bf7ef096e98c3dd6532a5c26a1c537a9a83b30c93fee576"),
             version: ("3.9"));
        public static Label StatusLbl;
        IntPtr mainHandle;
        public Form2(IntPtr handle)
        {
            mainHandle = handle;
            InitializeComponent();

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
        private void Form2_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();
            KeyAuthApp.check();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("Verifying login, wait for a moment...", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tenta fazer login com as credenciais fornecidas
            KeyAuthApp.login(username.Text, password.Text);

            // Verifica se o login foi bem-sucedido
            if (KeyAuthApp.response.success)
            {
                // Tenta novamente o login
                KeyAuthApp.login(username.Text, password.Text);
                if (KeyAuthApp.response.success)
                {
                    // Cria uma nova instância do Form1 com o handle apropriado e mostra o formulário
                    Form1 ML = new Form1(mainHandle);
                    ML.Show();
                    // Esconde o formulário atual
                    this.Hide();
                }
            }
            else
            {
                // Exibe uma mensagem de erro em uma MessageBox
                MessageBox.Show("Falha no login. Por favor, tente novamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}







