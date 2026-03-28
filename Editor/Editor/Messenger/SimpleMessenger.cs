using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Messenger
{
	public class SimpleMessenger
	{
		public static SimpleMessenger Default { get; } = new SimpleMessenger();

		private readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

		public event Action<object, Type> OnMessageIntercepted;

		public void Register<TMessage>(Action<TMessage> action)
		{
			Type messageType = typeof(TMessage);
			if (!_subscribers.ContainsKey(messageType))
			{
				_subscribers[messageType] = new List<Action<object>>();
			}
			_subscribers[messageType].Add(param => action((TMessage)param));
		}

		public void Send<TMessage>(TMessage message)
		{
			Type messageType = typeof(TMessage);

			OnMessageIntercepted?.Invoke(message, messageType);

			if (_subscribers.ContainsKey(messageType))
			{
				foreach (var action in _subscribers[messageType])
				{
					action(message);
				}
			}
		}
	}

}
