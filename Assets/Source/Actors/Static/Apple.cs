using Assets.Source.Actors.Items;
using UnityEditor;

namespace DungeonCrawl.Actors.Static
{
    public class Apple : Item
    {
        public override int DefaultSpriteId => 896;
        public override string DefaultName => "Apple"; 
        
        public Apple()
        {
            OffensiveStats.AttackDamage = 0;
            OffensiveStats.Accuracy = 0;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            DefensiveStats.CurrentHealth = 10;
            DefensiveStats.MaxHealth = 0;
        }

        public override bool Consumable { get; set; } = true;
    }
}