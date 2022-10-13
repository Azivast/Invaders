using System;
using System.Diagnostics;

namespace Invaders
{
    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);

        public delegate void FinalScoreEvent(int value, string text);
        public delegate void ChangeSceneEvent(string newScene);
        public delegate void GameOverEvent();
        
        public event ValueChangedEvent LoseHealth;
        public event FinalScoreEvent NewName;
        public event ValueChangedEvent NewScore;
        public event ChangeSceneEvent ChangeToScene;
        public event GameOverEvent GameOver;

        
        private int newScore;
        private int healthLost;
        
        // Called from other classes:
        public void PublishLooseHealth(int amount) 
            => healthLost += amount;

        public void PublishNewScore(int score)
            => newScore = score;
        
        public void PublishFinalScore(int score, string playerName)
            => NewName?.Invoke(score, playerName);

        public void PublishChangeScene(string newScene)
            => ChangeToScene?.Invoke(newScene);
        public void PublishGameOver()
            => GameOver?.Invoke();


        public void Update(Scene scene)
        {
            // Invoke event when its condition has been met
            if (healthLost != 0) 
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }
            if (newScore != 0) 
            {
                NewScore?.Invoke(scene, newScore);
                newScore = 0;
            }
        }
    }
}