using System.Collections.Generic;
using Enemies.Enemy_Types;
using UnityEngine;

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
        [field: SerializeField]
        public List<Enemy> Enemies { get; private set; }
    }
}