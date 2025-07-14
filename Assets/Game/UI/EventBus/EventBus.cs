using System;

public class EventBus<T>
{
    private event Action<T> OnEvent;

    public void Register(Action<T> listener)
    {
        OnEvent += listener;
    }
    public void Unregister(Action<T> listener)
    {
        OnEvent -= listener;
    }
    public void Invoke(T eventArgs)
    {
        OnEvent?.Invoke(eventArgs);
    }
}