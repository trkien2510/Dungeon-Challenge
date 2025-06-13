using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public HealthBar healthBar;

    public float currentHealth;
    public float maxHealth = 100f;
    public int damage = 10;
    public float speed = 5f;
    public int statsPoints = 0;

    private bool isDead = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (currentHealth <= 0f || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            PlayerAttack playerAttack = GetComponent<PlayerAttack>();
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            StartCoroutine(FadeOutAndDisappear());
        }
    }

    private IEnumerator FadeOutAndDisappear()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1.5f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            foreach (var renderer in renderers)
            {
                Color c = renderer.color;
                c.a = alpha;
                renderer.color = c;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        foreach (var renderer in renderers)
        {
            Color c = renderer.color;
            c.a = 0f;
            renderer.color = c;
        }

        gameObject.SetActive(false);
        UIManager.Instance.GameOver();
    }
}
