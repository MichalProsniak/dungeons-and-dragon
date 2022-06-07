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
            const string insertCommand = @"INSERT INTO player (player_name, max_hp, current_hp, position_x, position_y,
            armor, evade, attack_damage, accuracy, sword_number, armor_number, key_number)
                        VALUES (@player_name, @max_hp, @current_hp, @position_x, @position_y, @armor,
                                @evade, @attack_damage, @accuracy, @sword_number, @armor_number, @key_number);";
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