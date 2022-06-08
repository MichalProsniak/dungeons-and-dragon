using Assets.Source.Core;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class DragonsBreath: Character
    {
        private int _timer;
        private int _counter;
        public DragonsBreath()
        {
            DefensiveStats.MaxHealth = 0;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 0;
            OffensiveStats.Accuracy = 0;
            OffensiveStats.IsWeapon = false;
            _timer = 500;
            _counter = 0;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_counter == _timer)
            {
                OnDeath();
                _counter = 0;
            }

            _counter++;
        }

        protected override void OnDeath()
        {
            ActorManager.Singleton.DestroyActor(this);
        }

        public override int DefaultSpriteId => 494;
        public override string DefaultName { get; set; } =  "DragonBreath";
    }
}
