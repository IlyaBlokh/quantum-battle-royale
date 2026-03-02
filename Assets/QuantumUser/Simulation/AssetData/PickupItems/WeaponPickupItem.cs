namespace Quantum
{
  public unsafe class WeaponPickupItem : PickupItemBase
  {
    public WeaponBase WeaponBase;
    
    public override void Pickup(Frame f, EntityRef entityPickedUp, EntityRef entityPickingUp)
    {
      if (f.Unsafe.TryGetPointer(entityPickingUp, out Weapon* weapon))
      {
        weapon->WeaponData = WeaponBase;
        weapon->CooldownTime = 0;
        WeaponBase.OnInit(f, entityPickingUp, weapon);
        
        f.Events.OnWeaponPickup(entityPickingUp, WeaponBase.WeaponType);
      }

      f.Destroy(entityPickedUp);
    }
  }
}