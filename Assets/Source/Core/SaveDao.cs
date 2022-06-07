using System;
using System.Data;
using DungeonCrawl.Actors.Characters;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Source.Core
{
    public class SaveDao
    {
        private readonly string _connectionString;
        
        public SaveDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(Player player)
        {
            const string insertCommand = @"INSERT INTO player (id, player_name, hp, x, y)
                        VALUES (@id, @player_name, @hp, @x, @y);";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Debug.Log("Saving...");
                    var cmd = new SqlCommand(insertCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@player_name", player.DefaultName);
                    cmd.Parameters.AddWithValue("@id", 1);
                    cmd.Parameters.AddWithValue("@hp", 5);
                    cmd.Parameters.AddWithValue("@x", player.Position.x);
                    cmd.Parameters.AddWithValue("@y", player.Position.y);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}