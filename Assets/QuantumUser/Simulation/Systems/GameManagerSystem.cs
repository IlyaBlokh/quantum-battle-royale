using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class GameManagerSystem : SystemMainThread, ISignalOnComponentAdded<GameManager>, ISignalPLayerKilled, ISignalOnPlayerDisconnected
    {
        public void OnAdded(Frame f, EntityRef entity, GameManager* gameManager)
        {
            GameManagerConfig config = f.FindAsset(gameManager->GameManagerConfig);
            gameManager->TimeToWaitForPlayers = config.TimeToWaitForPlayers;
        }

        public override void Update(Frame f)
        {
            GameManager* gameManager = f.Unsafe.GetPointerSingleton<GameManager>();
            if (gameManager->CurrentGameState == GameState.WaitingForPlayers)
            {
                gameManager->TimeToWaitForPlayers -= f.DeltaTime;
                if (gameManager->TimeToWaitForPlayers > FP._0)
                    return;
            }
            
            GameManagerConfig config = f.FindAsset(gameManager->GameManagerConfig);
            if (config.EnableSandbox)
            { 
                gameManager->CurrentGameState = GameState.Playing;
                return;
            }
            
            gameManager->CurrentGameState = f.ComponentCount<PlayerLink>() > 1
                ? GameState.Playing 
                : GameState.GameOver;

            if (gameManager->CurrentGameState == GameState.GameOver)
            { 
                if (GetWinner(f, out EntityRef winner))
                    f.Events.OnGameOver(winner);
                else
                    Log.Error("No winner found");
            }
        }

        public void PLayerKilled(Frame f)
        {
            EvaluateGameOverCondition(f);
        }

        public void OnPlayerDisconnected(Frame f, PlayerRef player)
        {
            foreach (var entityPair in f.GetComponentIterator<PlayerLink>())
            {
                if (entityPair.Component.PlayerRef == player)
                {
                    f.Destroy(entityPair.Entity);
                    break;
                }
            }
            
            EvaluateGameOverCondition(f);
        }

        private bool GetWinner(Frame f, out EntityRef winner)
        {
            winner = EntityRef.None;
            foreach (var entityPair in f.GetComponentIterator<PlayerLink>()) 
                winner = entityPair.Entity;
            
            return winner != EntityRef.None;
        }

        private void EvaluateGameOverCondition(Frame f)
        {
            GameManager* gameManager = f.Unsafe.GetPointerSingleton<GameManager>();
            if (gameManager->CurrentGameState != GameState.Playing)
                return;
            
            var count = f.ComponentCount<PlayerLink>();
            if (count > 1)
                return;

            if (GetWinner(f, out EntityRef winner))
            {
                f.Events.OnGameOver(winner);
                gameManager->CurrentGameState = GameState.GameOver;
            }
        }
    }
}
