using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class MainMenu : Menu
    {
        public MainMenu(SceneManager sceneManager, RenderWindow window) : base(sceneManager)
        {
            buttons.AddButton(new Button("Play", () => Events.PublishChangeScene("GamePlay")), this);
            buttons.AddButton(new Button("High Score", () => Events.PublishChangeScene("HighScore")),this);
            buttons.AddButton(new Button("Quit", () => Events.PublishChangeScene("Quit")),this);

            text.Scale = new Vector2f(0.7f, 0.7f);
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            target.Draw(DrawText(
                "INVADERS",
                new Vector2f(Program.ViewSize.Width / 2, 150),
                "middle"
                ));
            
        }
    }
}