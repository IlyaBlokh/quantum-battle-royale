using Quantum;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace QuantumUser.View
{
  public class PlayerWeapon : MonoBehaviour
  {
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public Rig Rig { get; private set; }
  }
}