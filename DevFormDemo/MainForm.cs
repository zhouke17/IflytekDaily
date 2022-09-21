using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DevFormDemo
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.tip_layer.Visible = true;
            label.Parent = pictureBox1;
            label.Text = "图片显示文字";
            label.BackColor = Color.Black;
            label.ForeColor = Color.White;
        }


        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            //args.AutoCloseOptions.Delay = 5000;
            args.Caption = "Auto-close message";
            args.Text = "This message closes automatically after 5 seconds.";
            args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
            // change the default button
            args.DefaultButtonIndex = 1;
            // set to false to hide the countdown
            //args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
            if (XtraMessageBox.Show(args) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        int clickTimes = 0;
        private void simpleButton_Click(object sender, System.EventArgs e)
        {
            ++clickTimes;
            if (clickTimes == 1 || clickTimes == 2)
            {
                this.toolTip1.SetToolTip(this.simpleButton1, $"点击{clickTimes}次");
            }
            else if (clickTimes == 3 || clickTimes == 4)
            {
                this.toolTip1.SetToolTip(this.simpleButton1, $"点击{clickTimes}次");
            }
            else
            {
                this.toolTip1.RemoveAll();
                clickTimes = 0;
            }
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            this.pictureBox1.Visible = false;
            this.tip_layer.Visible = true;
        }

        private void tip_layer_Click(object sender, System.EventArgs e)
        {
            this.pictureBox1.Visible = true;
            this.tip_layer.Visible = false;
        }

        public void Remove(LayoutControlGroup group)
        {
            // Normal Dispose forgets to remove Controls, so have to do this ourselves.
            List<Control> controls = new List<Control>();
            AddControls(group, controls);
            var owner = group.Owner;
            owner.BeginUpdate();
            group.Dispose();
            foreach (var c in controls)
            {
                c.Dispose();
            }
            owner.EndUpdate();
            group = null;
        }
        private static void AddControls(BaseLayoutItem layoutItem, List<Control> controls)
        {
            if (layoutItem is LayoutControlGroup)
            {
                foreach (BaseLayoutItem item in ((LayoutControlGroup)layoutItem).Items)
                {
                    AddControls(item, controls);
                }
            }
            else if (layoutItem is LayoutControlItem)
            {
                var ic = (LayoutControlItem)layoutItem;
                if (ic.Control != null)
                    controls.Add(ic.Control);
            }
            else if (layoutItem is TabbedControlGroup)
            {
                foreach (BaseLayoutItem page in ((TabbedControlGroup)layoutItem).TabPages)
                {
                    AddControls(page, controls);
                }
            }
        }

        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            Remove(layoutControlGroup2);
        }
    }
}
