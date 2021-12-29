using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IPlayerCrashedListener
{
    public GameConfig gameConfig;

    public BooleanValue gameOver;

    public BooleanValue isPaused;

    public IntegerValue extraLivesRemaining;

    public PlayerCrashedEvent playerCrashedEvent;

    public IntegerValue score;

    public SoundManager soundManager;

    public IntegerValue time;

    private IPlayerRespawner _respawner;

    void Start()
    {
        extraLivesRemaining.Value = gameConfig.StartingLives - 1;
        score.Value = 0;

        playerCrashedEvent.AddListener(this);

        _respawner = GetComponent<IPlayerRespawner>();
        _respawner.Respawn(0);

        // start unpaused by default
        Time.timeScale = 1;
        isPaused.Value = false;

        gameOver.Value = false;
    }

    void Update()
    {
        if (gameConfig.TimeLimitEnabled && time.Value < 1)
        {
            time.Value = 0;
            OnGameOver();
        }
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
        if (!gameOver.Value)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            soundManager.PlayEngine(EngineSound.None);
            soundManager.PlayExplosion();
            Invoke(nameof(EndScene), gameConfig.DelayAfterDeath);

            gameOver.Value = true;
        }
    }

    private void EndScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    //
    // IPlayerCrashedListener
    //

    public void OnPlayerCrashed(Player player)
    {
        OnGameOver();
    }
}
