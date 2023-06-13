using UnityEngine;

public class EntityGoblin : EntityMonster
{
    private static readonly int SpeedAnimation = Animator.StringToHash("Speed");
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    
    [SerializeField] private float cooldown = 1.5f;
    [SerializeField] private float attackCooldown = 0.5f;
    
    [SerializeField] private float dashCooldown = 1;
    [SerializeField] private float dashLenght = 3;
    [SerializeField] private float dashSpeed = 0.1f;
    
    private Vector3 dashPosition;
    private bool isDashing;

    private int attackCounter = 0;

    private float _cooldown;

    protected override void OnUpdate()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) > minDistanceToLook || !IsAlive())
        {
            attackCounter = 0;
            return;
        }
        
        switch (attackCounter)
        {
            case 0:
                if (_cooldown > 0) break;
                if (CanAttack()) TryAttack();
                else
                {
                    Move(_player.transform.position);
                    break;
                }
                _cooldown = attackCooldown;
                attackCounter++;
                break;
            case 1:
                if (_cooldown > 0) break;
                if (CanAttack() && !isDashing) DashStart();
                else
                {
                    attackCounter = 0;
                    break;
                }
                break;
            case 2:
                if (_cooldown > 0) break;
                if (CanAttack()) TryAttack();
                else
                {
                    attackCounter = 0;
                    break;
                }
                _cooldown = cooldown;
                attackCounter = 0;
                break;
        }
        if (!isDashing)
            _cooldown -= Time.deltaTime;
        Animator.SetFloat(SpeedAnimation, Mathf.Abs(RigidBody2D.velocity.x));
    }
    
    private void FixedUpdate()
    {
        Dashing();
    }

    protected override void TryAttack()
    {
        if (_cooldown <= 0)
        {
            Animator.SetTrigger(AttackAnimation);
        }

    }

    public override void Damage(float damage, GameObject damager)
    {
        base.Damage(damage, damager);

        Animator.SetTrigger(DamageAnimation);
    }

    public override void Dead()
    {
        Animator.StopPlayback();
        base.Dead();
        Animator.SetTrigger(DeadAnimation);
        Destroy(gameObject, 2f);
    }

    public override void FlipSprite()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) < minDistanceToLook)
            SpriteRenderer.flipX = ((_player.transform.position - transform.position).x < 0);
        else
            base.FlipSprite();
    }

    private void DashStart()
    {
        isDashing = true;
        dashPosition = CalculateDashPosition();
    }

    private void Dashing()
    {
        if (!isDashing) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            dashPosition,
            dashSpeed
        );
        FlipSprite();
        if (transform.position == dashPosition) DashEnd();
    }

    private void DashEnd()
    {
        isDashing = false;
        _cooldown = dashCooldown;
        attackCounter++;
    }

    private Vector3 CalculateDashPosition()
    {
        var directionX = (_player.transform.position - transform.position).normalized.x;
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
            position.x + directionX * (raycastHit2D.distance - CapsuleCollider2D.bounds.size.x / 2),
            position.y,
            0
        );
    }
}
