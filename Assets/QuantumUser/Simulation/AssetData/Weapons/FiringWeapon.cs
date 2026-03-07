using Photon.Deterministic;
using Quantum.QuantumUser.Simulation.Systems;

namespace Quantum
{
  public abstract unsafe class FiringWeapon : WeaponBase
  {
    public BulletData Bullet;
    public byte MaxAmmo;

    public override void OnInit(Frame f, EntityRef entity, Weapon* weapon)
    {
      weapon->Ammo = MaxAmmo;
    }
    
    protected void Fire(Frame f, WeaponSystem.Filter filter)
    {
      if (filter.Weapon->Ammo <= 0)
        return;

      FP multiplier = FP._1;
      if (f.TryGet(filter.Entity, out Character character)) 
        multiplier = f.FindAsset(character.CharacterConfig).FireRateModifier;

      filter.Weapon->CooldownTime = Cooldown * multiplier;
      filter.Weapon->Ammo--;
      f.Signals.CreateBullet(filter.Entity, this);
      f.Events.OnAmmoChanged(filter.Entity, filter.Weapon->Ammo);
    }
  }
}