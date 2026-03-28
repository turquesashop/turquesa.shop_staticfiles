using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Messenger;
using Editor.Views;

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
				new GalleryViewModel("Galería"),
				new CreateViewModel("Crear"),
				new EditViewModel("Modificar"),
				new SortViewModel("Ordenar"),
				new ConfigViewModel("Config"),
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
}