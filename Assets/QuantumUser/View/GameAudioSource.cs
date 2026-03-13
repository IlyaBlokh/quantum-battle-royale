using Quantum;
using UnityEngine;

namespace QuantumUser.View
{
  public class GameAudioSource : QuantumSceneViewComponent
  {
    [SerializeField] private AudioSource audioSource;

    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventOnShotFired>(this, OnShotFired);
    }

    private void OnShotFired(EventOnShotFired e)
    {
      Frame predicted = e.Game.Frames.Predicted;
      if (!e.Game.PlayerIsLocal(predicted.Get<PlayerLink>(e.Entity).PlayerRef))
        return;

      var weapon = predicted.Get<Weapon>(e.Entity);
      WeaponBase data = predicted.FindAsset(weapon.WeaponData);
      audioSource.PlayOneShot(data.ShotSound);
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}