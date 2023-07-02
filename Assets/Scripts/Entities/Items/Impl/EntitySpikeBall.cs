using UnityEngine;

public class EntitySpikeBall : EntityItem
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityLiving entityLiving))
            entityLiving.TakeDamage(damage, gameObject);
    }
}