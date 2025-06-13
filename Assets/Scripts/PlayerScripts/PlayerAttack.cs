using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    private Animator weaponAnim;
    private bool canAttack = true;
    private float timeAttack = 0.5f;
    private float attackDuration = 0.25f;

    void Start()
    {
        weaponAnim = weapon.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.timeScale == 1)
        {
            if (canAttack)
            {
                weapon.SetActive(true);
                weaponAnim.SetTrigger("Attack");
                canAttack = false;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.swordSwing);
                StartCoroutine(TimeAttack());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        MeleeEnemy enemy = collider.gameObject.GetComponent<MeleeEnemy>();
        GhostScript enemyGhost = collider.gameObject.GetComponent<GhostScript>();
        UndeadHealth enemyUndead = collider.gameObject.GetComponent<UndeadHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(PlayerStats.Instance.damage);
        }
        else if (enemyGhost != null)
        {
            enemyGhost.TakeDamage(PlayerStats.Instance.damage);
        }
        else if (enemyUndead != null)
        {
            enemyUndead.TakeDamage(PlayerStats.Instance.damage);
        }
    }

    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(timeAttack);
        weaponAnim.SetTrigger("Attack");
        weapon.SetActive(false);
        yield return new WaitForSeconds(attackDuration);
        canAttack = true;
    }
}
