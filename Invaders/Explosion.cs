using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private readonly Dictionary<int, IntRect> frames;
        private int frameInternal = 1; // Used by CurrentFrame property. Do not access directly
        private const float timePerFrame = 0.2f;
        private float frameTimer = timePerFrame;

        private int currentFrame
        {
            get { return frameInternal; }
            set
            {
                if (value <= frames.Count)
                {
                    frameInternal = value;
                }
                else IsDead = true;
            }
        }

        public Explosion(Vector2f position) : base("explosion")
        {
            sprite.Position = position;
            frames = new Dictionary<int, IntRect>
            {
                {1, new IntRect(0, 0, 103, 103)},
                {2, new IntRect(103, 0, 103, 103)},
                {3, new IntRect(206, 0, 103, 103)},
            };
        }
        
        public override void Create(Scene scene)
        {
            frames.TryGetValue(1, out IntRect frame);
            sprite.TextureRect = frame;
            sprite.Origin = new Vector2f(frame.Width/2, frame.Height/2);
            
            base.Create(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            Animate(deltaTime);
        }

        private void Animate(float deltaTime)
        {
            frameTimer = MathF.Max(frameTimer -= deltaTime, 0); // count timer

            if (frameTimer == 0)
            {
                frameTimer = timePerFrame; // reset timer
                currentFrame++;
                frames.TryGetValue(currentFrame, out IntRect newFrame);
                sprite.TextureRect = newFrame;
            }
        }
    }
}