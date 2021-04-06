using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class GameStats : ISerializable
{
    public double playerDamage;

    public int currentLevel;
    public double currentEXP;
    public double expToLevel;

    public int currentStage;
    public int maxStage;

    public double enemyHealth;
    public double enemyEXP;

    public GameStats()
    {
        InitializeDefaultValues();
    }

    public void InitializeDefaultValues()
    {
        playerDamage = 3;
        currentEXP = 0;
        currentStage = 1;
        maxStage = 1;

        CalculateStageValues();
        CalculateLevelValues();
    }

    // Recalculates all values that rely on the currentStage. Formulas subject to change.
    public void CalculateStageValues()
    {
        if (currentStage > maxStage)
        {
            maxStage = currentStage;
        }

        double enemHP = 4 + currentStage + Math.Pow(currentStage, 1.31);
        enemyHealth = Math.Round(enemHP, 0, MidpointRounding.AwayFromZero);

        double enemEXP = 2 + Math.Pow(maxStage, 1.11) + Math.Pow(currentStage, 1.17);
        enemyEXP = Math.Round(enemEXP, 0, MidpointRounding.AwayFromZero);
    }

    // Recalculates all values that rely on the currentLevel. Formulas subject to change.
    public void CalculateLevelValues()
    {
        double toLevel = 50 * Math.Pow(currentLevel, 0.83) + Math.Pow(currentLevel, 1.29);
        expToLevel = Math.Round(toLevel, 0, MidpointRounding.AwayFromZero);
    }

    // SERIALIZE DATA HERE (automatically called when serializing)
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new System.NotImplementedException();
    }

    // DESERIALIZE DATA HERE (automatically called when deserializing)
    public GameStats(SerializationInfo info, StreamingContext context)
    {

    }
}
