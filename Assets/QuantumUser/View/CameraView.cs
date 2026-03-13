using Quantum;
using Unity.Cinemachine;
using UnityEngine;

namespace QuantumUser.View
{
  public class CameraView : QuantumSceneViewComponent
  {
    [SerializeField] private CinemachineFollow follow;

    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventOnWeaponPickup>(this, OnWeaponPickup);
    }

    private void OnWeaponPickup(EventOnWeaponPickup e)
    {
      Frame predicted = e.Game.Frames.Predicted;
      if (!e.Game.PlayerIsLocal(predicted.Get<PlayerLink>(e.PickedUpEntity).PlayerRef))
        return;

      var weapon = e.Game.Frames.Verified.Get<Weapon>(e.PickedUpEntity);
      WeaponBase data = e.Game.Frames.Verified.FindAsset(weapon.WeaponData);
      follow.FollowOffset = follow.FollowOffset.SetY(data.CameraYOffset.AsFloat);
    }
    
    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}