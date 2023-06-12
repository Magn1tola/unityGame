using UnityEngine;

public class MovementController : MonoBehaviour
{
    private static readonly int SpeedAnimation = Animator.StringToHash("speed");
    private static readonly int JumpAnimation = Animator.StringToHash("Jump");

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpHeight = 5;

    private Animator _animator;

    private EntityPlayer _player;
    private Rigidbody2D _rigidbody2D;

    public void Start()
    {
        _player = GetComponent<EntityPlayer>();
        _rigidbody2D = _player.rigidBody2D;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _player.IsGrounded())
        {
            _rigidbody2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            _animator.SetTrigger(JumpAnimation);
        }
    }

    private void FixedUpdate()
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
}