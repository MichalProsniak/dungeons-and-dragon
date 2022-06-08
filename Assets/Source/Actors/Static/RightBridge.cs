namespace DungeonCrawl.Actors.Static
{
    public class RightBridge : Actor
    {
        public override int DefaultSpriteId => 731;
        public override string DefaultName { get; set; } =  "RightBridge"; 
        public override bool Detectable => false;
    }
}