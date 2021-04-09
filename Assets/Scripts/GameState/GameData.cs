using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public Material whiteFlashMat;

    private void Awake()
    {
        instance = this;    
    }
}
