using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invaders
{
    public class PlayerShip : Actor
    {
        private const int Speed = 100;
        private const float ImmortalTime = 100;
        private float immortalTimer = 0;
        private int Health = 3;

        public PlayerShip() : base("spriteSheet")
        {
        }

        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(224, 832, 99, 75);
            facing = new Vector2f(0, -1);
            base.Create(scene);
        }
        
        private void OnLoseHealth(Scene scene, int amount) 
        {
            if (immortalTimer <= 0)
            {
                Health -= amount;
                immortalTimer = ImmortalTime;
                Console.WriteLine("hit player");
                scene.Spawn(new Explosion(Position));
            }
        }
        
        protected override void TryShoot(Scene scene)
        {
            if (!ReadyToShoot) return;
            
            cooldownTimer = ShootCooldown;
            ReadyToShoot = false;
            
            Bullet bullet1 = new Bullet(this);
            bullet1.Create(new Vector2f(Bounds.Left, Bounds.Top), facing, scene);
            
            Bullet bullet2 = new Bullet(this);
            bullet2.Create(new Vector2f(Bounds.Left + Bounds.Width, Bounds.Top), facing, scene);
            
            scene.Spawn(bullet1);
            scene.Spawn(bullet2);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            immortalTimer--;
            
            Vector2f movement = new Vector2f();
            if (Keyboard.IsKeyPressed(Left))
            {
                movement += new Vector2f(-1, 0);
            }
            if (Keyboard.IsKeyPressed(Right))
            {
                movement += new Vector2f(1, 0);
            }
            if (Keyboard.IsKeyPressed(Up))
            {
                movement += new Vector2f(0, -1);
            }
            if (Keyboard.IsKeyPressed(Down))
            {
                movement += new Vector2f(0, 1);
            }

            Position += movement * Speed * deltaTime;

            if (Keyboard.IsKeyPressed(Space))
            {
                TryShoot(scene);
            }

            base.Update(scene, deltaTime);
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}