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
        
        public virtual Vector2f Position
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
        
        public void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(0, 0, 222, 39);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);

            text.Font = scene.Assets.LoadFont(Font);
            text.CharacterSize = 72;
            text.Scale = new Vector2f(0.3f, 0.3f);
            text.FillColor = Color.Black;

            base.Create(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            sprite.Color = Color.White;
            CollideWithMouse(scene);
        }
        
        private void CollideWithMouse(Scene scene)
        {
            if (!MouseCollision.HitBox.Intersects(Bounds)) return;
            
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                clickAction();
            }
            else sprite.Color = Color.Cyan;
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
            target.Draw(text);
        }
    }
}