using UnityEngine;

namespace Towers.Upgrades
{
    /// <summary>
    /// Class used to handle skill augmentation in skill tree.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAugmentation", menuName = "Towers/Augmentation")]
    public class Augmentation : ScriptableObject
    {
        /// <summary>
        /// Icon displayed.
        /// </summary>
        [field: SerializeField]
        public Sprite Icon { get; private set; }


        /// <summary>
        /// Price in skill points.
        /// </summary>
        [field: SerializeField]
        public int Price { get; private set; }


        /// <summary>
        /// Short description.
        /// </summary>
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
    }
}