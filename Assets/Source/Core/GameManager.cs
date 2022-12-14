using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Loads the initial map and can be used for keeping some important game variables
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            MapLoader.LoadMap(1);
            ActorManager.Singleton.Spawn<Player>(40, -38);
        }

        public static bool Door1Opened { get; set; } = false;
    }
}
