using System.Collections;
using System.Collections.Generic;
using Levels;
using Towers;
using UnityEngine;

namespace Enemies.Enemy_Types
{
    /// <summary>
    /// Enemy base class.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [Header("Display")]

        /// <summary>
        /// Selector object.
        /// </summary>
        [SerializeField]
        protected GameObject selector;

        /// <summary>
        /// Health bar component.
        /// </summary>
        [SerializeField]
        protected HealthBar healthBar;

        /// <summary>
        /// Dot display object.
        /// </summary>
        [SerializeField]
        protected GameObject dotDisplay;

        /// <summary>
        /// Sprite renderer component.
        /// </summary>
        [SerializeField]
        protected SpriteRenderer spriteRenderer;

        /// <summary>
        /// Animator component.
        /// </summary>
        [SerializeField]
        protected Animator animator;


        /// <summary>
        /// Health max.
        /// </summary>
        [field: SerializeField, Header("Behaviour Variables")]
        public int HealthMax { get; protected set; }

        /// <summary>
        /// Current health.
        /// </summary>
        public float Health { get; protected set; }


        /// <summary>
        /// Armor max value.
        /// </summary>
        [field: SerializeField]
        public float ArmorMax { get; protected set; }

        /// <summary>
        /// Current armor.
        /// </summary>
        public float Armor { get; protected set; }


        /// <summary>
        /// Resistance max value (resistance is used for dot and magic attacks).
        /// </summary>
        [field: SerializeField]
        public float Resistance { get; protected set; }

        /// <summary>
        /// Resistance max value (resistance is used for dot and magic attacks).
        /// </summary>
        private float ResistancePercentage => (100 - Resistance) / 100;


        /// <summary>
        /// Speed max.
        /// </summary>
        [field: SerializeField]
        public float SpeedMax { get; protected set; }

        /// <summary>
        /// Current speed.
        /// </summary>
        private float Speed { get; set; }


        /// <summary>
        /// Number of lives removed if the enemy reaches the end of the path.
        /// </summary>
        [field: SerializeField]
        public int LivesTaken { get; protected set; }


        /// <summary>
        /// Gold value gained if the enemy is killed.
        /// </summary>
        [field: SerializeField]
        public int GoldGained { get; protected set; }


        /// <summary>
        /// Does the enemy actually flies?
        /// </summary>
        [field: SerializeField]
        public bool Flying { get; protected set; }


        /// <summary>
        /// Damage position transform component.
        /// </summary>
        [field: SerializeField]
        public Vector2 DamagePosition { get; protected set; }

        
        /// <summary>
        /// Description used in bestiary.
        /// </summary>
        [field: SerializeField, Header("Bestiary related")]
        public string Description { get; protected set; }


        /// <summary>
        /// Screen shot used in bestiary.
        /// </summary>
        [field: SerializeField]
        public Sprite ScreenShot { get; protected set; }


        /// <summary>
        /// Special info box.
        /// </summary>
        [field: SerializeField]
        public string Special { get; protected set; }


        /// <summary>
        /// Does the enemy is moving?
        /// </summary>
        protected bool _moving = true;

        /// <summary>
        /// Does the enemy is slow down?
        /// </summary>
        protected bool _isSlowDown = false;

        /// <summary>
        /// Loaded path.
        /// </summary>
        protected List<Vector2> _path;

        /// <summary>
        /// Progression on path.
        /// </summary>
        protected int _pathIndex;

        /// <summary>
        /// Path ratio.
        /// </summary>
        public float PathRatio => (float)_pathIndex / (float)_path.Count;

        /// <summary>
        /// Information UI component.
        /// </summary>
        public BackgroudSelecter InformationUI { get; set; }

        /// <summary>
        /// Current dot used.
        /// </summary>
        protected List<Attack> _dots;

        /// <summary>
        /// Current spawner related to this enemy.
        /// </summary>
        protected Spawner _spawner;


        /// <summary>
        /// Is the enemy dotted?
        /// </summary>
        public bool IsDotted => _dots.Count != 0;


        /// <summary>
        /// Initialize method, called at enemy creation or re-use.
        /// </summary>
        /// <param name="newPath">New path used</param>
        /// <param name="spawner">Spawner that spawns this enemy</param>
        /// <param name="pathIndex">Current progression on the path</param>
        public virtual void Initialize(List<Vector2> newPath, int pathIndex, Spawner spawner)
        {
            _spawner = spawner;

            dotDisplay.SetActive(false);
            _dots = new List<Attack>();

            Speed = SpeedMax;
            Health = HealthMax;
            Armor = ArmorMax;

            healthBar.ResetSize();

            _pathIndex = pathIndex;
            _path = newPath;

            transform.position = _path[_pathIndex];
        }


