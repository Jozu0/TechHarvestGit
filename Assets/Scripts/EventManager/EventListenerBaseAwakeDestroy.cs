using UnityEngine;
using System;



public abstract class EventListenerBaseAwakeDestroy : MonoBehaviour
{
    // Les scripts enfants vont renvoyer un tableau d'événements à écouter
    protected abstract (GameEventType eventType, Action<object,float> handler)[] GetEventBindings();

    public void Awake()
    {
        foreach (var (eventType, handler) in GetEventBindings())
        {
            EventManager.Subscribe(eventType, handler);
        }
    }

    private void OnDestroy()
    {
        foreach (var (eventType, handler) in GetEventBindings())
        {
            EventManager.Unsubscribe(eventType, handler);
        }
    }
}