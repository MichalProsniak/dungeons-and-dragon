using System;
using System.Data;
using DungeonCrawl.Actors.Characters;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using Assets.Source.Actors.Items;
using DungeonCrawl.Actors.Static;
using UnityEngine;


namespace Source.Core
{
    public class LoadDao
    {
        private readonly string _connectionString;
        
        public LoadDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetCurrentMap()
        {
            const string selectCommand = @"SELECT MAX(current_map) FROM game_state;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(selectCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int mapNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    return mapNumber;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public void GetPlayerStats(Player player)
        {
            const string selectCommand = @"SELECT player_name, max_hp, current_hp, position_x,
                                    position_y, armor, evade, attack_damage, accuracy FROM player;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(selectCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        player.DefaultName = reader.GetString("player_name");
                        player.DefensiveStats.MaxHealth = reader.GetInt32("max_hp");
                        player.DefensiveStats.CurrentHealth = reader.GetInt32("current_hp");
                        player.Position = (reader.GetInt32("position_x"), reader.GetInt32("position_y"));
                        player.DefensiveStats.Armor = reader.GetInt32("armor");
                        player.DefensiveStats.Evade = reader.GetInt32("evade");
                        player.OffensiveStats.AttackDamage = reader.GetInt32("attack_damage");
                        player.OffensiveStats.Accuracy = reader.GetInt32("accuracy");
                    }
                    
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public void GetInventory(Player player)
        {
            const string selectCommand = @"SELECT sword_number, armor_number, key_number FROM player;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(selectCommand, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.GetInt32("sword_number"); i++)
                        {
                            player.swordNumber++;
                            player.Inventory.Add(new Sword());
                        }
                        for (int i = 0; i < reader.GetInt32("armor_number"); i++)
                        {
                            player.armorNumber++;
                            player.Inventory.Add(new Armor());
                        }
                        for (int i = 0; i < reader.GetInt32("key_number"); i++)
                        {
                            player.keyNumber++;
                            player.Inventory.Add(new Key());
                        }
                        
                    }
                    
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        
    }
}