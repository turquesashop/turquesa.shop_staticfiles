using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Messenger;

namespace Editor.ViewModels
{
	public partial class MainWindowViewModel : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<TabItemViewModel> _tabs;

		[ObservableProperty]
		private TabItemViewModel _selectedTab;

		public MainWindowViewModel()
		{
			SimpleMessenger.Default.Send("Instantiating ObservableCollection<TabItemViewModel>");
			Tabs = new ObservableCollection<TabItemViewModel>
			{
				new GalleryViewModel(),
				new CreateElementViewModel(),
				new EditElementViewModel(),
				new OrderElementsViewModel()
			};

			SimpleMessenger.Default.Send("Setting Selected Tab");
			SelectedTab = Tabs[0];

			SimpleMessenger.Default.Send("Disabling Edit Tab");
			Tabs[2].IsEnabled = false;
		}

		partial void OnSelectedTabChanged(TabItemViewModel value)
		{
			if (value is null)
			{
				SimpleMessenger.Default.Send("Tab value was null.");
				return;
			}

			SimpleMessenger.Default.Send($"Selected tab changed to: {value.Header}");
		}

		[RelayCommand]
		private void SetSelectedTab(TabItemViewModel tab) 
		{
			SelectedTab = tab;
		}
	}

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
	}

	// 3. THE FOUR SPECIFIC TAB CLASSES
	public partial class GalleryViewModel : TabItemViewModel
	{
		public GalleryViewModel() : base("Galería") { }

		[RelayCommand]
		private void SendTestMessage()
		{
			SimpleMessenger.Default.Send("Hello from the Gallery!");
		}

	}
	public partial class CreateElementViewModel : TabItemViewModel
	{
		public CreateElementViewModel() : base("Crear") { }

		[RelayCommand]
		private void SendTestMessage()
		{
			SimpleMessenger.Default.Send("Hello from the CreateElementViewModel!");
		}
	}
	public partial class EditElementViewModel : TabItemViewModel
	{
		public EditElementViewModel() : base("Editar") { }

		[RelayCommand]
		private void SendTestMessage()
		{
			SimpleMessenger.Default.Send("Hello from the EditElementViewModel!");
		}
	}
	public partial class OrderElementsViewModel : TabItemViewModel
	{
		public OrderElementsViewModel() : base("Ordenar") { }

		[RelayCommand]
		private void SendTestMessage()
		{
			SimpleMessenger.Default.Send("Hello from the OrderElementsViewModel!");
		}
	}
}