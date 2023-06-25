public class EntityCoin : EntityItem
{
    protected override void OnCollide(EntityPlayer player)
    {
        player.Data.Coins += 1;

        Destroy(gameObject);
    }
}