using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static readonly int SpeedAnimation = Animator.StringToHash("speed");
    private static readonly int JumpAnimation = Animator.StringToHash("Jump");

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float dashCooldown = 3;
    [SerializeField] private float dashLenght = 5;
    [SerializeField] private float dashSpeed = 1;

    private Animator _animator;
    private Collider2D _collider2D;
    private EntityPlayer _player;
    private Rigidbody2D _rigidbody2D;

    private Vector3 dashPosition;
    private bool isDashing;
    private float dashCurrentCooldown;
    
    public void Start()
    {
        _player = GetComponent<EntityPlayer>();
        _rigidbody2D = _player.RigidBody2D;
        _animator = _player.Animator;
        _collider2D = _player.CapsuleCollider2D;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _player.IsGrounded())
        {
            _rigidbody2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            _animator.SetTrigger(JumpAnimation);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) DashStart();

        CalculateDashCooldown(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        Dashing();
    }

    private void Move()
    {
        var moveX = Input.GetAxis("Horizontal");
        if (moveX != 0)
        {
            var velocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector2(moveX * moveSpeed, velocity.y);

            _animator.SetFloat(SpeedAnimation, Mathf.Abs(velocity.x));

            _player.FlipSprite();
        }
    }

    private void CalculateDashCooldown(float value)
    {
        if (dashCurrentCooldown - value < 0)
            dashCurrentCooldown = 0;
        else
            dashCurrentCooldown -= value;
    }

    private void DashStart()
    {
        isDashing = true;
        dashPosition = CalculateDashPosition();
    }

    private void Dashing()
    {
        if (!isDashing || dashCurrentCooldown > 0)
        {
            isDashing = false;
            return;
        }

        dashCurrentCooldown -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position,
            dashPosition,
            dashSpeed
        );
        if (transform.position == dashPosition) DashEnd();
    }

    private void DashEnd()
    {
        isDashing = false;
        dashCurrentCooldown = dashCooldown;
    }

    private Vector3 CalculateDashPosition()
    {
        var directionX = _rigidbody2D.velocity.normalized.x;
        var position = transform.position;
        var raycastHit2D = Physics2D.Linecast(
            position,
            new Vector2(position.x + dashLenght * directionX, position.y),
            LayerMask.GetMask("Ground")
        );

        if (!raycastHit2D.collider)
            return new Vector3(
                position.x + directionX * dashLenght,
                position.y,
                0
            );

        return new Vector3(
            position.x + directionX * (raycastHit2D.distance - _collider2D.bounds.size.x / 2),
            position.y,
            0
        );
    }
}