using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gym_Attendance_app.RJControls
{
    internal static class FormsRounded
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse
        );

        /// <summary>
        /// Aplica bordes redondeados a un formulario, con esquinas personalizadas.
        /// </summary>
        /// <param name="frm">El formulario al que se le aplicarán los bordes redondeados.</param>
        /// <param name="topLeft">Radio de la esquina superior izquierda.</param>
        /// <param name="topRight">Radio de la esquina superior derecha.</param>
        /// <param name="bottomRight">Radio de la esquina inferior derecha.</param>
        /// <param name="bottomLeft">Radio de la esquina inferior izquierda.</param>
        public static void ApplyCustomRoundedCorners(Form frm, int topLeft, int topRight, int bottomRight, int bottomLeft)
        {
            if (frm == null) throw new ArgumentNullException(nameof(frm));

            // Crea un GraphicsPath para definir las esquinas personalizadas
            GraphicsPath path = new GraphicsPath();

            int width = frm.Width;
            int height = frm.Height;

            // Esquina superior izquierda
            path.AddArc(0, 0, topLeft * 2, topLeft * 2, 180, 90);
            // Borde superior
            path.AddLine(topLeft, 0, width - topRight, 0);
            // Esquina superior derecha
            path.AddArc(width - (topRight * 2), 0, topRight * 2, topRight * 2, 270, 90);
            // Borde derecho
            path.AddLine(width, topRight, width, height - bottomRight);
            // Esquina inferior derecha
            path.AddArc(width - (bottomRight * 2), height - (bottomRight * 2), bottomRight * 2, bottomRight * 2, 0, 90);
            // Borde inferior
            path.AddLine(width - bottomRight, height, bottomLeft, height);
            // Esquina inferior izquierda
            path.AddArc(0, height - (bottomLeft * 2), bottomLeft * 2, bottomLeft * 2, 90, 90);
            // Borde izquierdo
            path.AddLine(0, height - bottomLeft, 0, topLeft);

            path.CloseFigure();

            // Asigna el Path al formulario
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Region = new Region(path);
        }
    }
}
