using Towers;
using UnityEngine;

/// <summary>
/// Class used in Library to contain one tower info.
/// </summary>
public class TowerIcon : MonoBehaviour
{
    /// <summary>
    /// Base tower.
    /// </summary>
    [SerializeField]
    private TowerInfo _base;

    /// <summary>
    /// First level tower, upgrade of right.
    /// </summary>
    [SerializeField]
    private TowerInfo _firstLevelRight;

    /// <summary>
    /// First level tower, upgrade of left.
    /// </summary>
    [SerializeField]
    private TowerInfo _firstLevelLeft;

    /// <summary>
    /// Second level tower, upgrade of right.
    /// </summary>
    [SerializeField]
    private TowerInfo _secondLevelRight;

    /// <summary>
    /// Second level tower, upgrade of left.
    /// </summary>
    [SerializeField]
    private TowerInfo _secondLevelLeft;


    /// <summary>
    /// Border image component.
    /// </summary>
    [SerializeField]
    private GameObject _border;



    /// <summary>
    /// Initialize method.
    /// </summary>
    /// <param name="newData">New data used for this tower</param>
    /// <param name="maxLevel">The new max level of this tower</param>
    /// <param name="last">Does this tower icon is the last one?</param>
    /// <param name="library">Library object used to activate the info</param>
    public void Populate(Tower newData, int maxLevel, bool last, Library library)
    {
        _base.Initialize(library);
        
        _base.Tower = newData;

        if (maxLevel > 0)
        {
            _firstLevelRight.Tower = newData.Upgrades[0];
            _firstLevelLeft.Tower = newData.Upgrades[1];
            _firstLevelRight.Initialize(library);
            _firstLevelLeft.Initialize(library);

            if (maxLevel > 1)
            {
                _secondLevelRight.Tower = newData.Upgrades[0].Upgrades[0];
                _secondLevelLeft.Tower = newData.Upgrades[1].Upgrades[0];
                _secondLevelRight.Initialize(library);
                _secondLevelLeft.Initialize(library);
            }
        }

        if (last)
            _border.SetActive(false);
    }
}