        /// <summary>
        /// Update method, called each frame.
        /// </summary>
        protected void Update()
        {
            if (_moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _path[_pathIndex], Time.deltaTime * Speed);

                if ((Vector2)transform.position == _path[_pathIndex] && _pathIndex + 1 < _path.Count)
                    _pathIndex++;
                else if (_pathIndex + 1 == _path.Count)
                    Die(true);

                spriteRenderer.flipX = transform.position.x - _path[_pathIndex].x > 0;
            }
        }


        /// <summary>
        /// Method to remove health to enemy.
        /// </summary>
        /// <param name="newAttack">The related attack</param>
        public void TakeDamage(Attack newAttack)
        {
            float armorThrough = newAttack.ArmorThrough;
            float damage = newAttack.Damage;

            int damageLeft = Mathf.FloorToInt(Armor - armorThrough < 0 ? damage + (damage * (armorThrough - Armor) / 100) / 2 : damage - ((Armor - armorThrough) / 100 * damage));

            TakeDamage(damageLeft);

            if (newAttack.DotDuration > 0)
                ApplyDot(newAttack);
        }


        /// <summary>
        /// Method used to remove health to an enemy without using shield.
        /// </summary>
        /// <param name="damage">The amount of damage the enemy is taking</param>
        public void TakeDamage(float damage)
        {
            if (Health - damage <= 0)
                Die(false);
            else
                Health -= damage;

            healthBar.ChangeSize(Health / HealthMax);

            if (InformationUI)
                InformationUI.UpdateEnemyInformation(this);
        }


        /// <summary>
        /// Method used to apply a slowdown on the enemy.
        /// </summary>
        /// <param name="slowDownRatio">The percentage of speed loss</param>
        /// <param name="slowDownTime">Time before slowdown is reset</param>
        public void ApplySlowDown(float slowDownRatio, float slowDownTime)
        {
            if(!_isSlowDown)
            {
                _isSlowDown = true;
                StartCoroutine(ResetSlowDown(slowDownTime));
                Speed = SpeedMax * (100 - slowDownRatio * ResistancePercentage) / 100f;

                if (InformationUI)
                    InformationUI.UpdateEnemyInformation(this);
            }
        }

        /// <summary>
        /// Coroutine used to delay the slow down reset.
        /// </summary>
        /// <param name="slowDownTime">The slow down time</param>
        /// <returns>Yield the slow down reset time</returns>
        protected IEnumerator ResetSlowDown(float slowDownTime)
        {
            yield return new WaitForSeconds(slowDownTime);
            _isSlowDown = false;
            Speed = SpeedMax;

            if (InformationUI)
                InformationUI.UpdateEnemyInformation(this);
        }


        /// <summary>
        /// Method used to apply a dot on the enemy.
        /// </summary>
        /// <param name="newAttack">The new dot applied</param>
        protected void ApplyDot(Attack newAttack)
        {
            if (_dots.Count > 3)
            {
                Attack attackRemoved = _dots[0];
                foreach(Attack attack in _dots)
                    if (attack.DotDamage * attack.DotDuration * attack.ArmorThroughMalus < attackRemoved.DotDamage * attackRemoved.DotDuration * attackRemoved.ArmorThroughMalus)
                        attackRemoved = attack;

                _dots.Remove(attackRemoved);
            }

            _dots.Add(newAttack);

            dotDisplay.SetActive(true);

            if (InformationUI)
                InformationUI.UpdateEnemyInformation(this);

            if (isActiveAndEnabled)
                StartCoroutine(TakePersistentDamage(newAttack));
        }


        /// <summary>
        /// Coroutine used take damage from dot.
        /// </summary>
        /// <param name="newDot">The dot applied</param>
        /// <returns>Yield 0.5 sec</returns>
        protected IEnumerator TakePersistentDamage(Attack newDot)
        {
            Armor -= newDot.ArmorThroughMalus * ResistancePercentage;
            float timeRemaining = newDot.DotDuration;

            while(timeRemaining > 0)
            {
                timeRemaining -= 0.5f;
                TakeDamage(newDot.DotDamage);
                yield return new WaitForSeconds(0.5f);
            }

            _dots.Remove(newDot);
            Armor += newDot.ArmorThroughMalus * ResistancePercentage;

            dotDisplay.SetActive(_dots.Count != 0);
        }


        /// <summary>
        /// Method called when a tower destroy a bit of armor each hit.
        /// </summary>
        /// <param name="percentage">The percentage broken by hit</param>
        public void DestroyArmor(int percentage)
        {
            ArmorMax = Mathf.Clamp(ArmorMax - percentage, 0, ArmorMax);
            Armor = Mathf.Clamp(Armor - percentage, 0, Armor);

            if (InformationUI)
                InformationUI.UpdateEnemyInformation(this);
        }


        /// <summary>
        /// Method used when the life of an enemy drops below zero or other specific behavior.
        /// </summary>
        /// <param name="reachEnd">Does the enemy reaches the end?</param>
        protected void Die(bool reachEnd)
        {
            _spawner.EnemyKilled();

            if (InformationUI)
            {
                InformationUI.ErasePreviousEnemy();
                InformationUI.DisableEnemyInformation();
            }

            StopAllCoroutines();

            if (!reachEnd)
                RessourceController.Instance.AddGold(GoldGained, true);
            else
                RessourceController.Instance.RemoveLives(LivesTaken);
        
            Destroy(gameObject);
        }


        /// <summary>
        /// Activate or deactivate selector state.
        /// </summary>
        /// <param name="state">The state value</param>
        public void SetSelector(bool state) 
        { 
            selector.SetActive(state);
        }


        /// <summary>
        /// Collision enter on 2D objects.
        /// </summary>
        /// <param name="collision">Collision object</param>
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TowerCollider towerCollider))
                towerCollider.EnemyCollide(this);
        }


        /// <summary>
        /// Collision exit on 2D objects.
        /// </summary>
        /// <param name="collision">Collision object</param>
        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TowerCollider towerCollider))
                towerCollider.EnemyExit(this);
        }
    }
}