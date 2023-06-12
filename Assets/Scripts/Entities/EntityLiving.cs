using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public abstract class EntityLiving : Entity, IEntityDamageable, IEntityHealable
{
    [SerializeField] private float maxHealth = 3;

    private CapsuleCollider2D _capsuleCollider2D;

    private float _health;

    protected Animator Animator;

    public virtual void Damage(float damage, GameObject damager)
    {
        if (damage <= 0) return;

        rigidBody2D.velocity = Vector2.zero;

        var direction = damager.transform.position.x > gameObject.transform.position.x ? -1f : 1f;
        rigidBody2D.AddForce(transform.right * (direction * 2f) + transform.up * 5f, ForceMode2D.Impulse);

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
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();

        _health = maxHealth;
    }

    public virtual void Attack()
    {
        var attackDirection = SpriteRenderer.flipX ? -1f : 1f;
        var bounds = _capsuleCollider2D.bounds;
        var startPosition = new Vector2(
            bounds.center.x + _capsuleCollider2D.size.x * attackDirection,
            bounds.center.y
        );

        var raycastHits2D = Physics2D.BoxCastAll(
            startPosition,
            _capsuleCollider2D.bounds.size,
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

    public virtual void Dead() => UnityEngine.Debug.Log("dead");
}