using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnObstacleListener
{
    void OnSpawnObstacle();
}

[CreateAssetMenu(fileName = "SpawnObstacleEvent", menuName = "ScriptableObjects/SpawnObstacleEvent", order = 1)]
public class SpawnObstacleEvent : ScriptableObject
{
    private readonly List<ISpawnObstacleListener> _listeners =  new List<ISpawnObstacleListener>();

    public void AddListener(ISpawnObstacleListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(ISpawnObstacleListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Emit()
    {
        _listeners.ForEach((listener) => listener.OnSpawnObstacle());
    }
}
