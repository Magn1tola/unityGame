public class PotionInstanceDead : EntityPotion
{
    private void Awake() => Effect = new InstantDeadEffect();
}