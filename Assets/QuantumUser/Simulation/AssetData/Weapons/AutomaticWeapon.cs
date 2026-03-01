using Quantum.QuantumUser.Simulation.Systems;

namespace Quantum
{
  public class AutomaticWeapon : FiringWeapon
  {
    public override void OnFireHeld(Frame f, WeaponSystem.Filter filter)
    {
      Fire(f, filter);
    }
  }
}