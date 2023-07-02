using UnityEngine;

public class EntityFallingRock : EntityItem
{
    [SerializeField] public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out EntityPlayer player)) OnCollide(player);
        Destroy(gameObject);
    }

    protected override void OnCollide(EntityPlayer player) => player.TakeDamage(damage, gameObject);
}