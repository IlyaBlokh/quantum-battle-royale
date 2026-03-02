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

      filter.Weapon->CooldownTime = Cooldown;
      filter.Weapon->Ammo--;
      f.Signals.CreateBullet(filter.Entity, this);
      f.Events.OnAmmoChanged(filter.Entity, filter.Weapon->Ammo);
    }
  }
}