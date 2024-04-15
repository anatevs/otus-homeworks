using System;
using System.Collections.Generic;

public sealed class EventBus
{
    private readonly Dictionary<Type, EventBusHandlersList> _events = new();

    public void Subscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);

        if (!_events.ContainsKey(type))
        {
            _events.Add(type, new EventBusHandlersList());
        }

        _events[type].Subscribe(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);

        if (_events.ContainsKey(type))
        {
            _events[type].Unsubscribe(handler);
        }
    }

    public void RaiseEvent<T>(T evnt)
    {
        Type type = evnt.GetType();
        _events[type].RaiseEvent(evnt);
    }

    private sealed class EventBusHandlersList
    {
        private readonly List<Delegate> _handlers = new();

        private int _currentIndex = -1;

        public void Subscribe<T>(Action<T> handler)
        {
            _handlers.Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            int index = _handlers.IndexOf(handler);
            _handlers.RemoveAt(index);

            if (index <= _currentIndex)
            {
                _currentIndex--;
            }
        }

        public void RaiseEvent<T>(T evnt)
        {
            for (_currentIndex = 0; _currentIndex < _handlers.Count; _currentIndex++)
            {
                Action<T> handler = (Action<T>)_handlers[_currentIndex];
                handler.Invoke(evnt);
            }

            _currentIndex = -1;
        }
    }
}