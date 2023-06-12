using UnityEngine;

public class EntityCoin : EntityItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D) return;
        if (collision.gameObject.TryGetComponent(out EntityPlayer player))
        {
            player.Data.Money += 1;

            Destroy(gameObject);
        }
    }
}