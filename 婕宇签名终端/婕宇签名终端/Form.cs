using System.Drawing;
using System.Windows.Forms;
using 捷宇科技签字版;

namespace 婕宇签名终端
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, System.EventArgs e)
        {
            SignBoard_JieYu.Instance.OpenDevice(null,
                () =>
                {
                    SignBoard_JieYu.Instance.SetSignCallBack(GetImage);
                },
                (msg, ex) =>
                {
                    MessageBox.Show($"{msg}");
                });
        }

        public void GetImage(Image image)
        {
            this.pictureBox1.Image = image;
        }

        private void btn_Sign_Click(object sender, System.EventArgs e)
        {

        }

        private void btn_Finger_Click(object sender, System.EventArgs e)
        {

        }

        private void btn_SignFinger_Click(object sender, System.EventArgs e)
        {

        }

        private void btn_FaceComparison_Click(object sender, System.EventArgs e)
        {
            SignBoard_JieYu.Instance.SignType = SignType.Face;
            SignBoard_JieYu.Instance.OpenDevice(null, () =>
            {
                SignBoard_JieYu.Instance.GetIdCardInfo(GetUserInfo);
            }, (msg, ex) =>
            {
                MessageBox.Show(msg);
            });

        }

        private void GetUserInfo(UserInfo obj)
        {
            MessageBox.Show($"验证通过：{obj.PassFlag}\t\n姓名：{obj.Name}\t\n身份证号：{obj.Id}");
        }
    }
}
