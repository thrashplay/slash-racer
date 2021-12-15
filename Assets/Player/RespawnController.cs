using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerRespawner
{
    // Respawns the player at a position on the track indicated by the progress value.
    void Respawn(float progress);
}

public class RespawnController : MonoBehaviour, IPlayerRespawner
{
    public GameObject playerPrefab;

    public float respawnDelay = 3;

    // timestamp at which the player should be spawned
    private float respawnTime = float.MaxValue;

    // y-coordinate to spawn the player at
    private float spawnY;

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup >= respawnTime)
        {
            Instantiate(playerPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);
            respawnTime = float.MaxValue;
        }
    }

    //
    // IPlayerRespawner
    //

    public void Respawn(float progress)
    {
        spawnY = progress;
        respawnTime = Time.realtimeSinceStartup + respawnDelay;
    }
}
