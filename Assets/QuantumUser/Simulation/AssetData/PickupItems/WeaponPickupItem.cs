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
        weapon->Type = WeaponBase.WeaponType;
        WeaponBase.OnInit(f, entityPickingUp, weapon);
      }

      f.Destroy(entityPickedUp);
    }
  }
}