using System;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class used to improve UI interactivity.
    /// </summary>
    public class Hider : MonoBehaviour
    {
        /// <summary>
        /// Pause controller component used to pause, unpause.
        /// </summary>
        [SerializeField]
        private DisplayController displayController;

        /// <summary>
        /// Collider that is used to prevent inputs.
        /// </summary>
        [SerializeField] 
        private BoxCollider2D collider2D;


        
        /// <summary>
        /// Method called each time the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            collider2D.size = new Vector2(Screen.width, Screen.height);
        }


        /// <summary>
        /// Method called when the button is clicked.
        /// </summary>
        public void ResetDisplay()
        {
            displayController.ResetDisplay();
        }
    }
}