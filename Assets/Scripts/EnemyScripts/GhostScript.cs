using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private UndeadHealth undeadHealth;

    [Header("health")]
    public EnemyHealthBar healthBar;
    private float health;
    private float maxHealth = 50;

    [Header("Movement")]
    public float moveSpeed = 3f;
    private Vector2 moveDirection;
    private float changeDirectionTime = 1f;
    private float changeDirectionTimer;

    public float wallCheckDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        if (anim != null)
        {
            StartCoroutine(WaitToIdle());
        }

        PickRandomDirection();
        changeDirectionTimer = changeDirectionTime;
    }

    void Update()
    {
        Move();

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0f)
        {
            PickRandomDirection();
            changeDirectionTimer = changeDirectionTime;
        }

        if (IsHittingWall())
        {
            PickRandomDirection();
            changeDirectionTimer = changeDirectionTime;
        }
    }

    void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void PickRandomDirection()
    {
        int axis = Random.Range(0, 2);
        float dir = Random.value < 0.5f ? -1f : 1f;
        if (axis == 0)
            moveDirection = new Vector2(dir, axis).normalized;
        else
            moveDirection = new Vector2(axis, dir).normalized;
    }

    bool IsHittingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, wallCheckDistance, LayerMask.GetMask("Wall"));
        return hit.collider != null;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(WaitToDead());
        }
    }

    IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Idle");
    }

    IEnumerator WaitToDead()
    {
        anim.SetTrigger("Dead");
        if (undeadHealth != null)
        {
            undeadHealth.TakeDamage(5000);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void SetSummoner(UndeadHealth undead)
    {
        undeadHealth = undead;
    }
}
