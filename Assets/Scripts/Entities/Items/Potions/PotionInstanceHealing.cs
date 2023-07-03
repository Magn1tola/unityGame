public class PotionInstanceHealing : EntityPotion
{
    private void Awake() => Effect = new InstantHealEffect();
}