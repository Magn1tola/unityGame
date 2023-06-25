using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityPlayer : EntityLiving
{
    private static readonly int FallAnimation = Animator.StringToHash("Fall");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");

    private static readonly int DamageAnimation = Animator.StringToHash("TakeHit");
    private static readonly int DeathAnimation = Animator.StringToHash("Dead");

    public readonly PlayerData Data = new();

    protected override void OnUpdate()
    {
        if (!IsGrounded()) Animator.SetTrigger(FallAnimation);

        if (Input.GetKeyDown(KeyCode.Space) && !MovementController.BlockInput) Animator.SetTrigger(AttackAnimation);
    }

    public override void Damage(float damage, GameObject damager)
    {
        Animator.SetTrigger(DamageAnimation);
        base.Damage(damage, damager);
    }

    protected override void Dead()
    {
        base.Dead();
        Animator.SetTrigger(DeathAnimation);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}