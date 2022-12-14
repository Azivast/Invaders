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
        private const float TimePerFrame = 0.1f;
        private float frameTimer = TimePerFrame;

        private int CurrentFrame
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

        public Explosion(Vector2f position) : base("spriteSheet")
        {
            sprite.Position = position;
            frames = new Dictionary<int, IntRect>
            {   // Rects of each frame in the sprite sheet
                {1, new IntRect(472, 0, 126, 126)},
                {2, new IntRect(600, 0, 126, 126)},
                {3, new IntRect(727, 0, 126, 126)},
                {4, new IntRect(854, 0, 126, 126)},
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
            // Count the timer
            frameTimer = MathF.Max(frameTimer -= deltaTime, 0);

            // Change frame once timer is done
            if (frameTimer == 0)
            {
                frameTimer = TimePerFrame; // reset timer
                CurrentFrame++;
                frames.TryGetValue(CurrentFrame, out IntRect newFrame);
                sprite.TextureRect = newFrame;
            }
        }
    }
}