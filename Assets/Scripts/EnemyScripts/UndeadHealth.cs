using System.Collections;
using UnityEngine;

public class UndeadHealth : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    private Animator anim;
    private float currentHealth;
    private float maxHealth = 50000f;

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

        anim = GetComponent<Animator>();
    }

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
            StartCoroutine(DeadRoutin());
        }
    }

    private IEnumerator DeadRoutin()
    {
        GetComponent<UndeadScript>().enabled = false;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        UIManager.Instance.GameComplete();
    }
}
