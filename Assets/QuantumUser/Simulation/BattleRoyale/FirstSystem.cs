using UnityEngine.Scripting;

namespace Quantum
{
    using Photon.Deterministic;

    public unsafe class FirstSystem : SystemMainThread
    {
        public override void Update(Frame frame)
        {
            Log.Info("FirstSystem update called");
        }
    }
}
