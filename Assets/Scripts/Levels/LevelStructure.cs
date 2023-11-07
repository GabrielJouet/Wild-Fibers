using System;
using System.Collections.Generic;
using Levels.Path;
using Miscellanious.Enums;
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
        
        /// <summary>
        /// Object used in classic level.
        /// </summary>
        [field: SerializeField, Header("Level Objects")]
        public GameObject ClassicObject { get; private set; }
        
        /// <summary>
        /// Object used in side level.
        /// </summary>
        [field: SerializeField]
        public GameObject SideObject { get; private set; }
        
        /// <summary>
        /// Object used in challenge level.
        /// </summary>
        [field: SerializeField]
        public GameObject ChallengeObject { get; private set; }


        /// <summary>
        /// Method called to activate level objects based on level type given.
        /// </summary>
        public void ActivateLevelObjectsBasedOnLevelType(LevelType levelType)
        {
            switch (levelType)
            {
                case LevelType.CLASSIC:
                    ClassicObject.SetActive(true);
                    SideObject.SetActive(false);
                    ChallengeObject.SetActive(false);
                    break;
                    
                case LevelType.SIDE:
                    ClassicObject.SetActive(false);
                    SideObject.SetActive(true);
                    ChallengeObject.SetActive(false);
                    break;
                    
                case LevelType.CHALLENGE:
                    ClassicObject.SetActive(false);
                    SideObject.SetActive(false);
                    ChallengeObject.SetActive(true);
                    break;
            };
        }
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
        public Vector2 Position { get; private set; }
        
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