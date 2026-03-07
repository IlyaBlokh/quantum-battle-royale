using Quantum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumUser.View
{
  public class ShrinkingCircleCountdownUI : QuantumSceneViewComponent
  {
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private Image countdownProgress;
    
    public override void OnUpdateView()
    {
      var shrinkingCircle = PredictedFrame.GetSingleton<ShrinkingCircle>();
      countdownText.text = $"{shrinkingCircle.CurrentTimeToNextState.AsFloat:N0}";
      countdownProgress.fillAmount = shrinkingCircle.CurrentTimeToNextState.AsFloat / shrinkingCircle.CurrentState.TimeToNextState.AsFloat;
    }
  }
}