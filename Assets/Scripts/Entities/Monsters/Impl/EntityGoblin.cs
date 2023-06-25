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
    [SerializeField] private float dashSpeed = 0.1f;
    private int attackCounter;
    private float currentCooldown;

    private Vector3 dashPosition;
    private bool isDashing;

    private void FixedUpdate() => Dashing();

    protected override void OnUpdate()
    {
        if (!IsPlayerVisible() || !IsAlive())
        {
            attackCounter = 0;
            return;
        }

        currentCooldown -= Time.deltaTime;
        Animator.SetFloat(SpeedAnimation, Mathf.Abs(RigidBody2D.velocity.x));

        if (currentCooldown > 0) return;
        switch (attackCounter)
        {
            case 0:
                if (CanAttack()) TryAttack();
                else
                {
                    Move(_player.transform.position);
                    break;
                }

                currentCooldown = attackCooldown;
                attackCounter++;
                break;

            case 1:
                if (CanAttack() && !isDashing) DashStart();
                else attackCounter = 0;
                break;

            case 2:
                if (CanAttack()) TryAttack();
                else
                {
                    attackCounter = 0;
                    break;
                }

                currentCooldown = cooldown;
                attackCounter = 0;
                break;
        }
    }

    protected override void TryAttack()
    {
        currentCooldown = attackCooldown;
        Animator.SetTrigger(AttackAnimation);
    }

    public override void Damage(float damage, GameObject damager)
    {
        base.Damage(damage, damager);
        Animator.SetTrigger(DamageAnimation);
    }

    protected override void Dead()
    {
        Animator.StopPlayback();
        Animator.SetTrigger(DeadAnimation);

        base.Dead();
    }

    public override void FlipSprite()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) < minDistanceToLook)
            SpriteRenderer.flipX = (_player.transform.position - transform.position).x < 0;
        else
            base.FlipSprite();
    }

    private void DashStart()
    {
        if (isDashing) return;
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
        currentCooldown = dashCooldown;
        attackCounter++;
    }

    private Vector3 CalculateDashPosition()
    {
        var playerPosition = _player.transform.position;
        var startPosition = transform.position;
        var directionX = (playerPosition - startPosition).normalized.x;
        var endPosition = new Vector2(
            playerPosition.x + attackDistance * 0.8f * directionX,
            startPosition.y
        );

        var raycastHit2D = Physics2D.Linecast(
            startPosition,
            endPosition,
            LayerMask.GetMask("Ground")
        );

        if (raycastHit2D.collider)
            endPosition.x = raycastHit2D.point.x;

        return new Vector3(
            endPosition.x + CapsuleCollider2D.bounds.size.x / 2 * directionX,
            startPosition.y,
            0
        );
    }
}