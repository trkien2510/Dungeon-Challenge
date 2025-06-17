using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image bg;
    public TextMeshProUGUI txtGameOver;
    public float fadeDuration = 2f;
    public float delayBeforeHome = 3f;

    private void Awake()
    {
        Instance = this;
        if (bg != null && txtGameOver != null)
        {
            bg.gameObject.SetActive(false);
            txtGameOver.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        StartCoroutine(FadeInCoroutine());
        SaveSystem.DeleteSave();
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsed = 0f;
        Color color = bg.color;
        bg.gameObject.SetActive(true);
        txtGameOver.gameObject.SetActive(true);
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            bg.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(delayBeforeHome);
        SceneManager.LoadScene("Home");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SaveSystem.DeleteSave();
    }

    public void Continue()
    {
        LoadGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SaveSystem.DeleteSave();
    }

    public void QuitToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
        SaveGame();
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlusHP()
    {
        if (PlayerStats.Instance.statsPoints > 0)
        {
            PlayerStats.Instance.maxHealth += 10;
            PlayerStats.Instance.statsPoints--;
            PlayerStats.Instance.healthBar.SetMaxHealth(PlayerStats.Instance.maxHealth);
        }
    }

    public void PlusDMG()
    {
        if (PlayerStats.Instance.statsPoints > 0)
        {
            PlayerStats.Instance.damage += 2;
            PlayerStats.Instance.statsPoints--;
        }
    }

    public void PlusSPD()
    {
        if (PlayerStats.Instance.statsPoints > 0)
        {
            PlayerStats.Instance.speed += 0.25f;
            PlayerStats.Instance.statsPoints--;
        }
    }

    public void MinusHP()
    {
        if (PlayerStats.Instance.maxHealth > 100)
        {
            PlayerStats.Instance.maxHealth -= 10;
            PlayerStats.Instance.statsPoints++;
            if (PlayerStats.Instance.currentHealth > PlayerStats.Instance.maxHealth)
            {
                PlayerStats.Instance.currentHealth = PlayerStats.Instance.maxHealth;
            }
        }
    }

    public void MinusDMG()
    {
        if (PlayerStats.Instance.damage > 10)
        {
            PlayerStats.Instance.damage -= 2;
            PlayerStats.Instance.statsPoints++;
        }
    }

    public void MinusSPD()
    {
        if (PlayerStats.Instance.speed > 5f)
        {
            PlayerStats.Instance.speed -= 0.25f;
            PlayerStats.Instance.statsPoints++;
        }
    }
}
