using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invaders
{
    public class EnemyShip : Actor
    {
        private const double FiringFrequency = 0.0005f;
        protected override float ShootCooldown { get; } = 1.5f;
        private Random random;
        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 8;
                bounds.Top += 0;
                bounds.Width = 80;
                bounds.Height = 80;
                return bounds;
            }
        }

        public EnemyShip(float speed) : base("spriteSheet")
        {
            Speed = speed;
        }

        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(224, 0, 102, 84);
            base.Create(scene);

            random = new Random();
            Position = new Vector2f(
                random.Next((int)Bounds.Width, (int)(Program.ViewSize.Width - Bounds.Width)), // rand starting pos
                0 - Bounds.Height
                );
            Facing.X = random.Next(0, 2) * 2 - 1; // 0*2-1 = -1 or 1*2-1 = 1
            Facing.Y = 1;
            Facing = Facing / MathF.Sqrt(Facing.X * Facing.X + Facing.Y * Facing.Y); // normalize
        }
        
        public void Kill(Scene scene)
        {
            IsDead = true;
            scene.Spawn(new Explosion(Position));
        }

        protected override void Move(float deltaTime)
        {
            // Mirror X direction when hitting a wall
            if (Bounds.Left <= Program.ViewSize.Left)
            {
                Facing.X = Math.Abs(Facing.X);
            }
            else if (Bounds.Left + Bounds.Width >= Program.ViewSize.Width)
            {
                Facing.X = Math.Abs(Facing.X)*-1;
            }
            
            // Teleport to top of screen if at bottom 
            if (Position.Y >= Program.ViewSize.Height + Bounds.Height)
                Position = new Vector2f(Position.X, Program.ViewSize.Top - Bounds.Height);
            
            // Move
            Position += Facing * Speed * deltaTime;
        }
        
        protected override void CollideWithEntity(Scene scene, Entity e)
        {
            if (e is not PlayerShip) return; // Only handles collision with player
            scene.Events.PublishLooseHealth(1);
            Kill(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            // Move
            Move(deltaTime);
            
            // Rotate
            float newRotation = MathF.Atan2(-Facing.X, Facing.Y) * 180 / MathF.PI; // Rotation as degrees
            sprite.Rotation = newRotation; // rotate sprite in direction
            
            // Shoot
            if (random.NextDouble() <= FiringFrequency ) TryShoot(scene);

            base.Update(scene, deltaTime);
        }
    }
}