using System;
using UnityEngine;

namespace Levels.Waves
{
    /// <summary>
    /// Class used to store enemy information.
    /// </summary>
    [Serializable]
    public class EnemyPattern
    {
        /// <summary>
        /// Number of enemies in this group.
        /// </summary>
        [field: SerializeField, Min(1), Space(10)]
        public int NumberOfEnemies { get; private set; }


        /// <summary>
        /// Time between each enemy.
        /// </summary>
        [field: SerializeField, Min(0.05f)]
        public float TimeBetweenEnemies { get; private set; }
    }
}