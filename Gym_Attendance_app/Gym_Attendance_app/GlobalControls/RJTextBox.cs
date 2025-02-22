using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gym_Attendance_app.RJControls
{
    [DefaultEvent("_TextChanged")]
    public partial class RJTextBox : UserControl
    {
        #region Fields
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private Color borderFocusColor = Color.HotPink;
        private bool isFocused = false;

        // Placeholder fields
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "Placeholder";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;

        // Borders
        private int borderRadius = 0;
        #endregion

        #region Constructor
        public RJTextBox()
        {
            InitializeComponent();
            textBox1.TextChanged += (s, e) => _TextChanged?.Invoke(s, e);
            textBox1.Enter += (s, e) => { isFocused = true; RemovePlaceholder(); Invalidate(); };
            textBox1.Leave += (s, e) => { isFocused = false; SetPlaceholder(); Invalidate(); };
            SetPlaceholder();
        }
        #endregion

        #region Events
        public event EventHandler _TextChanged;
        #endregion

        #region Properties
        [Category("RJ Code Advance"), Description("Background color of the text box.")]
        public override Color BackColor
        {
            get => base.BackColor;
            set { base.BackColor = textBox1.BackColor = value; }
        }

        [Category("RJ Code Advance"), Description("BorderRadius of the text box")]
        public int BoderRadius1
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate(); // Redraw the control
                }
            }
        }

        [Category("RJ Code Advance"), Description("Color of the border.")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("RJ Code Advance"), Description("Color of the border when focused.")]
        public Color BorderFocusColor
        {
            get => borderFocusColor;
            set => borderFocusColor = value;
        }

        [Category("RJ Code Advance"), Description("Size of the border.")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Category("RJ Code Advance"), Description("Font style of the text box.")]
        public override Font Font
        {
            get => base.Font;
            set { base.Font = textBox1.Font = value; if (DesignMode) UpdateControlHeight(); }
        }

        [Category("RJ Code Advance"), Description("Foreground color of the text box.")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set { base.ForeColor = textBox1.ForeColor = value; }
        }

        [Category("RJ Code Advance"), Description("Enable multiline mode.")]
        public bool Multiline
        {
            get => textBox1.Multiline;
            set { textBox1.Multiline = value; UpdateControlHeight(); }
        }

        [Category("RJ Code Advance"), Description("Placeholder text of the text box.")]
        public string PlaceholderText
        {
            get => placeholderText;
            set
            {
                placeholderText = value;
                SetPlaceholder();
            }
        }

        [Category("RJ Code Advance"), Description("Set password character mode.")]
        public bool PasswordChar
        {
            get { return isPasswordChar; }
            set
            {
                isPasswordChar = value;
                textBox1.UseSystemPasswordChar = value;
            }
        }

        [Category("RJ Code Advance"), Description("Text content of the text box.")]
        public string Texts
        {
            get => isPlaceholder ? string.Empty : textBox1.Text;
            set
            {
                textBox1.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    SetPlaceholder();
                }
                else
                {
                    RemovePlaceholder();
                }
            }
        }

        [Category("RJ Code Advance"), Description("Color of the placeholder.")]
        public Color PlaceholderColor
        {
            get => placeholderColor;
            set
            {
                placeholderColor = value;
                if (isPlaceholder)
                {
                    textBox1.ForeColor = value;
                }
            }
        }

        [Category("RJ Code Advance"), Description("Enable underlined border style.")]
        public bool UnderlinedStyle
        {
            get => underlinedStyle;
            set { underlinedStyle = value; Invalidate(); }
        }
        #endregion

        #region Overridden Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics grahp = e.Graphics;

            if (borderRadius > 1) // Rounded TextBox
            {
                // Fields
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    // Drawing
                    this.Region = new Region(pathBorderSmooth);// Set the rounded region of UserControl
                    if (borderRadius > 15) SetTextBoxRoundedRegion(); // Set the rounded region of TextBox
                    grahp.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    if (isFocused) penBorder.Color = borderFocusColor;

                    if (underlinedStyle)
                    {
                        // Draw border smoothing
                        grahp.DrawPath(penBorderSmooth, pathBorderSmooth);
                        // Draw border
                        grahp.SmoothingMode = SmoothingMode.None;
                        grahp.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                    }
                    else // Normal Style
                    {
                        // Draw border smoothing
                        grahp.DrawPath(penBorderSmooth, pathBorderSmooth);
                        // Draw border
                        grahp.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else // Square / Normal TextBox 
            {
                using (Pen penBoder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);

                    penBoder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    if (isFocused) penBoder.Color = borderFocusColor;

                    if (underlinedStyle)
                    {
                        grahp.DrawLine(penBoder, 0, Height - 1, Width, Height - 1);
                    }
                    else
                    {
                        grahp.DrawRectangle(penBoder, 0, 0, Width - 0.5F, Height - 0.5F);
                    }
                }
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DesignMode) UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
        #endregion

        #region Private Methods
        private void UpdateControlHeight()
        {
            if (!textBox1.Multiline)
            {
                int txtHeight = TextRenderer.MeasureText("Text", Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;
                Height = textBox1.Height + Padding.Top + Padding.Bottom;
            }
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && placeholderText != "")
            {
                isPlaceholder = true;
                textBox1.Text = placeholderText;
                textBox1.ForeColor = placeholderColor;

                if (isPasswordChar)
                {
                    textBox1.UseSystemPasswordChar = false;
                }
            }
        }

        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textBox1.Text = "";
                textBox1.ForeColor = this.ForeColor;

                if (isPasswordChar)
                {
                    textBox1.UseSystemPasswordChar = true;
                }
            }
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (Multiline)
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize);
                textBox1.Region = new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize * 2);
                textBox1.Region = new Region(pathTxt);
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion

        #region Event Handlers
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
            {
                _TextChanged.Invoke(sender, e);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        #endregion
    }
}
