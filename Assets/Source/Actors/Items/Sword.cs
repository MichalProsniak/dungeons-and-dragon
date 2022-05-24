using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Core;
using UnityEngine;

namespace Assets.Source.Actors.Items
{
    public class Sword : Item
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";
        protected void DestroyItem()
        {
            // ActorManager actorManager = new ActorManager();
            ActorManager.Singleton.DestroyActor(this);
            //Debug.Log("Well, I was already dead anyway...");
        }
    }

    
}
