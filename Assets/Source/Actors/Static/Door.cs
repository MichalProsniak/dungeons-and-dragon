using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Static
{
    public class Door : Actor
    {
        public override int DefaultSpriteId => 538;
        public override string DefaultName { get; set; } =  "Door";

        public void OnDeath()
        {
            ActorManager.Singleton.DestroyActor(this);
            
        }
        
    }
}