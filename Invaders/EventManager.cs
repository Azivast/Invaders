namespace Invaders
{
    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);
        
        public event ValueChangedEvent LoseHealth;

        public int HealthLost;
        
        // Called from other classes:
        public void PublishLooseHealth(int amount) 
            => HealthLost += amount;

        public void Update(Scene scene)
        {
            // Invoke event when its condition has been met
            if (HealthLost != 0) 
            {
                LoseHealth?.Invoke(scene, HealthLost);
                HealthLost = 0;
            }
        }
    }
}