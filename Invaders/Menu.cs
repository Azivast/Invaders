using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Invaders
{
    public abstract class Menu
    {
        protected enum MenuEntries {}
        protected MenuEntries selectedEntry;

        public virtual void Update()
        {
            switch (selectedEntry)
            {
            }
        }

        public virtual void Render(RenderTarget target)
        {
            
        }
    }
}