public class PotionRateHealing : EntityPotion
{
    private void Awake() => Effect = new RateHealEffect();
}