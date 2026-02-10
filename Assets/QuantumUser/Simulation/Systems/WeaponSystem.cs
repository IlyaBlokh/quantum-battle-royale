using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class WeaponSystem : SystemMainThreadFilter<WeaponSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public PlayerLink* Player;
            public Weapon* Weapon;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (filter.Weapon->CooldownTime > 0)
            {
                filter.Weapon->CooldownTime -= f.DeltaTime;
                return;
            }
            
            var input = f.GetPlayerInput(filter.Player->PlayerRef);
            if (input->Fire.WasPressed)
            {
                var weaponData = f.FindAsset(filter.Weapon->WeaponData);
                filter.Weapon->CooldownTime = weaponData.Cooldown;
                f.Signals.CreateBullet(filter.Entity, weaponData);
            }
        }
    }
}
