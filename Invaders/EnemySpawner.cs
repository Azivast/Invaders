using System;
using SFML.System;

namespace Invaders
{
    public class EnemySpawner : Entity
    {
        private Clock clock;
        private const float SpawnIntervalMax = 3;
        private const float SpawnIntervalMin = 0.4f;
        private const float SpeedVariation = 0.2f;
        private const float MaxSpeed = 300;
        private float spawnInterval = SpawnIntervalMax;
        private float spawnTimer;
        private readonly Random random = new Random();

        public override void Create(Scene scene)
        {
            base.Create(scene);
            spawnTimer = spawnInterval-0.01f; // Spawn enemy at start
            clock = new Clock();
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            
            // Count timer
            spawnInterval = Math.Clamp(spawnInterval - deltaTime * 0.05f, SpawnIntervalMin, SpawnIntervalMax);
            
            // Determine speed of new ship
            float speed = Math.Max((200 + clock.ElapsedTime.AsSeconds()), MaxSpeed); // cap the speed
            speed += speed * SpeedVariation * (float)random.NextDouble(); // randomize it a bit
            
            // Spawn if timer is ready
            spawnTimer += deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                scene.Spawn(new EnemyShip(speed));
                spawnTimer = 0;
            }
        }
    }
}