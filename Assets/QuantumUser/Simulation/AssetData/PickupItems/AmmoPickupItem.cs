namespace Quantum
{
  public unsafe class AmmoPickupItem : PickupItemBase
  {
    public override void Pickup(Frame f, EntityRef entityPickedUp, EntityRef entityPickingUp)
    {
      if (f.Unsafe.TryGetPointer(entityPickingUp, out Weapon* weapon))
      {
        WeaponBase weaponData = f.FindAsset(weapon->WeaponData);
        if (weaponData is FiringWeapon firingWeapon)
        {
          weapon->Ammo = firingWeapon.MaxAmmo;
          f.Events.OnAmmoChanged(entityPickingUp, weapon->Ammo);
          f.Destroy(entityPickedUp);
        }
      }
    }
  }
}