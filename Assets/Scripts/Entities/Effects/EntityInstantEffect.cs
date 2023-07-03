public abstract class EntityInstantEffect : EntityEffect
{
    protected override void OnApplying() => Effect();
    protected abstract override void Effect();
}