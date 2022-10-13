using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class GameOver : Menu
    {
        private readonly string file = $"assets/highscore.txt";
        
        private int storedScore;
        private int currentScore;
        private string playerName;

        public GameOver(SceneManager sceneManager) : base(sceneManager)
        {
            // Setup button
            MenuPosition = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            AddButton(new Button("Continue", () => Events.PublishChangeScene("HighScore")));

            // Events
            Events.NewScore += GetScore;
        }

        private void GetScore(Scene _, int score , string name)
        {
            currentScore = score;
            playerName = name;
        }
        
        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);

            target.Draw(DrawText(
                $"Total Score: {currentScore}", 
                text, 
                new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2), 
                "middle",
                -50
                ));
            target.Draw(DrawText(
                $"Name: {playerName}", 
                text, 
                new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2), 
                "middle",
                50
            ));
        }
    }
}