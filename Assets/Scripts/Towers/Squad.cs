using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle squad of towers data.
/// </summary>
[CreateAssetMenu(fileName = "NewSquad", menuName = "Towers/Squad")]
public class Squad : ScriptableObject
{
    /// <summary>
    /// Avaiable towers in this squad.
    /// </summary>
    [SerializeField]
    private List<Tower> _towers;

    /// <summary>
    /// Avaiable towers in this squad.
    /// </summary>
    public List<Tower> Towers { get => _towers; private set => _towers = value; }


    /// <summary>
    /// List of augmentations available for this squad.
    /// </summary>
    /// <remarks>These augmentations are for squad only, not for towers.</remarks>
    [SerializeField]
    private List<Augmentation> _augmentations;

    /// <summary>
    /// List of augmentations available for this squad.
    /// </summary>
    /// <remarks>These augmentations are for squad only, not for towers.</remarks>
    public List<Augmentation> Augmentations { get => _augmentations; private set => _augmentations = value; }


    /// <summary>
    /// The sprite of the squad used in various UI.
    /// </summary>
    [SerializeField]
    private Sprite _spriteSquad;

    /// <summary>
    /// The sprite of the squad used in various UI.
    /// </summary>
    public Sprite SquadSprite { get => _spriteSquad; private set => _spriteSquad = value; }


    /// <summary>
    /// How many bonus lives the squad has.
    /// </summary>
    [SerializeField]
    private int _livesBonus;

    /// <summary>
    /// How many bonus lives the squad has.
    /// </summary>
    public int LivesBonus { get; private set; }


    /// <summary>
    /// How much gold bonus this squad has.
    /// </summary>
    [SerializeField]
    private int _goldBonus;

    /// <summary>
    /// How much gold bonus this squad has.
    /// </summary>
    public int GoldBonus { get; private set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="clone">The cloned obbject</param>
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
    public void UpdateTowerDatas(List<int> newLevels)
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

        LivesBonus = _livesBonus;
        GoldBonus = _goldBonus;
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