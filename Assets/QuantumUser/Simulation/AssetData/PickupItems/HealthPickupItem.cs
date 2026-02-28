namespace Quantum
{
  public unsafe class HealthPickupItem : PickupItemBase
  {
    public override void Pickup(Frame f, EntityRef entityPickedUp, EntityRef entityPickingUp)
    {
      if (f.Unsafe.TryGetPointer(entityPickingUp, out Damageable* damageable))
        f.Signals.DamageableHealthRestored(entityPickingUp, damageable);
      
      f.Destroy(entityPickedUp);
    }
  }
}