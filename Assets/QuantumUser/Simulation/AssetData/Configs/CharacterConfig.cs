using Photon.Deterministic;

namespace Quantum
{
  public class CharacterConfig : AssetObject
  {
    [RangeEx(0.5, 2)]
    public FP HealthMultiplier;
    
    [RangeEx(0.5, 2)]
    public FP FireRateModifier;
  }
}