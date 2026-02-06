using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class MoveCharacterSystem : SystemMainThreadFilter<MoveCharacterSystem.Filter>, ISignalOnTriggerEnter2D, ISignalOnTriggerExit2D
    {
        public struct Filter
        {
            public EntityRef Entity;
            public KCC* KCC;
            public PlayerLink* PlayerLink;
            public Transform2D* Transform;
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            Input* input = frame.GetPlayerInput(filter.PlayerLink->PlayerRef);
            RotatePlayer(filter, input);
            MovePlayer(frame, filter, input);
        }

        private void RotatePlayer(Filter filter, Input* input)
        {
            FPVector2 direction = input->MousePosition - filter.Transform->Position;
            filter.Transform->Rotation = FPVector2.RadiansSigned(FPVector2.Up, direction);
        }

        private static void MovePlayer(Frame frame, Filter filter, Input* input)
        {
            FPVector2 direction = input->Direction;
            if (direction.Magnitude > 1)
                direction = direction.Normalized;

            KCCSettings kccSettings = frame.FindAsset(filter.KCC->Settings);
            kccSettings.Move(frame, filter.Entity, direction);
        }

        public void OnTriggerEnter2D(Frame f, TriggerInfo2D info)
        {
            if (!f.TryGet(info.Entity, out PlayerLink playerLink))
                return;
            if (!f.TryGet<Grass>(info.Other, out _))
                return;

            f.Events.OnPlayerEnterGrass(playerLink.PlayerRef);
        }

        public void OnTriggerExit2D(Frame f, ExitInfo2D info)
        {
            if (!f.TryGet(info.Entity, out PlayerLink playerLink))
                return;
            if (!f.TryGet<Grass>(info.Other, out _))
                return;
            
            f.Events.OnPlayerExitGrass(playerLink.PlayerRef);
        }
    }
}
