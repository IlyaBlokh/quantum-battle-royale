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
    [SerializeField] private TMP_Text gameStateInfo;

    private QuantumRunner _quantumRunner;
    
    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventShrinkingCircleStateChanged>(this, OnShrinkingCircleStateChanged);
      if (PredictedFrame.GetSingleton<GameManager>().CurrentGameState == GameState.WaitingForPlayers) 
        gameStateInfo.text = "Waiting for players";

      _quantumRunner = QuantumRunner.Default;
    }

    public override void OnUpdateView()
    {
      var gameManager = PredictedFrame.GetSingleton<GameManager>();
      if (gameManager.CurrentGameState == GameState.WaitingForPlayers)
      {
        GameManagerConfig data = PredictedFrame.FindAsset(gameManager.GameManagerConfig);
        float time = gameManager.TimeToWaitForPlayers.AsFloat;
        countdownText.text = $"{time:N0}";
        countdownProgress.fillAmount = time / data.TimeToWaitForPlayers.AsFloat;
      }
      else
      {
        DisableRoomJoining();
        
        var shrinkingCircle = PredictedFrame.GetSingleton<ShrinkingCircle>();
        float time = Mathf.Max(0, shrinkingCircle.CurrentTimeToNextState.AsFloat);
        countdownText.text = $"{time:N0}";
        countdownProgress.fillAmount = time / shrinkingCircle.CurrentState.TimeToNextState.AsFloat;
      }
    }

    private void DisableRoomJoining()
    {
      if (_quantumRunner.NetworkClient == null)
        return;
      
      if (!_quantumRunner.NetworkClient.CurrentRoom.IsVisible)
        return;
      
      _quantumRunner.NetworkClient.CurrentRoom.IsVisible = false;
    }

    private void OnShrinkingCircleStateChanged(EventShrinkingCircleStateChanged callback)
    {
      gameStateInfo.text = ShrinkingCircleStateToMessage();
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }

    private string ShrinkingCircleStateToMessage()
    {
      var state = PredictedFrame.GetSingleton<ShrinkingCircle>().CurrentState.CircleStateUnion.Field;
      switch (state)
      {
        case CircleStateUnion.INITIALSTATE: return "Area initialized";
        case CircleStateUnion.PRESHRINKSTATE: return "Go to safe area!";
        case CircleStateUnion.SHRINKSTATE: return "Area is shrinking";
        case CircleStateUnion.COOLDOWNSTATE: return "Cooldown";
        default: throw new ArgumentException("Unknown state");
      }
    }
  }
}