using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayOrContinue : MonoBehaviour
{
    public GameObject playButton;
    public GameObject continueButton;
    private string saveLocation;

    private void Awake()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "SaveDate.json");
    }

    void Start()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            if (saveData.playerPosition != Vector3.zero)
            {
                continueButton.SetActive(true);
                playButton.SetActive(true);
            }
            else
            {
                continueButton.SetActive(false);
                playButton.SetActive(true);
            }
        }
        else
        {
            continueButton.SetActive(false);
            playButton.SetActive(true);
        }
    }
}
