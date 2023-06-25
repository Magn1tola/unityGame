using UnityEngine;

public abstract class EntityItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out EntityPlayer player)) OnCollide(player);
    }

    protected virtual void OnCollide(EntityPlayer player)
    {
    }
}