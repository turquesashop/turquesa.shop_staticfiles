using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Editor.Messenger;
using Editor.ViewModels;

namespace Editor.Views
{
	internal class GalleryViewModel : TabItemViewModel
	{
		public GalleryViewModel(string header) : base(header)
		{
		}
	}
}
