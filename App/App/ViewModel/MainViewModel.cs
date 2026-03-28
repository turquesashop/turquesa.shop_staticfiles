using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App;

// 1. THE MAIN VIEW MODEL
public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<TabItemViewModel> _tabs;

	[ObservableProperty]
	private TabItemViewModel _selectedTab;

	public MainViewModel()
	{
		// Initialize your four tabs
		Tabs = new ObservableCollection<TabItemViewModel>
		{
			new GalleryViewModel(),
			new CreateElementViewModel(),
			new EditElementViewModel(),
			new OrderElementsViewModel()
		};

		// Set the Gallery as the active tab when the app starts
		SelectedTab = Tabs[0];

		// Example: Disable the Edit tab until an item is actually selected later
		Tabs[2].IsEnabled = false;
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
public class GalleryViewModel : TabItemViewModel { public GalleryViewModel() : base("Gallery") { } }
public class CreateElementViewModel : TabItemViewModel { public CreateElementViewModel() : base("Create Element") { } }
public class EditElementViewModel : TabItemViewModel { public EditElementViewModel() : base("Edit Element") { } }
public class OrderElementsViewModel : TabItemViewModel { public OrderElementsViewModel() : base("Order Elements") { } }