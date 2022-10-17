using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Actor : Entity
    {
        public float Speed = 200;
        protected virtual float ShootCooldown { get; } = 0.5f; // property so that it can be overriden in children
        public bool ReadyToShoot;
        protected float cooldownTimer;
        private SoundBuffer laserBuffer;
        protected Sound laserSound;
        
        protected Vector2f facing;
        
        
        protected FloatRect hitBox;
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

        protected Actor(string textureName) : base(textureName) {}

        public override void Create(Scene scene)
        {
            base.Create(scene);
            cooldownTimer = ShootCooldown;
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2, sprite.TextureRect.Height / 2);
            laserBuffer = new SoundBuffer(scene.Assets.LoadSoundBuffer("sfx_laser1"));
            laserSound = new Sound(laserBuffer);
        }

        protected virtual void TryShoot(Scene scene)
        {
            if (!ReadyToShoot) return;
            
            cooldownTimer = ShootCooldown;
            ReadyToShoot = false;
            
            Bullet bullet = new Bullet(this);
            bullet.Create(Position, facing, scene);
            scene.Spawn(bullet);
            laserSound.Play(); 
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
    }
}