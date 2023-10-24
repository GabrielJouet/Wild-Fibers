using UnityEngine;

namespace Towers.Upgrades
{
    [CreateAssetMenu(menuName = "Towers/Specs", fileName = "NewSpec")]
    public class TowerSpec : ScriptableObject
    {
        /// <summary>
        /// Icon of the tower spec.
        /// </summary>
        [field: SerializeField]
        public Sprite Icon { get; private set; }


        /// <summary>
        /// Price of the tower spec.
        /// </summary>
        [field: SerializeField]
        public int Price { get; private set; }


        /// <summary>
        /// Name of the tower spec.
        /// </summary>
        [field: SerializeField]
        public string Name { get; private set; }


        /// <summary>
        /// Description of the tower spec.
        /// </summary>
        [field: SerializeField]
        public string Description { get; private set; }
    }
}
