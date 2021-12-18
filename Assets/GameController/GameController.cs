using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IPlayerCrashedListener
{
    public int initialPlayerLives = 3;

    public BooleanValue isPaused;

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

        // start unpaused by default
        Time.timeScale = 1;
        isPaused.Value = false;
    }

    public void OnPause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            isPaused.Value = true;
        }
    }

    public void OnUnpause()
    {
        if (Time.timeScale != 1) 
        {
            Time.timeScale = 1;
            isPaused.Value = false;
        }
    }

    // called when the player quits the game
    public void OnGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
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
        else
        {
            OnGameOver();
        }
    }
}
