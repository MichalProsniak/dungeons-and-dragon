using System;
using System.Text.RegularExpressions;
using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public Ghost()
        {
            DefensiveStats.MaxHealth = 20;
            DefensiveStats.CurrentHealth = DefensiveStats.MaxHealth;
            DefensiveStats.Armor = 0;
            DefensiveStats.Evade = 4;
            OffensiveStats.AttackDamage = 3;
            OffensiveStats.Accuracy = 4;
            OffensiveStats.IsWeapon = false;
        }
        private int movementCounter = 0;
        protected override void OnUpdate(float deltaTime)
        {
            if (DefensiveStats.CurrentHealth <= 0)
            {
                OnDeath();
            }

            movementCounter++;
            if (movementCounter == 120)
            {
                var lines = Regex.Split(Resources.Load<TextAsset>($"map_1").text, "\r\n|\r|\n");
                var split = lines[0].Split(' ');
                var width = int.Parse(split[0]);
                var height = int.Parse(split[1]);
                var targetPosition = GetCorrectNewPosition(width, height);
                var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

                if (actorAtTargetPosition == null || actorAtTargetPosition is Wall)
                {
                    // No obstacle found, just move
                    Position = targetPosition;
                }

                movementCounter = 0;
            }
           

        }

        private (int, int) GetCorrectNewPosition(int width, int height)
        {
            Direction direction = GhostPositionDirection();
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);
            if (Position.x + vector.x >= 0 && Position.x + vector.x < width &&
                Position.y + vector.y <= 0 && Position.y + vector.y > height * -1)
            {
               return targetPosition;
            }

            return Position;

        }

        private Direction GhostPositionDirection()
        {
            System.Random rnd = new System.Random();
            int ghostMovementDirection = rnd.Next(0, 4);
            switch (ghostMovementDirection)
            {
                case 0:
                    return Direction.Down;
                case 1:
                    return Direction.Left;
                case 2:
                    return Direction.Right;
                default:
                    return Direction.Up;
            }
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

        public override int DefaultSpriteId => 314;
        public override string DefaultName => "Ghost";
    }
}