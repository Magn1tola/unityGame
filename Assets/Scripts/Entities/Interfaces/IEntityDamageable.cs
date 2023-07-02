using UnityEngine;

internal interface IEntityDamageable
{
    void TakeDamage(float damage, GameObject damager);
}