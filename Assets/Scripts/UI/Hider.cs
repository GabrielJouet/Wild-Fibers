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
        /// Method called when the button is clicked.
        /// </summary>
        public void ResetDisplay()
        {
            displayController.ResetDisplay();
        }
    }
}