using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class WeaponSystem : SystemMainThreadFilter<WeaponSystem.Filter>, ISignalOnComponentAdded<Weapon>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public PlayerLink* Player;
            public Weapon* Weapon;
        }

        public void OnAdded(Frame f, EntityRef entity, Weapon* component)
        {
            WeaponBase data = f.FindAsset(component->WeaponData);
            data.OnInit(f, entity, component);
        }

        public override void Update(Frame f, ref Filter filter)
        {
            WeaponBase data = f.FindAsset(filter.Weapon->WeaponData);
            data.OnUpdate(f, filter);
        }
    }
}
