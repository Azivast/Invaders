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
        private readonly Color SelectedColor = Color.White;
        private readonly Color UnSelectedColor = new Color(150, 150, 150);
        private const string Font = "kenvector_future";
        private readonly Text text = new Text();
        private readonly Action clickAction;
        public bool Selected = false;
        public bool Active = true;

        public override Vector2f Position
        {
            get
            {
                return sprite.Position;
            }
            set
            {
                sprite.Position = value;
                text.Position = sprite.Position - new Vector2f(text.GetGlobalBounds().Width/2, text.GetGlobalBounds().Height);
            }
        }
        
        public Button(string buttonText, Action clickAction) : base("spriteSheet")
        {
            text.DisplayedString = buttonText;
            this.clickAction = clickAction;
        }
        
        public void Click()
        {
            if (Active)
                clickAction();
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
            if (Active && Selected) sprite.Color = SelectedColor;
            else sprite.Color = UnSelectedColor;
            
            base.Render(target);
            target.Draw(text);

        }
    }
}