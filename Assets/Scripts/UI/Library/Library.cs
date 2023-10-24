using System.Collections.Generic;
using Enemies.Enemy_Types;
using TMPro;
using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Library
{
    /// <summary>
    /// Class used to handle enemy and tower info in library.
    /// </summary>
    public class Library : MonoBehaviour
    {
        [Header("Components")]

        [Space(10)]

        /// <summary>
        /// Enemy info prefab component.
        /// </summary>
        [SerializeField]
        private GameObject enemyInfoPrefab;

        /// <summary>
        /// Tower panel parent object.
        /// </summary>
        [SerializeField]
        private GameObject towerPanel;

        /// <summary>
        /// Enemy panel parent object.
        /// </summary>
        [SerializeField]
        private GameObject enemyPanel;

        /// <summary>
        /// Tower list game object.
        /// </summary>
        [SerializeField]
        private GameObject towerList;

        /// <summary>
        /// Enemy list game object.
        /// </summary>
        [SerializeField]
        private GameObject enemyList;

        /// <summary>
        /// Tower tree prefab, used for tower display.
        /// </summary>
        [SerializeField]
        private GameObject towerTreePrefab;

        /// <summary>
        /// Separator image component.
        /// </summary>
        [SerializeField]
        private Transform separator;

        /// <summary>
        /// Towers tab image component.
        /// </summary>
        [SerializeField]
        private Image towerTab;

        /// <summary>
        /// Enemies tab image component.
        /// </summary>
        [SerializeField]
        private Image enemiesTab;


        [Header("Tower related")]

        /// <summary>
        /// Tower name text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerName;

        /// <summary>
        /// Tower description text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerDescription;

        /// <summary>
        /// Tower price text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerPrice;

        /// <summary>
        /// Tower damage text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerDamage;

        /// <summary>
        /// Tower fire rate text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerFireRate;

        /// <summary>
        /// Tower armor through text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerArmorThrough;

        /// <summary>
        /// Tower dot text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerDot;

        /// <summary>
        /// Tower special text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerSpecial;

        /// <summary>
        /// Tower number of shots text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI towerShots;


        [Header("Enemy related")]

        /// <summary>
        /// Enemy name text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyName;

        /// <summary>
        /// Enemy description text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyDescription;

        /// <summary>
        /// Enemy gold gained text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyGold;

        /// <summary>
        /// Enemy health text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyHealth;

        /// <summary>
        /// Enemy speed text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemySpeed;

        /// <summary>
        /// Enemy armor text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyArmor;

        /// <summary>
        /// Enemy resistance text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyResistance;

        /// <summary>
        /// Enemy lives lost text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemyLivesLost;

        /// <summary>
        /// Enemy special text component.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI enemySpecial;


        /// <summary>
        /// Color used for deactivated tabs.
        /// </summary>
        private readonly Color _deactivatedColor = new Color(0.7f, 0.7f, 0.7f);



        /// <summary>
        /// Awake method, called at first.
        /// </summary>
        private void Start()
        {
            List<Tower> towers = Controller.Instance.SquadController.CurrentSquad.Towers;

            List<Enemy> enemies = Controller.Instance.EnemyController.Enemies;

            for (int i = 0; i < Controller.Instance.SaveController.SaveFile.EnemiesUnlocked.Count; i ++)
            {
                EnemyInfo enemyBuffered = Instantiate(enemyInfoPrefab, enemyList.transform).GetComponent<EnemyInfo>();
                enemyBuffered.Enemy = enemies[i];
                enemyBuffered.Initialize(this);
            }

            int maxLevel = Controller.Instance.SaveController.SaveFile.CurrentSquad.TowerLevelMax;

            for (int i = 0; i < 4; i ++)
                Instantiate(towerTreePrefab, towerList.transform).GetComponent<TowerIcon>().Populate(towers[i], maxLevel, i != 3, this);

            RectTransform towerPanel = towerList.GetComponent<RectTransform>();
            VerticalLayoutGroup towerLayout = towerList.GetComponent<VerticalLayoutGroup>();
            towerPanel.sizeDelta = new Vector2(towerPanel.sizeDelta.x, towerLayout.padding.top * 2 + towerLayout.spacing * 3 + 4 * towerTreePrefab.GetComponent<RectTransform>().sizeDelta.y);

            ShowPanel(false);
        }



        /// <summary>
        /// Method used to show the panels.
        /// </summary>
        /// <param name="enemy">Does the panel shown is the enemy one?</param>
        public void ShowPanel(bool enemy)
        {
            towerList.transform.parent.parent.gameObject.SetActive(!enemy);
            enemyList.SetActive(enemy);

            separator.localScale = new Vector3(enemy ? -1 : 1, 1, 1);
            towerTab.color = enemy ? _deactivatedColor : Color.white;
            enemiesTab.color = !enemy ? _deactivatedColor : Color.white;

            towerPanel.SetActive(!enemy);
            enemyPanel.SetActive(enemy);
        }


        /// <summary>
        /// Method called to show an object info.
        /// </summary>
        /// <param name="newInfo">The info object to display</param>
        public void ShowInfo(ObjectInfo newInfo)
        {
            if (newInfo.TryGetComponent(out TowerInfo towerInfo))
                ShowTowerInfo(towerInfo);
            else if (newInfo.TryGetComponent(out EnemyInfo enemyInfo))
                ShowEnemyInfo(enemyInfo);
        }


        /// <summary>
        /// Method called to show a tower info.
        /// </summary>
        /// <param name="newInfo">The info tower to display</param>
        private void ShowTowerInfo(TowerInfo newInfo)
        {
            Tower newTower = newInfo.Tower;

            towerName.text = newTower.name;
            towerDescription.text = newTower.LibraryDescription;
            towerPrice.text = newTower.Price.ToString();
            towerDamage.text = newTower.Damage.ToString();
            towerFireRate.text = newTower.FireRateInfo;
            towerArmorThrough.text = newTower.ArmorThroughInfo;
            towerDot.text = newTower.DotInfo;
            towerSpecial.text = newTower.Special;
            towerShots.text = newTower.Shots.ToString();
        }


        /// <summary>
        /// Method called to show an enemy info.
        /// </summary>
        /// <param name="newInfo">The info enemy to show</param>
        private void ShowEnemyInfo(EnemyInfo newInfo)
        {
            Enemy newEnemy = newInfo.Enemy;

            enemyName.text = newEnemy.name;
            enemyDescription.text = newEnemy.Description;
            enemyGold.text = newEnemy.GoldGained.ToString();
            enemyHealth.text = newEnemy.HealthMax.ToString();
            enemySpeed.text = Converter.TransformSpeed(newEnemy.SpeedMax);
            enemyArmor.text = Converter.TransformArmor(newEnemy.ArmorMax / 100);
            enemyResistance.text = Converter.TransformResistance(newEnemy.Resistance / 100);
            enemyLivesLost.text = newEnemy.LivesTaken.ToString();
            enemySpecial.text = newEnemy.Special;
        }
    }
}