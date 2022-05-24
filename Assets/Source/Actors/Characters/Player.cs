using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private int _openCloseInventoryCounter = 0;

        public Player()
        {
            DefensiveStats.MaxHealth = 50;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 6;
            OffensiveStats.Accuracy = 7;
            OffensiveStats.IsWeapon = false;
        }
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
            PlayerInformationInterface();
            
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            ActorManager.Singleton.DestroyActor(this);
            Debug.Log("Oh no, I'm dead!");
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Ragnar";

        private new void Awake()
        {
            base.Awake();
            string message = $"NAME: {DefaultName}\nHEALTH: {DefensiveStats.CurrentHealth}";
            UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomLeft);
        }

        private void PlayerInformationInterface()
        {
            string message = $"NAME: {DefaultName}\nHEALTH: {DefensiveStats.CurrentHealth}";
            UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomLeft);
        }
    }
}
