using UnityEngine;

public class HPController : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHp = 3;
    private float currentHp;

    public delegate void AcceptedDamage(float damage, GameObject instigator);
    public event AcceptedDamage OnApplyDamage;

    public delegate void AcceptedHealPoints(float damage, GameObject instigator);
    public event AcceptedHealPoints OnHealed;

    public delegate void Deaded(float damage, GameObject instigator);
    public event Deaded OnDead;


    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {

    }

    public void ApplyDamage(float damage, GameObject instigator)
    {
        Debug.Log(damage);

        if (currentHp < damage)
            currentHp = 0;
        else
            currentHp -= damage;

        if (currentHp == 0)
            Dead();

        OnApplyDamage(damage, instigator);
    }

    private void Dead()
    {
        if (currentHp != 0)
            return;

        Debug.Log("dead");
    }
}