using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityPlayer : EntityLiving
{
    private static readonly int IsFallingAnimation = Animator.StringToHash("isFalling");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    private static readonly int DamageAnimation = Animator.StringToHash("TakeHit");
    private static readonly int DeathAnimation = Animator.StringToHash("Dead");


    public readonly PlayerData Data = new();

    protected override void OnUpdate()
    {
        // TODO: Animator.SetBool(IsFallingAnimation, !IsGrounded());

        if (Input.GetKeyDown(KeyCode.Space)) Animator.SetTrigger(AttackAnimation);
    }

    public override void Damage(float damage, GameObject damager)
    {
        Animator.SetTrigger(DamageAnimation);
        base.Damage(damage, damager);
    }

    public override void Dead()
    {
        base.Dead();
        Animator.SetTrigger(DeathAnimation);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}