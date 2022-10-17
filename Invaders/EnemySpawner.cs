using System;
using SFML.System;

namespace Invaders
{
    public class EnemySpawner : Entity
    {
        private Clock clock;
        private const float SpawnIntervalMax = 3;
        private const float SpawnIntervalMin = 0.3f;
        private const float SpeedVariation = 0.2f;
        private float spawnInterval = SpawnIntervalMax;
        private float spawnTimer;
        private Random random = new Random();

        public override void Create(Scene scene)
        {
            base.Create(scene);
            spawnTimer = spawnInterval-0.01f; // Spawn enemy at start
            clock = new Clock();
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);


            spawnInterval = Math.Clamp(spawnInterval - deltaTime * 0.05f, SpawnIntervalMin, SpawnIntervalMax);
            float speed = (100 + clock.ElapsedTime.AsSeconds());
            speed += speed * SpeedVariation * (float)random.NextDouble(); // randomize speed a bit
            
            spawnTimer += deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                scene.Spawn(new EnemyShip(speed));
                spawnTimer = 0;
            }
        }
    }
}