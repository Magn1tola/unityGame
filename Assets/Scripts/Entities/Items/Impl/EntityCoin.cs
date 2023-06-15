public class EntityCoin : EntityItem
{
    protected override void OnCollide(EntityPlayer player)
    {
        player.Data.Money += 1;

        Destroy(gameObject);
    }
}