namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  /// <summary>
  /// A Unity script that creates empty input for any Quantum game.
  /// </summary>
  public class QuantumDebugInput : MonoBehaviour {

    private Vector3 _mouseHitPosition;
    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    /// <summary>
    /// Set an empty input when polled by the simulation.
    /// </summary>
    /// <param name="callback"></param>
    public void PollInput(CallbackPollInput callback) {
#if DEBUG
      if (callback.IsInputSet) {
        Debug.LogWarning($"{nameof(QuantumDebugInput)}.{nameof(PollInput)}: Input was already set by another user script, unsubscribing from the poll input callback. Please delete this component.", this);
        QuantumCallback.UnsubscribeListener(this);
        return;
      }
#endif
      
      Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
      if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 100, layerMask: 1 << UnityEngine.LayerMask.NameToLayer("Ground"))) 
        _mouseHitPosition = hit.point;
      
      var input = new Input {
        Direction = new FPVector2(
          UnityEngine.Input.GetAxis("Horizontal").ToFP(),
          UnityEngine.Input.GetAxis("Vertical").ToFP()),
        
        MousePosition = _mouseHitPosition.ToFPVector3().XZ
      };
      
      callback.SetInput(input, DeterministicInputFlags.Repeatable);
    }
  }
}
