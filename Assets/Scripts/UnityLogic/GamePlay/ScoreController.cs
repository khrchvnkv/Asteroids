using System;

namespace UnityLogic.GamePlay
{
    public sealed class ScoreController
    {
        public int Score { get; private set; }

        public event Action<int> OnScoreChanged;

        public ScoreController()
        {
            GameCore.Instance.EventManager.Subscribe<OnEnemyKilledEvent>(this, OnEnemyKilled);
        }
        ~ScoreController()
        {
            GameCore.Instance.EventManager.Unsubscribe<OnEnemyKilledEvent>(this);
        }
        public void ResetScore()
        {
            Score = 0;
            UpdateScore();
        }
        private void OnEnemyKilled(in OnEnemyKilledEvent args)
        {
            Score++;
            UpdateScore();
        }
        private void UpdateScore()
        {
            OnScoreChanged?.Invoke(Score);
        }
    }
}