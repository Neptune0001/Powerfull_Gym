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
    internal class ControlUtils
    {
        // Mostrar un cuadro de diálogo personalizado
        public static DialogResult MessageBox_Show(string message, string title, bool yesOrNo, string type)
        {
            using (frmMessageBox frm = new frmMessageBox())
            {
                frm.txtfrmMessage.Text = message;
                frm.lblTitle.Text = title;

                // Configurar la pestaña según el valor de YesOrNo
                frm.tabControl1.SelectedIndex = yesOrNo ? 1 : 0;

                // Asignar el icono al PictureBox según el tipo
                switch (type.ToLower())
                {
                    case "info":
                        frm.pictureBIcon.Image = SystemIcons.Information.ToBitmap(); // Icono de información
                        frm.pictureBColorType.BackColor = Color.FromArgb(0, 120, 215); // Color azul
                        break;

                    case "warning":
                        frm.pictureBIcon.Image = SystemIcons.Warning.ToBitmap(); // Icono de advertencia
                        frm.pictureBColorType.BackColor = Color.FromArgb(255, 185, 0); // Color amarillo
                        break;

                    case "error":
                        frm.pictureBIcon.Image = SystemIcons.Error.ToBitmap(); // Icono de error
                        frm.pictureBColorType.BackColor = Color.FromArgb(220, 53, 69); // Color rojo
                        break;

                    case "question":
                        frm.pictureBIcon.Image = SystemIcons.Question.ToBitmap(); // Icono de pregunta
                        frm.pictureBColorType.BackColor = Color.FromArgb(23, 162, 184); // Color azul claro
                        break;

                    default:
                        frm.pictureBIcon.Image = null; // Sin icono si el tipo no coincide
                        frm.pictureBColorType.BackColor = Color.FromArgb(0, 120, 215); // Color azul
                        break;
                }

                // Mostrar el cuadro de diálogo y devolver el resultado
                DialogResult result = frm.ShowDialog();
                frm.Close();
                return result;
            }
        }

        // Minimizar un formulario
        public static void MinimizeForm(Form form)
        {
            form.WindowState = FormWindowState.Minimized;
        }

        // Maximize a form
        public static void MaximizeForm(Form form)
        {
            if (form.WindowState == FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Normal;
            }
            else
            {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        // Confirmar y salir de la aplicación
        public static void ConfirmAndExit()
        {
            if (MessageBox_Show("Are you sure you want to exit?", "Exit", true, "question") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Manejar el placeholder al entrar en un TextBox
        public static void HandleEnterPlaceholder(RJTextBox textBox, string placeholder, Font focusedFont, Color focusedColor)
        {
            if (textBox.Texts == placeholder)
            {
                textBox.Texts = "";
                textBox.ForeColor = focusedColor;
                textBox.Font = focusedFont;
            }
        }

        // Manejar el placeholder al salir de un TextBox
        public static void HandleLeavePlaceholder(RJTextBox textBox, string placeholder, Font defaultFont, Color defaultColor)
        {
            if (string.IsNullOrEmpty(textBox.Texts))
            {
                textBox.Texts = placeholder;
                textBox.ForeColor = defaultColor;
                textBox.Font = defaultFont;
            }
        }

        // Alternar la visibilidad de la contraseña en un TextBox
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

        // Validar los campos de autenticación
        public static void ValidateAuthenticationFields(RJTextBox emailTextBox, RJTextBox passwordTextBox)
        {
            if (string.IsNullOrEmpty(emailTextBox.Texts) || emailTextBox.Texts == "Email")
            {
                MessageBox.Show("Please enter your email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Texts) || passwordTextBox.Texts == "Password")
            {
                MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Método para inicializar el estado del botón de mostrar/ocultar contraseña
        public static void InitializeButton_HideShowPassState(string hidePasswordImage, RJButton btnShowHidePassword)
        {
            if (File.Exists(hidePasswordImage))
            {
                btnShowHidePassword.BackgroundImage = Image.FromFile(hidePasswordImage);
                btnShowHidePassword.BackgroundImage.Tag = "Hidden";
            }
            else
            {
                ControlUtils.MessageBox_Show("La imagen inicial no se encontró.", "Error", false, "error");
            }
        }
    }
}
