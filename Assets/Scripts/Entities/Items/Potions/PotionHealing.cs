using UnityEngine;

public class PotionHealing : EntityPotion
{
    [SerializeField] private float heal;

    protected override void AddEffect(EntityPlayer player) => player.Heal(heal);
}