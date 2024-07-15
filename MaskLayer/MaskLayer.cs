using System.Windows.Forms;

namespace MaskFo
{
    public partial class MaskLayer : UserControl
    {
        private bool isIn = false;
        public MaskLayer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.Opaque, true);
        }

        private void MaskLayer_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
        }

        private void MaskLayer_MouseLeave(object sender, System.EventArgs e)
        {
        }

        private void MaskLayer_MouseEnter(object sender, System.EventArgs e)
        {
        }

        private void MaskLayer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Visible = true;
        }
    }
}
