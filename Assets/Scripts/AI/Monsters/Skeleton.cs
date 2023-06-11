using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Skeleton : BaseAI
{
    private Animator animator;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));

        if (Vector2.Distance(player.transform.position, transform.position) > attackDistance * 0.9f && currentCooldown == 0)
            MoveToTargetPosition(player.transform.position);
        else if (currentCooldown == 0)
            animator.SetTrigger("Attack");

    }

    protected override void ApplyDamage(float damage, GameObject instigator)
    {
        base.ApplyDamage(damage, instigator);
        animator.SetTrigger("Damage");
    }

    protected override void Dead()
    {
        animator.SetTrigger("Dead");
        Destroy(gameObject, 2f);
    }
}
