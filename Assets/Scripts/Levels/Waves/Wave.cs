using System.Collections.Generic;
using UnityEngine;

namespace Levels.Waves
{
    /// <summary>
    /// Class used to store wave data.
    /// </summary>
    [CreateAssetMenu(menuName = "Levels/Wave")]
    public class Wave : ScriptableObject
    {
        /// <summary>
        /// All groups in current wave.
        /// </summary>
        [field: SerializeField]
        public List<EnemyGroup> EnemyGroups { get; private set; }

        /// <summary>
        /// Time between wave.
        /// </summary>
        [field: SerializeField, Min(0.1f), Space(15)]
        public float TimeBeforeNextWave { get; private set; }


        /// <summary>
        /// Gold bonus by calling early wave.
        /// </summary>
        [field: SerializeField]
        public int GoldBonus { get; private set; }
    }
}