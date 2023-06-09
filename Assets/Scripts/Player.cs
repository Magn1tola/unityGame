using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float fallingThreshold = -0;

    [SerializeField] private int maxHp = 3;

    private int curHp;
    private bool isFalling = false;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        curHp = maxHp;
    }

    private void Update()
    {

        isFalling = (rigidbody2D.velocity.y < fallingThreshold);
        animator.SetBool("isFalling", isFalling);


        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetAxis("Horizontal") != 0)
            MoveX(Input.GetAxis("Horizontal"));
    }

    private void FixedUpdate() { }

    private void MoveX(float direction) // Функция говно полное ПЕРЕДЕЛАТЬ
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x));
        Flip();
    }


    private void Jump()
    {
        if (!IsGrounded())
            return;

        rigidbody2D.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    private void Flip()
    {
        if (rigidbody2D.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rigidbody2D.velocity.x < 0)
            spriteRenderer.flipX = true;
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boxCollider2D.bounds.center, 1f);
        return colliders.Length > 1;
    }
}
