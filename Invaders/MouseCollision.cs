using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    public static class MouseCollision
    {
        public static RenderWindow Window;
        public static FloatRect HitBox
        {
            get => new FloatRect(Mouse.GetPosition(Window).X, Mouse.GetPosition(Window).Y, 1,
                1);
        }
    }
}