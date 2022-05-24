using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public DefensiveStats DefensiveStats { get; set; } = new DefensiveStats();
        public OffensiveStats OffensiveStats { get; set; } = new OffensiveStats();
        

        public void ApplyDamage(int damage)
        {
            int health = DefensiveStats.CurrentHealth;
            health -= damage;

            if (health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
