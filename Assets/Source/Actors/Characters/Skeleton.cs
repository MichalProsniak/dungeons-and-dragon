using System;
using DungeonCrawl.Core;
using UnityEngine;
using Assets.Source.Actors.Items;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        System.Random rnd = new System.Random();
        public Skeleton()
        {
            DefensiveStats.MaxHealth = 20;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 2;
            DefensiveStats.Evade = 0;
            OffensiveStats.AttackDamage = 2;
            OffensiveStats.Accuracy = 6;
            OffensiveStats.IsWeapon = false;
        }

        private int _moveCounter = 0;
        protected override void OnUpdate(float deltaTime)
        {
            if (_moveCounter == 240)
            {
                int direction = rnd.Next(0, 4);
                switch (direction)
                {
                    case 0:
                        SkeletonMove(Direction.Up);
                        break;
                    case 1:
                        SkeletonMove(Direction.Down);
                        break;
                    case 2:
                        SkeletonMove(Direction.Left);
                        break;
                    case 3:
                        SkeletonMove(Direction.Right);
                        break;
                }

                _moveCounter = 0;
            }

            _moveCounter++;
            
            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
        
        public void SkeletonMove(Direction direction)
        {
            if (!IsPlayerNear())
            {
                var vector = direction.ToVector();
                (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

                var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

                if (actorAtTargetPosition == null)
                {
                    // No obstacle found, just move
                    Position = targetPosition;
                }
                else
                {
                    if (actorAtTargetPosition.OnCollision(this))
                    {
                        // Allowed to move
                        if (actorAtTargetPosition is Skeleton)
                        {
                            Position = targetPosition;
                        }
                        else if (actorAtTargetPosition is Item)
                        {
                            Position = targetPosition;
                        }
                    }
                }
            }
        }

        public bool IsPlayerNear()
        {
            
            (int x, int y) targetPosition1 = (Position.x + 1, Position.y);
            (int x, int y) targetPosition2 = (Position.x - 1, Position.y);
            (int x, int y) targetPosition3 = (Position.x, Position.y + 1);
            (int x, int y) targetPosition4 = (Position.x, Position.y - 1);

            if (ActorManager.Singleton.GetActorAt(targetPosition1) is Player || ActorManager.Singleton.GetActorAt(targetPosition2) is Player || 
                ActorManager.Singleton.GetActorAt(targetPosition3) is Player || ActorManager.Singleton.GetActorAt(targetPosition4) is Player)
            {
                return true;
            }
            return false;
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            // ActorManager actorManager = new ActorManager();
            ActorManager.Singleton.DestroyActor(this);
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
