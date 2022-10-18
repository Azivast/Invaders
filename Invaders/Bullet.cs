using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private Vector2f direction;
        private readonly float speed = 200;
        private readonly Actor parent;

        private FloatRect hitBoxLocalBounds;
        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 0;
                bounds.Top += 11;
                bounds.Width = 13;
                bounds.Height = 13;
                return bounds;
            }
        }

        public Bullet(Actor parent) : base("spriteSheet")
        {
            this.parent = parent;
            speed += parent.Speed; // speed is also inherited from parent
        }

        public void Create(Vector2f position, Vector2f direction, Scene scene)
        {
            base.Create(scene);
            this.direction = direction;
            sprite.Rotation = MathF.Atan2(direction.X, -direction.Y) * 180 / MathF.PI; // rotation as degrees
            Position = position;
            sprite.TextureRect = new IntRect(438, 16, 13, 54);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            // Move
            Position += direction * speed * deltaTime;
;
            // Remove once outside window
            if (!Program.ViewSize.Contains(Position.X, Position.Y))
                IsDead = true;
                
            base.Update(scene, deltaTime);
        }

        protected override void CollideWithEntity(Scene scene, Entity e)
        {
            // Do nothing if bullet collides with ship of same type, ie player or enemy.
            if (parent is EnemyShip && e is EnemyShip) return;
            if (parent is PlayerShip && e is PlayerShip) return;

            // Otherwise kill ship (implementation dependent on type)
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