using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public Skeleton()
        {
            DefensiveStats.MaxHealth = 10;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 1;
            OffensiveStats.Accuracy = 6;
            OffensiveStats.IsWeapon = false;
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
