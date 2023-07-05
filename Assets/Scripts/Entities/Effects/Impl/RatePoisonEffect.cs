using UnityEngine;

public class RatePoisonEffect : EntityRateEffect
{
    private const float Damage = 1f;

    protected override void OnApplying()
    {
        Duration = 6f;
        Rate = 2f;
        base.OnApplying();
    }

    protected override void Effect() => Player.TakeDamage(Damage, new GameObject());
}
