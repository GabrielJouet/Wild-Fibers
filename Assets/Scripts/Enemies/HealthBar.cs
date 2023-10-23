using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    /// <summary>
    /// Class used to display and update a health bar.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        /// <summary>
        /// Transform component of the health bar.
        /// </summary>
        [FormerlySerializedAs("_healthBar")] [SerializeField]
        private Transform healthBar;

        /// <summary>
        /// Sprite renderer component of the health bar.
        /// </summary>
        [FormerlySerializedAs("_spriteRenderer")] [SerializeField]
        private SpriteRenderer spriteRenderer;



        /// <summary>
        /// Method used to change the health bar size.
        /// </summary>
        /// <param name="percentage">The new percentage scale to apply</param>
        public void ChangeSize(float percentage)
        {
            healthBar.localScale = new Vector3(percentage, 1, 1);
            healthBar.localPosition = new Vector3(-(1 - healthBar.localScale.x) * spriteRenderer.size.x / 2 + 0.005f, 0.005f, 0);
        }


        /// <summary>
        /// Reset health bar size.
        /// </summary>
        public void ResetSize()
        {
            healthBar.localScale = Vector3.one;
            healthBar.localPosition = new Vector3(0.005f, 0.005f, 0);
        }
    }
}