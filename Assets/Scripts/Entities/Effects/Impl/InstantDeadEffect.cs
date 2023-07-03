using UnityEngine;

public class InstantDeadEffect : EntityInstantEffect
{
    protected override void Effect() => Player.TakeDamage(Player.Health, new GameObject());
}
