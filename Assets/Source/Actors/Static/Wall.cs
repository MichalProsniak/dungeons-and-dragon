using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";
        
        public override DefensiveStats DefensiveStats { get; set; }
        public override OffensiveStats OffensiveStats { get; set; }
    }
}
