using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;

    public Rigidbody2D rigidBody2D { get; private set; }

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        Init();
    }

    private void Update() => OnUpdate();

    protected virtual void Init()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    public bool IsGrounded() => Mathf.Abs(rigidBody2D.velocity.y) < 0.05F;

    public void FlipSprite() => SpriteRenderer.flipX = !(rigidBody2D.velocity.x > 0);
}