using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public float currentHealth;
    public float maxHealth = 100000f;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
