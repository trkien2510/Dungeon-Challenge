using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public Vector3 playerPosition;
    public int currentLevel;
    public int currentExperience;
    public int expToNextLevel;
    public float currentHealth;
    public float maxHealth;
    public int damageStat;
    public float speedStat;
    public int statsPoints;
    public int score;
}
