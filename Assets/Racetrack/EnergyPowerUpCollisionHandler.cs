using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPowerUpCollisionHandler : MonoBehaviour
{
    public GameConfig gameConfig;

    public IntegerValue time;

    private SoundManager _soundManager;

    void Start()
    {
        _soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _soundManager.PlayItemPickedUp();

            time.Value += gameConfig.StarBonusTime;
            Destroy(gameObject);
        }
    }
}
