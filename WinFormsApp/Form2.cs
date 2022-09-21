namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        #region 禁止方法缩小
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                if ((int)m.WParam != 0xf020 && (int)m.WParam != 0xf030 && (int)m.WParam != 0xf060 && (int)m.WParam != 0xf120)
                {
                    m.WParam = IntPtr.Zero;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Form2_MouseClick");
        }

        private void Form2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Form2_MouseDoubleClick");
        }

        private void Form2_MaximumSizeChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Form2_MaximumSizeChanged");
        }

        private void Form2_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Form2_DoubleClick");
        }
    }
}
