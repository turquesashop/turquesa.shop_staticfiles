using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Messenger;
using Editor.Views;

namespace Editor.ViewModels
{
	// 2. THE BASE TAB CLASS (Handles the Header and IsEnabled state)
	public abstract partial class TabItemViewModel : ObservableObject
	{
		[ObservableProperty]
		private string _header;

		[ObservableProperty]
		private bool _isEnabled = true;

		protected TabItemViewModel(string header)
		{
			Header = header;
		}

		[RelayCommand]
		private void SendTestMessage()
		{
			SimpleMessenger.Default.Send($"Hello from the {Header}!");
		}
	}
}