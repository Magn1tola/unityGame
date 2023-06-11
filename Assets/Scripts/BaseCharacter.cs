using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HPController))]
public class BaseCharacter : MonoBehaviour
{
    protected CapsuleCollider2D capsuleCollider2D;
    protected Rigidbody2D rigidBody2D;
    protected SpriteRenderer spriteRenderer;
    private HPController hpController;


    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected float damage = 1f;


    protected virtual void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        hpController = GetComponent<HPController>();

        hpController.OnApplyDamage += ApplyDamage;
        hpController.OnHealed += ApplyHeal;
        hpController.OnDead += Dead;
    }
    public virtual bool IsGrounded()
    {
        Vector2 circlePosition = new Vector2(capsuleCollider2D.bounds.center.x, capsuleCollider2D.bounds.center.y - (capsuleCollider2D.size.y * transform.lossyScale.y / 2f));
        float circleRadius = capsuleCollider2D.size.x * transform.lossyScale.x / 2f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circlePosition, circleRadius);
        return colliders.Length > 1;
    }
    public virtual void Flip()
    {
        if (rigidBody2D.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rigidBody2D.velocity.x < 0)
            spriteRenderer.flipX = true;
    }
    protected virtual void ApplyDamage(float damage, GameObject instigator)
    {
        if (damage <= 0)
            return;

        float direction = (instigator.transform.position.x > gameObject.transform.position.x) ? -1f : 1f;
        rigidBody2D.velocity = new Vector2(0f, 0f);
        rigidBody2D.AddForce(transform.right * direction * 2f + transform.up * 5f, ForceMode2D.Impulse);
    }

    protected virtual void Attack()
    {
        float attackDirection = (spriteRenderer.flipX) ? -1f : 1f;
        Vector2 startposition = new Vector2(capsuleCollider2D.bounds.center.x + capsuleCollider2D.size.x * attackDirection, capsuleCollider2D.bounds.center.y);
        RaycastHit2D[] raycastHits2D = Physics2D.BoxCastAll(startposition, capsuleCollider2D.bounds.size, 0f, Vector2.right * attackDirection, attackDistance);

        foreach (RaycastHit2D hit in raycastHits2D)
        {
            if (hit.collider.gameObject == gameObject)
                continue;

            if (hit.collider.gameObject.TryGetComponent(out IDamage damageable))
                damageable.ApplyDamage(damage, gameObject);
        }

    }

    protected virtual void Dead() {
        Debug.Log("dead");
    }
    protected virtual void ApplyHeal(float hp){}
}
