using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityGoblin : EntityMonster
{
    private static readonly int SpeedAnimation = Animator.StringToHash("Speed");
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");

    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float teleportCooldown = 1;
    [SerializeField] private float teleportTime = 1;

    private EffectController _teleportEffect;
    
    private int _attackCounter;
    private float _currentCooldown;

    protected override void Init()
    {
        base.Init();

        _teleportEffect = Instantiate(Resources.Load<GameObject>("TeleportEffect")).GetComponent<EffectController>();
        _teleportEffect.DisableEffect();
    }

    protected override void OnUpdate()
    {
        if (!IsPlayerVisible() || !IsAlive())
        {
            _attackCounter = 0;
            return;
        }

        _currentCooldown -= Time.deltaTime;

        Animator.SetFloat(SpeedAnimation, Mathf.Abs(RigidBody2D.velocity.x));

        Move(_player.transform.position);
        
        if (_currentCooldown > 0) return;
        switch (_attackCounter)
        {
            case 0:
                if (CanAttack()) TryAttack();
                _currentCooldown = attackCooldown;
                _attackCounter++;
                break;

            case 1:
                if (CanAttack()) StartCoroutine(Teleportation());
                _attackCounter = 0;
                break;
        }
    }

    protected override void TryAttack()
    {
        _currentCooldown = attackCooldown;
        Animator.SetTrigger(AttackAnimation);
    }

    public override void TakeDamage(float damage, GameObject damager)
    {
        base.TakeDamage(damage, damager);
        Animator.SetTrigger(DamageAnimation);
    }

    protected override void Dead()
    {
        Animator.StopPlayback();
        Animator.SetTrigger(DeadAnimation);
        
        _teleportEffect.DestroyEffect();
        
        base.Dead();
    }
    
    public override void FlipSprite()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) < minDistanceToLook)
            SpriteRenderer.flipX = (_player.transform.position - transform.position).x < 0;
        else
            base.FlipSprite();
    }

    private IEnumerator Teleportation()
    {
        BeginTeleportation();
        yield return new WaitForSeconds(teleportTime);
        EndTeleportation();
    }

    private void BeginTeleportation()
    {
        _teleportEffect.transform.position = transform.position;
        _teleportEffect.EnableEffect();
        transform.position = Vector3.zero;
        RigidBody2D.simulated = false;
    }

    private void EndTeleportation()
    {
        RigidBody2D.simulated = true;
        transform.position = CalculateTeleportPosition();
        _teleportEffect.transform.position = transform.position;
        _teleportEffect.EnableEffect();
        _currentCooldown = teleportCooldown;
    }

    private Vector3 CalculateTeleportPosition()
    {
        var playerPosition = _player.transform.position;
        var directionX = Random.value < 0.5f ? 1f : -1f;
        if (directionX == 0) directionX = 1;
        var endPosition = new Vector2(
            playerPosition.x + attackDistance * 0.8f * directionX,
            playerPosition.y
        );

        var raycastHit2D = Physics2D.Linecast(
            playerPosition,
            endPosition,
            LayerMask.GetMask("Ground")
        );

        if (raycastHit2D.collider)
            endPosition.x = raycastHit2D.point.x;

        return new Vector3(
            endPosition.x + CapsuleCollider2D.bounds.size.x / 2 * directionX,
            playerPosition.y,
            0
        );
    }
}