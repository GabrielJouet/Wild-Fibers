using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Parameters
{
    /// <summary>
    /// Class used to handles wipe save.
    /// </summary>
    public class WipeSave : MonoBehaviour
    {
        /// <summary>
        /// The try text component.
        /// </summary>
        [SerializeField] 
        private TextMeshProUGUI tryText;

        /// <summary>
        /// Button component used to handle the different inputs.
        /// </summary>
        [SerializeField] 
        private Button buttonComponent;
        
        
        /// <summary>
        /// How many time the player tries to restart its save?
        /// </summary>
        private int _numberOfTries;


        /// <summary>
        /// Method called each time the object is activated.
        /// </summary>
        private void OnEnable()
        {
            _numberOfTries = 0;
            tryText.text = GetRetryTextBasedOnTriesNumber();
            buttonComponent.enabled = true;
        }


        /// <summary>
        /// Method used to wipe the entire save.
        /// </summary>
        public void WipeEntireSave()
        {
            _numberOfTries++;

            if (_numberOfTries > 3)
            {
                Controller.Instance.SaveController.ResetData();
                buttonComponent.enabled = false;
            }

            tryText.text = GetRetryTextBasedOnTriesNumber();
        }


        /// <summary>
        /// Method called to return a new text based on number of tries.
        /// </summary>
        /// <returns>The new text returned</returns>
        private string GetRetryTextBasedOnTriesNumber()
        {
            return _numberOfTries switch
            {
                0 => "I know what I am doing",
                1 => "I am sure",
                2 => "Really sure",
                3 => "Really",
                4 => "Done",
                _ => ""
            };
        }
    }
}