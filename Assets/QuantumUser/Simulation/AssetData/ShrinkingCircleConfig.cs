using Photon.Deterministic;
using Quantum.Prototypes;

namespace Quantum
{
    public class ShrinkingCircleConfig : AssetObject
    {
        public ShrinkingCircleStatePrototype[] States;
        public FP DamagePerSecond;
        public FPVector2 MinumumBounds, MaximumBounds;
    }
}
