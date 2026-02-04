using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class MoveCharacterSystem : SystemMainThreadFilter<MoveCharacterSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public KCC* KCC;
            public PlayerLink* PlayerLink;
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            Input* input = frame.GetPlayerInput(filter.PlayerLink->PlayerRef);
            FPVector2 direction = input->Direction;
            if (direction.Magnitude > 1) 
                direction = direction.Normalized;

            KCCSettings kccSettings = frame.FindAsset(filter.KCC->Settings);
            kccSettings.Move(frame, filter.Entity, direction);
        }
    }
}
