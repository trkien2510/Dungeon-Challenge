using UnityEngine;

public class Peak : MonoBehaviour
{
    private int damage = 10;
    private float damageInterval = 1f;
    private float damageTimer = 0f;

    private void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                PlayerStats.Instance.TakeDamage(damage);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}
