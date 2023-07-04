using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public float damageAttackMultiple = 1f;
    public float damageTakeMultiple = 1f;
    public float maxHealthMultiple = 1f;
    public float maxStaminaMultiple = 1f;
    private readonly List<EntityEffect> _effects = new();
    private EntityPlayer _player;

    private void Awake()
    {
        _player = GetComponent<EntityPlayer>();
    }

    public void AddEffect(EntityEffect effect)
    {
        _effects.Add(effect);
        effect.ApplyEffect(_player);
    }

    public void RemoveEffect(EntityEffect effect) => _effects.Remove(effect);

    public void StartEffectRateUpdate(IEnumerator enumerator) => StartCoroutine(enumerator);
}