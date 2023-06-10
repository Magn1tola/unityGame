using UnityEngine;

public class Wallet : MonoBehaviour, IWallet
{
    private int money = 0;
    public delegate void AddedMoney(int count);
    public static event AddedMoney OnAddedMoney;

    public void AddMoney(int count)
    {
        if (count <= 0)
            return;

        money += count;
    }

}
