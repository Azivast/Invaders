using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class GameOver : Menu
    {
        private string input;
        private int currentScore;

        public GameOver(SceneManager sceneManager, RenderWindow window) : base(sceneManager, window)
        {
            // Continue Button
            buttons.Position = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            buttons.AddButton(new Button("Continue", NextScene),this);
            buttons.Buttons[0].Active = false; // will be activated once a name is entered
            
            Events.NewScore += GetScore;
        }

        public override void LoadScene(RenderWindow window)
        {
            input = "";
            window.TextEntered += TextEntered;
            base.LoadScene(window);
        }
        
        public override void UnLoadScene(RenderWindow window)
        {
            window.TextEntered -= TextEntered;
            base.UnLoadScene(window);
        }

        private void GetScore(Scene _, int score)
        {
            currentScore = score;
        }

        private void NextScene()
        {
            Events.PublishFinalScore(currentScore, input.Trim());
            Events.PublishChangeScene("HighScore");
        }
        
        private void TextEntered(object s, EventArgs e)
        {
            if (e is not TextEventArgs eventArgs) return;
            
            if ((char.IsLetter(eventArgs.Unicode[0]) || char.IsNumber(eventArgs.Unicode[0])) && input.Length <= 16) // add if nr or letter
                input += eventArgs.Unicode;

            else if (eventArgs.Unicode.Equals("\b")) // remove if backspace
            {
                if (input.Length > 0)
                {
                    input = input.Remove(input.Length - 1);
                }
            }
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
            buttons.Buttons[0].Active = input.Length >= 2; // Disable button if name is less than 2 characters
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);

            target.Draw(DrawText(
                $"Total Score: {currentScore}", 
                text, 
                new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2), 
                "middle",
                -text.GetGlobalBounds().Height
            ));
            target.Draw(DrawText(
                $"Enter Name: {input}", 
                text, 
                new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2), 
                "middle",
                +text.GetGlobalBounds().Height
            ));
        }
    }
}