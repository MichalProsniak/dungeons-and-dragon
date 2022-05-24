using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public Skeleton()
        {
            DefensiveStats.MaxHealth = 20;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 2;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 2;
            OffensiveStats.Accuracy = 6;
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
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
