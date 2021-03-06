using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    private bool hasCollided = false;

    private void OnEnable()
    {
        hasCollided = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collision with an enemy
        if (collision.gameObject.layer == 8 && !hasCollided)
        {
            hasCollided = true;
            double dmg = StatsManager.instance.gameStats.playerDamage * UnityEngine.Random.Range(0.85f, 1.15f);
            dmg = Math.Round(dmg, 0, MidpointRounding.AwayFromZero);

            collision.GetComponent<Enemy>().TakeDamage(dmg);
            gameObject.SetActive(false);
        }
    }
}
