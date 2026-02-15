using Photon.Deterministic;

namespace Quantum
{
  public abstract class PickupItemBase : AssetObject
  {
    public FP PickupTime;
    
    public abstract void Pickup(Frame f, EntityRef entityPickedUp, EntityRef entityPickingUp);
  }
}