using UnityEngine;

/// <summary>
/// Class used in Library to contain one tower info.
/// </summary>
public class TowerIcon : MonoBehaviour
{
    public TowerInfo First { get => transform.GetChild(0).GetComponent<TowerInfo>(); }


    public void Populate(TowerData newData, int maxLevel)
    {
        transform.GetChild(0).GetComponent<TowerInfo>().Tower = newData;

        if (maxLevel > 0)
        {
            transform.GetChild(1).GetComponent<TowerInfo>().Tower = newData.Upgrades[0];
            transform.GetChild(2).GetComponent<TowerInfo>().Tower = newData.Upgrades[1];

            if (maxLevel > 1)
            {
                transform.GetChild(3).GetComponent<TowerInfo>().Tower = newData.Upgrades[0].Upgrades[0];
                transform.GetChild(4).GetComponent<TowerInfo>().Tower = newData.Upgrades[1].Upgrades[0];
            }
        }
    }
}
