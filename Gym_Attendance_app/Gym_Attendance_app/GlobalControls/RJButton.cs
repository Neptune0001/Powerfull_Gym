using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Gym_Attendance_app
{
    internal class RJButton : Button
    {
        #region Fields
        private int BorderSize = 0;
        private int BorderRadius = 40;
        private Color BorderColor = Color.PaleVioletRed;
        private Color hoverColor = Color.LightGray;
        private Color originalBackColor;
        #endregion

        #region Properties
        [Category("RJ Code Advance")]
        public int BorderSize1
        {
            get => BorderSize;
            set
            {
                BorderSize = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public int BorderRadius1
        {
            get => BorderRadius;
            set
            {
                BorderRadius = (value <= this.Height) ? value : this.Height;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public Color BorderColor1
        {
            get => BorderColor;
            set
            {
                BorderColor = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        public Color BackgroundColor
        {
            get => this.BackColor;
            set => this.BackColor = value;
        }

        [Category("RJ Code Advance")]
        public Color TextColor
        {
            get => this.ForeColor;
            set => this.ForeColor = value;
        }

        [Category("RJ Code Advance")]
        public Color HoverColor
        {
            get => hoverColor;
            set => hoverColor = value;
        }
        #endregion

        #region Constructor
        public RJButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
            this.MouseEnter += new EventHandler(Button_MouseEnter);
            this.MouseLeave += new EventHandler(Button_MouseLeave);
        }
        #endregion

        #region Paint Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (BorderRadius > 2) // Rounded Button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, BorderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, BorderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(BorderColor, BorderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    if (BorderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if (BorderSize >= 1)
                {
                    using (Pen penBorder = new Pen(BorderColor, BorderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        #endregion

        #region Event Handlers
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
            originalBackColor = this.BackColor;
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Invalidate();
            }
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (BorderRadius > this.Height)
                BorderRadius = this.Height;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = hoverColor;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = originalBackColor;
        }
        #endregion
    }
}
