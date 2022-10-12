using System;
using System.Diagnostics;

namespace Invaders
{
    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);
        public delegate void ChangeSceneEvent(string newScene);
        public delegate void GameOverEvent();
        
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent ScoreBroadcast;
        public event ChangeSceneEvent ChangeToScene;
        public event GameOverEvent GameOver;

        
        private int newScore;
        private int healthLost;
        
        // Called from other classes:
        public void PublishLooseHealth(int amount) 
            => healthLost += amount;

        public void PublishScoreEvent(int newScore)
        {
            Debug.WriteLine($"Event: PublishHighScoreEvent called with score {newScore}");
            this.newScore = newScore;
        }
        public void PublishChangeSceneEvent(string newScene)
            => ChangeToScene?.Invoke(newScene);
        public void PublishGameOverEvent()
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
                ScoreBroadcast?.Invoke(scene, newScore);
                newScore = 0;
            }
        }
    }
}