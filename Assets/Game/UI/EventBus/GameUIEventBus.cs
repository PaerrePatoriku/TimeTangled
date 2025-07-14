using Assets.Game.UI.EventBus.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class TestArgs : EventArgs
{
    public string ArgA { get; set; }
}

//Container luokka jokaiselle UI event bussille.
public class GameUIEventBus : MonoBehaviour
{
    //TODO: Tarvitaan enemm‰n lifecycle hallintaa.
    private Dictionary<Type, object> eventBuses = new();
    public void Register<T>(Action<T> listener) where T : EventArgs
    {
        GetEventBus<T>().Register(listener);
    }
    public void Unregister<T>(Action<T> listener) where T : EventArgs
    {
        GetEventBus<T>().Unregister(listener);
    }
    public virtual void UpdateEvent<T>(T eArgs) where T : EventArgs
    {
        Debug.Log(eArgs);
        GetEventBus<T>().Invoke(eArgs);
        //if (eventBuses.TryGetValue(typeof(T), out var bus))
        //{
            //var typedBus = bus as EventBus<T>;
            //typedBus.Invoke(eArgs);
        //}
    }
    public EventBus<T> GetEventBus<T>() where T : EventArgs
    {
        if (!eventBuses.TryGetValue(typeof(T), out var bus))
        {
            var newBus = new EventBus<T>();
            eventBuses[typeof(T)] = newBus;
            return newBus;
        };
        return (EventBus<T>)bus;
    }
    private AttributedDelegate[] GetInstanceAttributedMethods(MonoBehaviour monoObject)
    {
        List<AttributedDelegate> delegates = new List<AttributedDelegate>();
        var methods = monoObject.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute<UISignal>();
            if (attr == null) continue;

            var type = method.GetParameters()[0].ParameterType; //Setataan eventType lennosta sill‰ attributella ei ole kontekstia. Pit‰‰ p‰‰tell‰ metodista
            attr.EventType = type;

            var eventAction = method.CreateDelegate(typeof(Action<>).MakeGenericType(type), monoObject);
            if (eventAction == null)
            {
                Debug.LogError("Cannot create registry for " + method.Name + "with attribute " + attr.ToString());
                continue;
            }
   
            delegates.Add(new AttributedDelegate(type, eventAction));
        }
        return delegates.ToArray();
    }
    public void RegisterInstance(MonoBehaviour monoObject)
    {
        var methods = GetInstanceAttributedMethods(monoObject);
        foreach (var attributedMethod in methods)
        {
            typeof(GameUIEventBus)
            .GetMethod(nameof(Register))
            .MakeGenericMethod(attributedMethod.type)
            .Invoke(this, new object[] { attributedMethod.eventAction });
        }

    }
    public void UnregisterInstance(MonoBehaviour monoObject)
    {
        var methods = GetInstanceAttributedMethods(monoObject);
        foreach (var attributedMethod in methods)
        {
            typeof(GameUIEventBus)
            .GetMethod(nameof(Unregister))
            .MakeGenericMethod(attributedMethod.type)
            .Invoke(this, new object[] { attributedMethod.eventAction });
        }

    }
}
class AttributedDelegate
{

    public Type type;
    public Delegate eventAction;

    public AttributedDelegate(Type type, Delegate eventAction)
    {
        this.type = type;
        this.eventAction = eventAction;
    }
}
