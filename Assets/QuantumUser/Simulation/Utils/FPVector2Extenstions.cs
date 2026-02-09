using Photon.Deterministic;

namespace Quantum
{
    public static class FPVector2Extenstions
    {
        public static FPVector2 Rotate(this FPVector2 v, FP angle)
        {
            // FP cos = FPMath.Cos(angle);
            // FP sin = FPMath.Sin(angle);
            // return new FPVector2(cos * v.X - sin * v.Y, sin * v.X + cos * v.Y);
            //
            return FPVector2.Rotate(v, angle);
        }
    }
}
