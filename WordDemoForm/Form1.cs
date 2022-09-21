using Microsoft.Office.Interop.Word;
using System;
using System.Windows.Forms;

namespace WordDemoForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();

            Document doc = ap.Documents.Open(@"C:\Users\kezhou3\Desktop\Sample.docx", ReadOnly: false, Visible: false);
            doc.Activate();

            // These 3 lines did the trick.
            doc.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekPrimaryFooter;
            doc.Application.Selection.MoveRight(WdUnits.wdCharacter, 1);
            doc.Application.Selection.Delete(WdUnits.wdCharacter, 9);

            ap.Documents.Close(SaveChanges: false, OriginalFormat: false, RouteDocument: false);

            ((_Application)ap).Quit(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ap);
        }
    }
}
