using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Actors.Items
{
    public class Inventory 
    {
        private List<string> _playerInventory = new List<string>{"Sword1","Shield", "Key" };
        public List<string> _PlayerInventory
        {
            get { return _playerInventory; }
            set
            {
                _playerInventory = value;
            }

        }
    }
}
