using Photon.Deterministic;
using Quantum.QuantumUser.Simulation.Systems;

namespace Quantum
{
  public unsafe class SemiAutomaticWeapon : FiringWeapon
  {
    public int BulletsPerClick;
    public FP BurstInterval;

    public override void OnFirePressed(Frame f, WeaponSystem.Filter filter)
    {
      Fire(f, filter);
    }

    public override void OnFireHeld(Frame f, WeaponSystem.Filter filter)
    {
      if (filter.Weapon->BurstShotsRemaining <= 0)
        filter.Weapon->BurstShotsRemaining = BulletsPerClick;

      Fire(f, filter);
      filter.Weapon->BurstShotsRemaining--;

      if (filter.Weapon->BurstShotsRemaining > 0)
        filter.Weapon->CooldownTime = BurstInterval;
    }
  }
}