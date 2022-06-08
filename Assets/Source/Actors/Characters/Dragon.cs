using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Dragon : Character
    {
        private int _timer = 800;
        private int _counter = 0;
        private int _nextFireDirection = 0;
        
        public Dragon()
        {
            DefensiveStats.MaxHealth = 100;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 3;
            DefensiveStats.Evade = 2;
            OffensiveStats.AttackDamage = 6;
            OffensiveStats.Accuracy = 7;
            OffensiveStats.IsWeapon = false;
        }
        protected override void OnUpdate(float deltaTime)
        {
            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }
            

            _counter++;

            if (DefensiveStats.CurrentHealth > 50)
            {
                if (_counter == _timer)
                {
                    _counter = 0;
                    if (_nextFireDirection == 0)
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y - 3);
                        _nextFireDirection = 1;
                    }
                    else if (_nextFireDirection == 1)
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y-2);
                        _nextFireDirection = 2;
                    }
                    else if (_nextFireDirection == 2)
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y+3);
                        _nextFireDirection = 3;
                    }
                    else
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y-2);
                        _nextFireDirection = 0;
                    }
                }
            }
            else
            {
                if (_counter == _timer)
                {
                    _counter = 0;
                    if (_nextFireDirection == 0)
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y - 2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y - 3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y+3);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y+3);
                        _nextFireDirection = 1;
                    }
                    else
                    {
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+1, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+2, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x+3, Position.y-2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-1, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-2, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y+1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y+2);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y-1);
                        ActorManager.Singleton.Spawn<DragonsBreath>(Position.x-3, Position.y-2);
                        _nextFireDirection = 0;
                    }
                }
            }
            
            
            
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            // ActorManager actorManager = new ActorManager();
            // ActorManager.Singleton.DestroyActor(this);
            ActorManager.Singleton.DestroyAllActors();
            UserInterface.Singleton.SetText("DRAGON DESTROYED\nCONGRATULATIONS", UserInterface.TextPosition.MiddleCenter);
        }

        public override int DefaultSpriteId => 411;
        public override string DefaultName { get; set; } =  "Dragon";
    }
}