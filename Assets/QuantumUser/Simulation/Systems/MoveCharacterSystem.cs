using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class MoveCharacterSystem : SystemMainThreadFilter<MoveCharacterSystem.Filter>, ISignalOnPlayerAdded
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

        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            RuntimePlayer playerData = f.GetPlayerData(player);
            f.Create(playerData.PlayerAvatar);
        }
    }
}
