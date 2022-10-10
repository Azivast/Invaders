using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;

namespace Invaders
{
    public class MainMenu : Menu
    {
        new enum MenuEntries
        {
            Play,
            HighScore,
            Settings,
            Quit,
            
        }
        private Text entryText = new Text("", scene)
        public MainMenu()
        {
        }

        public override void Update()
        {
            switch (selectedEntry)
            {
            }
        }

        public override void Render(RenderTarget target)
        {
            foreach (string entry in Enum.GetNames((typeof(MenuEntries))))
            {
                target.Draw(new);
            }
        }
    }
}