using Quantum;
using UnityEngine;

namespace QuantumUser.View
{
  public unsafe class PlayerView : QuantumEntityViewComponent
  {
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveZ = Animator.StringToHash("moveZ");
  
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject overheadUI;

    private bool _isLocalPlayer;
    private Renderer[] _renderers;

    private void Awake()
    {
      _renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
    }

    public override void OnActivate(Frame frame)
    {
      PlayerRef playerRef = frame.Get<PlayerLink>(EntityRef).PlayerRef;
      _isLocalPlayer = _game.PlayerIsLocal(playerRef);

      int layer = UnityEngine.LayerMask.NameToLayer(_isLocalPlayer ? "Player_Local" : "Player_Remote");
      foreach (Renderer rend in _renderers)
      {
        rend.gameObject.layer = layer;
        rend.enabled = true;
      }
    
      overheadUI.SetActive(true);
    
      QuantumEvent.Subscribe<EventOnPlayerEnterGrass>(this, OnPlayerEnterGrass);
      QuantumEvent.Subscribe<EventOnPlayerExitGrass>(this, OnPlayerExitGrass);
    }

    public override void OnUpdateView()
    {
      UpdateAnimator();
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }

    private void OnPlayerEnterGrass(EventOnPlayerEnterGrass callback)
    {
      ToggleVisibility(callback.Player, false);
    }

    private void OnPlayerExitGrass(EventOnPlayerExitGrass callback)
    {
      ToggleVisibility(callback.Player, true);
    }

    private void ToggleVisibility(PlayerRef playerRef, bool visible)
    {
      if (playerRef != PredictedFrame.Get<PlayerLink>(EntityRef).PlayerRef)
        return;
      if (_isLocalPlayer)
        return;
    
      foreach (Renderer rend in _renderers) 
        rend.enabled = visible;
    
      overheadUI.SetActive(visible);
    }

    private void UpdateAnimator()
    {
      if (!PredictedFrame.Exists(EntityRef))
        return;
      
      var kcc = PredictedFrame.Get<KCC>(EntityRef);
      var velocity = kcc.Velocity;
      animator.SetFloat(MoveX, velocity.X.AsFloat);
      animator.SetFloat(MoveZ, velocity.Y.AsFloat);
    }
  }
} 