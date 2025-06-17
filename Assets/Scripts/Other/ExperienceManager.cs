using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public GameObject txtLevel;
    public GameObject sliderExperience;

    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 50;

    void Start()
    {
        Instance = this;
        if (File.Exists(Path.Combine(Application.persistentDataPath, "SaveDate.json")))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "SaveDate.json")));
            currentLevel = saveData.currentLevel;
            currentExperience = saveData.currentExperience;
            experienceToNextLevel = saveData.expToNextLevel;
        }
        AddExperience(0);
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        while (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }

        txtLevel.GetComponent<TextMeshProUGUI>().text = "Lv: " + currentLevel.ToString();
        sliderExperience.GetComponent<Slider>().maxValue = experienceToNextLevel;
        sliderExperience.GetComponent<Slider>().value = currentExperience;
    }

    void LevelUp()
    {
        currentExperience -= experienceToNextLevel;
        currentLevel++;
        experienceToNextLevel += experienceToNextLevel;
        PlayerStats.Instance.statsPoints++;
    }
}
