using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Actor : Entity
    {
        protected virtual float ShootCooldown { get; } = 0.5f; // property so that it can be overriden in children
        public bool ReadyToShoot;
        protected float cooldownTimer;
        
        protected Vector2f facing;
        
        protected Actor(string textureName) : base(textureName) {}

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2, sprite.TextureRect.Height / 2);
        }

        protected virtual void TryShoot(Scene scene)
        {
            if (!ReadyToShoot) return;
            
            cooldownTimer = ShootCooldown;
            ReadyToShoot = false;
            
            Bullet bullet = new Bullet(this);
            bullet.Create(Position, facing, scene);
            scene.Spawn(bullet);
        }
        
        protected virtual void Move(float deltaTime) {}

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            
            // Update shooting cooldown timer
            if (!ReadyToShoot)
                cooldownTimer -= deltaTime;
            if (cooldownTimer <= 0)
            {
                ReadyToShoot = true;
                cooldownTimer = ShootCooldown;
            }
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);
        }
    }
}