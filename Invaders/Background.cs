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
        private readonly Sprite sprite;
        private readonly Vector2f direction = new Vector2f(0, 1);
        private readonly List<Vector2f> positions = new List<Vector2f>();

        public Background(RenderTarget target)
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/textures/blue.png");
            
            // Populate screen with positions to draw the background texture at.
            // Logic adapted from 'platformer-tutorial.pdf' on canvas.
            View view = target.GetView();
            Vector2f topLeft = view.Center - 0.5f * view.Size;
            int tilesX = (int)MathF.Ceiling(view.Size.X / sprite.Texture.Size.X); //nr of tiles in x axis
            int tilesY = (int)MathF.Ceiling(view.Size.Y / sprite.Texture.Size.Y); //nr of tiles in y axis
            
            // Save positions
            for (int row = -2; row <= tilesY; row++) // 2 extra rows needed above screen to prevent gaps when scrolling
            {
                for (int col = 0; col <= tilesX; col++) 
                {
                    positions.Add(topLeft + (sprite.Texture.Size.Y-1) * new Vector2f(col, row)); //-1 prevents gaps
                }
            }
        }

        public static void IncreaseSpeed(int amount)
        {
            speed = StartingSpeed + amount;
        }
        public void Update(float deltaTime)
        {
            // Update each position
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] += direction * speed * deltaTime; // move the background

                // Teleport to top when moving outside the window so that background scrolls infinitely
                if (positions[i].Y > Program.ViewSize.Height)
                {
                    // Depending on velocity the background will move different amounts past the window.
                    // We have to take this movement into account when teleporting to top in order to 
                    // prevent gaps from forming between the textures.
                    float movementPastWindow = Program.ViewSize.Height - positions[i].Y;
                    positions[i] = new Vector2f(
                        positions[i].X, 
                        Program.ViewSize.Top - sprite.Texture.Size.Y*2 - movementPastWindow
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