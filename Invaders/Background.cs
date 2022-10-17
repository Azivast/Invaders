using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Background
    {
        private const int StartingSpeed = 50;
        private static int speed = StartingSpeed;
        private static int newSpeed = StartingSpeed;
        private readonly Sprite sprite;
        private readonly Vector2f direction = new Vector2f(0, 1);
        private readonly List<Vector2f> positions = new List<Vector2f>();

        public Background(RenderTarget target)
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/textures/blue.png");


            // Populate screen
            View view = target.GetView();
            Vector2f topLeft = view.Center - 0.5f * view.Size;
            int tilesX = (int)MathF.Ceiling(view.Size.X / sprite.Texture.Size.X);
            int tilesY = (int)MathF.Ceiling(view.Size.Y / sprite.Texture.Size.Y);
            
            //Goes though tile by tile and places the right texture based on row
            for (int row = -2; row <= tilesY; row++) // 2 extra rows needed above screen to prevent gaps when scrolling
            {
                for (int col = 0; col <= tilesX; col++) 
                {
                    positions.Add(topLeft + (sprite.Texture.Size.Y-1) * new Vector2f(col, row));
                }
            }
        }

        public static void IncreaseSpeed(int amount)
        {
            speed = StartingSpeed + amount;
            //newSpeed = StartingSpeed + amount;
        }

        // private int SmoothStepInterpolation(int n0, int n1, float t)
        // {
        //     t = t * t * (3 - 2 * t);
        //     return (int)(n0 + t * (n1 - n0));
        // }
        
        public void Update(float deltaTime)
        {
            // if (newSpeed != 0 && newSpeed != speed)
            //     speed = SmoothStepInterpolation(speed, newSpeed, 10);
            // else if (newSpeed == speed)
            //     newSpeed = 0;


                // Update each position
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] += direction * speed * deltaTime;

                if (positions[i].Y > Program.ViewSize.Height)
                {
                    float movementPastScreen = Program.ViewSize.Height - positions[i].Y;
                    positions[i] = new Vector2f(
                        positions[i].X, 
                        Program.ViewSize.Top - sprite.Texture.Size.Y*2 - movementPastScreen
                        );
                }
            }
        }

        public void Render(RenderTarget target)
        {
            View view = target.GetView();
            
            foreach (Vector2f position in positions)
            {
                sprite.Position = position;
                target.Draw(sprite);
            }
        }
    }
}