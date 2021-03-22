using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : MonoBehaviour
{
    [SerializeField]
    private InfoIcon _firstLevel;

    [SerializeField]
    private List<InfoIcon> _secondLevel;

    [SerializeField]
    private List<InfoIcon> _thirdLevel;


    public void Populate(TowerData newData)
    {
        _firstLevel.Tower = newData;

        _secondLevel[0].Tower = newData.Upgrades[0];
        _secondLevel[1].Tower = newData.Upgrades[1];

        if (newData.Upgrades[0].Upgrades.Count > 0)
        {
            _thirdLevel[0].Tower = newData.Upgrades[0].Upgrades[0];
            _thirdLevel[1].Tower = newData.Upgrades[1].Upgrades[0];
        }
    }
}
