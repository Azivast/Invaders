using System.Diagnostics;
using System.Reflection.Metadata;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Entity
    {
        private readonly string textureName = "blank"; // set by children that need a texture
        protected Sprite sprite;
        public bool IsDead = false;
        public virtual Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public virtual FloatRect Bounds => sprite.GetGlobalBounds();

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }
        
        protected Entity() 
        {
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
            foreach (Entity found in scene.FindIntersects(Bounds))
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