using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StatsManager sm;
    private CameraShake shaker;
    private SpriteRenderer sr;

    private Material whiteFlashMat;
    private Material defaultMat;

    private double health;
    private double expToGive;

    private void Start()
    {
        sm = StatsManager.instance;
        shaker = CameraShake.instance;
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        whiteFlashMat = GameData.instance.whiteFlashMat;
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
        StartCoroutine(FlashWhite());

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
        sm.GainEnemyExp();

        Destroy(gameObject);
    }

    private IEnumerator FlashWhite()
    {
        sr.material = whiteFlashMat;
        yield return new WaitForSeconds(0.1f);
        sr.material = defaultMat;
    }
}
