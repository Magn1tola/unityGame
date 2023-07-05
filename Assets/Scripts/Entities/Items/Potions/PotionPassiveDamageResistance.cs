public class PotionPassiveDamageResistance : EntityPotion
{
    private void Awake() => Effect = new DamageResistancePassiveEffect();
}
