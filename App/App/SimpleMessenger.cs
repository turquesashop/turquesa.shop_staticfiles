

namespace App;

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
		if (message is null) throw new ArgumentNullException(nameof(message));
		Type messageType = typeof(TMessage);
		OnMessageIntercepted?.Invoke(message, messageType);
		if (_subscribers.TryGetValue(messageType, out _)) 
		{
			foreach (var action in _subscribers[messageType])
			{
				action(message);
			}
		}
	}
}
