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
            AddButton(new Button("Play", () => Events.PublishChangeSceneEvent("NameInput")));
            AddButton(new Button("High Score", () => Events.PublishChangeSceneEvent("HighScore")));
            AddButton(new Button("Quit", () => Events.PublishChangeSceneEvent("Quit")));
        }
    }
}