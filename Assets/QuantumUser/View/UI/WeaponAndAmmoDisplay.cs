using Quantum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumUser.View
{
  public class WeaponAndAmmoDisplay : MonoBehaviour
  {
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TMP_Text _ammoCount;

    private void Awake()
    {
      QuantumEvent.Subscribe<EventOnPlayerSpawn>(this, OnPlayerSpawn);
      QuantumEvent.Subscribe<EventOnWeaponPickup>(this, OnWeaponPickup);
      QuantumEvent.Subscribe<EventOnAmmoChanged>(this, OnAmmoChanged);
    }

    private void OnPlayerSpawn(EventOnPlayerSpawn e)
    {
      if (!e.Game.PlayerIsLocal(e.PlayerLink.PlayerRef))
        return;
      
      SetImageAndText(e.Game.Frames.Verified, e.PlayerEntity);
    }

    private void SetImageAndText(Frame frame, EntityRef entityRef)
    {
      var weapon = frame.Get<Weapon>(entityRef);
      _weaponImage.sprite = frame.FindAsset(weapon.WeaponData).WeaponSprite;
      _ammoCount.text = $"{weapon.Ammo}";
    }

    private void OnWeaponPickup(EventOnWeaponPickup e)
    {
      Frame frame = e.Game.Frames.Verified;
      if (e.Game.PlayerIsLocal(frame.Get<PlayerLink>(e.PickedUpEntity).PlayerRef)) 
        SetImageAndText(e.Game.Frames.Verified, e.PickedUpEntity);
    }

    private void OnAmmoChanged(EventOnAmmoChanged e)
    {
      Frame frame = e.Game.Frames.Predicted;
      if (e.Game.PlayerIsLocal(frame.Get<PlayerLink>(e.Entity).PlayerRef)) 
        SetImageAndText(frame, e.Entity);
    }

    private void OnDestroy()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}