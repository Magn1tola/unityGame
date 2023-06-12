using UnityEngine;

public class EntityChest : EntityItem, IEntityDamageable
{
    private static readonly int IsOpenedAnimation = Animator.StringToHash("IsOpened");

    private Animator _animator;

    private bool _isOpened;

    private DropItems _items;

    private void Awake()
    {
        _items = GetComponent<DropItems>();
        _animator = GetComponent<Animator>();
    }

    public void Damage(float damage, GameObject damager)
    {
        if (_isOpened || !damager.GetComponent<EntityPlayer>()) return;

        _isOpened = true;
        _animator.SetBool(IsOpenedAnimation, _isOpened);

        _items.Drop();
    }
}