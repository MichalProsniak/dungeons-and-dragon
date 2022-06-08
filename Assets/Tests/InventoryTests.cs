using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Actors.Items;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Static;
using NUnit.Framework;

namespace DungeonCrawl.CItems.Tests
{
    [TestFixture]
    public class InventoryTests
    {
        [Test]
        public void Inventory_WhenItemAdded_DisplayItem()
        {
            string expectedValue = "INVENTORY \nSWORD: 2\nARMOR: 1\nKEY: 1\n";
            List<Actor> itemsToAdd = new List<Actor>()
            {
                new Sword(),
                new Sword(),
                new Armor(),
                new Key()
            };
            var inventory = new Inventory();
            for (int i = 0; i < itemsToAdd.Count; i++)
            {
                inventory.Add(itemsToAdd[i]);
            }
            Assert.AreEqual(expectedValue,inventory.ToString());

        }

        [Test]
        public void Inventory_WhenIsItemInInventory_ReturnTrue()
        {
            List<Actor> itemsToAdd = new List<Actor>()
            {
                new Sword(),
                new Sword(),
                new Armor(),
                new Key()
            };

            var inventory = new Inventory();
            bool isKeyInInv = false;
            for (int i = 0; i < itemsToAdd.Count; i++)
            {
                inventory.Add(itemsToAdd[i]);
            }

            Assert.True(inventory.IsKeyInInventory());
        }

    }
    
}