using System;
using System.Buffers.Text;
using System.Numerics;
using System.Runtime.Serialization.Json;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class Button : Entity
    {
        private const string Font = "kenvector_future";
        private Text text = new Text();
        private Action clickAction;
        private bool selected = false;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                sprite.Color = value ? Color.Cyan : Color.Red;
            }
        }

        public Action Click
        {
            get { return clickAction; }
            set { clickAction = value;  }
        }

        public override Vector2f Position
        {
            get { return sprite.Position; }
            set
            {
                sprite.Position = value;
                text.Position = sprite.Position - new Vector2f(text.GetGlobalBounds().Width/2, text.GetGlobalBounds().Height);
            }
        }
        public Button(string buttonText, Action clickAction) : base("UISheet")
        {
            text.DisplayedString = buttonText;
            this.clickAction = clickAction;
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            
            sprite.TextureRect = new IntRect(0, 0, 222, 39);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);

            text.Font = scene.Assets.LoadFont(Font);
            text.CharacterSize = 72;
            text.Scale = new Vector2f(0.3f, 0.3f);
            text.FillColor = Color.Black;

            base.Create(scene);
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
            target.Draw(text);
        }
    }
}