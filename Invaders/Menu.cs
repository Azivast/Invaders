using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public abstract class Menu : Scene
    {
        protected List<Button> MenuEntries = new List<Button>();
        protected Vector2f MenuPosition;
        protected const string Font = "kenvector_future";
        protected const int ButtonSpacing = 20;
        protected Text text;

        public Menu(SceneManager sceneManager) : base(sceneManager)
        {
            MenuPosition = new Vector2f(Program.ViewSize.Width / 2, 100);
            
            text = new Text
            {
                Font = Assets.LoadFont(Font),
                CharacterSize = 72,
                Scale = new Vector2f(0.4f, 0.4f),
                FillColor = Color.White,
                Position = new Vector2f(Program.ViewSize.Width/2, 100)
            };
        }

        protected virtual void AddButton(Button button)
        {
            // Setup buttons
            MenuEntries.Add(button);
            button.Create(this);
            button.Position = new( // Set spacing
                MenuPosition.X,
                MenuPosition.Y + (MenuEntries.IndexOf(button) * (button.Bounds.Height + ButtonSpacing))
            );

            entities.Add(button);
        }

        // Used to draw and align multiple strings using the same Text variable.
        protected Text DrawText(string displayedString, Text text, Vector2f position, string align = "left", float yOffset = 0)
        {
            text.DisplayedString = displayedString;
            switch (align.ToLower())
            {
                case "left":
                    text.Origin = new Vector2f(0, 0);
                    break;
                case "right":
                    text.Origin = new Vector2f(text.GetLocalBounds().Width, 0);
                    break;
                case "middle":
                case "center":
                    text.Origin = new Vector2f(text.GetLocalBounds().Width/2, 0);
                    break;
            }
            text.Position = position + new Vector2f(0, yOffset);
            return text;
        }
    }
}