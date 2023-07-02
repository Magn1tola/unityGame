using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static readonly int WalkSpeedAnimation = Animator.StringToHash("walkSpeed");
    private static readonly int IsWalkingAnimation = Animator.StringToHash("isWalking");
    private static readonly int JumpAnimation = Animator.StringToHash("Jump");

    public static bool BlockInput;

    [SerializeField] private float baseMoveSpeed = 6;
    [SerializeField] private float jumpHeight = 5;
    
    [Header("Dash")]
    [SerializeField] private float dashCooldown = 3;
    [SerializeField] private float dashLenght = 5;
    [SerializeField] private float dashSpeed = 1;

    private Animator _animator;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private EntityPlayer _player;

    private float _dashCooldown;
    private Vector3 _dashPosition;
    private bool _isDashing;

    private float MoveSpeed => baseMoveSpeed + _player.Data.MoveSpeedLvl.LvlIncrease;

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDashing) DashStart();

        CalculateDashCooldown(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();

        if (BlockInput) return;

        Dashing();
    }

    private void Move()
    {
        _animator.SetFloat(WalkSpeedAnimation, MoveSpeed / _player.Data.MoveSpeedLvl.MaxLvlIncrease);

        var moveX = Input.GetAxis("Horizontal");
        if (BlockInput) moveX = 0;

        var velocity = _rigidbody2D.velocity;
        _rigidbody2D.velocity = new Vector2(moveX * baseMoveSpeed, velocity.y);

        if (moveX != 0)
        {
            _animator.SetBool(IsWalkingAnimation, true);

            _player.FlipSprite();
        }
        else
        {
            _animator.SetBool(IsWalkingAnimation, false);
        }
    }

    private void CalculateDashCooldown(float value)
    {
        if (_dashCooldown - value < 0) _dashCooldown = 0;
        else _dashCooldown -= value;
    }

    private void DashStart()
    {
        if (_isDashing || _dashCooldown > 0 || _rigidbody2D.velocity.x == 0 || !_player.CheckStamina(10f)) return;
        _player.UseStamina(_player.StaminaForDash);
        _isDashing = true;
        _dashPosition = CalculateDashPosition();

        var position = transform.position;
        var effectPosition = new Vector3(
            (position.x + _dashPosition.x) / 2,
            position.y - 0.5f,
            0
        );

        var effectRotation = _dashPosition.x > position.x
            ? new Quaternion(0, 0, 0, 0)
            : new Quaternion(0, 0, 180, 0);

        Instantiate(Resources.Load<GameObject>("DashEffect"), effectPosition, effectRotation);
    }

    private void Dashing()
    {
        if (!_isDashing || _dashCooldown > 0)
        {
            _isDashing = false;
            return;
        }

        _dashCooldown -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position,
            _dashPosition,
            dashSpeed
        );
        if (transform.position == _dashPosition) DashEnd();
    }

    private void DashEnd()
    {
        _isDashing = false;
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