using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    public static StageSystem instance;

    private StatsManager sm;
    private GameStats stats;

    [SerializeField]
    private Text stageText;
    [SerializeField]
    private Text enemiesRemainText;

    private int stageMaxEnemyKills = 20;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sm = StatsManager.instance;
        stats = sm.gameStats;

        // Update the stage and enemies remaining accordingly
        UpdateStageText();

        UpdateEnemiesRemainText();
    }

    public void OnEnemyDeath()
    {
        stats.stageEnemyKills += 1;

        if(stats.stageEnemyKills >= stageMaxEnemyKills)
        {
            // Create boss here ?
            AdvanceStage();
        }

        UpdateEnemiesRemainText();
    }

    private void UpdateEnemiesRemainText()
    {
        enemiesRemainText.text = stats.stageEnemyKills + " / " + stageMaxEnemyKills;
    }

    private void UpdateStageText()
    {
        stageText.text = stats.currentStage.ToString();
    }

    private void AdvanceStage()
    {
        sm.AdvanceStage();
        UpdateEnemiesRemainText();
        UpdateStageText();
    }
}
