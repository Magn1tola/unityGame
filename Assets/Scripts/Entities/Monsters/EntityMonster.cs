using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DropItems))]
public abstract class EntityMonster : EntityLiving
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float minDistanceToLook;
    private DropItems _dropItems;

    protected EntityPlayer _player;

    private EffectController _deadEffect;
    
    protected override void Init()
    {
        base.Init();

        _player = FindObjectOfType<EntityPlayer>();
        _dropItems = GetComponent<DropItems>();
        _deadEffect = Instantiate(Resources.Load<GameObject>("DestroyEffect")).GetComponent<EffectController>();
        _deadEffect.DisableEffect();
    }
    
    protected virtual bool CanAttack() =>
        Vector2.Distance(_player.transform.position, transform.position) <= attackDistance;

    protected bool IsPlayerVisible() =>
        Vector2.Distance(_player.transform.position, transform.position) <= minDistanceToLook;

    protected virtual bool CanMove(Vector2 to) => CheckGround(to) && !CanAttack();

    protected virtual void Move(Vector2 to)
    {
        var directionX = to.x > transform.position.x ? 1f : -1f;
        RigidBody2D.velocity = CanMove(to)
            ? new Vector2(directionX * moveSpeed, RigidBody2D.velocity.y)
            : Vector2.zero;
        FlipSprite();
    }

    private bool CheckGround(Vector2 to)
    {
        var directionX = to.x > transform.position.x ? 1f : -1f;
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
        return raycastHit2D.collider;
    }

    protected override void Dead()
    {
        base.Dead();
        _dropItems.Drop();
        StartCoroutine(PreDestroy());
    }

    private IEnumerator PreDestroy()
    {
        yield return new WaitForSeconds(2f);
        _deadEffect.transform.position = transform.position;
        _deadEffect.EnableEffect();
        Destroy(gameObject);
    }
}