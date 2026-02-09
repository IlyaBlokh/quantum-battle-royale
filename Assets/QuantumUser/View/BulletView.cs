using Quantum;
using UnityEngine;

namespace QuantumUser.View
{
    public class BulletView : QuantumEntityViewComponent
    {
        [SerializeField] private Transform bulletTransform;

        public override void OnActivate(Frame frame)
        {
            Bullet bulletRef = PredictedFrame.Get<Bullet>(EntityRef);
            
            bulletTransform.localPosition = bulletTransform.localPosition.SetY(bulletRef.HeightOffset.AsFloat);
        }
    }
}
