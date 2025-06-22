using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] GameObject weapon;

    Vector2 input;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            ProccessInputs();
            Animate();
            WeaponDirection();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = input * PlayerStats.Instance.speed;
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (input.x != 0 || input.y != 0)
        {
            moveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("Running", input.magnitude);
        anim.SetFloat("LastMoveX", moveDirection.x);
        anim.SetFloat("LastMoveY", moveDirection.y);
    }

    void WeaponDirection()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;

        weapon.transform.rotation = Quaternion.Euler(0, 0, snappedAngle + 135f);
    }
}
