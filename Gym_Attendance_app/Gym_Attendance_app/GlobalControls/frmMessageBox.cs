using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Attendance_app.RJControls
{
    public partial class frmMessageBox : Form
    {
        public frmMessageBox()
        {
            InitializeComponent();
        }

        private void frmMessageBox_Load(object sender, EventArgs e)
        {
            //FormsRounded.ApplyCustomRoundedCorners(this,
            //    topLeft: 0,
            //    topRight: 0,
            //    bottomRight: 10,
            //    bottomLeft: 10);
        }

        private void btnOkfrmMessage_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnYesfrmMessage_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNofrmMessage_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
