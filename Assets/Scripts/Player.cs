using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Wallet))]
public class Player : BaseCharacter
{
    private Animator animator;
    private Wallet wallet;

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 5;


    private float cooldown = 1;
    private float currentCooldown = 0;
    private bool isFalling = false;
    private bool blockInput = false;


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        wallet = GetComponent<Wallet>();
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

    private bool IsGrounded()
    {
        Vector2 circlePosition = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - (boxCollider2D.size.y * transform.lossyScale.y / 2f));
        float circleRadius = boxCollider2D.size.x * transform.lossyScale.x / 2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circlePosition, circleRadius);
        return colliders.Length > 1;
    }

    protected override void ApplyDamage(float damage, GameObject instigator)
    {
        base.ApplyDamage(damage, instigator);
    }

    protected override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Attack");
    }

    protected override void Dead()
    {
        base.Dead();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }


}
