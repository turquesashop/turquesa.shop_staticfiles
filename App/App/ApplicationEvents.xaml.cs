using System.Windows.Controls;

namespace App
{
	/// <summary>
	/// Interaction logic for ApplicationEvents.xaml
	/// </summary>
	public partial class ApplicationEvents : UserControl
	{
		public ApplicationEvents()
		{
			InitializeComponent();
			DataContext = new MessengerLogViewModel();
		}
	}
}
