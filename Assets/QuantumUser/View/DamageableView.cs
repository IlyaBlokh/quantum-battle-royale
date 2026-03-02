using System.Collections;
using Quantum;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumUser.View
{
    public class DamageableView : QuantumEntityViewComponent
    {
        [SerializeField] private Image healthBarFill;
        [SerializeField] private float animationDuration = 0.3f;

        private Coroutine _healthAnimationCoroutine;

        public override void OnActivate(Frame frame)
        {
            QuantumEvent.Subscribe<EventOnHealthUpdate>(this, OnDamageApplied);
        }

        public override void OnDeactivate()
        {
            QuantumEvent.UnsubscribeListener(this);
            
            if (_healthAnimationCoroutine != null)
                StopCoroutine(_healthAnimationCoroutine);
        }

        private void OnDamageApplied(EventOnHealthUpdate e)
        {
            if (e.Target != EntityRef)
                return;

            float targetFill = (e.CurrentHealth / e.MaxHealth).AsFloat;

            if (_healthAnimationCoroutine != null)
                StopCoroutine(_healthAnimationCoroutine);

            _healthAnimationCoroutine = StartCoroutine(AnimateHealthBar(targetFill));
        }

        private IEnumerator AnimateHealthBar(float targetFill)
        {
            float startFill = healthBarFill.fillAmount;
            float elapsed = 0f;

            while (elapsed < animationDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / animationDuration;
                
                t = t * t * (3f - 2f * t);
                
                healthBarFill.fillAmount = Mathf.Lerp(startFill, targetFill, t);
                yield return null;
            }

            healthBarFill.fillAmount = targetFill;
            _healthAnimationCoroutine = null;
        }
    }
}