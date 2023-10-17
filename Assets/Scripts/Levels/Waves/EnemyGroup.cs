using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels.Waves
{
    /// <summary>
    /// Class used to store enemy group data.
    /// </summary>
    [Serializable]
    public class EnemyGroup
    {
        /// <summary>
        /// Enemy groups in this pattern.
        /// </summary>
        [field: SerializeField, Header("Parameters")]
        public List<EnemyPattern> Patterns { get; private set; }

        /// <summary>
        /// Enemy used in this group.
        /// </summary>
        [field: SerializeField, Space(15)]
        public GameObject Enemy { get; private set; }


        /// <summary>
        /// Time between each pattern.
        /// </summary>
        [field: SerializeField, Min(0.1f)]
        public float TimeBetweenPattern { get; private set; }


        /// <summary>
        /// Path used by the enemy.
        /// </summary>
        [field: SerializeField]
        public int PathIndex { get; private set; }
    }
}