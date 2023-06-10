using UnityEngine;

public class HPController : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHp = 3;
    private float currentHp;

    public delegate void AcceptedDamage(float damage, GameObject instigator);
    public static event AcceptedDamage OnApplyDamage;


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