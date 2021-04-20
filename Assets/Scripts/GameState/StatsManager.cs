using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public GameStats gameStats { get; private set; }

    private TextGenerator textGenerator;

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

    private void Start()
    {
        textGenerator = TextGenerator.instance;
    }

    // Gives player exp when enemy dies
    public void GainEnemyExp()
    {
        double toGain = UnityEngine.Random.Range(0.95f, 1.05f) * gameStats.enemyEXP;
        toGain = Math.Round(toGain, 0, MidpointRounding.AwayFromZero);
        gameStats.currentEXP += toGain;
        textGenerator.CreateEXPText(toGain);
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

    public void AdvanceStage()
    {
        gameStats.currentStage += 1;
        gameStats.stageEnemyKills = 0;
        gameStats.CalculateStageValues();
    }
}
