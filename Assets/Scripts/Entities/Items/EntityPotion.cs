using UnityEngine;

public abstract class EntityPotion : EntityPhysicItem
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityPlayer player))
        {
            AddEffect(player);

            Destroy(gameObject);
        }
    }

    protected abstract void AddEffect(EntityPlayer player);
}