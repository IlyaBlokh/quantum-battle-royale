using Quantum.QuantumUser.Simulation.Systems;

namespace Quantum
{
  public class SemiAutomaticWeapon : FiringWeapon
  {
    public override void OnFirePressed(Frame f, WeaponSystem.Filter filter)
    {
      Fire(f, filter);
    }
  }
}