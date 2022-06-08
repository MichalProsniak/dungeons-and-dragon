using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Gargoyle : Character
    {
        public Gargoyle()
        {
            DefensiveStats.MaxHealth = 30;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 5;
            DefensiveStats.Evade = -10;
            OffensiveStats.AttackDamage = 5;
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

        public override int DefaultSpriteId => 317;
        public override string DefaultName { get; set; } =  "Gargoyle";
    }
}