using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaskFo
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer2;
        private Point previePoint;
        public Form1()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();

            timer2 = new Timer();
            timer2.Tick += Timer2_Tick;
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            Point curPoint = Cursor.Position;
            if (curPoint != previePoint)
            {
                this.timer.Start();
            }
            else
            {
                this.timer.Stop();
                this.maskLayer1.Visible = false;
            }
            // 更新上一个鼠标位置
            previePoint = curPoint;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Rectangle rec = this.maskLayer1.RectangleToScreen(maskLayer1.ClientRectangle);
            if (rec.Contains(Control.MousePosition))
            {
                this.maskLayer1.Visible = true;
            }
        }

        private void maskLayer1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.maskLayer1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.maskLayer1.Visible = true;
        }

        private void maskLayer1_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new Action(() =>
                {
                    this.maskLayer1.Visible = false;
                    this.maskLayer1.Visible = true;
                }));
            });
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            previePoint = e.Location;
        }
    }
}
