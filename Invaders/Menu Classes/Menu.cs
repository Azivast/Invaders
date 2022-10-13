using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public abstract class Menu : Scene
    {
        protected const string Font = "kenvector_future";
        protected ButtonList buttons;
        protected Text text;

        public Menu(SceneManager sceneManager, RenderWindow window) : base(sceneManager)
        {
            buttons = new ButtonList();
            buttons.Position = new Vector2f(Program.ViewSize.Width / 2, Program.ViewSize.Height/2 - 100);
            
            text = new Text
            {
                Font = Assets.LoadFont(Font),
                CharacterSize = 72,
                Scale = new Vector2f(0.4f, 0.4f),
                FillColor = Color.White,
                Position = new Vector2f(Program.ViewSize.Width/2, 100)
            };
        }

        public override void UpdateAll(float deltaTime)
        {
            buttons.Update();
            base.UpdateAll(deltaTime);
        }

        public override void LoadScene(RenderWindow window)
        {
            window.KeyReleased += buttons.OnKeyPressed;
        }

        public override void UnLoadScene(RenderWindow window)
        {
            window.KeyReleased -= buttons.OnKeyPressed;
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