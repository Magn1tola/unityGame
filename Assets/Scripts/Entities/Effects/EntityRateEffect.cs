using System.Collections;
using UnityEngine;

public abstract class EntityRateEffect : EntityEffect
{
    protected float Duration = 3;
    private float _rate = 1;

    private float _duration;
    protected override void OnApplying() => Player.effectsController.StartEffectRateUpdate(RateCoroutine());

    protected abstract override void Effect();

    protected virtual void RateUpdate() => Effect();

    private IEnumerator RateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_rate);
            if (_duration >= Duration) break;
            _duration += _rate;
            RateUpdate();
        }
    }
}
