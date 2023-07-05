using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public float damageAttackMultiple = 1f;
    public float damageTakeMultiple = 1f;
    public float maxHealthMultiple = 1f;
    public float maxStaminaMultiple = 1f;
    public readonly List<EntityEffect> Effects = new();
    private EntityPlayer _player;

    private void Awake()
    {
        _player = GetComponent<EntityPlayer>();
    }

    public void AddEffect(EntityEffect effect)
    {
        if (Effects.Exists(item => item.GetType() == effect.GetType())) return;
        Effects.Add(effect);
        effect.ApplyEffect(_player);
    }

    public void RemoveEffect(EntityEffect effect) => Effects.Remove(effect);

    public void StartEffectRateUpdate(IEnumerator enumerator) => StartCoroutine(enumerator);
}