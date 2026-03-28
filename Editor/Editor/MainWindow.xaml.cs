using System.Windows;
using Editor.Messenger;
using Editor.ViewModels;

namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			SimpleMessenger.Default.Send("Main Window Initialized");

            DataContext = new MainWindowViewModel();
			SimpleMessenger.Default.Send("Data Context Set");
		}
    }
}