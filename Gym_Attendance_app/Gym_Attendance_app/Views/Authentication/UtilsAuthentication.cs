using Gym_Attendance_app.RJControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Attendance_app.Views.Authentication
{
    internal class UtilsAuthentication
    {
        public static void ConfirmAndExit()
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public static void MinimizeForm(Form form)
        {
            form.WindowState = FormWindowState.Minimized;
        }

        public static void HandleEnterPlaceholder(RJTextBox textBox, string placeholder, Font focusedFont, Color focusedColor)
        {
            if (textBox.Texts == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = focusedColor;
                textBox.Font = focusedFont;
            }
        }

        public static void HandleLeavePlaceholder(RJTextBox textBox, string placeholder, Font defaultFont, Color defaultColor)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Texts = placeholder;
                textBox.ForeColor = defaultColor;
                textBox.Font = defaultFont;
            }
        }

        public static void TogglePasswordVisibility(RJButton button, RJTextBox textBox, string showImagePath, string hideImagePath)
        {
            if (!File.Exists(showImagePath) || !File.Exists(hideImagePath))
            {
                MessageBox.Show("Las imágenes necesarias no se encontraron.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (button.BackgroundImage?.Tag?.ToString() == "Hidden")
            {
                button.BackgroundImage = Image.FromFile(showImagePath);
                button.BackgroundImage.Tag = "Visible";
                textBox.PasswordChar = false;
            }
            else
            {
                button.BackgroundImage = Image.FromFile(hideImagePath);
                button.BackgroundImage.Tag = "Hidden";
                textBox.PasswordChar = true;
            }
        }

    }
}
