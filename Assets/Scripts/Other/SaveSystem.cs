using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private static string saveLocation = Path.Combine(Application.persistentDataPath, "SaveDate.json");

    public static void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            currentLevel = ExperienceManager.Instance.currentLevel,
            currentExperience = ExperienceManager.Instance.currentExperience,
            expToNextLevel = ExperienceManager.Instance.experienceToNextLevel,
            currentHealth = PlayerStats.Instance.currentHealth,
            maxHealth = PlayerStats.Instance.maxHealth,
            damageStat = PlayerStats.Instance.damage,
            speedStat = PlayerStats.Instance.speed,
            statsPoints = PlayerStats.Instance.statsPoints,
            sceneName = SceneManager.GetActiveScene().name
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public static void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            SceneManager.LoadScene(saveData.sceneName);

            UnityEngine.Events.UnityAction<Scene, LoadSceneMode> onSceneLoaded = null;
            onSceneLoaded = (scene, mode) =>
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
                ExperienceManager.Instance.currentLevel = saveData.currentLevel;
                ExperienceManager.Instance.currentExperience = saveData.currentExperience;
                ExperienceManager.Instance.experienceToNextLevel = saveData.expToNextLevel;
                PlayerStats.Instance.currentHealth = saveData.currentHealth;
                PlayerStats.Instance.maxHealth = saveData.maxHealth;
                PlayerStats.Instance.damage = saveData.damageStat;
                PlayerStats.Instance.speed = saveData.speedStat;
                PlayerStats.Instance.statsPoints = saveData.statsPoints;

                SceneManager.sceneLoaded -= onSceneLoaded;
            };
            SceneManager.sceneLoaded += onSceneLoaded;
        }
        else
        {
            SaveGame();
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
        }
    }
}
