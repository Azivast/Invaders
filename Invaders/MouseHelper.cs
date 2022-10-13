using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    public static class MouseHelper
    {
        public static RenderWindow Window;

        private static bool pressedLastFrame;
        private static bool pressedThisFrame;
        private static bool mouseJustPressed;

        public static bool MouseJustPressed => Mouse.IsButtonPressed(Mouse.Button.Left);

        public static FloatRect MouseHitBox
        {
            get => new FloatRect(Mouse.GetPosition(Window).X, Mouse.GetPosition(Window).Y, 1,
                1);
        }

        public static void Update()
        {
            // We only want to check the same frame the mouse button is pressed.
            // Otherwise a button press in one scene might trigger a press in a new scene upon scene change.

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
                pressedThisFrame = true;
            else
                pressedThisFrame = false;

            if (pressedLastFrame && pressedThisFrame)
                mouseJustPressed = true;
            else
                mouseJustPressed = false;


            
            if (!pressedThisFrame)
            {
                pressedLastFrame = false;
            }
        }
    }
}