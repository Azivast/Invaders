﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class MainMenu : Scene
    {
        private readonly List<Button> MenuEntries;

        private const int ButtonSpacing = 20;
        
        public MainMenu(SceneManager sceneManager) : base(sceneManager)
        {
            MenuEntries = new List<Button>
            {
                new Button("Play", () => Events.PublicChangeSceneEvent("GamePlay")),
                new Button("High Score", () => Events.PublicChangeSceneEvent("HighScore")),
                new Button("Settings", () => Events.PublicChangeSceneEvent("GamePlay")),
                new Button("Quit", () => Events.PublicChangeSceneEvent("HighScore")),
            };
            
            
            // Setup buttons
            for (int i = 0; i < MenuEntries.Count; i++)
            {
                Button button = MenuEntries[i];
                button.Create(this);
                button.Position = new( // Set spacing
                    Program.ViewSize.Width / 2,
                    100 + (i * (button.Bounds.Height + ButtonSpacing))
                    );
            }
            
            foreach (Button button in MenuEntries)
            {
                entities.Add(button);
            }
        }
    }
}