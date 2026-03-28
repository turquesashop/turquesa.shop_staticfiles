using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.ViewModels;

namespace Editor.Views
{
	public partial class ConfigViewModel : TabItemViewModel
    {
        [ObservableProperty]
        private string _rootPath = string.Empty;

        public ConfigViewModel(string header) : base(header)
        {
		}

		[RelayCommand]
        private void SelectFolder() 
        {
            var dialog = new Microsoft.Win32.OpenFolderDialog { Title = "Selecciona la carpeta de tu proyecto." };

            if (dialog.ShowDialog().Equals(true)) 
            {
                RootPath = dialog.FolderName;
			}
		}
	}
}
