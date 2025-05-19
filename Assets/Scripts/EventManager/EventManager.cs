using System;
using System.Collections.Generic;

// Types d'événements possibles dans notre jeu
public enum GameEventType
{
    GameStart,
    GameOver,
    RessourceHit,
    RessourceDead,
    RessourceInitialization
}

// Classe statique pour gérer les événements globaux
public static class EventManager
{
    // Dictionnaire stockant les différents événements et leurs abonnés
    private static Dictionary<GameEventType, Action<object, float>> eventDictionary = 
        new Dictionary<GameEventType, Action<object,float>>();
    
    // S'abonner à un événement
    public static void Subscribe(GameEventType eventType, Action<object,float> listener)
    {
        eventDictionary.TryAdd(eventType, null);
        
        eventDictionary[eventType] += listener;
    }
    
    // Se désabonner d'un événement
    public static void Unsubscribe(GameEventType eventType, Action<object,float> listener)
    {
        if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType] -= listener;
        }
    }
    
    // Déclencher un événement
    public static void TriggerEvent(GameEventType eventType, object data = null, float value = 0f)
    {
        if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType].Invoke(data,value);
        }
    }
}