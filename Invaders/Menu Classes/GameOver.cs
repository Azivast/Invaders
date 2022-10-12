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
    public class GameOver : Scene
    {
        private readonly List<Button> MenuEntries;
        private const string Font = "kenvector_future";
        private readonly string file = $"assets/highscore.txt";
        
        private int storedScore;
        private int currentScore;
        private Text highScoreText = new Text();
        private Text nameText = new Text();
        
        public GameOver(SceneManager sceneManager) : base(sceneManager)
        {
            
            highScoreText.Font = Assets.LoadFont(Font);
            highScoreText.CharacterSize = 72;
            highScoreText.Scale = new Vector2f(0.3f, 0.3f);
            highScoreText.FillColor = Color.White;
            
            nameText.Font = Assets.LoadFont(Font);
            nameText.CharacterSize = 72;
            nameText.Scale = new Vector2f(0.3f, 0.3f);
            nameText.FillColor = Color.White;

            // Setup button
            Button button = new Button("Continue", () => Events.PublishChangeSceneEvent("HighScore"));
            button.Create(this);
            button.Position = new( // Set spacing
                Program.ViewSize.Width / 2,
                100 + (button.Bounds.Height)
                );
            entities.Add(button);

                // Events
            Events.ScoreBroadcast += GetScore;
        }

        private void GetScore(Scene _, int score)
        {
            currentScore = score;
        }
        
        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);

            highScoreText.DisplayedString = $"Total Score: {currentScore}";
            nameText.DisplayedString = $"Total Score: blablahtest";
            target.Draw(highScoreText);
        }
    }
}