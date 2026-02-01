using Quantum;
using UnityEngine;
using Input = Quantum.Input;

public unsafe class PlayerView : QuantumEntityViewComponent
{
  private static readonly int MoveX = Animator.StringToHash("moveX");
  private static readonly int MoveZ = Animator.StringToHash("moveZ");
  
  [SerializeField] private Animator animator;

  public override void OnUpdateView()
  {
    UpdateAnimator();
  }

  private void UpdateAnimator()
  {
    if (PredictedFrame.TryGet(EntityRef, out PlayerLink playerLink))
    {
      Input* input = PredictedFrame.GetPlayerInput(playerLink.PlayerRef);
      var kcc = PredictedFrame.Get<KCC>(EntityRef);
      var velocity = kcc.Velocity;
      animator.SetFloat(MoveX, velocity.X.AsFloat);
      animator.SetFloat(MoveZ, velocity.Y.AsFloat);

    }
  }
} 