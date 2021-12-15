using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IPlayerCrashedListener
{
    public int initialPlayerLives = 3;

    public IntegerValue playerLives;

    public PlayerCrashedEvent playerCrashedEvent;

    public IntegerValue score;

    private IPlayerRespawner _respawner;

    void Start()
    {
        playerLives.Value = initialPlayerLives;
        score.Value = 0;

        playerCrashedEvent.AddListener(this);

        _respawner = GetComponent<IPlayerRespawner>();
        _respawner.Respawn(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    // IPlayerCrashedListener
    //

    public void OnPlayerCrashed(Player player)
    {
        playerLives.Value -= 1;
        if (playerLives.Value >= 0)
        {
            _respawner.Respawn(player.Position.y);
        }
    }
}
