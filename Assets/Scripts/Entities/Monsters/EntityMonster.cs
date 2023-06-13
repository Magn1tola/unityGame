using UnityEngine;

[RequireComponent(typeof(DropItems))]
public abstract class EntityMonster : EntityLiving
{
    [SerializeField] private float moveSpeed;
    [SerializeField] protected float minDistanceToLook;

    protected EntityPlayer _player;
    private DropItems _dropItems;


    protected override void Init()
    {
        base.Init();

        _player = FindObjectOfType<EntityPlayer>();
        _dropItems = GetComponent<DropItems>();
    }

    protected abstract void TryAttack();

    protected bool CanAttack() => Vector2.Distance(_player.transform.position, transform.position) <= attackDistance;

    protected void Move(Vector2 to)
    {
        var directionX = to.x > transform.position.x ? 1f : -1f;
        RigidBody2D.velocity = CheckGround(directionX) 
            ? new Vector2(directionX * moveSpeed, RigidBody2D.velocity.y) 
            : Vector2.zero;
        FlipSprite();
    }

    private bool CheckGround(float directionX)
    {
        var position = transform.position;
        var colliderBoundsSize = CapsuleCollider2D.bounds.size;
        var checkPosition = new Vector2(
            position.x + colliderBoundsSize.x / 2f * directionX,
            position.y - colliderBoundsSize.y / 2f);
        var raycastHit2D = Physics2D.Raycast(
            checkPosition,
            Vector2.down,
            1f,
            LayerMask.GetMask("Ground"));
        return (raycastHit2D.collider);
    }
    
    public override void Dead()
    {
        base.Dead();
        _dropItems.Drop();
    }
}