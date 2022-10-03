// using System;
// using System.Collections.Generic;
// using SFML.Graphics;
// using SFML.System;
//
// namespace Invaders
// {
//     public class BulletManager
//     {
//         private List<Bullet> bullets;
//         
//         public bool ReadyToShoot;
//         private const float ShootCooldown = 2;
//         private float cooldownTimer;
//         
//         private Actor parent;
//         public Actor Parent
//         {
//             get => parent;
//         }
//
//         public BulletManager(Actor parent)
//         {
//             this.parent = parent;
//             bullets = new List<Bullet>();
//         }
//
//         public void SpawnBullet(Vector2f position, Vector2f direction, Scene scene)
//         {
//             Bullet bullet = new Bullet();
//             bullet.Create(position, direction, scene);
//             bullets.Add(bullet);
//             
//             cooldownTimer = ShootCooldown;
//             ReadyToShoot = false;
//         }
//
//         public void Update(Scene scene, float deltaTime)
//         {
//             // Update cooldown timer
//             if (!ReadyToShoot)
//                 cooldownTimer -= deltaTime;
//             if (cooldownTimer <= 0)
//             {
//                 ReadyToShoot = true;
//                 cooldownTimer = ShootCooldown;
//             }
//
//             // Update bullets
//             for (int i = bullets.Count - 1; i >= 0; i--)
//             {
//                 bullets[i].Update(scene, deltaTime);
//                 if (bullets[i].Position.Y < Program.ViewSize.Top)
//                 {
//                     bullets.RemoveAt(i);
//                 }
//             }
//         }
//
//         public void Render(RenderTarget target)
//         {
//             foreach (Bullet bullet in bullets)
//             {
//                 bullet.Render(target);
//             }
//         }
//     }
// }