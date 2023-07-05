using System.Collections;
using UnityEngine;

public abstract class EntityRateEffect : EntityEffect
{
    protected float Duration = 3;
    protected float Rate = 1;

    private float _duration;
    protected override void OnApplying() => Player.effectsController.StartEffectRateUpdate(RateCoroutine());

    protected abstract override void Effect();

    private void RateUpdate() => Effect();

    private IEnumerator RateCoroutine()
    {
        while (true)
        {
            if (_duration >= Duration) break;
            yield return new WaitForSeconds(Rate);
            _duration += Rate;
            RateUpdate();
        }
        RemoveEffect();
    }
}
