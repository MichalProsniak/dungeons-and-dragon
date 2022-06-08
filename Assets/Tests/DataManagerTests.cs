using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DungeonCrawl.Core;
using NUnit.Framework.Internal;

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

    [Test]
    [TestCaseSource(nameof(ConnectionStringTests))]
    public void DataBaseConnection_WhenConnected_ReturnTrue(string connectionString, bool expectedValue)
    {
      //Arrange
      var dataManager = new DataManager();
      //Act
      var actualValue = dataManager.OpenConnection(connectionString);
     
      //Assert
      Assert.AreEqual(expectedValue, actualValue);
      
    }
    
    public static IEnumerable ConnectionStringTests
    {
      get
      {
        yield return new TestCaseData("Server=localhost;Database=Dungeons;User Id=Damian;Password=Explorer1;", true);
        yield return new TestCaseData("Server=localhost;Database=Dungeons;User Id=Damian;Password=Explorer12;", false);
        yield return new TestCaseData("Server=localhost;Database=Dungeons;User Id=Damian;Password=test;", false);
      }
    }
    // [Test]
    // public void Test()
    // {
    //   var dataManager = new DataManager();
    //   var actualValue = dataManager.OpenConnection();
    //   Assert.IsTrue(actualValue);
    // }

  }
}
