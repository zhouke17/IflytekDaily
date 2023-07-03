using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTestDemo.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string argName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(argName));
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public MainWindowViewModel()
        {
            Name = "Hello WPF!";
        }
    }
}
