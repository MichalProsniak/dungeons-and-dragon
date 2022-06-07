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

        public void InsertDataToDB(Player player)
        {
            const string insertCommand = @"INSERT INTO player (player_name, max_hp, current_hp, position_x, position_y,
            armor, evade, attack_damage, accuracy, sword_number, armor_number, key_number)
                        VALUES (@player_name, @max_hp, @current_hp, @position_x, @position_y, @armor,
                                @evade, @attack_damage, @accuracy, @sword_number, @armor_number, @key_number);
                        SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Debug.Log("Saving...");
                    var cmd = new SqlCommand(insertCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@player_name", player.DefaultName);
                    cmd.Parameters.AddWithValue("@max_hp", player.DefensiveStats.MaxHealth);
                    cmd.Parameters.AddWithValue("@current_hp", player.DefensiveStats.CurrentHealth);
                    cmd.Parameters.AddWithValue("@position_x", player.Position.x);
                    cmd.Parameters.AddWithValue("@position_y", player.Position.y);
                    cmd.Parameters.AddWithValue("@armor", player.DefensiveStats.Armor);
                    cmd.Parameters.AddWithValue("@evade", player.DefensiveStats.Evade);
                    cmd.Parameters.AddWithValue("@attack_damage", player.OffensiveStats.AttackDamage);
                    cmd.Parameters.AddWithValue("@accuracy", player.OffensiveStats.Accuracy);
                    cmd.Parameters.AddWithValue("@sword_number", player.swordNumber);
                    cmd.Parameters.AddWithValue("@armor_number", player.armorNumber);
                    cmd.Parameters.AddWithValue("@key_number", player.keyNumber);
                    int playerId = Convert.ToInt32(cmd.ExecuteScalar());
                    this.UpdateGameState(playerId, player);
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
        public bool IsDBEmpty()
        {
            
            const string insertCommand = @"SELECT * FROM player;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Debug.Log("Reading...");
                    var cmd = new SqlCommand(insertCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();
                    if (!reader.Read()) // first row was not found == no data was returned by the query
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new RuntimeWrappedException(e);
            }
        }

        public void DeleteFromDB()
        {
            const string insertCommand = @"DELETE FROM game_state; DELETE  FROM player;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Debug.Log("Reading...");
                    var cmd = new SqlCommand(insertCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new RuntimeWrappedException(e);
            }
        }

        public void Save(Player player)
        {
            if (!IsDBEmpty())
            {
                Debug.Log("Delete + INSERT");
               this.DeleteFromDB(); 
               this.InsertDataToDB(player);
            }
            else
            {
                Debug.Log("INSERT");
                this.InsertDataToDB(player);
            }
        }
        public void UpdateGameState(int id, Player player)
        {
            const string insertCommand = @"INSERT INTO game_state (current_map, player_id)
                        VALUES (@current_map, @player_id);";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Debug.Log("Saving...");
                    var cmd = new SqlCommand(insertCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@current_map", player.currentMap);
                    cmd.Parameters.AddWithValue("@player_id", id);
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