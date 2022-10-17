using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private Vector2f direction;
        private float speed = 200;
        private Actor parent;
        public Actor Parent => parent;
        
        
        private FloatRect hitBox;
        public override FloatRect HitBox
        {
            get
            {
                return new FloatRect(
                    Position.X - sprite.Origin.X + hitBox.Left,
                    Position.Y - sprite.Origin.Y + hitBox.Top,
                    hitBox.Width,
                    hitBox.Height);
            }
        }

        public Bullet(Actor parent) : base("spriteSheet")
        {
            this.parent = parent;
            this.speed += parent.Speed;
        }

        public void Create(Vector2f position, Vector2f direction, Scene scene) // no need for override since we are overloading
        {
            base.Create(scene);
            this.direction = direction;
            sprite.Rotation = MathF.Atan2(direction.X, -direction.Y) * 180 / MathF.PI; // Rotation as degrees
            Position = position;
            sprite.TextureRect = new IntRect(438, 16, 13, 54);
            hitBox = new FloatRect(0, 11, 13, 13);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Position += direction * speed * deltaTime;
;
            if (!Program.ViewSize.Contains(Position.X, Position.Y))
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