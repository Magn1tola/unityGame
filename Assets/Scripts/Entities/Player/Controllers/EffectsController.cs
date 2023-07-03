using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private EntityPlayer _player;
    private readonly List<EntityEffect> _effects = new();

    private void Awake() => _player = GetComponent<EntityPlayer>();

    public void AddEffect(EntityEffect effect)
    {
        _effects.Add(effect);
        effect.ApplyEffect(_player);
    }

    public void StartEffectRateUpdate(IEnumerator enumerator) => StartCoroutine(enumerator);
}
