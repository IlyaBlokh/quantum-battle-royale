using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class DamageableSystem : SystemMainThreadFilter<DamageableSystem.Filter>, ISignalOnComponentAdded<Damageable>, ISignalDamageableHit, ISignalDamageableHealthRestored
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Damageable* Damageable;
        }

        public void OnAdded(Frame f, EntityRef entity, Damageable* component)
        {
            DamageableBase damageableBase = f.FindAsset(component->DamageableData);
            
            if (damageableBase == null)
                return;
            
            component->Health = damageableBase.MaxHealth;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (!f.Has<PlayerLink>(filter.Entity))
                return;
            
            var shrinkingCircle = f.Unsafe.GetPointerSingleton<ShrinkingCircle>();
            if (PlayerIsOutsideCirce(f, filter, shrinkingCircle))
            {
                var damageableAsset = f.FindAsset(filter.Damageable->DamageableData);
                var shrinkingCircleAsset = f.FindAsset(shrinkingCircle->ShrinkingCircleConfig);
                damageableAsset.DamageableHit(f, filter.Entity, filter.Entity, shrinkingCircleAsset.DamagePerSecond * f.DeltaTime, filter.Damageable);
            }
        }

        private bool PlayerIsOutsideCirce(Frame f, Filter filter, ShrinkingCircle* shrinkingCircle)
        {
            var transform = f.Unsafe.GetPointer<Transform2D>(filter.Entity);

            var distance = FPVector2.Distance(shrinkingCircle->Position, transform->Position);
            return distance > shrinkingCircle->CurrentRadius / 2;
        }

        public void DamageableHit(Frame f, EntityRef victim, EntityRef hitter, FP damage, Damageable* damageable)
        {
            DamageableBase damageableBase = f.FindAsset(damageable->DamageableData);
            damageableBase.DamageableHit(f, victim, hitter, damage, damageable);
        }

        public void DamageableHealthRestored(Frame f, EntityRef entity, Damageable* damageable)
        {
            FP maxHealth = f.FindAsset(damageable->DamageableData).MaxHealth;
            damageable->Health = maxHealth;
            f.Events.OnHealthUpdate(entity, maxHealth, damageable->Health);
        }
    }
}
