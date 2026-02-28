using Photon.Deterministic;

namespace Quantum
{
    public abstract unsafe class DamageableBase : AssetObject
    {
        public FP MaxHealth;
        
        public abstract void DamageableHit(Frame f, EntityRef target, EntityRef hitter, FP damage, Damageable* damageable);
    }
}
