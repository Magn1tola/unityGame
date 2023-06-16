using System;
using UnityEngine;

public class EntityFlightEye : EntityMonster
{
    private static readonly int DamageAnimation = Animator.StringToHash("Damage");
    private static readonly int DeadAnimation = Animator.StringToHash("Dead");

    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private float maxLeftXOffset;
    [SerializeField] private float maxRightXOffset;
    [SerializeField] private float cooldown = 1.5f;
    private float _cooldown;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    protected override void Init()
    {
        base.Init();

        startPosition = transform.position;
        CalculateTargetPosition();
    }

    protected override void OnUpdate()
    {
        if (!IsAlive()) return;

        if (CanAttack()) TryAttack();
        Move(targetPosition);
    }

    protected override void TryAttack()
    {
        if (_cooldown <= 0)
        {
            UnityEngine.Debug.Log("!ATTACK!");
            Attack();
            _cooldown = cooldown;
        }

        _cooldown -= Time.deltaTime;
    }

    public override void Attack()
    {
        var rock = Instantiate(
            rockPrefab,
            transform.position,
            new Quaternion(0, 0, 0, 0)).GetComponent<EntityFallingRock>();
        rock.damage = damage;
    }

    protected override bool CanMove(Vector2 to) => true;

    protected override void Move(Vector2 to)
    {
        base.Move(to);
        if (Math.Abs(transform.position.x - targetPosition.x) < 0.1f) CalculateTargetPosition();
    }

    private void CalculateTargetPosition()
    {
        targetPosition = transform.position.x < startPosition.x
            ? new Vector2(startPosition.x + maxRightXOffset, startPosition.y)
            : new Vector2(startPosition.x - maxLeftXOffset, startPosition.y);
    }

    protected override bool CanAttack()
    {
        return Math.Abs(_player.transform.position.x - transform.position.x) < attackDistance;
    }

    public override void Damage(float damage, GameObject damager)
    {
        base.Damage(damage, damager);
        Animator.SetTrigger(DamageAnimation);
    }

    public override void Dead()
    {
        Animator.StopPlayback();

        base.Dead();

        Animator.SetTrigger(DeadAnimation);

        Destroy(gameObject, 2f);
    }
}