using System.Windows.Controls;
using Editor.Messenger;

namespace Editor.Views
{
	/// <summary>
	/// Interaction logic for ApplicationEventsView.xaml
	/// </summary>
	public partial class ApplicationEventsView : UserControl
	{
		public ApplicationEventsView()
		{
			InitializeComponent();
			DataContext = new MessengerLogViewModel();
		}
	}
}
