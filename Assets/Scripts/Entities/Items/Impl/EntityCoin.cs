using UnityEngine;

public class EntityCoin : EntityItem
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