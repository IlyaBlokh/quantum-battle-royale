using Quantum;
using UnityEngine;

namespace QuantumUser.View
{
  public class PlayerWeapon : MonoBehaviour
  {
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
  }
}