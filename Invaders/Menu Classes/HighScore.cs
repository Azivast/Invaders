using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly string file = $"assets/highscore.dat";
        private SaveData data = new SaveData(10);

        public HighScore(SceneManager sceneManager) : base(sceneManager)
        {
            // Back Button
            MenuPosition = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            AddButton(new Button("Back", () => Events.PublishChangeScene("MainMenu")));

            // Events
            Events.ChangeToScene += OnLoad;
            Events.NewScore += OnScore;
        }

        private void OnLoad(string scene)
        {
            if (scene != "HighScore") return;
            
            data = LoadData(file);
        }
        
        private void OnScore(Scene _, int score, string name)
        {
            SaveAndSortHighScore(file, score, name);
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);

            target.Draw(DrawText(
                "High Score",
                text,
                new Vector2f(Program.ViewSize.Width/2, 0),
                "middle",
                WSpacing / 2
            ));
            
            // Draw Score list
            for (int i = 0; i <= data.Count-1; i++)
            {
                target.Draw(DrawText(
                    $"{i+1}:",
                    text,
                    new Vector2f(Program.ViewSize.Left + WSpacing, HSpacing * i),
                    "left",
                    100
                    ));
                target.Draw(DrawText(
                    $"{data.PlayerName[i]}",
                    text,
                    new Vector2f(Program.ViewSize.Left + WSpacing + 50, HSpacing * i),
                    "left",
                    100
                ));
                target.Draw(DrawText(
                    $"{data.Score[i]}",
                    text,
                    new Vector2f(Program.ViewSize.Width - 50, HSpacing * i),
                    "right",
                    100
                ));
                
                //if (data.Score[i] == 0) break;
            }
        }
    }
}