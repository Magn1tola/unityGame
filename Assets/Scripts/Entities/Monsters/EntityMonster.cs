using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityMonster : EntityLiving
{
    [SerializeField] private float moveSpeed;
    [SerializeField] protected float minDistanceToLook;

    private EntityPlayer _player;

    protected override void Init()
    {
        base.Init();

        _player = FindObjectOfType<EntityPlayer>();
    }

    protected override void OnUpdate()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) > minDistanceToLook || !IsAlive())
            return;

        if (CanAttack()) TryAttack();
        else Move(_player.transform.position);
    }

    protected abstract void TryAttack();

    protected bool CanAttack() => Vector2.Distance(_player.transform.position, transform.position) <= attackDistance;

    protected void Move(Vector2 to)
    {
        var directionX = to.x > transform.position.x ? 1f : -1f;
        RigidBody2D.velocity = new Vector2(moveSpeed * directionX, RigidBody2D.velocity.y);
        FlipSprite();
    }
}