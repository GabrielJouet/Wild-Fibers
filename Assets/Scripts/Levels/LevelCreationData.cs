using System;
using System.Collections.Generic;
using Levels.Path;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Data used to build the level from the ground up.
    /// </summary>
    [Serializable]
    public class LevelCreationData
    {
        /// <summary>
        /// Chat game object needs to be activated for this level to be working.
        /// </summary>
        [field: SerializeField]
        public GameObject LevelStructure { get; private set; }
    
        /// <summary>
        /// Light intensity in this level.
        /// </summary>
        [field: SerializeField, Range(0, 1f)]
        public float LightIntensity { get; private set; }
    
        /// <summary>
        /// Color of the light in this level.
        /// </summary>
        [field: SerializeField]
        public Color LightColor { get; private set; }

        /// <summary>
        /// Paths available in this level.
        /// </summary>
        [field: SerializeField] 
        public List<RandomPath> Paths { get; private set; }
    }

    /// <summary>
    /// Data used to store the level creation Data.
    /// </summary>
    [Serializable]
    public class LevelCreation
    {
        /// <summary>
        /// Parent game object for this level.
        /// </summary>
        [field: SerializeField]
        public GameObject ParentObject { get; private set; }
        
        /// <summary>
        /// All content for one level.
        /// </summary>
        [field: SerializeField]
        public List<LevelCreationData> LevelsCreation { get; private set; }
    }
}