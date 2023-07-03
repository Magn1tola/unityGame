public class RateHealEffect: EntityRateEffect
{
    public float Heal = 1;
    protected override void Effect() => Player.Heal(Heal);
}
