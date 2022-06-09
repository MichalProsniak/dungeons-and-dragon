using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Bat : Character
    {
        
        public Bat()
        {
            DefensiveStats.MaxHealth = 10;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 1;
            DefensiveStats.Evade = 4;
            OffensiveStats.AttackDamage = 2;
            OffensiveStats.Accuracy = 4;
            OffensiveStats.IsWeapon = false;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            ActorManager.Singleton.DestroyActor(this);
        }

        public override int DefaultSpriteId => 409;
        public override string DefaultName { get; set; } =  "Bat";
    }
}