public abstract class EntityPotion : EntityItem
{
    protected EntityEffect Effect;
    protected override void OnCollide(EntityPlayer player)
    {
        player.effectsController.AddEffect(Effect);

        Destroy(gameObject);
    }
}