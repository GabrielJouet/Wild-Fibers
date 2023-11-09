using System.Collections;
using System.Collections.Generic;
using Enemies.Enemy_Types;
using Levels;
using Miscellanious;
using Towers.Projectiles;
using Towers.Upgrades;
using UI;
using UI.InGame;
using UnityEngine;

namespace Towers
{
    /// <summary>
    /// Tower class, main object of the game.
    /// </summary>
    /// <remarks>Needs a static depth manager</remarks>
    [RequireComponent(typeof(DepthManager))]
    public abstract class Tower : MonoBehaviour
    {
        /// <summary>
        /// Range display.
        /// </summary>
        [SerializeField]
        protected Transform transformRange;

        /// <summary>
        /// Collider used in range detection.
        /// </summary>
        [SerializeField]
        protected Transform towerCollider;

        /// <summary>
        /// Selector object used when clicked.
        /// </summary>
        [SerializeField]
        protected GameObject selector;



        /// <summary>
        /// Description.
        /// </summary>
        [field: SerializeField, TextArea, Header("Description")]
        public string Description { get; private set; }
        
        /// <summary>
        /// Name of this tower.
        /// </summary>
        [field: SerializeField]
        public string Name { get; private set; }


        /// <summary>
        /// Description.
        /// </summary>
        [field: SerializeField, TextArea]
        public string LibraryDescription { get; private set; }


        /// <summary>
        /// Base price of the tower.
        /// </summary>
        [SerializeField]
        protected int _price;

        /// <summary>
        /// Base price of the tower.
        /// </summary>
        public float PriceFactor { get; set; } = 1f;

        /// <summary>
        /// Resell Price factor of the tower.
        /// </summary>
        public float ResellPriceFactor { get; private set; } = 1f;

        /// <summary>
        /// Calculated price of the tower.
        /// </summary>
        public int Price { get => Mathf.FloorToInt(_price * PriceFactor); }


        /// <summary>
        /// Icon of the tower.
        /// </summary>
        [field: SerializeField]
        public Sprite Icon { get; set; }



        /// <summary>
        /// Projectile used in attack.
        /// </summary>
        [field: SerializeField, Header("Damage Related")]
        public GameObject Projectile { get; set; }


        /// <summary>
        /// Time between attack in second.
        /// </summary>
        [field: SerializeField]
        public float TimeShots { get; set; }


        /// <summary>
        /// Damage per attack.
        /// </summary>
        [field: SerializeField]
        public int Damage { get; set; }


        /// <summary>
        /// Armor through on each attack.
        /// </summary>
        [field: SerializeField]
        public float ArmorThrough { get; set; }


        /// <summary>
        /// Number of projectile per attack.
        /// </summary>
        [field: SerializeField]
        public int Shots { get; set; }


        /// <summary>
        /// Range of the tower.
        /// </summary>
        [field: SerializeField]
        public float Range { get; set; }


        /// <summary>
        /// Can the tower hits flying target?
        /// </summary>
        [field: SerializeField]
        public bool HitFlying { get; set; }


        /// <summary>
        /// Does the towers choose its target?
        /// </summary>
        [field: SerializeField]
        public bool ShotsRandomly { get; set; }

        

        /// <summary>
        /// Armor malus with each shot.
        /// </summary>
        [field: SerializeField, Header("Dot related")]
        public float ArmorThroughMalus { get; set; }


        /// <summary>
        /// Dot over time damage.
        /// </summary>
        [field: SerializeField]
        public int Dot { get; set; }


        /// <summary>
        /// Dot duration.
        /// </summary>
        [field: SerializeField]
        public float DotDuration { get; set; }



        /// <summary>
        /// Tower upgrades.
        /// </summary>
        [field: SerializeField, Header("Upgrades")]
        public List<Tower> Upgrades { get; set; }


        /// <summary>
        /// Tower specs (only for last level).
        /// </summary>
        [field: SerializeField]
        public List<TowerSpec> Specs { get; set; }


        /// <summary>
        /// Tower Augmentations (only for first level).
        /// </summary>
        [field: SerializeField]
        public List<Augmentation> Augmentations { get; set; }

        /// <summary>
        /// Current augmentation level of this tower.
        /// </summary>
        public int AugmentationLevel { get; set; }



        /// <summary>
        /// A special info box.
        /// </summary>
        [field: SerializeField, TextArea, Header("Info related")]
        public string Special { get; set; }


        /// <summary>
        /// Screen shot displaying what the tower can do.
        /// </summary>
        [field: SerializeField]
        public Sprite ScreenShot { get; set; }


