public class InstantHealEffect : EntityInstantEffect
{
    private const float Heal = 2;
    protected override void Effect() => Player.Heal(Heal);
}
