using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private static readonly int PlayAnimation = Animator.StringToHash("Trigger");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DestroyEffect() => Destroy(gameObject);
    public void DisableEffect() => gameObject.SetActive(false);

    public void EnableEffect()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(PlayAnimation);
    }
}