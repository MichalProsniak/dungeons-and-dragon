using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Dragon : Character
    {
        public Dragon()
        {
            DefensiveStats.MaxHealth = 100;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 3;
            DefensiveStats.Evade = 2;
            OffensiveStats.AttackDamage = 6;
            OffensiveStats.Accuracy = 7;
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
            // ActorManager.Singleton.DestroyActor(this);
            ActorManager.Singleton.DestroyAllActors();
            UserInterface.Singleton.SetText("DRAGON DESTROYED\nCONGRATULATIONS", UserInterface.TextPosition.MiddleCenter);
        }

        public override int DefaultSpriteId => 411;
        public override string DefaultName => "Dragon";
    }
}