using Quantum;
using TMPro;
using UnityEngine;

namespace QuantumUser.View
{
  public class GameOverUI : QuantumSceneViewComponent
  {
    [SerializeField] private GameObject winnerParent;
    [SerializeField] private TMP_Text winnerText;
    
    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventOnGameOver>(this, OnGameOver);
    }

    private void OnGameOver(EventOnGameOver e)
    {
      var f = e.Game.Frames.Predicted;
      var playerRef = f.Get<PlayerLink>(e.Winner).PlayerRef;
      var playerData = f.GetPlayerData(playerRef);
      winnerText.text = $"{playerData.PlayerNickname} won!";
      winnerParent.SetActive(true);
    }
    
    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}