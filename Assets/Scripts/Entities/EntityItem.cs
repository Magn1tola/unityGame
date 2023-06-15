using UnityEngine;

public abstract class EntityItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out EntityPlayer player) && collision.collider is CapsuleCollider2D)
            OnCollide(player);
    }

    protected virtual void OnCollide(EntityPlayer player)
    {
    }
}