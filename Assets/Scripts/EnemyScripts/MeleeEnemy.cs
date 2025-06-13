using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("for attack")]
    public GameObject meleeRange;
    private bool canAttack = false;
    private bool isAttacking = false;
    private float attackDuration = 2f;
    private float damage = 10;

    [Header("for moving")]
    private Transform playerPosition;
    private Vector2 moveDirection;
    private Vector3 targetPosition;
    private float speed = 3f;
    private bool isFacingRight = true;
    private bool isMoving;
    private bool playerInRange = false;

    [Header("health")]
    public EnemyHealthBar healthBar;
    private float health;
    private float maxHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        maxHealth = 30;
    }

    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
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
            anim.SetBool("Running", false);
            return;
        }

        if (!playerInRange)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Running", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, targetPosition);
        if (distance > 0.1f && isMoving)
        {
            moveDirection = (targetPosition - transform.position).normalized;
            rb.velocity = moveDirection * speed;
            anim.SetBool("Running", true);
        }
        else if (!isMoving && playerInRange)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Running", false);
            canAttack = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Running", false);
            if (!canAttack)
            {
                canAttack = true;
                StartCoroutine(TimeAttack());
            }
        }
    }

    private void DecideTargetPosition()
    {
        if (playerPosition == null)
        {
            playerInRange = false;
            isMoving = false;
            anim.SetBool("Running", false);
            return;
        }

        playerInRange = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));

        if (!playerInRange)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
            anim.SetBool("Running", false);
            return;
        }

        Vector3 leftPlayer = new Vector3(playerPosition.position.x - 0.75f, playerPosition.position.y + 0.25f, 0);
        Vector3 rightPlayer = new Vector3(playerPosition.position.x + 0.75f, playerPosition.position.y + 0.25f, 0);

        float distanceToLeft = Vector2.Distance(transform.position, leftPlayer);
        float distanceToRight = Vector2.Distance(transform.position, rightPlayer);

        bool wallLeft = Physics2D.Raycast(transform.position, (leftPlayer - transform.position).normalized, distanceToLeft, LayerMask.GetMask("Wall"));
        bool wallRight = Physics2D.Raycast(transform.position, (rightPlayer - transform.position).normalized, distanceToRight, LayerMask.GetMask("Wall"));

        if (distanceToLeft < distanceToRight && !wallLeft)
        {
            targetPosition = leftPlayer;
            if (!isFacingRight) Flip();
            isMoving = true;
        }
        else if (!wallRight)
        {
            targetPosition = rightPlayer;
            if (isFacingRight) Flip();
            isMoving = true;
        }
        else
        {
            isMoving = false;
            anim.SetBool("Running", false);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator TimeAttack()
    {
        isAttacking = true;
        anim.SetBool("Attacking", true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.7f);
        meleeRange.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Attacking", false);
        meleeRange.SetActive(false);
        yield return new WaitForSeconds(attackDuration);
        canAttack = false;
        isAttacking = false;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(damage);
        }
    }

    IEnumerator WaitToDead()
    {
        anim.SetTrigger("Dead");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.skeletonDead);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        ExperienceManager.Instance.AddExperience(10);
        Destroy(gameObject);
    }
}