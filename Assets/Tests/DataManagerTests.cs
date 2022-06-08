using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DungeonCrawl.Core;

namespace DungeonCrawl.Core.Tests
{
  [TestFixture]
  public class DataMangerTests
  {
    [Test]
    public void Singleton_WhenCalled_ReturnsSameInstance()
    {
      DataManager dataManager1 = DataManager.Singleton;
      DataManager dataManager2 = DataManager.Singleton;
      
      Assert.AreEqual(dataManager1, dataManager2);
    }

  }
}
