using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Core;
using DungeonCrawl.Actors;
using UnityEngine;

namespace Assets.Source.Actors.Items
{
    public abstract class Item : Actor
    {
        //public bool isActorOnItem;
        public override bool OnCollision(Actor anotherActor)
        {
            UserInterface.Singleton.SetText("Press \"E\" to pickup!", UserInterface.TextPosition.BottomCenter);
            Inventory.isActorOnItem = true;
            return true;
        }

        private (int x, int y) _position;
        public override int Z => -1;
    }
}
