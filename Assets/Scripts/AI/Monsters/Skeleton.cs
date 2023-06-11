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

    private void Update()
    {
        
    }
    protected override void ApplyDamage(float damage, GameObject instigator)
    {
        base.ApplyDamage(damage, instigator);
        animator.SetTrigger("Damage");
    }

    protected override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Attack");
    }

    protected override void Dead()
    {
        animator.SetTrigger("Dead");
        Destroy(gameObject, 3f);
    }
}
