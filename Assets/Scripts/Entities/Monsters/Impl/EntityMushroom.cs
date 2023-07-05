using UnityEngine;

public class EntityMushroom : EntityMonster
{
    private static readonly int SpeedAnimation = Animator.StringToHash("Speed");
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    private static readonly int ShotAnimation = Animator.StringToHash("Shot");
    
    [SerializeField] private float cooldown = 1.5f;
    [SerializeField] private float dangerDistance = 3f;
    [SerializeField] private GameObject projectile;
    
    private float _cooldown;
    
    protected override void OnUpdate()
    {
        if (!IsPlayerVisible() || !IsAlive()) return;

        Animator.SetFloat(SpeedAnimation, Mathf.Abs(RigidBody2D.velocity.x));
        
        if (CanAttack()) 
            TryAttack();
        else if (!IsPlayerNear())
            TryShot();
        
        Move(new Vector2( transform.position.x * XPlayerSide() * -1, 0));
        FlipSprite();
        
        _cooldown -= Time.deltaTime;
    }
    
    private bool IsPlayerNear() =>
        Vector3.Distance(transform.position, _player.transform.position) < dangerDistance;

    private float XPlayerSide() => 
        _player.transform.position.x > transform.position.x ? -1f : 1f;

    protected override bool CanMove(Vector2 to) => base.CanMove(to) && IsPlayerNear();

    public override void FlipSprite()
    {
        if (IsPlayerVisible() && !IsPlayerNear() || CanAttack())
            SpriteRenderer.flipX = _player.transform.position.x < transform.position.x;
        else 
            base.FlipSprite();
    }
    
    protected override void TryAttack()
    {
        if (_cooldown > 0) return;
        Animator.SetTrigger(AttackAnimation);
        _cooldown = cooldown;
    }

    private void TryShot()
    {
        if (_cooldown > 0) return;
        Animator.SetTrigger(ShotAnimation);
        _cooldown = cooldown;
    }

    public void Shot() =>
        Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));

    public override void TakeDamage(float damage, GameObject damager)
    {
        base.TakeDamage(damage, damager);

        Animator.SetTrigger(DamageAnimation);
    }

    protected override void Dead()
    {
        Animator.StopPlayback();
        Animator.SetTrigger(DeadAnimation);

        base.Dead();
    }
}
