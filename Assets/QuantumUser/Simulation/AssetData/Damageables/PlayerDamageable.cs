using Photon.Deterministic;

namespace Quantum
{
  public class PlayerDamageable : DamageableBase
  {
    private const int LootDropOffset = 2;

    public override unsafe void DamageableHit(Frame f, EntityRef target, EntityRef hitter, FP damage, Damageable* damageable)
    {
      damageable->Health -= damage;

      if (damageable->Health <= 0)
      {
        DropLoop(f, target);
        f.Destroy(target);
        return;
      }
      f.Events.OnHealthUpdate(target, MaxHealth, damageable->Health);
    }

    private unsafe void DropLoop(Frame f, EntityRef target)
    {
      var targetTransform = f.Get<Transform2D>(target);
      EntityRef healthLoot = f.Create(f.SimulationConfig.HealthPickupItem);
      f.Unsafe.GetPointer<Transform2D>(healthLoot)->Position = targetTransform.Position + targetTransform.Right * LootDropOffset;
      
      if (!f.TryGet(target, out Weapon weapon))
        return;

      WeaponBase weaponData = f.FindAsset(weapon.WeaponData);
      EntityRef weaponLoot = f.Create(f.SimulationConfig.GetWeaponPrototype(weaponData.WeaponType));
      f.Unsafe.GetPointer<Transform2D>(weaponLoot)->Position = targetTransform.Position + targetTransform.Left * LootDropOffset;
    }
  }
}