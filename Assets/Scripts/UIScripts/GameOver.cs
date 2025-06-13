using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;

    public CanvasGroup gameOverGroup;
    public float fadeDuration = 2f;
    public float delayBeforeHome = 3f;

    private void Awake()
    {
        Instance = this;
        gameOverGroup.alpha = 0f;
        gameOverGroup.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeInAndGoHome());
    }

    private IEnumerator FadeInAndGoHome()
    {
        gameOverGroup.gameObject.SetActive(true);
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            gameOverGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeHome);

        SceneManager.LoadScene("Home");
    }
}
