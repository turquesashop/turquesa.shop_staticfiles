using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Configuration;

namespace Editor.Messenger
{
	public partial class MessengerLogViewModel : ObservableObject
	{
		public ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();

		public MessengerLogViewModel()
		{
			SimpleMessenger.Default.OnMessageIntercepted += LogMessage;
		}

		private void LogMessage(object message, Type messageType)
		{
			string time = DateTime.Now.ToString("HH:mm:ss.fff");
			string header = $"[{time}] {messageType.Name}";
			string payload = JsonSerializer.Serialize(message, JsonDefaults.Options);
			string fullLogEntry = $"{header}\n{payload}\n-------------------------";
			Application.Current.Dispatcher.Invoke(() =>
			{
				//Logs.Add(fullLogEntry);
				Logs.Insert(0, fullLogEntry);
			});
		}
	}

}
