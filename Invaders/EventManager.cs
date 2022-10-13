using System;
using System.Diagnostics;

namespace Invaders
{
    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);
        public delegate void StringChangedEvent(string text);
        public delegate void ScoreChangedEvent(Scene scene, int value, string playerName);
        public delegate void ChangeSceneEvent(string newScene);
        public delegate void GameOverEvent();
        
        public event ValueChangedEvent LoseHealth;
        public event StringChangedEvent NewName;
        public event ScoreChangedEvent NewScore;
        public event ChangeSceneEvent ChangeToScene;
        public event GameOverEvent GameOver;

        
        private int newScore;
        private string newPlayerName;
        private int healthLost;
        
        // Called from other classes:
        public void PublishLooseHealth(int amount) 
            => healthLost += amount;
        
        public void PublishNewName(string playerName)
            => NewName?.Invoke(playerName);

        public void PublishNewScore(int newScore, string playerName)
        {
            this.newScore = newScore;
            newPlayerName = playerName;
        }
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
                NewScore?.Invoke(scene, newScore, newPlayerName);
                newScore = 0;
            }
        }
    }
}