using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public abstract class EntityLiving : Entity, IEntityDamageable, IEntityHealable
{
    [Header("Health")]
    [SerializeField] protected float maxHealth = 3f;
    
    [Header("Combat")]
    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected float damage = 1f;

    public float MaxHealth => maxHealth;
    public float Health { get; protected set; }

    public CapsuleCollider2D CapsuleCollider2D { get; private set; }

    public Animator Animator { get; private set; }

    public virtual void TakeDamage(float damage, GameObject damager)
    {
        if (damage <= 0) return;

        RigidBody2D.velocity = Vector2.zero;

        var direction = damager.transform.position.x > gameObject.transform.position.x ? -1f : 1f;
        RigidBody2D.AddForce(transform.right * (direction * 2f) + transform.up * 5f, ForceMode2D.Impulse);

        Health -= damage;

        if (Health <= 0) Dead();
    }

    public virtual void Heal(float health)
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

        var raycastHits2D = GetHitsAtAttackDistance();

        foreach (var hit in raycastHits2D)
        {
            if (hit.collider.gameObject == gameObject) continue;

            if (hit.collider.gameObject.TryGetComponent(out IEntityDamageable damageable))
                damageable.TakeDamage(damage, gameObject);
        }
    }

    protected RaycastHit2D[] GetHitsAtAttackDistance()
    {
        var attackDirection = SpriteRenderer.flipX ? -1f : 1f;
        var bounds = CapsuleCollider2D.bounds;
        var startPosition = new Vector2(
            bounds.center.x + CapsuleCollider2D.size.x * attackDirection,
            bounds.center.y
        );

        return Physics2D.BoxCastAll(
            startPosition,
            bounds.size,
            0f,
            Vector2.right * attackDirection,
            attackDistance
        );
    }

    protected bool IsAlive()
    {
        return Health > 0;
    }

    protected virtual void Dead()
    {
        UnityEngine.Debug.Log("Dead");
    }
}