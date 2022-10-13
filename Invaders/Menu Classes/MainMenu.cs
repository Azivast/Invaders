using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class MainMenu : Menu
    {
        public MainMenu(SceneManager sceneManager, RenderWindow window) : base(sceneManager, window)
        {
            buttons.AddButton(new Button("Play", () => Events.PublishChangeScene("GamePlay")), this);
            buttons.AddButton(new Button("High Score", () => Events.PublishChangeScene("HighScore")),this);
            buttons.AddButton(new Button("Quit", () => Events.PublishChangeScene("Quit")),this);
        }
    }
}