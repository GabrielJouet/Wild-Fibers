using UnityEngine;

namespace UI.Parameters
{
    /// <summary>
    /// Class used as parent for all menu objects.
    /// </summary>
    [RequireComponent(typeof(Transform), typeof(BoxCollider2D))]
    public class Menu : MonoBehaviour
    {
        /// <summary>
        /// Rect transform component shortcut.
        /// </summary>
        [SerializeField] 
        private RectTransform rectTransform;

        /// <summary>
        /// Box collider component shortcut.
        /// </summary>
        [SerializeField] 
        private BoxCollider2D boxCollider2D;
        
        
        /// <summary>
        /// Method used for initialization.
        /// </summary>
        private void Awake()
        {
            boxCollider2D.size = new Vector2(Screen.width + rectTransform.sizeDelta.x, Screen.height + rectTransform.sizeDelta.y);
        }
    }
}