using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityPlayer : EntityLiving
{
    private static readonly int FallAnimation = Animator.StringToHash("Fall");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    private static readonly int DamageAnimation = Animator.StringToHash("TakeHit");
    private static readonly int DeathAnimation = Animator.StringToHash("Dead");

    public readonly PlayerData Data = new();

    [SerializeField] private float maxStamina = 20;
    private float stamina;
    
    public float Stamina => stamina;
    public float MaxStamina => maxStamina;

    protected override void Init()
    {
        base.Init();
        stamina = maxStamina;
    }

    protected override void OnUpdate()
    {
        if (!IsGrounded()) Animator.SetTrigger(FallAnimation);

        if (Input.GetKeyDown(KeyCode.Space) && !MovementController.BlockInput) TryAttack();
        
        CalculateStamina();
    }

    private void CalculateStamina()
    {
        if (stamina < maxStamina)
            stamina += Time.deltaTime;
    }

    public void UseStamina(float value)
    {
        stamina -= value;
        if (stamina < 0) stamina = 0;
    }

    public bool CheckStamina(float value) => stamina - value >= 0;
    
    protected override void TryAttack()
    {
        if (!IsAlive() || !CheckStamina(5f)) return;
        Animator.SetTrigger(AttackAnimation);
    }

    public override void Attack()
    {
        base.Attack();
        UseStamina(5f);
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