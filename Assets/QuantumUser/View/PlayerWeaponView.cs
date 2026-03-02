using System.Collections.Generic;
using System.Linq;
using Quantum;

namespace QuantumUser.View
{
  public class PlayerWeaponView : QuantumEntityViewComponent
  {
    private PlayerWeapon _currentWeapon;
    private Dictionary<WeaponType, PlayerWeapon> _playerWeapons;

    private void Awake()
    {
      _playerWeapons = GetComponentsInChildren<PlayerWeapon>(this)
        .ToDictionary(w => w.WeaponType, w => w);
    }

    public override void OnActivate(Frame frame)
    {
      foreach (PlayerWeapon weapon in _playerWeapons.Values)
      {
        weapon.gameObject.SetActive(false);
      }

      _currentWeapon = _playerWeapons[WeaponType.Pistol];
      _currentWeapon.gameObject.SetActive(true);
      
      QuantumEvent.Subscribe<EventOnWeaponPickup>(this, OnWeaponPickup);
    }

    private void OnWeaponPickup(EventOnWeaponPickup e)
    {
      if (e.PickedUpEntity != EntityRef)
        return;
      
      if (e.Type == _currentWeapon.WeaponType)
        return;
      
      _currentWeapon.gameObject.SetActive(false);
      _currentWeapon = _playerWeapons[e.Type];
      _currentWeapon.gameObject.SetActive(true);
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}