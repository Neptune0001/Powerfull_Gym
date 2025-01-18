using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Attendance_app.Views.Authentication
{
    public partial class LoginView : Form
    {
        #region Dlls to move the form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        Rectangle sizeGripRectangle;
        bool inSizeDrag = false;
        const int GRIP_SIZE = 15;

        int w = 0;
        int h = 0;
        #endregion

        // Rutas de las imágenes definidas como constantes estáticas
        private static readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        private readonly string showPasswordImage = Path.Combine(projectPath, "Gym_Attendance_app/Resources/icons/ojoShowPassword.png");
        private readonly string hidePasswordImage = Path.Combine(projectPath, "Gym_Attendance_app/Resources/icons/ojoHidePassword.png");

        public LoginView()
        {
            InitializeComponent();
            InitializeButtonState();
        }

        // Método para inicializar el estado del botón de mostrar/ocultar contraseña
        private void InitializeButtonState()
        {
            if (File.Exists(hidePasswordImage))
            {
                btnShowHidePassword.BackgroundImage = Image.FromFile(hidePasswordImage);
                btnShowHidePassword.BackgroundImage.Tag = "Hidden";
            }
            else
            {
                MessageBox.Show("La imagen inicial no se encontró.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseLogin_MouseClick(object sender, MouseEventArgs e)
        {
            UtilsAuthentication.ConfirmAndExit();
        }

        private void btnMinimizeLogin_MouseClick(object sender, MouseEventArgs e)
        {
            UtilsAuthentication.MinimizeForm(this);
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            UtilsAuthentication.HandleEnterPlaceholder(txtEmail, "Email", new Font("Montserrat Medium", 11.25f, FontStyle.Bold | FontStyle.Italic), Color.Black);
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            UtilsAuthentication.HandleLeavePlaceholder(txtEmail, "Email", new Font("Montserrat Medium", 11.25f, FontStyle.Bold | FontStyle.Italic), Color.DimGray);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            UtilsAuthentication.HandleEnterPlaceholder(txtPassword, "Password", new Font("Montserrat Medium", 11.25f, FontStyle.Bold | FontStyle.Italic), Color.Black);
            txtPassword.PasswordChar = true;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            UtilsAuthentication.HandleLeavePlaceholder(txtPassword, "Password", new Font("Montserrat Medium", 11.25f, FontStyle.Regular), Color.DimGray);
            txtPassword.PasswordChar = false;
        }

        private void btnShowHidePassword_Click(object sender, EventArgs e)
        {
            // Validar si las imágenes existen
            if (!File.Exists(showPasswordImage) || !File.Exists(hidePasswordImage))
            {
                MessageBox.Show("Las imágenes necesarias no se encontraron.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Alternar estado del botón de mostrar/ocultar contraseña
            if (btnShowHidePassword.BackgroundImage?.Tag?.ToString() == "Hidden")
            {
                btnShowHidePassword.BackgroundImage = Image.FromFile(showPasswordImage);
                btnShowHidePassword.BackgroundImage.Tag = "Visible";
                txtPassword.PasswordChar = false;
            }
            else
            {
                btnShowHidePassword.BackgroundImage = Image.FromFile(hidePasswordImage);
                btnShowHidePassword.BackgroundImage.Tag = "Hidden";
                txtPassword.PasswordChar = true;
            }
        }

        // Método para establecer el estilo de fuente del campo de contraseña
        private void SetPasswordFontStyle(bool isFocused)
        {
            txtPassword.Font = new Font("Montserrat Medium", 11.25f, isFocused ? FontStyle.Bold | FontStyle.Italic : FontStyle.Regular);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            // Assuming you have another form called MainView
            Register mainView = new Register();
            mainView.Show();
            this.Hide();
        }
    }
}
