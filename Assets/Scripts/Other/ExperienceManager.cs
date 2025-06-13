using System.Collections;
using System.Collections.Generic;
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
