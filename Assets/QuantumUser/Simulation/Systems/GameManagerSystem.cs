using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class GameManagerSystem : SystemMainThread, ISignalOnComponentAdded<GameManager>, ISignalPLayerKilled
    {
        public void OnAdded(Frame f, EntityRef entity, GameManager* gameManager)
        {
            GameManagerConfig config = f.FindAsset(gameManager->GameManagerConfig);
            gameManager->TimeToWaitForPlayers = config.TimeToWaitForPlayers;
        }

        public override void Update(Frame f)
        {
            GameManager* gameManager = f.Unsafe.GetPointerSingleton<GameManager>();
            if (gameManager->CurrentGameState != GameState.WaitingForPlayers)
                return;
            
            gameManager->TimeToWaitForPlayers -= f.DeltaTime;

            if (gameManager->TimeToWaitForPlayers > FP._0)
                return;
            
            gameManager->CurrentGameState = f.ComponentCount<PlayerLink>() > 1
                ? GameState.Playing 
                : GameState.GameOver;

            if (gameManager->CurrentGameState == GameState.GameOver)
            {
                EntityRef winner = GetWinner(f);
                if (winner == EntityRef.None)
                {
                    Log.Error("No winner found");
                    return;
                }
                
                f.Events.OnGameOver(winner);
            }
        }

        public void PLayerKilled(Frame f)
        {
        }

        private EntityRef GetWinner(Frame f)
        {
            EntityRef winner = EntityRef.None;
            foreach (var entityPair in f.GetComponentIterator<PlayerLink>()) 
                winner = entityPair.Entity;
            
            return winner;
        }
    }
}
