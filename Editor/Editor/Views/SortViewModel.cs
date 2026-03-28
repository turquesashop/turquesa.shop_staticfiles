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
	internal class SortViewModel : TabItemViewModel
	{
		public SortViewModel(string header) : base(header)
		{
		}
	}
}
