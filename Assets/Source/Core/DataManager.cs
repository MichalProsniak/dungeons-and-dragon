using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Source.Core;
using UnityEngine;

namespace DungeonCrawl.Core
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Singleton { get; private set; }
        public string ConnectionString => @"Server=localhost;Database=Dungeons;Integrated Security=True;";
         //public string ConnectionString => @"Server=localhost;Database=Dungeons;User Id=Damian;Password=Explorer1;";
         
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;
            Singleton.OpenConnection();
        }

        public void OpenConnection()
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(ConnectionString);
                connection.Open();
                Debug.Log(connection.ServerVersion);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
