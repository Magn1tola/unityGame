using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public abstract class EntityLiving : Entity, IEntityDamageable, IEntityHealable
{
    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float maxHealth = 3f;

    public float MaxHealth => maxHealth;
    public float Health { get; private set; }

    public CapsuleCollider2D CapsuleCollider2D { get; private set; }

    public Animator Animator { get; private set; }

    public virtual void Damage(float damage, GameObject damager)
    {
        if (damage <= 0) return;

        RigidBody2D.velocity = Vector2.zero;

        var direction = damager.transform.position.x > gameObject.transform.position.x ? -1f : 1f;
        RigidBody2D.AddForce(transform.right * (direction * 2f) + transform.up * 5f, ForceMode2D.Impulse);

        Health -= damage;

        if (Health <= 0) Dead();
    }

    public void Heal(float health)
    {
        if (Health + health > maxHealth) Health = maxHealth;
        else Health += health;
    }

    protected override void Init()
    {
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();

        Health = maxHealth;
    }

    protected abstract void TryAttack();

    public virtual void Attack()
    {
        if (!IsAlive()) return;

        var attackDirection = SpriteRenderer.flipX ? -1f : 1f;
        var bounds = CapsuleCollider2D.bounds;
        var startPosition = new Vector2(
            bounds.center.x + CapsuleCollider2D.size.x * attackDirection,
            bounds.center.y
        );

        var raycastHits2D = Physics2D.BoxCastAll(
            startPosition,
            CapsuleCollider2D.bounds.size,
            0f,
            Vector2.right * attackDirection,
            attackDistance
        );

        foreach (var hit in raycastHits2D)
        {
            if (hit.collider.gameObject == gameObject) continue;

            if (hit.collider.gameObject.TryGetComponent(out IEntityDamageable damageable))
                damageable.Damage(damage, gameObject);
        }
    }

    protected bool IsAlive() => Health > 0;

    protected virtual void Dead() => UnityEngine.Debug.Log("Dead");
}