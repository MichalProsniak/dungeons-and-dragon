using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using System;
using System.Text.RegularExpressions;
using Assets.Source.Actors.Items;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id)
        {
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    SpawnActor(character, (x, -y));
                }
            }

            // Set default camera size and position
            CameraController.Singleton.Size = 10;
            CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#':
                    ActorManager.Singleton.Spawn<Wall>(position);
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'S':
                    ActorManager.Singleton.Spawn<Sword>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'G':
                    ActorManager.Singleton.Spawn<Gargoyle>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'g':
                    ActorManager.Singleton.Spawn<Ghost>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'D':
                    ActorManager.Singleton.Spawn<Door>(position);
                    break;
                case 'K':
                    ActorManager.Singleton.Spawn<Key>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'H':
                    ActorManager.Singleton.Spawn<Apple>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'A':
                    ActorManager.Singleton.Spawn<Armor>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'f':
                    ActorManager.Singleton.Spawn<CampFire>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'F':
                    ActorManager.Singleton.Spawn<Torch>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'T':
                    ActorManager.Singleton.Spawn<DoubleTree>(position);
                    break;
                case 't':
                    ActorManager.Singleton.Spawn<SingleTree>(position);
                    break;
                case '1':
                    ActorManager.Singleton.Spawn<LeftBridge>(position);
                    break;
                case '2':
                    ActorManager.Singleton.Spawn<MiddleBridge>(position);
                    ActorManager.Singleton.Spawn<RiverFilling>(position);
                    break;
                case '3':
                    ActorManager.Singleton.Spawn<RightBridge>(position);
                    break;
                case 'R':
                    ActorManager.Singleton.Spawn<RiverFilling>(position);
                    break;
                case ' ':
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
