using System;
using System.IO;
using System.Linq;
using Assets.Source.Actors.Items;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Source.Core

{
    public class LoadJSON
    {
        //public string pathToJson =
        //    @"C:\Users\Sensiczek\AppData\LocalLow\DefaultCompany\DungeonCrawl\test.json";
        //Application.persistentDataPath
        public string pathToJson;
        
        private void GetFiles()
        {
            var file = new DirectoryInfo(Application.persistentDataPath).GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();
            pathToJson = Application.persistentDataPath + @"\" + file.Name;
        }
        
        public void Load(Player player)
        {
            GetFiles();
            using (StreamReader file = File.OpenText(pathToJson))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject playerJsonData = (JObject)JToken.ReadFrom(reader);
                player.DefaultName=playerJsonData["player_name"].ToString();
                player.currentMap = Convert.ToInt32(playerJsonData["current_map"]);
                player.armorNumber = Convert.ToInt32(playerJsonData["armor_number"]);
                player.swordNumber = Convert.ToInt32(playerJsonData["sword_number"]);
                player.DefensiveStats.MaxHealth = Convert.ToInt32(playerJsonData["max_hp"]);
                player.DefensiveStats.CurrentHealth = Convert.ToInt32(playerJsonData["current_hp"]);
                player.Position = (Convert.ToInt32(playerJsonData["position_x"]), Convert.ToInt32(playerJsonData["position_y"]));
                player.DefensiveStats.Armor = Convert.ToInt32(playerJsonData["armor"]);
                player.DefensiveStats.Evade = Convert.ToInt32(playerJsonData["evade"]);
                player.OffensiveStats.AttackDamage = Convert.ToInt32(playerJsonData["attack_damage"]);
                player.OffensiveStats.Accuracy = Convert.ToInt32(playerJsonData["accuracy"]);
                player.keyNumber = Convert.ToInt32(playerJsonData["key_number"]);


            }
            
        }
        public void LoadInvetory(Player player, int swordNumber, int armorNumber, int keyNumber)
        {
            for (int i = 0; i < swordNumber; i++)
            {
                player.swordNumber++;
                player.Inventory.Add(new Sword());
                if (i == 0)
                {
                    player.SetSprite(27);
                }
            }
            for (int i = 0; i < armorNumber; i++)
            {
                player.armorNumber++;
                player.Inventory.Add(new Armor());
            }
            for (int i = 0; i < keyNumber; i++)
            {
                player.keyNumber++;
                player.Inventory.Add(new Key());
            }
        }
    }
}