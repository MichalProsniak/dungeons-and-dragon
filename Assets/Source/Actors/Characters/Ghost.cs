using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public Ghost()
        {
            DefensiveStats.MaxHealth = 20;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 4;
            OffensiveStats.AttackDamage = 3;
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
            // ActorManager actorManager = new ActorManager();
            ActorManager.Singleton.DestroyActor(this);
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 314;
        public override string DefaultName => "Ghost";
    }
}