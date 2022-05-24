using System;
using Assets.Source.Actors.Items;
using Assets.Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private int _openCloseInventoryCounter = 0;
        private string _tempInvDisplay = "INVENTORY \n";

        public Player()
        {
            DefensiveStats.MaxHealth = 50;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 5;
            OffensiveStats.Accuracy = 7;
            OffensiveStats.IsWeapon = false;
            
        }
        protected override void OnUpdate(float deltaTime)
        {
            Inventory playerInventory = new Inventory();

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
                    //UserInterface.Singleton.SetText("INVENTORY", UserInterface.TextPosition.MiddleLeft);
                    for (int i = 0; i < playerInventory._PlayerInventory.Count; i++)
                    {
                        _tempInvDisplay += playerInventory._PlayerInventory[i] + "\n";
                    }
                    UserInterface.Singleton.SetText(_tempInvDisplay, UserInterface.TextPosition.MiddleLeft);
                    _tempInvDisplay = "INVENTORY \n";
                }
                else
                {
                    _openCloseInventoryCounter = 0;
                    UserInterface.Singleton.SetText("", UserInterface.TextPosition.MiddleLeft);

                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInventory._PlayerInventory.Add("cos10");
                UserInterface.Singleton.SetText(playerInventory._PlayerInventory.Count.ToString(), UserInterface.TextPosition.TopRight);
                UserInterface.Singleton.SetText("to jest konsola", UserInterface.TextPosition.MiddleRight);
            }
            PlayerInformationInterface();
            
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
