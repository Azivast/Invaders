using System;
using System.Numerics;
using Microsoft.Win32.SafeHandles;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invaders
{
    public class PlayerShip : Actor
    {
        public readonly int MaxHealth = 3;
        private const float ImmortalTime = 3;
        private float immortalTimer = 0;
        public bool IsMortal => immortalTimer <= 0;
        public int Health { get; private set; }
        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 19;
                bounds.Top += 18;
                bounds.Width = 100;
                bounds.Height = 75;
                return bounds;
            }
        }

        public PlayerShip() : base("spriteSheet") { }

        public override void Create(Scene scene)
        {
            Speed = 300;
            sprite.TextureRect = new IntRect(327, 0, 104, 82);
            Facing = new Vector2f(0, -1);
            Health = MaxHealth;
            base.Create(scene);

            // Subscribe to events
            scene.Events.LoseHealth += OnLoseHealth;
        }
        
        private void OnLoseHealth(Scene scene, int amount) 
        {
            if (IsMortal)
            {
                Health -= amount;
                if (Health <= 0)
                {
                    scene.Events.LoseHealth -= OnLoseHealth;
                    IsDead = true;
                    scene.Events.PublishGameOver();
                }
                else
                {
                    immortalTimer = ImmortalTime;
                    scene.Spawn(new Explosion(Position));
                }
            }
        }
        
        

        public override void Destroy(Scene scene)
        {
            scene.Events.LoseHealth -= OnLoseHealth;
            base.Destroy(scene);
        }
        

        protected override void TryShoot(Scene scene)
        {
            if (!ReadyToShoot) return;
            
            CooldownTimer = ShootCooldown;
            ReadyToShoot = false;
            
            Bullet bullet1 = new Bullet(this);
            bullet1.Create(new Vector2f(Bounds.Left + 5, Bounds.Top), Facing, scene);
            
            Bullet bullet2 = new Bullet(this);
            bullet2.Create(new Vector2f(Bounds.Left - 5 + Bounds.Width, Bounds.Top), Facing, scene);
            
            scene.Spawn(bullet1);
            scene.Spawn(bullet2);
            
            LaserSound.Play(); 
        }

        public override void Update(Scene scene, float deltaTime)
        {
            immortalTimer = Math.Max(immortalTimer -= deltaTime, 0);
            Move(deltaTime);
            if (Keyboard.IsKeyPressed(Space)) TryShoot(scene);
            base.Update(scene, deltaTime);
        }
        
        protected override void Move(float deltaTime)
        {
            // Get input
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
            
            // Limit movement to within program window
            Vector2f newPos = Position + movement * Speed * deltaTime;
            newPos.X = Math.Clamp
            (
                newPos.X, 
                Program.ViewSize.Left + Bounds.Width/2,
                Program.ViewSize.Left + Program.ViewSize.Width - Bounds.Width/2
            );
            
            newPos.Y = Math.Clamp
            (
                newPos.Y, 
                Program.ViewSize.Top + Bounds.Height/2, 
                Program.ViewSize.Top + Program.ViewSize.Height - Bounds.Height/2
            );

            
            // Move ship
            Position = newPos;
        }

        public override void Render(RenderTarget target)
        {
            if (IsMortal) sprite.Color = Color.White;
            else sprite.Color = new Color(255, 255, 255,  100);
            base.Render(target);
        }
    }
}