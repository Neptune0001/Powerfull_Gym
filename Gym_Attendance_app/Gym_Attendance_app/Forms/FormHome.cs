using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Gym_Attendance_app.Views.Authentication;

namespace Gym_Attendance_app.Forms
{
    public partial class FormHome : Form
    {
        #region Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        #endregion

        #region Constructor
        public FormHome()
        {
            InitializeComponent();
            leftBorderBtn = new Panel { Size = new Size(7, 50) };
            panelMenu.Controls.Add(leftBorderBtn);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        #endregion

        #region Color Struct
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(52, 152, 219); // Azul vibrante
            public static Color color2 = Color.FromArgb(26, 188, 156); // Verde azulado
            public static Color color3 = Color.FromArgb(39, 174, 96);  // Verde moderno
            public static Color color4 = Color.FromArgb(155, 89, 182); // Púrpura elegante
            public static Color color5 = Color.FromArgb(241, 196, 15); // Amarillo brillante
            public static Color color6 = Color.FromArgb(231, 76, 60);  // Rojo intenso
        }
        #endregion

        #region Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(44, 62, 80);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(30, 41, 59);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.FromArgb(189, 195, 199);
            lblTitleChildForm.Text = "Home";
        }
        #endregion

        #region Form Events
        private void FormHome_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Panel Top
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ControlUtils.ConfirmAndExit();
        }

        private void btnMaximizedWindow_Click(object sender, EventArgs e)
        {
            ControlUtils.MaximizeForm(this);
        }

        private void btnMinimizeWindow_Click(object sender, EventArgs e)
        {
            ControlUtils.MinimizeForm(this);
        }
        #endregion

        #region Menu Buttons
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new Dashboard.FormDashboard());
        }
        private void btnClients_Click(object sender, EventArgs e)
        { 
            ActivateButton(sender, RGBColors.color2); 
            OpenChildForm(new Clients.FormClients());
        }
        private void btnEmployees_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new Employees.FormEmployees());
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new Reports.FormReports());
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new Settings.FormSettings());
        }
        #endregion

        #region Home Button Reset
        private void btnHomePicture_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
        #endregion
    }
}
