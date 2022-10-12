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
    public class NameInput : Menu
    {
        private string input;
        
        public NameInput(SceneManager sceneManager, RenderWindow window) : base(sceneManager)
        {
            // Continue Button
            MenuPosition = new(Program.ViewSize.Width / 2, Program.ViewSize.Height - 100);
            AddButton((new Button("Continue", () => Events.PublishChangeSceneEvent("GamePlay"))));
            
            window.TextEntered += TextEntered;
        }
        
        private void TextEntered(object s, EventArgs e)
        {
            if (e is TextEventArgs eventArgs)
                input += eventArgs.Unicode;
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
        }


        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            target.Draw(DrawText(input, text, new Vector2f(Program.ViewSize.Width/2, 100), "middle"));
        }
    }
}