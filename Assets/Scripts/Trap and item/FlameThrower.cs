using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    private int damage = 10;
    private float damageInterval = 2f;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        StartCoroutine(Activated());
    }

    IEnumerator Activated()
    {
        while (true)
        {
            boxCollider2D.enabled = true;
            anim.SetBool("Flame", true);
            yield return new WaitForSeconds(1f);
            boxCollider2D.enabled = false;
            anim.SetBool("Flame", false);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(damage);
        }
    }
}
