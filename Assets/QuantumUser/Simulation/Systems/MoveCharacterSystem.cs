using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class MoveCharacterSystem : SystemMainThreadFilter<MoveCharacterSystem.Filter>
    {
        public override void Update(Frame frame, ref Filter filter)
        {
            filter.CharacterController->Move(frame, filter.Entity, default);
        }

        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* CharacterController;
        }
    }
}
