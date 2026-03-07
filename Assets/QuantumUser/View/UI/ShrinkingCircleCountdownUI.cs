using System;
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

    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventOnGameOver>(this, OnGameOver);
    }

    public override void OnUpdateView()
    {
      var shrinkingCircle = PredictedFrame.GetSingleton<ShrinkingCircle>();
      countdownText.text = $"{shrinkingCircle.CurrentTimeToNextState.AsFloat:N0}";
      countdownProgress.fillAmount = shrinkingCircle.CurrentTimeToNextState.AsFloat / shrinkingCircle.CurrentState.TimeToNextState.AsFloat;
    }

    private void OnGameOver(EventOnGameOver e)
    {
      var f = e.Game.Frames.Predicted;
      var playerRef = f.Get<PlayerLink>(e.Winner).PlayerRef;
      var playerData = f.GetPlayerData(playerRef);
      Debug.Log($"Winner is {playerData.PlayerNickname}");
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}