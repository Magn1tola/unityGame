using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private HPController hpController;

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float damage = 1;


    private float cooldown = 1;
    private float currentCooldown = 0;
    private bool isFalling = false;
    private bool blockInput = false;


    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        hpController = GetComponent<HPController>();

        hpController.OnApplyDamage += ApplyDamage;
    }

    private void Update()
    {

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            blockInput = true;
        }
        else
            blockInput = false;

        isFalling = !IsGrounded();
        animator.SetBool("isFalling", isFalling);

        if (Input.GetKeyDown(KeyCode.W))
            Jump();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
            MoveX(Input.GetAxis("Horizontal"));
    }

    private void MoveX(float direction) // Функция говно полное ПЕРЕДЕЛАТЬ
    {
        if (blockInput)
            return;

        rigidBody2D.velocity = new Vector2(direction * speed, rigidBody2D.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(rigidBody2D.velocity.x));

        Flip();
    }


    private void Jump()
    {
        if (!IsGrounded() || blockInput)
            return;

        rigidBody2D.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    private void Flip()
    {
        if (rigidBody2D.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rigidBody2D.velocity.x < 0)
            spriteRenderer.flipX = true;
    }

    private bool IsGrounded()
    {
        Vector2 circlePosition = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - (boxCollider2D.size.y * transform.lossyScale.y / 2f));
        float circleRadius = boxCollider2D.size.x * transform.lossyScale.x / 2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circlePosition, circleRadius);
        return colliders.Length > 1;
    }

    private void ApplyDamage(float damage, GameObject instigator)
    {
        if (damage <= 0)
            return;

        float direction;
        if (instigator.transform.position.x > gameObject.transform.position.x)
            direction = -1f;
        else
            direction = 1f;

        currentCooldown = cooldown;

        rigidBody2D.velocity = new Vector2(0f, 0f);
        rigidBody2D.AddForce(transform.right * direction * 2f + transform.up * 5f, ForceMode2D.Impulse);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        float attackDistance = 1f;
        Vector2 startposition = new Vector2(boxCollider2D.bounds.center.x + boxCollider2D.size.x, boxCollider2D.bounds.center.y);
        RaycastHit2D[] raycastHits2D = Physics2D.BoxCastAll(startposition, boxCollider2D.bounds.size, 0f, Vector2.right, attackDistance);

        foreach (RaycastHit2D hit in raycastHits2D)
        {
            if (hit.collider.gameObject == gameObject)
                continue;

            if (hit.collider.gameObject.TryGetComponent(out IDamage damageable))
                damageable.ApplyDamage(damage, gameObject);
        }

    }
}
