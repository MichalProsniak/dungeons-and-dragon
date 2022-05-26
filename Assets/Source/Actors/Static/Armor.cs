namespace DungeonCrawl.Actors.Static
{
    public class Armor : Actor
    {
        public override int DefaultSpriteId => 80;
        public override string DefaultName => "Armor";
        public Armor()
        {
            OffensiveStats.AttackDamage = 0;
            OffensiveStats.Accuracy = 0;
            DefensiveStats.Armor = 2;
            DefensiveStats.Evade = 0;
            DefensiveStats.CurrentHealth = 0;
            DefensiveStats.MaxHealth = 20;
        }
    }
}