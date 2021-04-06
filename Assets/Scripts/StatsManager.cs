using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public GameStats gameStats;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameStats = new GameStats();
            DontDestroyOnLoad(gameObject);

            // Load game data here??
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
