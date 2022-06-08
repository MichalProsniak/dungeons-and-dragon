using System.Numerics;
using Assets.Source.Actors.Items;
using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DungeonCrawl.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public Inventory Inventory { get; set; }
        public virtual bool Consumable { get; set; } = false;
        public bool IsPicked { get; set; } = false;
        public (int x, int y) Position
        {
            get => _position;
            set
            {
                _position = value;
                transform.position = new Vector3(value.x, value.y, Z);
            }
        }

        private (int x, int y) _position;
        private SpriteRenderer _spriteRenderer;

        protected void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SetSprite(DefaultSpriteId);
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public void SetSprite(int id)
        {
            _spriteRenderer.sprite = ActorManager.Singleton.GetSprite(id);
        }
        private System.Random _rnd = new System.Random();

        public Actor TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.TopLeft);
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.MiddleCenter);
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.BottomCenter);
            }
            else if (actorAtTargetPosition is OpenedDoor)
            {
                Position = targetPosition;
                ActorManager.Singleton.DestroyAllDestroyableActors();
                MapLoader.LoadMap(2);
            }
            else if (actorAtTargetPosition is OpenDoor2)
            {
                Position = targetPosition;
                ActorManager.Singleton.DestroyAllDestroyableActors();
                MapLoader.LoadMap(3);
                // UserInterface.Singleton.SetText("LVL 3!", UserInterface.TextPosition.MiddleCenter);
                
            }
            else if (actorAtTargetPosition is Door)
            {
                if (!Inventory.IsKeyInInventory())
                {
                    UserInterface.Singleton.SetText("You have to find KEY!", UserInterface.TextPosition.TopLeft);
                }
                else
                {
                    ActorManager.Singleton.DestroyActor(actorAtTargetPosition);
                    ActorManager.Singleton.Spawn<OpenDoor2>(actorAtTargetPosition._position);
                    UserInterface.Singleton.SetText("You opened door!", UserInterface.TextPosition.TopLeft);
                }
                
                
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    // Allowed to move
                    if (actorAtTargetPosition is Skeleton || 
                        actorAtTargetPosition is Gargoyle || 
                        actorAtTargetPosition is Ghost || 
                        actorAtTargetPosition is Dragon ||
                        actorAtTargetPosition is Bat)
                    {
                        // attack
                        string hitMessage = FightMechanics(this, actorAtTargetPosition);
                        UserInterface.Singleton.SetText($"NAME: {actorAtTargetPosition.DefaultName}\nHEALTH: {actorAtTargetPosition.DefensiveStats.CurrentHealth}",
                            UserInterface.TextPosition.TopLeft);
                        UserInterface.Singleton.SetText(hitMessage,
                            UserInterface.TextPosition.BottomCenter);
                    }
                    else if (actorAtTargetPosition is Item)
                    {
                        Position = targetPosition;
                    }
                    else if (actorAtTargetPosition is DragonsBreath)
                    {
                        Position = targetPosition;
                        DefensiveStats.CurrentHealth -= 5;
                    }
                }
            }
            return actorAtTargetPosition;
        }
        

        private string FightMechanics<T>(T attacker, T defender) where T: Actor
        {
            string hitMessage = "";
            int hitAccuracyLevel = _rnd.Next(0, 10);
            if (hitAccuracyLevel <= attacker.OffensiveStats.Accuracy - defender.DefensiveStats.Evade)
            {
                AttackMechanics(attacker, defender);
                hitMessage += "Player: HIT";
            }
            else
            {
                hitMessage += "Player: MISS";
            }
            if (defender.DefensiveStats.CurrentHealth > 0)
            {
                hitAccuracyLevel = _rnd.Next(0, 10);
                if (hitAccuracyLevel <=
                    defender.OffensiveStats.Accuracy - attacker.DefensiveStats.Evade)
                {
                    AttackMechanics(defender, attacker);
                    hitMessage += "\nSkeleton: HIT";
                }
                else
                {
                    hitMessage += "\nSkeleton: MISS";
                }
            }
            
            return hitMessage;
        }
        

        private void AttackMechanics<T>(T attacker, T defender) where T : Actor
        {
            int attackPoints = attacker.OffensiveStats.AttackDamage - defender.DefensiveStats.Armor;
            int criticalChance = _rnd.Next(0, 5);
            switch (criticalChance)
            {
                case 0:
                    break;
                case 1:
                    attackPoints += 1;
                    break;
                case 2:
                    attackPoints -= 1;
                    break;
                case 3:
                    attackPoints = attackPoints * 2;
                    break;
                case 4:
                    attackPoints = (int)(attackPoints / 2);
                    break;
            }

            if (attackPoints <= 0)
            {
                attackPoints = 1;
            }
            defender.DefensiveStats.CurrentHealth -= attackPoints;
        }

        /// <summary>
        ///     Invoked whenever another actor attempts to walk on the same position
        ///     this actor is placed.
        /// </summary>
        /// <param name="anotherActor"></param>
        /// <returns>true if actor can walk on this position, false if not</returns>
        public virtual bool OnCollision(Actor anotherActor)
        {
            // All actors are passable by default
            return true;
        }

        

        /// <summary>
        ///     Invoked every animation frame, can be used for movement, character logic, etc
        /// </summary>
        /// <param name="deltaTime">Time (in seconds) since the last animation frame</param>
        protected virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual bool IsPlayerNear()
        {
            return false;
        }

        /// <summary>
        ///     Can this actor be detected with ActorManager.GetActorAt()? Should be false for purely cosmetic actors
        /// </summary>
        public virtual bool Detectable => true;

        /// <summary>
        ///     Z position of this Actor (0 by default)
        /// </summary>
        public virtual int Z => 0;

        /// <summary>
        ///     Id of the default sprite of this actor type
        /// </summary>
        public abstract int DefaultSpriteId { get; }

        /// <summary>
        ///     Default name assigned to this actor type
        /// </summary>
        public abstract string DefaultName { get; set; }

        public virtual bool Destroyable { get; set; } = true;
        
        public DefensiveStats DefensiveStats { get; set; } = new DefensiveStats();
        public OffensiveStats OffensiveStats { get; set; } = new OffensiveStats();
        
    }
}