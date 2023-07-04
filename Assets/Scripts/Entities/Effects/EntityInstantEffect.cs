public abstract class EntityInstantEffect : EntityEffect
{
    protected override void OnApplying()
    {
        Effect();
        RemoveEffect();
    }

    protected abstract override void Effect();
}