        /// <summary>
        /// Fire rate transformed.
        /// </summary>
        public string FireRateInfo { get => Converter.TransformFireRate(1 / TimeShots); }

        /// <summary>
        /// Armor through transformed.
        /// </summary>
        public string ArmorThroughInfo { get => Converter.TransformArmorThrough((ArmorThroughMalus + ArmorThrough) / 100); }

        /// <summary>
        /// Dot transformed.
        /// </summary>
        public string DotInfo { get => Converter.TransformDot(DotDuration * Dot * 2); }


        /// <summary>
        /// Information UI object.
        /// </summary>
        protected BackgroudSelecter _backgroundSelecter;

        /// <summary>
        /// The related tower slot.
        /// </summary>
        public TowerSlot Slot { get; private set; }

        /// <summary>
        /// Resource controller used to record money.
        /// </summary>
        protected RessourceController _ressourceController;

        /// <summary>
        /// List of enemies in range.
        /// </summary>
        protected List<Enemy> _availableEnemies = new List<Enemy>();

        /// <summary>
        /// Did the coroutine started?
        /// </summary>
        protected bool _coroutineStarted;

        /// <summary>
        /// How much gold was used on this tower from the beginning.
        /// </summary>
        public int CumulativeGold { get; protected set; } = 0;

        /// <summary>
        /// Basic attack of this tower.
        /// </summary>
        protected Attack _attack;

        /// <summary>
        /// Queue of attacks, used for multi shots towers.
        /// </summary>
        protected Queue<Attack> _nextAttack = new Queue<Attack>();



        /// <summary>
        /// Method used to initialize.
        /// </summary>
        /// <param name="newSlot">The parent slot</param>
        /// <param name="newRessourceController">The resource controller</param>
        /// <param name="newBackgroundSelecter">The background selecter component</param>
        public virtual void Initialize(TowerSlot newSlot, BackgroudSelecter newBackgroundSelecter, int cumulativeGold)
        {
            _nextAttack.Clear();
            selector.SetActive(false);
            transformRange.gameObject.SetActive(false);

            _attack = new Attack(Damage, ArmorThrough, DotDuration, ArmorThroughMalus, Dot);
            CheckAugmentation();

            CumulativeGold = cumulativeGold + Mathf.FloorToInt(_price * PriceFactor);

            transformRange.localScale = Range * Vector3.one;
            towerCollider.localScale = (0.9f * Range) * Vector3.one;

            towerCollider.GetComponent<TowerCollider>().ParentTower = this;

            transform.position = newSlot.transform.position;
            _backgroundSelecter = newBackgroundSelecter;
            _ressourceController = RessourceController.Instance;
            Slot = newSlot;

            SpecialBehavior();
        }


        /// <summary>
        /// Method used to add a special behavior at the start of the initialization.
        /// </summary>
        protected virtual void SpecialBehavior() { }


        /// <summary>
        /// FixedUpdate, called 50 times a second.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (_availableEnemies.Count > 0 && !_coroutineStarted)
                StartCoroutine(SummonProjectile());
        }


        /// <summary>
        /// Coroutine used to delay attacks.
        /// </summary>
        protected virtual IEnumerator SummonProjectile()
        {
            _coroutineStarted = true;

            foreach (Enemy current in RecoverAvailableEnemies(_availableEnemies.Count < Shots ? _availableEnemies.Count : Shots))
                Instantiate(Projectile).GetComponent<Projectile>().Initialize(_nextAttack.Dequeue(), current, transform);

            yield return new WaitForSeconds(TimeShots);
            _coroutineStarted = false;
        }



        #region Upgrades and Money related
        /// <summary>
        /// Method used to resell the tower.
        /// </summary>
        public void ResellTower()
        {
            ResellSpecialBehavior();

            _ressourceController.AddGold(Mathf.FloorToInt((CumulativeGold * ResellPriceFactor) * 0.65f), false);

            _backgroundSelecter.DisableTowerInformation();
            _backgroundSelecter.DisableTowerSellButton();

            Slot.ResetSlot();
            Destroy(gameObject);
        }


        /// <summary>
        /// Method override by children to improve resell behavior.
        /// </summary>
        protected virtual void ResellSpecialBehavior() { }


        /// <summary>
        /// Method used to upgrade the tower.
        /// </summary>
        public void UpgradeTower(Tower newData)
        {
            _ressourceController.RemoveGold(newData.Price);

            _backgroundSelecter.DeactivateTower();
            UpgradeSpecialBehavior();

            Destroy(gameObject);
            Instantiate(newData).Initialize(Slot, _backgroundSelecter, Price);
        }


        /// <summary>
        /// Method override by children to improve upgrade behavior.
        /// </summary>
        protected virtual void UpgradeSpecialBehavior() { }


