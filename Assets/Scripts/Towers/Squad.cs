using System.Collections.Generic;
using Towers.Upgrades;
using UnityEngine;

namespace Towers
{
    /// <summary>
    /// Class used to handle squad of towers data.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSquad", menuName = "Towers/Squad")]
    public class Squad : ScriptableObject
    {
        /// <summary>
        /// Available towers in this squad.
        /// </summary>
        [field: SerializeField]
        public List<Tower> Towers { get; private set; }


        /// <summary>
        /// List of augmentations available for this squad.
        /// </summary>
        /// <remarks>These augmentations are for squad only, not for towers.</remarks>
        [field: SerializeField]
        public List<Augmentation> Augmentations { get; private set; }


        /// <summary>
        /// The sprite of the squad used in various UI.
        /// </summary>
        [field: SerializeField]
        public Sprite SquadSprite { get; private set; }


        /// <summary>
        /// How many bonus lives the squad has.
        /// </summary>
        [field: SerializeField]
        public int LivesBonus { get; private set; }


        /// <summary>
        /// How much gold bonus this squad has.
        /// </summary>
        [field: SerializeField]
        public int GoldBonus { get; private set; }



        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="clone">The cloned object</param>
        public void Populate(Squad clone)
        {
            name = clone.name;

            Towers = new List<Tower>(clone.Towers);

            Augmentations = clone.Augmentations;
            SquadSprite = clone.SquadSprite;
            LivesBonus = clone.LivesBonus;
            GoldBonus = clone.GoldBonus;
        }


        /// <summary>
        /// Method used to update tower data based on augmentation level.
        /// </summary>
        /// <param name="newLevels">A list of augmentation level</param>
        public void UpdateTowerData(List<int> newLevels)
        {
            for (int i = 0; i < Towers.Count; i ++)
                Towers[i].AugmentationLevel = newLevels[i];

            switch (name)
            {
                case "Crazy Foliage":
                    UpdateCrazyFoliage(newLevels[4]);
                    break;
            }
        }


        /// <summary>
        /// Method used to update a specific squad, this one is crazy foliage.
        /// </summary>
        /// <param name="squadLevel">The squad level</param>
        private void UpdateCrazyFoliage(int squadLevel)
        {
            //Roots tower has a reduced price on first augmentation.
            Tower tower = Towers[1];
            if (tower.AugmentationLevel > 0)
            {
                tower.ReducePrice(0.8f);

                foreach (Tower upgrade in tower.Upgrades)
                {
                    foreach(Tower towerUpgrades in upgrade.Upgrades)
                        towerUpgrades.ReducePrice(0.8f);

                    tower.ReducePrice(0.8f);
                }
            }

            //Vines tower has a reduced price on the second augmentation.
            tower = Towers[2];
            if (tower.AugmentationLevel > 1)
            {
                foreach (Tower current in tower.Upgrades)
                    current.ReducePrice(0.75f);

                //And a better resell price on the third.
                if (tower.AugmentationLevel > 2)
                {
                    foreach (Tower upgrade in tower.Upgrades)
                    {
                        foreach (Tower towerUpgrades in upgrade.Upgrades)
                            towerUpgrades.IncreaseResellPrice(1.25f);

                        tower.IncreaseResellPrice(1.25f);
                    }

                    tower.IncreaseResellPrice(1.25f);
                }
            }

            if (squadLevel > 0)
            {
                GoldBonus += 10;

                if (squadLevel > 1)
                {
                    LivesBonus++;

                    if (squadLevel > 2)
                    {
                        GoldBonus += 10;

                        if (squadLevel > 3)
                        {
                            GoldBonus += 5;

                            if (squadLevel > 4)
                                LivesBonus += 2;
                        }
                    }
                }
            }
        }
    }
}