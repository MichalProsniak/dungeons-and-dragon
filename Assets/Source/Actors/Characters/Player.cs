using Assets.Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private int _openCloseInventoryCounter = 0;
        protected override void OnUpdate(float deltaTime)
        {
           
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_openCloseInventoryCounter == 0)
                {
                    // implement inventory to string
                    _openCloseInventoryCounter++;
                    UserInterface.Singleton.SetText("INVENTORY", UserInterface.TextPosition.MiddleLeft);
                }
                else
                {
                    _openCloseInventoryCounter = 0;
                    UserInterface.Singleton.SetText("", UserInterface.TextPosition.MiddleLeft);
                }
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
