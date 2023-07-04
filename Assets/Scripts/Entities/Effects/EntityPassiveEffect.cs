using System.Collections;
using UnityEngine;

public abstract class EntityPassiveEffect : EntityEffect
{
    protected float Duration = 3;
    
    protected override void OnApplying() => Player.effectsController.StartEffectRateUpdate(EffectCoroutine());

    protected abstract override void Effect();
    
    protected abstract void EndEffect();
    
    private IEnumerator EffectCoroutine()
    { 
        Effect();
        yield return new WaitForSeconds(Duration);
        EndEffect();
        RemoveEffect();
    }
}