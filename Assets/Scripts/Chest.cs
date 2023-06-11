using UnityEngine;

[RequireComponent(typeof(DropItems))]
[RequireComponent(typeof(HPController))]

public class Chest : MonoBehaviour
{
    private bool isOpened = false;

    private HPController hpController;
    private DropItems dropItems;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        dropItems = GetComponent<DropItems>();
        hpController = GetComponent<HPController>();

        hpController.OnApplyDamage += Open;
        hpController.OnDead += Dead;
    }

    private void Open(float damage, GameObject instigator)
    {
        if (isOpened && !instigator.TryGetComponent<Player>(out Player Player))
            return;

        isOpened = true;
        animator.SetBool("IsOpened", isOpened);
        dropItems.Drop();
    }

    private void Dead(){}
}
