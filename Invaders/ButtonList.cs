using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class ButtonList
    {
        private const int ButtonSpacing = 20;
        public readonly List<Button> Buttons = new List<Button>();
        private int selectedButtonIndex;
        public Vector2f Position;

        public void AddButton(Button button, Scene scene)
        {
            scene.Spawn(button);
            button.Position = new Vector2f(
                Position.X,
                Position.Y + (button.Bounds.Height+ButtonSpacing)*Buttons.Count // Set spacing
            );
            Buttons.Add(button);
        }

        public void Update()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                // Inform button if selected or not
                if (i == selectedButtonIndex) Buttons[selectedButtonIndex].Selected = true;
                else Buttons[i].Selected = false;
                
                // Mouse collision
                if (!MouseHelper.MouseHitBox.Intersects(Buttons[i].Bounds)) continue; // do nothing if not moused over
                selectedButtonIndex = i;
                if (MouseHelper.MouseJustPressed) Buttons[i].Click();
            }
        }
        
        public void OnKeyPressed(object s, KeyEventArgs e)
        {
            // Scroll list with arrow keys
            if (e.Code == Keyboard.Key.Up) selectedButtonIndex--;
            if (e.Code == Keyboard.Key.Down) selectedButtonIndex++;

            // Loop around when at ends of list
            if (selectedButtonIndex < 0) selectedButtonIndex = Buttons.Count - 1;
            else if (selectedButtonIndex > Buttons.Count - 1) selectedButtonIndex = 0;
            
            // Click with enter
            if (e.Code is Keyboard.Key.Enter or Keyboard.Key.Space)
            {
                Buttons[selectedButtonIndex].Click();
            }
        }
    }
}