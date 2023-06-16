using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float MaxMoveSpeed = 10f;

    private static readonly int WalkSpeedAnimation = Animator.StringToHash("walkSpeed");
    private static readonly int IsWalkingAnimation = Animator.StringToHash("isWalking");
    private static readonly int JumpAnimation = Animator.StringToHash("Jump");

    public static bool BlockInput;

    [SerializeField] [Range(1f, MaxMoveSpeed)]
    private float moveSpeed = 6;

    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float dashCooldown = 3;
    [SerializeField] private float dashLenght = 5;
    [SerializeField] private float dashSpeed = 1;

    private Animator _animator;
    private Collider2D _collider2D;
    private float _dashCooldown;
    private EntityPlayer _player;
    private Rigidbody2D _rigidbody2D;

    private Vector3 dashPosition;
    private bool isDashing;

    public void Start()
    {
        _player = GetComponent<EntityPlayer>();
        _rigidbody2D = _player.RigidBody2D;
        _animator = _player.Animator;
        _collider2D = _player.CapsuleCollider2D;
    }

    private void Update()
    {
        if (BlockInput) return;

        if (Input.GetKeyDown(KeyCode.W) && _player.IsGrounded())
        {
            _animator.SetTrigger(JumpAnimation);
            _rigidbody2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) DashStart();

        CalculateDashCooldown(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (BlockInput) return;

        Move();
        Dashing();
    }

    private void Move()
    {
        _animator.SetFloat(WalkSpeedAnimation, moveSpeed / MaxMoveSpeed);

        var moveX = Input.GetAxis("Horizontal");
        if (moveX != 0)
        {
            var velocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector2(moveX * moveSpeed, velocity.y);

            _animator.SetBool(IsWalkingAnimation, true);

            _player.FlipSprite();
        }
        else _animator.SetBool(IsWalkingAnimation, false);
    }

    private void CalculateDashCooldown(float value)
    {
        if (_dashCooldown - value < 0)
            _dashCooldown = 0;
        else _dashCooldown -= value;
    }

    private void DashStart()
    {
        if (isDashing || _dashCooldown > 0 || _rigidbody2D.velocity.x == 0) return;
        isDashing = true;
        dashPosition = CalculateDashPosition();

        var position = transform.position;
        var effectPosition = new Vector3(
            (position.x + dashPosition.x) / 2,
            position.y - 0.5f,
            0
        );

        var effectRotation = dashPosition.x > position.x
            ? new Quaternion(0, 0, 0, 0)
            : new Quaternion(0, 0, 180, 0);

        Instantiate(Resources.Load<GameObject>("DashEffect"), effectPosition, effectRotation);
    }

    private void Dashing()
    {
        if (!isDashing || _dashCooldown > 0)
        {
            isDashing = false;
            return;
        }

        _dashCooldown -= Time.deltaTime;
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
        _dashCooldown = dashCooldown;
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