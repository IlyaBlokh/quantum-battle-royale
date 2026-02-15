namespace Quantum
{
  public class BasicPickupItem : PickupItemBase
  {
    public override void Pickup(Frame f, EntityRef entityPickedUp, EntityRef entityPickingUp)
    {
      f.Destroy(entityPickedUp);
    }
  }
}