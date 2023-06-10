using UnityEngine;

namespace Damage
{
    interface IDamage
    {
        void ApplyDamage(float damage, GameObject instigator);
    }
}
