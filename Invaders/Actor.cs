using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Actor : Entity
    {
        public float Speed;
        protected Vector2f Facing;
        protected virtual float ShootCooldown { get; } = 0.5f; // property so that it can be overriden in children
        protected bool ReadyToShoot;
        protected float CooldownTimer;
        private SoundBuffer laserBuffer;
        protected Sound LaserSound;

        protected Actor(string textureName) : base(textureName) {}

        public override void Create(Scene scene)
        {
            base.Create(scene);
            CooldownTimer = ShootCooldown;
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2, sprite.TextureRect.Height / 2);
            laserBuffer = new SoundBuffer(scene.Assets.LoadSoundBuffer("sfx_laser1"));
            LaserSound = new Sound(laserBuffer);
        }

        public override void Destroy(Scene scene)
        {
            LaserSound.Dispose();
            laserBuffer.Dispose();
        } 

        protected virtual void TryShoot(Scene scene)
        {
            if (!ReadyToShoot) return;
            
            // Start cooldown
            CooldownTimer = ShootCooldown;
            ReadyToShoot = false;
            
            // Spawn bullet & play sound
            Bullet bullet = new Bullet(this);
            bullet.Create(Position, Facing, scene);
            scene.Spawn(bullet);
            LaserSound.Play(); 
        }
        
        protected virtual void Move(float deltaTime) {}

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            
            // Update shooting cooldown timer
            if (!ReadyToShoot)
                CooldownTimer -= deltaTime;
            if (CooldownTimer <= 0) // cooldown over
            {
                ReadyToShoot = true;
                CooldownTimer = ShootCooldown;
            }
        }
    }
}