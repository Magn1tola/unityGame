using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;

    public Rigidbody2D RigidBody2D { get; private set; }

    private void Awake()
    {
        RigidBody2D = GetComponent<Rigidbody2D>();
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

    public bool IsGrounded() => Mathf.Abs(RigidBody2D.velocity.y) < 0.05F;

    public virtual void FlipSprite()
    {
        if (RigidBody2D.velocity.x == 0) return;
        SpriteRenderer.flipX = !(RigidBody2D.velocity.x > 0);
    }
}