using UnityEngine;

public class MoneyChest : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    [SerializeField] private GameObject coinPrefab;
    private bool isOpened = false;

    private HPController hpController;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        hpController = GetComponent<HPController>();
        hpController.OnApplyDamage += Open;
    }

    private void Open(float damage, GameObject instigator)
    {
        if (isOpened && !instigator.TryGetComponent<Player>(out Player Player))
            return;

        isOpened = true;
        animator.SetBool("IsOpened", isOpened);

        float direction = 1;

        for (int i = 0; i < coinsCount; i++)
        {
            Vector3 position = new Vector3(transform.position.x + i * direction, transform.position.y + 0.7f, 0);
            GameObject coin = Instantiate(coinPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            coin.GetComponent<CoinDrop>().SetTargetPosition(position);
            direction *= -1;
        }

    }
}
