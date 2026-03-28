using System.Windows.Controls;

namespace Editor.Views
{
	/// <summary>
	/// Interaction logic for EditView.xaml
	/// </summary>
	public partial class EditView : UserControl
	{
		public EditView()
		{
			InitializeComponent();
			DataContext = new EditViewModel();
		}
	}
}
