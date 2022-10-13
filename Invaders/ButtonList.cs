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
        
        public List<Button> Buttons = new List<Button>();
        private int selectedButtonIndex;
        public Vector2f Position;

        public void AddButton(Button button, Scene scene)
        {
            scene.Spawn(button);
            button.Position = new( // Set spacing
                Position.X,
                Position.Y + (button.Bounds.Height+ButtonSpacing)*Buttons.Count
            );
            Buttons.Add(button);
        }

        public void Update()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i == selectedButtonIndex) Buttons[selectedButtonIndex].Selected = true;
                else Buttons[i].Selected = false;
                
                // Mouse collision
                if (!MouseHelper.MouseHitBox.Intersects(Buttons[i].Bounds)) continue;
                selectedButtonIndex = i;
                if (MouseHelper.MouseJustPressed) Buttons[i].Click();
            }
        }
        
        public void OnKeyPressed(object s, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Up) selectedButtonIndex--;
            if (e.Code == Keyboard.Key.Down) selectedButtonIndex++;

            // Loop around when at ends of list
            if (selectedButtonIndex < 0) selectedButtonIndex = Buttons.Count - 1;
            else if (selectedButtonIndex > Buttons.Count - 1) selectedButtonIndex = 0;
            
            if (e.Code == Keyboard.Key.Enter)
            {
                Console.WriteLine(this);
                Buttons[selectedButtonIndex].Click();
            }
        }
    }
}