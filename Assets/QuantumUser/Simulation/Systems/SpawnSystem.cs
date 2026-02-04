using Quantum.Collections;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class SpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            if (!firstTime)
                return;

            EntityRef playerEntityRef = CreatePlayer(f, player);
            PlacePlayerOnSpawnPosition(f, playerEntityRef);
        }

        private static EntityRef CreatePlayer(Frame f, PlayerRef player)
        {
            RuntimePlayer playerData = f.GetPlayerData(player);
            EntityRef playerEntity = f.Create(playerData.PlayerAvatar);
            f.Add(playerEntity, new PlayerLink { PlayerRef = player });

            KCC* kcc = f.Unsafe.GetPointer<KCC>(playerEntity);
            KCCSettings kccSettings = f.FindAsset(kcc->Settings);
            kcc->Acceleration = kccSettings.Acceleration;
            kcc->MaxSpeed = kccSettings.BaseSpeed;
            return playerEntity;
        }

        private void PlacePlayerOnSpawnPosition(Frame frame, EntityRef playerEntityRef)
        {
            SpawnPointManager* spawnPointManager = frame.Unsafe.GetPointerSingleton<SpawnPointManager>();
            QList<EntityRef> availableSpawnPoints = frame.ResolveList(spawnPointManager->AvailableSpawnPoints);
            QList<EntityRef> usedSpawnPoints = frame.ResolveList(spawnPointManager->UsedSpawnPoints);
            if (availableSpawnPoints.Count == 0 && usedSpawnPoints.Count == 0)
            {
                foreach (var componentPair in frame.GetComponentIterator<SpawnPoint>())
                {
                    availableSpawnPoints.Add(componentPair.Entity);
                }
            }
            int randomIndex = frame.RNG->Next(0, availableSpawnPoints.Count);
            EntityRef spawnPoint = availableSpawnPoints[randomIndex];
            Transform2D spawnPointTransform = frame.Get<Transform2D>(spawnPoint);
            Transform2D* playerTransform = frame.Unsafe.GetPointer<Transform2D>(playerEntityRef);
            playerTransform -> Position = spawnPointTransform.Position;
            
            availableSpawnPoints.RemoveAt(randomIndex);
            usedSpawnPoints.Add(spawnPoint);

            if (availableSpawnPoints.Count == 0)
            {
                spawnPointManager->AvailableSpawnPoints = usedSpawnPoints;
                spawnPointManager->UsedSpawnPoints = new QListPtr<EntityRef>();
            }
        }
    }
}
