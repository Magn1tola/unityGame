public class DamageResistancePassiveEffect : EntityPassiveEffect
{
    private const float Increase = 0.5f;

    protected override void OnApplying()
    {
        Duration = 30f;
        base.OnApplying();
    }

    protected override void Effect()
    {
        Player.effectsController.damageTakeMultiple -= Increase;
    }

    protected override void EndEffect()
    {
        Player.effectsController.damageTakeMultiple += Increase;
    }
}
