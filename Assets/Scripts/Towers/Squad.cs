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
    private List<TowerData> _towers;
    public List<TowerData> Towers { get => _towers; private set => _towers = value; }


    /// <summary>
    /// List of augmentations available for this squad.
    /// </summary>
    /// <remarks>These augmentations are for squad only, not for towers.</remarks>
    [SerializeField]
    private List<Augmentation> _augmentations;
    public List<Augmentation> Augmentations { get => _augmentations; private set => _augmentations = value; }


    /// <summary>
    /// The sprite of the squad used in various UI.
    /// </summary>
    [SerializeField]
    private Sprite _spriteSquad;
    public Sprite SquadSprite { get => _spriteSquad; private set => _spriteSquad = value; }


    /// <summary>
    /// How many bonus lives the squad has.
    /// </summary>
    [SerializeField]
    private int _livesBonus;
    public int LivesBonus { get; private set; }


    /// <summary>
    /// How much gold bonus this squad has.
    /// </summary>
    [SerializeField]
    private int _goldBonus;
    public int GoldBonus { get; private set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="clone">The cloned obbject</param>
    public void Populate(Squad clone)
    {
        name = clone.name;

        Towers = new List<TowerData>();
        foreach (TowerData tower in clone.Towers)
        {
            TowerData buffer = CreateInstance<TowerData>();
            buffer.Populate(tower);
            Towers.Add(buffer);
        }

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
        TowerData currentTower = Towers[1];
        if (currentTower.AugmentationLevel > 0)
        {
            currentTower.ReducePrice(0.8f);

            foreach (TowerData tower in currentTower.Upgrades)
            {
                foreach(TowerData towerUpgrades in tower.Upgrades)
                    towerUpgrades.ReducePrice(0.8f);

                tower.ReducePrice(0.8f);
            }
        }

        //Vines tower has a reduced price on the second augmentation.
        currentTower = Towers[2];
        if (currentTower.AugmentationLevel > 1)
        {
            foreach (TowerData current in currentTower.Upgrades)
                current.ReducePrice(25);

            //And a better resell price on the third.
            if (currentTower.AugmentationLevel > 2)
            {
                foreach (TowerData tower in currentTower.Upgrades)
                {
                    foreach (TowerData towerUpgrades in tower.Upgrades)
                        towerUpgrades.IncreaseResellPrice(1.25f);

                    tower.IncreaseResellPrice(1.25f);
                }

                currentTower.IncreaseResellPrice(1.25f);
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