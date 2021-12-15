using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerCrashedListener
{
    void OnPlayerCrashed(Player player);
}

[CreateAssetMenu(fileName = "PlayerCrashedEvent", menuName = "ScriptableObjects/PlayerCrashedEvent", order = 1)]
public class PlayerCrashedEvent : ScriptableObject
{
    private readonly List<IPlayerCrashedListener> _listeners =  new List<IPlayerCrashedListener>();

    public void AddListener(IPlayerCrashedListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(IPlayerCrashedListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Emit(Player player)
    {
        _listeners.ForEach((listener) => listener.OnPlayerCrashed(player));
    }
}
