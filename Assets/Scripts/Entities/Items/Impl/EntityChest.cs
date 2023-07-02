using UnityEngine;

public class EntityChest : EntityItem, IEntityDamageable
{
    private static readonly int IsOpenedAnimation = Animator.StringToHash("IsOpened");

    private Animator _animator;

    private bool _isOpened;

    private DropItems _dropItems;

    private void Awake()
    {
        _dropItems = GetComponent<DropItems>();
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, GameObject damager)
    {
        if (_isOpened || !damager.GetComponent<EntityPlayer>()) return;

        _isOpened = true;
        _animator.SetBool(IsOpenedAnimation, _isOpened);

        _dropItems.Drop();
    }
}