using Quantum;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumUser.View
{
  public class PickupItemView : QuantumEntityViewComponent
  {
    [SerializeField] private Image pickupProgress;

    public override void OnUpdateView()
    {
      if (!PredictedFrame.Exists(EntityRef)) 
        return;
      
      var pickUpItem = PredictedFrame.Get<PickUpItem>(EntityRef);
      var progress = pickUpItem.CurrentPickupTime / pickUpItem.PickupTime;
      pickupProgress.fillAmount = progress.AsFloat;
    }
  }
}