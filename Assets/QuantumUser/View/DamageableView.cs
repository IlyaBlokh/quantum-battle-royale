using Quantum;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QuantumUser.View
{
    public class DamageableView : QuantumEntityViewComponent
    {
        [SerializeField] private Image healthBarFill;
        [SerializeField] private float animationDuration = 0.3f;

        private Tween _healthTween;

        public override void OnActivate(Frame frame)
        {
            QuantumEvent.Subscribe<EventOnHealthUpdate>(this, OnDamageApplied);
        }

        public override void OnDeactivate()
        {
            QuantumEvent.UnsubscribeListener(this);
            _healthTween?.Kill();
        }

        private void OnDamageApplied(EventOnHealthUpdate e)
        {
            if (e.Target != EntityRef)
                return;

            float targetFill = (e.CurrentHealth / e.MaxHealth).AsFloat;

            _healthTween?.Kill();
            _healthTween = healthBarFill
                .DOFillAmount(targetFill, animationDuration)
                .SetEase(Ease.InOutQuad);
        }
    }
}