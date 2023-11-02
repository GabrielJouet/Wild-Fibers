﻿using System;
using Miscellanious.Enums;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class used to handles level data for classic, side and challenge ones.
    /// </summary>
    [CreateAssetMenu(menuName = "Levels/Data")]
    public class LevelData : ScriptableObject
    {
        /// <summary>
        /// Classic level data.
        /// </summary>
        [field: SerializeField]
        public Level Classic { get; private set; }


        /// <summary>
        /// Side level data.
        /// </summary>
        [field: SerializeField]
        public Level Side { get; private set; }


        /// <summary>
        /// Challenge level data.
        /// </summary>
        [field: SerializeField]
        public Level Challenge { get; private set; }
        
        
        /// <summary>
        /// Chat game object needs to be activated for this level to be working.
        /// </summary>
        [field: SerializeField, Header("Level objects")]
        public LevelStructure LevelStructure { get; private set; }



        /// <summary>
        /// Method used to check if the level exists in this data.
        /// </summary>
        /// <param name="levelChecked">The level to check</param>
        /// <returns>Returns true if found, false otherwise</returns>
        public bool LevelExists(Level levelChecked)
        {
            return levelChecked == Classic || levelChecked == Side || levelChecked == Challenge;
        }


        /// <summary>
        /// Method called to get the level based on level type needed.
        /// </summary>
        /// <param name="levelType">The level type needed</param>
        /// <returns>Returns the level wanted</returns>
        public Level GetLevelBasedOnType(LevelType levelType)
        {
            return levelType switch
            {
                LevelType.CLASSIC => Classic,
                LevelType.SIDE => Side,
                LevelType.CHALLENGE => Challenge
            };
        }
    }
}