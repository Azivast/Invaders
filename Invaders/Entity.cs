using System.Diagnostics;
using System.Reflection.Metadata;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Entity
    {
        private string textureName = "";
        protected Sprite sprite;
        public bool IsDead = false;

        public virtual Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();
        public virtual FloatRect HitBox => Bounds;
        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }
        protected Entity() // Entities with no need for sprite receive a blank texture
        {
            this.textureName = "blank";
            sprite = new Sprite();
        }

        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }
        
        public virtual void Destroy(Scene scene) {} // Implemented by children.
        
        protected virtual void CollideWithEntity(Scene scene, Entity other) {} // Implemented by children.

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(HitBox))
            {
                CollideWithEntity(scene, found);
            }
        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}