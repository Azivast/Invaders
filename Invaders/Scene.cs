using System.Collections.Generic;
using SFML.Graphics;

namespace Invaders
{
    public class Scene
    {
        private readonly List<Entity> entities;
        // public readonly AssetManger Assets;
        // public readonly EventManager Events;

        public Scene()
        {
            entities = new List<Entity>();
        }

        public void Spawn(Entity entity)
        {
            entity.Create(this);
            entities.Add(entity);
        }

        public void UpdateAll(float deltaTime)
        {
            // TODO: Better loops. Maybe use a queue and foreach?
            // Update all entities
            for (int i = entities.Count - 1; i >= 0; i--) // iterate backwards
            {
                entities[i].Update(this, deltaTime);
            }

            // Remove all dead entities
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].IsDead) entities.RemoveAt(i);
                else i++; // else => prevents skipping when [i+1] when removing [i]. 
            }
        }

        public void RenderAll(RenderTarget target)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(target);
            }
        }
        
        /// Search for entities of type T.
        /// Returns true if found and out 'found' as the entity.
        /// Code copied from the pacman tutorial for previous assignment. 
        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in entities)
            {
                if (!entity.IsDead && entity is T typed) {
                    found = typed;
                    return true;
                }
            }
            found = default(T);
            return false;
        }
    }
}