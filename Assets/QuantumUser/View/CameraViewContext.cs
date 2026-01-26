using Quantum;
using Unity.Cinemachine;
using UnityEngine;

namespace QuantumUser.View
{
  public class CameraViewContext : MonoBehaviour, IQuantumViewContext
  {
    [field: SerializeField] public CinemachineVirtualCameraBase VirtualCamera { get; private set; }
  }
}