        /// <summary>
        /// Method used to add a spec to the tower, will be override by children.
        /// </summary>
        /// <param name="newSpec">The new spec to add</param>
        public virtual void AddSpec(TowerSpec newSpec) { }


        /// <summary>
        /// Method used to check augmentation level of a tower and applying changes.
        /// </summary>
        protected void CheckAugmentation()
        {
            if (AugmentationLevel > 0)
            {
                LevelOneAugmentation();

                if (AugmentationLevel > 1)
                {
                    LevelTwoAugmentation();

                    if (AugmentationLevel > 2)
                    {
                        LevelThreeAugmentation();

                        if (AugmentationLevel > 3)
                        {
                            LevelFourAugmentation();

                            if (AugmentationLevel > 4)
                                LevelFiveAugmentation();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Method called when the tower has one augmentation.
        /// </summary>
        protected virtual void LevelOneAugmentation() { }


        /// <summary>
        /// Method called when the tower has two augmentations.
        /// </summary>
        protected virtual void LevelTwoAugmentation() { }


        /// <summary>
        /// Method called when the tower has three augmentations.
        /// </summary>
        protected virtual void LevelThreeAugmentation() { }


        /// <summary>
        /// Method called when the tower has four augmentations.
        /// </summary>
        protected virtual void LevelFourAugmentation() { }


        /// <summary>
        /// Method called when the tower has five augmentations.
        /// </summary>
        protected virtual void LevelFiveAugmentation() { }
        #endregion 



        #region Enemies interaction
        /// <summary>
        /// Method used to add an enemy to the list.
        /// </summary>
        /// <param name="enemy">The enemy to add</param>
        public void AddEnemy(Enemy enemy)
        {
            if (!(!HitFlying && enemy.Flying))
                _availableEnemies.Add(enemy);
        }


        /// <summary>
        /// Method used to remove an enemy to the list.
        /// </summary>
        /// <param name="enemy">The enemy to remove</param>
        public void RemoveEnemy(Enemy enemy)
        {
            if (_availableEnemies.Contains(enemy))
                _availableEnemies.Remove(enemy);
        }


        /// <summary>
        /// Method used to sort the enemy list.
        /// </summary>
        protected void SortEnemies()
        {
            _availableEnemies.Sort((a, b) => b.PathRatio.CompareTo(a.PathRatio));
        }


        /// <summary>
        /// Method used to recover available and prefered enemies.
        /// </summary>
        /// <param name="numberOfEnemiesToFound">How many enemies are needed</param>
        /// <returns>A list of found enemies</returns>
        protected List<Enemy> RecoverAvailableEnemies(int numberOfEnemiesToFound)
        {
            List<Enemy> availableEnemies = new List<Enemy>();

            if (ShotsRandomly)
                _availableEnemies.Shuffle();
            else
                SortEnemies();

            if (numberOfEnemiesToFound > _availableEnemies.Count)
            {
                foreach (Enemy buffer in _availableEnemies)
                {
                    _nextAttack.Enqueue(ChangeNextAttack(buffer));

                    availableEnemies.Add(buffer);
                }
            }

            foreach (Enemy buffer in _availableEnemies)
            {
                _nextAttack.Enqueue(ChangeNextAttack(buffer));

                availableEnemies.Add(buffer);

                if (availableEnemies.Count >= numberOfEnemiesToFound)
                    break;
            }

            return availableEnemies;
        }


        /// <summary>
        /// Method override by children to change their attack.
        /// </summary>
        /// <param name="enemy">Enemt targeted</param>
        protected virtual Attack ChangeNextAttack(Enemy enemy)
        {
            return _attack;
        }
        #endregion



        #region Reset related
        /// <summary>
        /// Method used to activate the range.
        /// </summary>
        public void ActivateRangeDisplay()
        {
            transformRange.gameObject.SetActive(true);
            selector.SetActive(true);
        }


        /// <summary>
        /// Method used to deactivate the range.
        /// </summary>
        public void DeactivateRangeDisplay()
        {
            transformRange.gameObject.SetActive(false);
            selector.SetActive(false);
        }
        #endregion



        /// <summary>
        /// Method used to reduce price of a tower with a ratio.
        /// </summary>
        /// <param name="ratio">Ratio of the price asked</param>
        public void ReducePrice(float ratio)
        {
            PriceFactor = ratio;
        }


        /// <summary>
        /// Method used to increase the resell price.
        /// </summary>
        /// <param name="factor">Factor of the resell price</param>
        public void IncreaseResellPrice(float factor)
        {
            ResellPriceFactor = factor;
        }
    }
}