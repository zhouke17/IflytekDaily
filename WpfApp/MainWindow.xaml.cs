using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();

        private INotifyPropertyChangedExetension<Student> studentList;
        public INotifyPropertyChangedExetension<Student> StudentList
        {
            get
            {
                return studentList;
            }
            set
            {
                studentList = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            StudentList = new INotifyPropertyChangedExetension<Student>();
            StudentList.Add(new Student { Name = "张三" });
            StudentList.CollectionChanged += StudentList_CollectionChanged;
            this.DataContext = this.StudentList;

            this.cmbColors.ItemsSource = typeof(System.Drawing.Color).GetProperties();
        }

        private void StudentList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show(e.Action.ToString());
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (cmbColors.SelectedIndex > 0)
                cmbColors.SelectedIndex = cmbColors.SelectedIndex - 1;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbColors.SelectedIndex < this.cmbColors.Items.Count - 1)
                this.cmbColors.SelectedIndex = this.cmbColors.SelectedIndex + 1;
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            this.cmbColors.SelectedItem = typeof(System.Drawing.Color).GetProperty("Blue");
        }

        private void cmbColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            System.Drawing.Color color = (System.Drawing.Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null);
            this.stackColor.Background = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
        }

        private void btnDoSynchronousCalculation_Click(object sender, RoutedEventArgs e)
        {
            int max = 10000;
            pbCalculationProgress.Value = 0;
            lbResults.Items.Clear();
            int result = 0;
            for (int i = 0; i < max; i++)
            {
                if (i % 42 == 0)
                {
                    lbResults.Items.Add(i);
                    result++;
                }
                System.Threading.Thread.Sleep(1);
                pbCalculationProgress.Value = Convert.ToInt32(((double)i / max) * 100);
            }
            MessageBox.Show("Numbers between 0 and 10000 divisible by 7: " + result);
        }

        private void btnDoAsynchronousCalculation_Click(object sender, RoutedEventArgs e)
        {
            pbCalculationProgress.Value = 0;
            lbResults.Items.Clear();

            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(10000);
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Numbers between 0 and 10000 divisible by 7: {e.Result}");
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            pbCalculationProgress.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                lbResults.Items.Add(e.UserState);
            }
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            int max = (int)e.Argument;
            int result = 0;
            for (int i = 1; i < max; i++)
            {
                var progress = Convert.ToInt32(((double)i / max) * 100);
                if (i % 42 == 0)
                {
                    result++;
                    (sender as BackgroundWorker).ReportProgress(progress, i);
                }
                else
                {
                    (sender as BackgroundWorker).ReportProgress(progress);
                }
                System.Threading.Thread.Sleep(1);
            }
            e.Result = result;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
