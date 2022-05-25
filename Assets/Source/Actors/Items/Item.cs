using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Core;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using UnityEngine;

namespace Assets.Source.Actors.Items
{
    public abstract class Item : Actor
    {
        //public bool isActorOnItem;
        public override bool OnCollision(Actor anotherActor)
        {
            UserInterface.Singleton.SetText($"Press \"E\" to pickup!", UserInterface.TextPosition.BottomCenter);
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (IsPicked)
            {
                OnDeath();
            }
        }
        
        protected void OnDeath()
        {
            ActorManager.Singleton.DestroyActor(this);
        }

        private (int x, int y) _position;
        public override int Z => -1;

    }
}
