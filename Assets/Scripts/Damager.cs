using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamage damageable))
            damageable.ApplyDamage(damage, gameObject);
    }
}
