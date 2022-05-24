using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Actors.Items
{
    internal class Inventory
    {
        private List<string> _playerInventory = new List<string>{"cos1", "cos2", "cos3"};

        public List<string> _PlayerInventory
        {
            get { return _playerInventory; }
            set { _playerInventory = value; }
        }
    }
}
