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
    public class NameInput : Menu
    {
        private string input = "";
        private RenderWindow window;
        
        public NameInput(SceneManager sceneManager, RenderWindow window) : base(sceneManager)
        {
            // Continue Button
            MenuPosition = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            AddButton(new Button("Continue", () => NextScene()));

            this.window = window;
            Events.ChangeToScene += SceneChange;
        }

        private void SceneChange(string newScene)
        {
            if (newScene.Equals("NameInput"))
                window.TextEntered += TextEntered;
            else
                window.TextEntered -= TextEntered;
        }

        private void NextScene()
        {
            Events.PublishNewName(input.Trim());
            Events.PublishChangeScene("GamePlay");
        }
        
        private void TextEntered(object s, EventArgs e)
        {
            if (e is not TextEventArgs eventArgs) return;
            
            if (char.IsLetter(eventArgs.Unicode[0]) || char.IsNumber(eventArgs.Unicode[0])) // add if nr or letter
                input += eventArgs.Unicode;

            else if (eventArgs.Unicode.Equals("\b")) // remove if backspace
            {
                if (input.Length > 0)
                {
                    input = input.Remove(input.Length - 1);
                }
            }
            
            else if (eventArgs.Unicode.Equals("\r") && !input.Equals("")) // enter pressed -> move to gameplay
            {
                NextScene();
            }
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
        }


        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            target.Draw(DrawText("Enter a n", text, new Vector2f(Program.ViewSize.Width/2, 100), "middle"));
            target.Draw(DrawText(input, text, new Vector2f(Program.ViewSize.Width/2, 100), "middle"));
        }
    }
}