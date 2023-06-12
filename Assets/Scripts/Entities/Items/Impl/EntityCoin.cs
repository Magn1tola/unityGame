using UnityEngine;

public class EntityCoin : EntityPhysicItem
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityPlayer player))
        {
            player.Data.Money += 1;

            Destroy(gameObject);
        }
    }
}