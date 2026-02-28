using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class DamageableSystem : SystemSignalsOnly, ISignalOnComponentAdded<Damageable>, ISignalDamageableHit, ISignalDamageableHealthRestored
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
