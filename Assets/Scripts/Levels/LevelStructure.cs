using System;
using System.Collections.Generic;
using Levels.Path;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class that will handle spawned objects references in level.
    /// </summary>
    public class LevelStructure : MonoBehaviour
    {
        /// <summary>
        /// Paths available in this level.
        /// </summary>
        [field: SerializeField] 
        public List<RandomPath> Paths { get; private set; }
        
        /// <summary>
        /// Next wave data to know where to spawn the button.
        /// </summary>
        [field: SerializeField]
        public NextWaveData NextWaveData { get; private set; }
    }


    /// <summary>
    /// Class used to handle the next wave button data in each level.
    /// </summary>
    [Serializable]
    public class NextWaveData
    {
        /// <summary>
        /// Position of the next wave button.
        /// </summary>
        [field: SerializeField]
        public Transform NextWaveButtonPlacement { get; private set; }
        
        /// <summary>
        /// Which side is used for this button.
        /// 0 : Top
        /// 1 : Left
        /// 2 : Bottom
        /// 3 : Right
        /// </summary>
        [field: SerializeField]
        public int Side { get; private set; }
    }
}