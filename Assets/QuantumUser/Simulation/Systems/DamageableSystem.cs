using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class DamageableSystem : SystemSignalsOnly, ISignalOnComponentAdded<Damageable>, ISignalDamageableHit
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Damageable* Damageable;
        }
        
        public void OnAdded(Frame f, EntityRef entity, Damageable* component)
        {   
            DamageableData damageableData = f.FindAsset(component->DamageableData);
            component->Health = damageableData.MaxHealth;
        }

        public void DamageableHit(Frame f, EntityRef damageableEntity, FP damage, Damageable* damageable)
        {
            damageable->Health -= damage;
            if (damageable->Health <= 0)
                f.Destroy(damageableEntity);
        }
    }
}
