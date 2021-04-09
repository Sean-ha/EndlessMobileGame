using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public GameStats gameStats { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameStats = new GameStats();
            Application.targetFrameRate = 60;

            DontDestroyOnLoad(gameObject);

            // Load game data here??

            // Also verify all statistics (e.g. check if currentEXP is >= expToLevel and if so, level up)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Gives player exp when enemy dies
    public void GainEnemyExp()
    {
        double toGain = Random.Range(0.9f, 1.1f) * gameStats.enemyEXP;
        gameStats.currentEXP += toGain;
    }

    public void LevelUp()
    {
        // Verify level up conditions are met
        if (gameStats.currentEXP >= gameStats.expToLevel)
        {
            gameStats.currentEXP -= gameStats.expToLevel;
            gameStats.currentLevel += 1;
            gameStats.CalculateLevelValues();
        }
    }
}
