using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityPlayer : EntityLiving
{
    private static readonly int IsFallingAnimation = Animator.StringToHash("isFalling");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");

    public readonly PlayerData Data = new();

    protected override void OnUpdate()
    {
        Animator.SetBool(IsFallingAnimation, !IsGrounded());

        if (Input.GetKeyDown(KeyCode.Space)) Animator.SetTrigger(AttackAnimation);
    }

    public override void Dead()
    {
        base.Dead();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}