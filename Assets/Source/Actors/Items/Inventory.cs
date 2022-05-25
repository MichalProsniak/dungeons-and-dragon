using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors;

namespace Assets.Source.Actors.Items
{
    public class Inventory
    {
        private List<Actor> _playerInventory = new List<Actor>();
        public static bool isActorOnItem;
        private int _bonusAD;
        private int _bonusHP;

        public void Add(Actor item)
        {
            _playerInventory.Add(item);
        }

        public int Count()
        {
            return _playerInventory.Count;
        }

        public Actor this[int index]
        {
            get { return _playerInventory[index]; } // indexer zapamietac 
        }

        public override string ToString()
        {
            string stringList = "INVENTORY \n";   
            for (int i = 0; i < _playerInventory.Count(); i++)
            {
                stringList += _playerInventory[i].DefaultName + "\n";
            }
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
