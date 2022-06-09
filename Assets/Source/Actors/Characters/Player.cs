using System.Collections.Generic;
using System;
using Assets.Source.Actors.Items;
using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private int _openCloseInventoryCounter = 0;
        private Actor _actorAtTargetPosition;
        public override bool Destroyable { get; set; } = false;
        private int _movementCounter = 0;
        private int _playerMovementSpeed = 60;
        private readonly SaveDao _saveDao;
        public int swordNumber = 0;
        public int armorNumber = 0;
        public int keyNumber = 0;
        public int currentMap = 1;
        private readonly LoadDao _loadDao;
        public int Z = -1;

        public Player()
        {
            DefensiveStats.MaxHealth = 50;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 1;
            OffensiveStats.Accuracy = 7;
            OffensiveStats.IsWeapon = false;
            Inventory = new Inventory();
            _saveDao = new SaveDao(DataManager.Singleton.ConnectionString);
            _loadDao = new LoadDao(DataManager.Singleton.ConnectionString);
        }
        //Inventory playerInventory = new Inventory();
        protected override void OnUpdate(float deltaTime)
        {
            if (DefensiveStats.CurrentHealth > DefensiveStats.MaxHealth)
            {
                DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            }
            
            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }
           
            if (Input.GetKey(KeyCode.W))
            {
                if (_movementCounter == _playerMovementSpeed)
                {
                    // Move up
                    _actorAtTargetPosition = TryMove(Direction.Up);
                    _movementCounter = 0;
                    MapChange(_actorAtTargetPosition);
                }

                _movementCounter++;


            }

            if (Input.GetKey(KeyCode.S))
            {
                if (_movementCounter == _playerMovementSpeed)
                {
                    _actorAtTargetPosition = TryMove(Direction.Down);
                    _movementCounter = 0;
                    MapChange(_actorAtTargetPosition);
                }

                _movementCounter++;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (_movementCounter == _playerMovementSpeed)
                {
                    
                    _actorAtTargetPosition = TryMove(Direction.Left);
                    _movementCounter = 0;
                    MapChange(_actorAtTargetPosition);
                }

                _movementCounter++;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (_movementCounter == _playerMovementSpeed)
                {
                    // Move up
                    _actorAtTargetPosition = TryMove(Direction.Right);
                    _movementCounter = 0;
                    MapChange(_actorAtTargetPosition);
                }

                _movementCounter++;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_openCloseInventoryCounter == 0)
                {
                    _openCloseInventoryCounter++;
                    UserInterface.Singleton.SetText(Inventory.ToString(), UserInterface.TextPosition.MiddleLeft);
                }
                else
                {
                    _openCloseInventoryCounter = 0;
                    UserInterface.Singleton.SetText("", UserInterface.TextPosition.MiddleLeft);
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                _saveDao.Save(this);
                Debug.Log("F5 clicked");
                UserInterface.Singleton.SetText("GAME SAVED!", UserInterface.TextPosition.TopLeft);
                
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                JSONInfo playerInfo = new JSONInfo();
                playerInfo.accuracy = this.OffensiveStats.Accuracy;
                playerInfo.armor = this.DefensiveStats.Armor;
                playerInfo.armor_number = this.armorNumber;
                playerInfo.attack_damage = this.OffensiveStats.AttackDamage;
                playerInfo.current_hp = this.DefensiveStats.CurrentHealth;
                playerInfo.max_hp = this.DefensiveStats.MaxHealth;
                playerInfo.current_map = this.currentMap;
                playerInfo.evade = this.DefensiveStats.Evade;
                playerInfo.key_number =this.keyNumber;
                playerInfo.player_name = this.DefaultName;
                playerInfo.position_x = this.Position.x;
                playerInfo.position_y = this.Position.y;
                playerInfo.sword_number = this.swordNumber;
                string jsonstring = SaveJSON.ConvertDataToJSON(playerInfo);
                SaveJSON.SaveJSONToFile(jsonstring);
                UserInterface.Singleton.SetText("GAME SAVED!", UserInterface.TextPosition.TopLeft);
                

            }

            if (Input.GetKeyDown(KeyCode.F11))
            {
                int temporaryKeyNumber = 0;
                int temporarySwordNumber = 0;
                int temporaryArmorNumber = 0;
                LoadJSON jsonFileToRead = new LoadJSON();
                ActorManager.Singleton.DestroyAllDestroyableActors();
                jsonFileToRead.Load(this);
                MapLoader.LoadMap(currentMap);
                Inventory.RemoveAllItems();
                temporaryKeyNumber = keyNumber;
                temporarySwordNumber = swordNumber;
                temporaryArmorNumber = armorNumber;
                keyNumber = 0;
                armorNumber = 0;
                swordNumber = 0;
                SetSprite(24);
                jsonFileToRead.LoadInvetory(this, temporarySwordNumber, temporaryArmorNumber, temporaryKeyNumber);
                
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                int mapIdToLoad = _loadDao.GetCurrentMap();
                currentMap = mapIdToLoad;
                Debug.Log(mapIdToLoad);
                UserInterface.Singleton.SetText("GAME LOAD!", UserInterface.TextPosition.TopLeft);
                ActorManager.Singleton.DestroyAllDestroyableActors();
                MapLoader.LoadMap(mapIdToLoad);
                _loadDao.GetPlayerStats(this);
                Inventory.RemoveAllItems();
                keyNumber = 0;
                armorNumber = 0;
                swordNumber = 0;
                SetSprite(24);
                _loadDao.GetInventory(this);

            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_actorAtTargetPosition is Item)
                {   if (_actorAtTargetPosition is Sword)
                    {
                        swordNumber++;
                        SetSprite(27);
                    }
                    else if (_actorAtTargetPosition is Armor)
                    {
                        armorNumber++;
                    }
                    else if (_actorAtTargetPosition is Key)
                    {
                        keyNumber++;
                    }
                    _actorAtTargetPosition.IsPicked = true;
                    
                    Inventory.Add(_actorAtTargetPosition);
                    AddStatisticsFromItem(_actorAtTargetPosition);
                    //UserInterface.Singleton.SetText(Inventory.Count().ToString(), UserInterface.TextPosition.MiddleRight);
                }
            }
            PlayerInformationInterface();
            CameraController.Singleton.Position = Position;
        }

        private void MapChange(Actor targetActor)
        {
            if (targetActor is OpenedDoor)
            {
                currentMap = 2;
            }
            else if (targetActor is OpenDoor2)
            {
                currentMap = 3;
            }
        }

        private void AddStatisticsFromItem(Actor item)
        {
            OffensiveStats.AttackDamage += item.OffensiveStats.AttackDamage;
            OffensiveStats.Accuracy += item.OffensiveStats.Accuracy;
            DefensiveStats.Armor += item.DefensiveStats.Armor;
            DefensiveStats.Evade += item.DefensiveStats.Evade;
            DefensiveStats.CurrentHealth += item.DefensiveStats.CurrentHealth;
            DefensiveStats.MaxHealth += item.DefensiveStats.MaxHealth;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            ActorManager.Singleton.DestroyAllActors();
            UserInterface.Singleton.SetText("YOU DIED\nTHE END", UserInterface.TextPosition.MiddleCenter);
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName { get; set; } = "Ragnar";
        

        private void PlayerInformationInterface()
        {
            string message = $"NAME: {DefaultName}\nHEALTH: {DefensiveStats.CurrentHealth}\\{DefensiveStats.MaxHealth}";
            UserInterface.Singleton.SetText(message, UserInterface.TextPosition.BottomLeft);
        }
    }
}
