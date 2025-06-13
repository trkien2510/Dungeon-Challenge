using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flank : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerStats.Instance.currentHealth < PlayerStats.Instance.maxHealth)
            {
                PlayerStats.Instance.currentHealth += PlayerStats.Instance.maxHealth * 0.15f;
                Destroy(gameObject);
            }
        }
    }
}
