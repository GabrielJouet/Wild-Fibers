using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSquad", menuName = "Towers/Squad")]
public class Squad : ScriptableObject
{
    [SerializeField]
    private List<TowerData> _towers;

    public List<TowerData> Towers { get => _towers; }


    [SerializeField]
    private List<Augmentation> _augmentations;

    public List<Augmentation> Augmentations { get => _augmentations; }


    [SerializeField]
    private Sprite _spriteSquad;
    public Sprite SquadSprite { get => _spriteSquad; }


    [SerializeField]
    private int _livesBonus;
    public int LivesBonus { get; private set; }


    [SerializeField]
    private int _goldBonus;
    public int GoldBonus { get; private set; }



    public void UpdateTowerDatas(List<int> newLevels)
    {
        for (int i = 0; i < _towers.Count; i ++)
            _towers[i].AugmentationLevel = newLevels[i];

        switch (name)
        {
            case "Crazy Foliage":
                UpdateCrazyFoliage(newLevels[4]);
                break;
        }
    }


    private void UpdateCrazyFoliage(int squadLevel)
    {
        TowerData currentTower = _towers[1];
        if (currentTower.AugmentationLevel > 0)
            currentTower.ReducePrice(0.9f);

        currentTower = _towers[2];
        if (currentTower.AugmentationLevel > 1)
        {
            foreach (TowerData current in currentTower.Upgrades)
                current.ReducePrice(25);

            if (currentTower.AugmentationLevel > 2)
                currentTower.IncreaseResellPrice(1.25f);
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