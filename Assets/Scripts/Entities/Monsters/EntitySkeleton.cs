using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntitySkeleton : EntityLiving
{
    private static readonly int SpeedAnimation = Animator.StringToHash("Speed");
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");

    protected override void OnUpdate()
    {
        Animator.SetFloat(SpeedAnimation, Mathf.Abs(rigidBody2D.velocity.x));

        /*if (Vector2.Distance(player.transform.position, transform.position) > attackDistance * 0.9f &&
            currentCooldown == 0)
            MoveToTargetPosition(player.transform.position);
        else if (currentCooldown == 0)
            animator.SetTrigger("Attack");*/ // todo
    }

    public override void Damage(float damage, GameObject damager)
    {
        base.Damage(damage, damager);

        Animator.SetTrigger(DamageAnimation);
    }

    public override void Dead()
    {
        Animator.SetTrigger(DeadAnimation);
        Destroy(gameObject, 2f);
    }
}