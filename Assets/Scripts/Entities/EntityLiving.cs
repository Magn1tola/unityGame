using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public abstract class EntityLiving : Entity, IEntityDamageable, IEntityHealable
{
    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] private float maxHealth = 3;

    private float _health;

    public CapsuleCollider2D CapsuleCollider2D { get; private set; }
    public Animator Animator { get; private set; }

    public virtual void Damage(float damage, GameObject damager)
    {
        if (damage <= 0) return;

        RigidBody2D.velocity = Vector2.zero;

        var direction = damager.transform.position.x > gameObject.transform.position.x ? -1f : 1f;
        RigidBody2D.AddForce(transform.right * (direction * 2f) + transform.up * 5f, ForceMode2D.Impulse);

        _health -= damage;

        if (_health <= 0) Dead();
    }

    public void Heal(float health)
    {
        if (_health + health > maxHealth) _health = maxHealth;
        else _health += health;
    }

    protected override void Init()
    {
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();

        _health = maxHealth;
    }

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
            {
                if (hit.collider is BoxCollider2D && damageable is EntityPlayer) continue;
                damageable.Damage(damage, gameObject);
            }
        }
    }

    protected bool IsAlive() => _health > 0;

    public virtual void Dead() => UnityEngine.Debug.Log("Dead");
}