using UnityEngine;

public class HPController : MonoBehaviour, IDamage, IHeal
{
    [SerializeField] private float maxHp = 3;
    private float currentHp;

    public delegate void AcceptedDamage(float damage, GameObject instigator);
    public event AcceptedDamage OnApplyDamage;

    public delegate void AcceptedHealPoints(float hp);
    public event AcceptedHealPoints OnHealed;

    public delegate void Deaded();
    public event Deaded OnDead;


    void Start()
    {
        currentHp = maxHp;
    }

    public void ApplyDamage(float damage, GameObject instigator)
    {
        if (currentHp < damage)
            currentHp = 0;
        else
            currentHp -= damage;

        if (currentHp == 0)
            Dead();

        OnApplyDamage(damage, instigator);
    }

    public void ApplyHeal(float hp)
    {
        if (maxHp < hp + currentHp)
        {
            currentHp = maxHp;
        }
        else
            currentHp += hp;
        OnHealed(hp);
    }

    public void Dead()
    {
        if (currentHp > 0)
            return;

        OnDead();
    }
}