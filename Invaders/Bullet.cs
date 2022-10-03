using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private Vector2f direction;
        private const float Speed = 250;
        private Actor parent;
        public Actor Parent
        {
            get => parent;
        }

        public Bullet(Actor parent) : base("spriteSheet")
        {
            this.parent = parent;
        }

        public void Create(Vector2f position, Vector2f direction, Scene scene) // no need for override since we are overloading
        {
            base.Create(scene);
            this.direction = direction;
            sprite.Rotation = MathF.Atan2(-direction.X, direction.Y) * 180 / MathF.PI; // Rotation as degrees
            Position = position;
            sprite.TextureRect = new IntRect(843, 62, 13, 54);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Position += direction * Speed * deltaTime;
;
            if (Position.Y < Program.ViewSize.Top - Bounds.Height)
                IsDead = true;
                
            base.Update(scene, deltaTime);
        }

        protected override void CollideWithEntity(Scene scene, Entity e)
        {
            if (parent is EnemyShip && e is EnemyShip) return;
            if (parent is PlayerShip && e is PlayerShip) return;

            if (e is PlayerShip)
            {
                scene.Events.PublishLooseHealth(1);
                IsDead = true;
            }
            else if (e is EnemyShip)
            {
                ((EnemyShip)e).Kill(scene);
                IsDead = true;
            } 
        }
    }
}