using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StatsManager sm;
    private CameraShake shaker;

    private double health;
    private double expToGive;

    private void Start()
    {
        sm = StatsManager.instance;
        shaker = CameraShake.instance;
        OnSpawn();
    }

    // Called when the enemy is spawned in.
    private void OnSpawn()
    {
        SetValues();
    }

    // Initializes enemy's stats
    private void SetValues()
    {
        health = sm.gameStats.enemyHealth;
        expToGive = sm.gameStats.enemyHealth;
    }

    // Called when hit by player
    public void TakeDamage(double toTake)
    {
        shaker.ShakeCamera();
        health -= toTake;
        TextGenerator.instance.CreateDamageText(toTake.ToString(), (Vector2)transform.position - new Vector2(0, 0.3f));
        if (health <= 0)
        {
            Die();
        }
    }

    // Called when enemy is to die
    private void Die()
    {
        // TODO: Give player EXP

        Destroy(gameObject);
    }
}
