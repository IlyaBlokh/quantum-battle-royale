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

        public override void Update(Frame frame, ref Filter filter)
        {
            if (filter.Weapon->CooldownTime > 0)
            {
                filter.Weapon->CooldownTime -= frame.DeltaTime;
                return;
            }
            
            var input = frame.GetPlayerInput(filter.Player->PlayerRef);
            if (input->Fire.WasPressed)
            {
                var weaponData = frame.FindAsset(filter.Weapon->WeaponData);
                filter.Weapon->CooldownTime = weaponData.Cooldown;
                frame.Signals.CreateBullet(filter.Entity, weaponData);
            }
        }
    }
}
