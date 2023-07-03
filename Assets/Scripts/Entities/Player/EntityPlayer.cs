using UnityEngine;
using UnityEngine.Events;

public class EntityPlayer : EntityLiving
{
    private static readonly int FallAnimation = Animator.StringToHash("Fall");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    private static readonly int DamageAnimation = Animator.StringToHash("TakeHit");
    private static readonly int DeathAnimation = Animator.StringToHash("Dead");

    [Header("Stamina")]
    [SerializeField] private float baseMaxStamina = 20;
    [SerializeField] private float baseStaminaRecoveryRate = 2;
    [SerializeField] private float staminaForAttack = 3f;
    [SerializeField] private float staminaForDash = 5f;
    
    public readonly PlayerData Data = new();

    public EffectsController effectsController;

    public float Stamina { get; private set; }

    public new float MaxHealth => maxHealth + Data.MaxHpLvl.LvlIncrease;
    private float Damage => damage + Data.StrengthLvl.LvlIncrease;
    public float MaxStamina => baseMaxStamina + Data.StaminaLvl.LvlIncrease;

    public float StaminaForDash => staminaForDash;

    public UnityEvent playerDead;
    protected override void Init()
    {
        base.Init();

        effectsController = GetComponent<EffectsController>();
        
        Stamina = MaxStamina;
    }

    protected override void OnUpdate()
    {
        if (!IsGrounded()) Animator.SetTrigger(FallAnimation);

        if (Input.GetKeyDown(KeyCode.Space) && !MovementController.BlockInput) TryAttack();

        CalculateStamina();
    }

    private void CalculateStamina()
    {
        if (Stamina < MaxStamina)
            Stamina += Time.deltaTime * baseStaminaRecoveryRate;
        if (Stamina > MaxStamina)
            Stamina = MaxStamina;
    }

    public void UseStamina(float value)
    {
        Stamina -= value;
        if (Stamina < 0) Stamina = 0;
    }

    public bool CheckStamina(float value)
    {
        return Stamina - value >= 0;
    }

    protected override void TryAttack()
    {
        if (!IsAlive() || !CheckStamina(5f)) return;
        Animator.SetTrigger(AttackAnimation);
    }

    public override void Attack()
    {
        UseStamina(staminaForAttack);
        if (!IsAlive()) return;

        var raycastHits2D = GetHitsAtAttackDistance();

        foreach (var hit in raycastHits2D)
        {
            if (hit.collider.gameObject == gameObject) continue;

            if (hit.collider.gameObject.TryGetComponent(out IEntityDamageable damageable))
                damageable.TakeDamage(Damage, gameObject);
        }
    }

    public override void TakeDamage(float damage, GameObject damager)
    {
        Animator.SetTrigger(DamageAnimation);
        base.TakeDamage(damage, damager);
    }

    public override void Heal(float health) => Health = (Health + health > MaxHealth) ? MaxHealth : Health + health;

    protected override void Dead()
    {
        base.Dead();
        Animator.SetTrigger(DeathAnimation);
        playerDead.Invoke();
    }
}