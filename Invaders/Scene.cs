using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public abstract class Scene
    {
        protected readonly List<Entity> entities;
        public readonly AssetManager Assets;
        private SceneManager sceneManager;

        public EventManager Events => sceneManager.Events;


        public Scene(SceneManager sceneManager)
        {
            entities = new List<Entity>();
            Assets = new AssetManager();
            this.sceneManager = sceneManager;
        }

        public virtual void Spawn(Entity entity)
        {
            entity.Create(this);
            entities.Add(entity);
        }

        public virtual void UpdateAll(float deltaTime)
        {
            // TODO: Better loops. Maybe use a queue and foreach?
            // Update all entities
            for (int i = entities.Count - 1; i >= 0; i--) // iterate backwards
            {
                entities[i].Update(this, deltaTime);
            }

            // Remove all dead entities
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (entities[i].IsDead) entities.RemoveAt(i);
            }
            
            Events.Update(this);
        }

        public virtual void RenderAll(RenderTarget target)
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
        
        public IEnumerable<Entity> FindIntersects(FloatRect bounds) 
        {
            int lastEntity = entities.Count - 1;
            for (int i = lastEntity; i >= 0; i--) // Iterates backwards so as to not break loop when removing entries.
            {
                Entity entity = entities[i];
                if (entity.IsDead) continue;
                if (entity.Bounds.Intersects(bounds)) 
                {
                    yield return entity;
                }
            }
        }
        
        // Loop backwards through entities and remove them
        public void Clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--) {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }
}