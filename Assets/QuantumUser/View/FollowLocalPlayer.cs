using Quantum;

namespace QuantumUser.View
{
  public class FollowLocalPlayer : QuantumViewComponent<CameraViewContext>
  {
    public override void OnActivate(Frame frame)
    {
      if (!frame.TryGet(_entityView.EntityRef, out PlayerLink link))
        return;
      
      if (!_game.PlayerIsLocal(link.PlayerRef))
        return;
      
      ViewContext.VirtualCamera.Follow = _entityView.Transform;
    }
  }
}