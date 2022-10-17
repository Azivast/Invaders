using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Stats
    {
        private const string Font = "kenvector_future";
        
        private int score = 0;
        private Text scoreText = new Text();
        private float gameTime = 0;
        private Clock clock = new Clock();
        private Sprite heart = new Sprite();
        
        private readonly PlayerShip playerShip;
        private readonly Scene scene;
        
        public int Score => score;

        public Stats(PlayerShip playerShip, Scene scene)
        {
            this.scene = scene;
            this.playerShip = playerShip;
            
            heart.Texture = scene.Assets.LoadTexture("spriteSheet");
            heart.TextureRect = new IntRect(327, 0, 104, 82);
            heart.Rotation = 45;
            heart.Scale = new Vector2f(0.4f, 0.4f);

            scoreText.Font = scene.Assets.LoadFont(Font);
            scoreText.DisplayedString = $"Score: {score}";
            scoreText.CharacterSize = 72;
            scoreText.Scale = new Vector2f(0.5f, 0.5f);
            scoreText.Position = new Vector2f
            (
                Program.ViewSize.Left+10,
                Program.ViewSize.Top
            );

            clock.Restart();

            scene.Events.GameOver += () => scene.Events.PublishNewScore(score);
        }

        public void Update(EventManager events)
        {
            score = (int)clock.ElapsedTime.AsSeconds();
            Background.IncreaseSpeed(score * 5); // Give the background a speed based on score
        }



        public void Render(RenderTarget target)
        {
            // Draw health
            heart.Position = new Vector2f(
                Program.ViewSize.Width - heart.GetGlobalBounds().Width/2 - 10, 
                Program.ViewSize.Top);
            for (int i = 0; i < playerShip.MaxHealth; i++) // Render all hearts
            {
                heart.Color = i < playerShip.Health
                    ? Color.White // Full heart
                    : new Color(20,20,20); // Empty heart
                target.Draw(heart);
                heart.Position -= new Vector2f(heart.GetGlobalBounds().Width, 0);
            }
            scoreText.DisplayedString = $"Score: {score}";
            target.Draw(scoreText);
        }
    }
}