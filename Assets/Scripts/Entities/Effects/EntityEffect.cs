public abstract class EntityEffect
{
    protected EntityPlayer Player;

    protected abstract void OnApplying();

    protected abstract void Effect();

    public void ApplyEffect(EntityPlayer player)
    {
        Player = player;
        OnApplying();
    }
}
