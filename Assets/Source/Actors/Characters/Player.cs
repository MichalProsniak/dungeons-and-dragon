using System.Collections.Generic;
using Assets.Source.Actors.Items;
using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private int _openCloseInventoryCounter = 0;
        private string _inventoryToString = "INVENTORY \n";
        private Actor _actorAtTargetPosition;
        public override bool Destroyable { get; set; } = false;

        public Player()
        {
            DefensiveStats.MaxHealth = 50;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 30;
            OffensiveStats.Accuracy = 7;
            OffensiveStats.IsWeapon = false;
            Inventory = new Inventory();
        }
        protected override void OnUpdate(float deltaTime)
        {
            Inventory playerInventory = new Inventory();
            

            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }
           
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                _actorAtTargetPosition = TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                _actorAtTargetPosition = TryMove(Direction.Down);
                // UserInterface.Singleton.SetText($"Press \"E\" to pickup!\n {_actorAtTargetPosition}", UserInterface.TextPosition.BottomCenter);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                _actorAtTargetPosition = TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                _actorAtTargetPosition = TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_openCloseInventoryCounter == 0)
                {
                    
                    _openCloseInventoryCounter++;
                    for (int i = 0; i < playerInventory._PlayerInventory.Count; i++)
                    {
                        _inventoryToString += playerInventory._PlayerInventory[i] + "\n";
                    }
                    UserInterface.Singleton.SetText(_inventoryToString, UserInterface.TextPosition.MiddleLeft);
                    _inventoryToString = "INVENTORY \n";
                }
                else
                {
                    _openCloseInventoryCounter = 0;
                    UserInterface.Singleton.SetText("", UserInterface.TextPosition.MiddleLeft);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_actorAtTargetPosition is Item)
                {
                    _actorAtTargetPosition.IsPicked = true;
                }
            }
            PlayerInformationInterface();
            CameraController.Singleton.Position = Position;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            // ActorManager.Singleton.DestroyActor(this);
            ActorManager.Singleton.DestroyAllActors();
            UserInterface.Singleton.SetText("YOU DIED\nTHE END", UserInterface.TextPosition.MiddleCenter);
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Ragnar";
        

        private void PlayerInformationInterface()
        {
            string message = $"NAME: {DefaultName}\nHEALTH: {DefensiveStats.CurrentHealth}";
            UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomLeft);
        }
    }
}
