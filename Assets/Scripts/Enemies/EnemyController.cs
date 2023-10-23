using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    /// <summary>
    /// Class used to handle enemies.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// All enemies in a list format.
        /// </summary>
        [field: SerializeField, FormerlySerializedAs("_allEnemies")]
        public List<Enemy> Enemies { get; private set; }
    }
}