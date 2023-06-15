public abstract class EntityPotion : EntityItem
{
    protected override void OnCollide(EntityPlayer player)
    {
        AddEffect(player);

        Destroy(gameObject);
    }

    protected abstract void AddEffect(EntityPlayer player);
}