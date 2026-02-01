using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class MoveCharacterSystem : SystemMainThreadFilter<MoveCharacterSystem.Filter>, ISignalOnPlayerAdded
    {
        public override void Update(Frame frame, ref Filter filter)
        {
            Input* input = frame.GetPlayerInput(filter.PlayerLink->PlayerRef);
            FPVector2 direction = input->Direction;
            if (direction.Magnitude > 1) 
                direction = direction.Normalized;

            KCCSettings kccSettings = frame.FindAsset(filter.KCC->Settings);
            kccSettings.Move(frame, filter.Entity, direction);
        }

        public struct Filter
        {
            public EntityRef Entity;
            public KCC* KCC;
            public PlayerLink* PlayerLink;
        }

        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            RuntimePlayer playerData = f.GetPlayerData(player);
            EntityRef playerEntity = f.Create(playerData.PlayerAvatar);
            f.Add(playerEntity, new PlayerLink { PlayerRef = player });

            KCC* kcc = f.Unsafe.GetPointer<KCC>(playerEntity);
            KCCSettings kccSettings = f.FindAsset(kcc->Settings);
            kcc->Acceleration = kccSettings.Acceleration;
            kcc->MaxSpeed = kccSettings.BaseSpeed;
        }
    }
}
