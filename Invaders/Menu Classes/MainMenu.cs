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
        public MainMenu(SceneManager sceneManager) : base(sceneManager)
        {
            AddButton(new Button("Play", () => Events.PublishChangeScene("NameInput")));
            AddButton(new Button("High Score", () => Events.PublishChangeScene("HighScore")));
            AddButton(new Button("Quit", () => Events.PublishChangeScene("Quit")));
        }
    }
}