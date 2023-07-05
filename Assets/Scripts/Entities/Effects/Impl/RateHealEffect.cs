public class RateHealEffect: EntityRateEffect
{
    public float Heal = 0.5f;

    protected override void OnApplying()
    {
        Rate = 0.5f;
        Duration = 4;
        base.OnApplying();
    }

    protected override void Effect() => Player.Heal(Heal);
}
