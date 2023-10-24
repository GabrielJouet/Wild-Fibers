using Towers;
using UnityEngine;

namespace UI.Library
{
    /// <summary>
    /// Class used in Library to contain one tower info.
    /// </summary>
    public class TowerIcon : MonoBehaviour
    {
        /// <summary>
        /// Base tower.
        /// </summary>
        [SerializeField]
        private TowerInfo @base;

        /// <summary>
        /// First level tower, upgrade of right.
        /// </summary>
        [SerializeField]
        private TowerInfo firstLevelRight;

        /// <summary>
        /// First level tower, upgrade of left.
        /// </summary>
        [SerializeField]
        private TowerInfo firstLevelLeft;

        /// <summary>
        /// Second level tower, upgrade of right.
        /// </summary>
        [SerializeField]
        private TowerInfo secondLevelRight;

        /// <summary>
        /// Second level tower, upgrade of left.
        /// </summary>
        [SerializeField]
        private TowerInfo secondLevelLeft;


        /// <summary>
        /// Border image component.
        /// </summary>
        [SerializeField]
        private GameObject border;



        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="newData">New data used for this tower</param>
        /// <param name="maxLevel">The new max level of this tower</param>
        /// <param name="last">Does this tower icon is the last one?</param>
        /// <param name="library">Library object used to activate the info</param>
        public void Populate(Tower newData, int maxLevel, bool last, Library library)
        {
            @base.Initialize(library);
        
            @base.Tower = newData;

            if (maxLevel > 0)
            {
                firstLevelRight.Tower = newData.Upgrades[0];
                firstLevelLeft.Tower = newData.Upgrades[1];
                firstLevelRight.Initialize(library);
                firstLevelLeft.Initialize(library);

                if (maxLevel > 1)
                {
                    secondLevelRight.Tower = newData.Upgrades[0].Upgrades[0];
                    secondLevelLeft.Tower = newData.Upgrades[1].Upgrades[0];
                    secondLevelRight.Initialize(library);
                    secondLevelLeft.Initialize(library);
                }
            }

            if (last)
                border.SetActive(false);
        }
    }
}