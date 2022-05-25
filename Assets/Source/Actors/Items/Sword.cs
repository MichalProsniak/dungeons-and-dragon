using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Characters;

namespace Assets.Source.Actors.Items
{
    public class Sword : Item
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";

        public Sword()
        {
            OffensiveStats.AttackDamage = 10;
            OffensiveStats.Accuracy = 1;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            DefensiveStats.CurrentHealth = 0;
            DefensiveStats.MaxHealth = 0;
        }



    }
    
}


