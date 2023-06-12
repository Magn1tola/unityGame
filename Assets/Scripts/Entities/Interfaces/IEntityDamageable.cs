using UnityEngine;

internal interface IEntityDamageable
{
    void Damage(float damage, GameObject damager);
}