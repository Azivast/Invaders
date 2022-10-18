using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.ScoreIO;

namespace Invaders
{
    public class HighScore : Menu
    {
        private const int HSpacing = 50;
        private const int WSpacing = 100;

        private readonly string file = $"highscore.dat";
        private SaveData data = new SaveData(10);

        public HighScore(SceneManager sceneManager, RenderWindow window) : base(sceneManager)
        {
            // Back Button
            buttons.Position = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            buttons.AddButton(new Button("Back", () => Events.PublishChangeScene("MainMenu")), this);

            // Events
            Events.NewName += OnName;
        }
        
        public override void LoadScene(RenderWindow window)
        {
            base.LoadScene(window);
            data = LoadData(file);
        }
        
        private void OnName(int score, string name)
        {
            SaveAndSortHighScore(file, score, name);
        }
        
        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);

            target.Draw(DrawText(
                "High Score",
                new Vector2f(Program.ViewSize.Width/2, 0),
                "middle",
                WSpacing / 2
            ));
            
            // Draw Score list
            for (int i = 0; i <= data.Count-1; i++)
            {
                target.Draw(DrawText( // place
                    $"{i+1}:",
                    new Vector2f(Program.ViewSize.Left + WSpacing, HSpacing * i),
                    "left",
                    100
                    ));
                target.Draw(DrawText( // name
                    $"{data.PlayerName[i]}",
                    new Vector2f(Program.ViewSize.Left + WSpacing + 50, HSpacing * i),
                    "left",
                    100
                ));
                target.Draw(DrawText( // score
                    $"{data.Score[i]}",
                    new Vector2f(Program.ViewSize.Width - 50, HSpacing * i),
                    "right",
                    100
                ));
            }
        }
    }
}