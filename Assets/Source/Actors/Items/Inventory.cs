using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;

namespace Assets.Source.Actors.Items
{
    public class Inventory
    {
        private List<Actor> _playerInventory = new List<Actor>();
        private int _bonusAD;
        private int _bonusHP;

        public void Add(Actor item)
        {
            if (!item.Consumable )
            {
                _playerInventory.Add(item); 
            }
        }

        public void RemoveAllItems()
        {
            _playerInventory.Clear();
        }

        public int Count()
        {
            return _playerInventory.Count;
        }

        public Actor this[int index]
        {
            get { return _playerInventory[index]; } // indexer zapamietac 
        }

        private string CountedItemMessage(int number, string itemName)
        {
            if (number > 0)
            {
                return $"{itemName}: {number}\n";
            }

            return "";
        }

        public override string ToString()
        {
            string stringList = "INVENTORY \n";
            int armors = 0;
            int swords = 0;
            int keys = 0;
            for (int i = 0; i < _playerInventory.Count(); i++)
            {
                if (_playerInventory[i] is Sword)
                {
                    swords++;
                }
                if (_playerInventory[i] is Armor)
                {
                    armors++;
                }
                if (_playerInventory[i] is Key)
                {
                    keys++;
                }
            }

            stringList += CountedItemMessage(swords, "SWORD");
            stringList += CountedItemMessage(armors, "ARMOR");
            stringList += CountedItemMessage(keys, "KEY");
            return stringList;
        }

        public bool IsKeyInInventory()
        {
            for (int i = 0; i < _playerInventory.Count(); i++)
            {
                if (_playerInventory[i].DefaultName == "Key")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
