using UnityEngine;
using System;



public abstract class EventListenerBase : MonoBehaviour
{
    // Les scripts enfants vont renvoyer un tableau d'événements à écouter
    protected abstract (GameEventType eventType, Action<object,float> handler)[] GetEventBindings();

    public void OnEnable()
    {
        foreach (var (eventType, handler) in GetEventBindings())
        {
            EventManager.Subscribe(eventType, handler);
        }
    }

    private void OnDisable()
    {
        foreach (var (eventType, handler) in GetEventBindings())
        {
            EventManager.Unsubscribe(eventType, handler);
        }
    }
}