using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPowerUpCollisionHandler : MonoBehaviour
{
    public GameConfig gameConfig;

    public IntegerValue time;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            time.Value += gameConfig.StarBonusTime;
            Destroy(gameObject);
        }
    }
}
