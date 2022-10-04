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
        private const int Speed = 100;
        private const double FiringFrequency = 0.0005f;
        protected override float ShootCooldown { get; } = 1.5f;
        private Random random;

        public EnemyShip() : base("spriteSheet") {}

        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(222, 0, 103, 84);
            base.Create(scene);

            random = new Random();
            Position = new Vector2f(
                random.Next((int)Bounds.Width, (int)(Program.ViewSize.Width - Bounds.Width)), // rand starting pos
                    0 - Bounds.Height);
            facing.X = random.Next(0, 2) * 2 - 1; // 0*2-1 = -1 or 1*2-1 = 1
            facing.Y = 1;
            facing = facing / MathF.Sqrt(facing.X * facing.X + facing.Y * facing.Y); // normalize
        }
        
        public void Kill(Scene scene)
        {
            IsDead = true;
            scene.Spawn(new Explosion(Position));
        }

        
        protected override void Move(float deltaTime)
        {
            // Mirror X direction when hitting a wall
            if (Bounds.Left <= Program.ViewSize.Left || Bounds.Left + Bounds.Width >= Program.ViewSize.Width)
                facing.X = -facing.X;
            
            // Teleport to top of screen if at bottom 
            if (Position.Y >= Program.ViewSize.Height)
                Position = new Vector2f(Position.X, Program.ViewSize.Top);
            
            // Move
            Position += facing * Speed * deltaTime;
        }
        
        protected override void CollideWithEntity(Scene scene, Entity e)
        {
            if (e is not PlayerShip) return;
            
            scene.Events.PublishLooseHealth(1);
            Kill(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            // Move
            Move(deltaTime);
            
            // Rotate
            float newRotation = MathF.Atan2(-facing.X, facing.Y) * 180 / MathF.PI; // Rotation as degrees
            sprite.Rotation = newRotation; // rotate sprite in direction
            
            // Shoot
            if (random.NextDouble() <= FiringFrequency ) TryShoot(scene);

            base.Update(scene, deltaTime);
        }
    }
}