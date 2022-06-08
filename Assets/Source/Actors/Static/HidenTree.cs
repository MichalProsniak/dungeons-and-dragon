namespace DungeonCrawl.Actors.Static
{
    public class HidenTree : Actor
    {
        public override int DefaultSpriteId => 48;
        public override string DefaultName { get; set; } =  "HiddenTree"; 
        public override bool Detectable => false;
        public override int Z => -1;
    }
}