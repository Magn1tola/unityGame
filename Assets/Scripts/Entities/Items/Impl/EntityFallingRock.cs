using UnityEngine;

public class EntityFallingRock : EntityItem
{
    [SerializeField] public float damage;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out EntityPlayer player) && collision.collider is CapsuleCollider2D)
            OnCollide(player);
        Destroy(gameObject);
    }
    protected override void OnCollide(EntityPlayer player)
    {
        player.Damage(damage, gameObject);
    }
}
