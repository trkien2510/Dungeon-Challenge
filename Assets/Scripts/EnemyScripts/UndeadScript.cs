using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("for attack")]
    public GameObject meleeRange;
    public float attackRange = 1.5f;
    private bool canAttack = true;
    private bool isAttacking = false;
    public float attackCooldown = 2f;
    private float damage = 50;

    [Header("Summon")]
    public GameObject summonPrefab;
    public Transform summonPoint;
    public float summonInterval = 10f;

    [Header("for moving")]
    private Transform playerPosition;
    private Vector2 moveDirection;
    private Vector3 targetPosition;
    public float speed = 2f;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(TimeSummon());
    }

    void Update()
    {
        DecideTargetPosition();
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        if (distanceToPlayer > attackRange)
        {
            moveDirection = (playerPosition.position - transform.position).normalized;
            rb.velocity = moveDirection * speed;

            if ((moveDirection.x > 0 && !isFacingRight) || (moveDirection.x < 0 && isFacingRight))
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (canAttack)
            {
                StartCoroutine(TimeAttack());
            }
        }
    }

    private void DecideTargetPosition()
    {
        if (playerPosition == null) return;

        targetPosition = playerPosition.position;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(damage);
        }
    }

    IEnumerator TimeAttack()
    {
        canAttack = false;
        isAttacking = true;
        anim.SetBool("Attack", true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        meleeRange.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack", false);
        meleeRange.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        canAttack = true;
    }

    private void Summon()
    {
        GameObject ghost = Instantiate(summonPrefab, summonPoint.position, Quaternion.identity);

        GhostScript ghostScript = ghost.GetComponent<GhostScript>();
        UndeadHealth undeadHealth = GetComponent<UndeadHealth>();

        if (ghostScript != null && undeadHealth != null)
        {
            ghostScript.SetSummoner(undeadHealth);
        }
    }


    IEnumerator TimeSummon()
    {
        yield return new WaitForSeconds(7f);
        while (true)
        {
            anim.SetBool("Summon", true);
            anim.SetBool("Attack", false);
            yield return new WaitForSeconds(1f);
            Summon();
            anim.SetBool("Summon", false);
            yield return new WaitForSeconds(summonInterval);
        }
    }
}
