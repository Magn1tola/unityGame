public abstract class EntityEffect
{
    protected EntityPlayer Player;
    
    protected abstract void OnApplying();

    protected abstract void Effect();

    protected void RemoveEffect() => Player.effectsController.RemoveEffect(this);
    
    public void ApplyEffect(EntityPlayer player)
    {
        Player = player;
        OnApplying();
    }
}
