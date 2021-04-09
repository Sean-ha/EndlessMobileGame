using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    private StatsManager sm;
    private GameStats stats;
    private GameObject barFill;

    private double displayEXP;
    private bool isFilling;
    // If the player should be levelling up
    private bool toLevel = false;

    private void Awake()
    {
        instance = this;
        barFill = transform.Find("LevelBarFill").gameObject;
    }

    private void Start()
    {
        sm = StatsManager.instance;
        stats = sm.gameStats;

        // Sets the bar to the correct length on start
        barFill.transform.localScale = new Vector2((float)(stats.currentEXP / stats.expToLevel), 1);
    }

    private void Update()
    {
        float ratio = (float)(stats.currentEXP / stats.expToLevel);
        if (barFill.transform.localScale.x < ratio)
        {
            if (isFilling)
            {
                // Cannot cancel out of a level up
                if (!toLevel)
                {
                    LeanTween.cancel(barFill);
                    IncreaseBar();
                }
            }
            else
            {
                isFilling = true;
                IncreaseBar();
            }
        }
    }

    private void IncreaseBar()
    {
        float ratio = (float)(stats.currentEXP / stats.expToLevel);

        // Bar is filled up
        if (ratio >= 1)
        {
            toLevel = true;
            LeanTween.scaleX(barFill, 1, 0.6f).setEaseOutExpo().setOnComplete(LevelUp);
        }
        else
        {
            LeanTween.scaleX(barFill, ratio, 0.6f).setEaseOutQuad().setOnComplete(StopFilling);
        }
    }

    private void StopFilling()
    {
        isFilling = false;
    }

    private void LevelUp()
    {
        sm.LevelUp();
        LeanTween.scaleX(barFill, (float)(stats.currentEXP / stats.expToLevel), 0.6f).setEaseOutQuad().setOnComplete(StopFilling);
        toLevel = false;
    }
}
