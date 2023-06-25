using UnityEngine;

public class EntitySkeleton : EntityMonster
{
    private static readonly int SpeedAnimation = Animator.StringToHash("Speed");
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");

    [SerializeField] private float cooldown = 1.5f;

    private float _cooldown;

    protected override void OnUpdate()
    {
        if (!IsPlayerVisible() || !IsAlive()) return;

        if (CanAttack()) TryAttack();
        else Move(_player.transform.position);

        Animator.SetFloat(SpeedAnimation, Mathf.Abs(RigidBody2D.velocity.x));
    }

    protected override void TryAttack()
    {
        if (_cooldown <= 0)
        {
            Animator.SetTrigger(AttackAnimation);

            _cooldown = cooldown;
        }

        _cooldown -= Time.deltaTime;
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
}