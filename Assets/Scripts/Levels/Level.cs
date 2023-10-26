using System.Collections.Generic;
using Levels.Waves;
using Miscellanious.Enums;
using Towers;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class used to store level related data.
    /// </summary>
    [CreateAssetMenu(menuName = "Levels/Level", fileName = "NewLevel")]
    public class Level : ScriptableObject
    {
        /// <summary>
        /// Level name.
        /// </summary>
        [field: SerializeField, Header("Display")]
        public string Name { get; private set; }


        /// <summary>
        /// Level thumbnail.
        /// </summary>
        [field: SerializeField]
        public Sprite Picture { get; private set; }


        /// <summary>
        /// Level description.
        /// </summary>
        [field: SerializeField]
        public string Description { get; private set; }


        /// <summary>
        /// Scene related.
        /// </summary>
        [field: SerializeField]
        public string PlaySceneName { get; private set; }


        /// <summary>
        /// Level type: classic, side or challenge.
        /// </summary>
        [field: SerializeField, Header("Level Parameters")]
        public LevelType Type { get; private set; }
        
        /// <summary>
        /// Number of waves.
        /// </summary>
        [field: SerializeField]
        public List<Wave> Waves { get; private set; }

        /// <summary>
        /// How many lives in this level?
        /// </summary>
        [field: SerializeField, Range(1,50)]
        public int LifeCount { get; private set; }


        /// <summary>
        /// Gold count at the start of the level.
        /// </summary>
        [field: SerializeField, Min(150)]
        public int GoldCount { get; private set; }


        /// <summary>
        /// Max tower level available.
        /// </summary>
        [field: SerializeField, Range(0, 3), Header("Tower parameters")]
        public int TowerLevel { get; private set; }

        /// <summary>
        /// Gold multiplier for income gold.
        /// </summary>
        [field: SerializeField, Range(0f, 1f)]
        public float GoldMultiplier { get; private set; }


        /// <summary>
        /// Non-allowed towers in this level.
        /// </summary>
        [field: SerializeField]
        public List<Tower> BlockedTowers { get; private set; }
        
        
        /// <summary>
        /// Chat game object needs to be activated for this level to be working.
        /// </summary>
        [field: SerializeField, Header("Level objects")]
        public LevelStructure LevelStructure { get; private set; }
    }
}