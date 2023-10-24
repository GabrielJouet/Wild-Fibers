using UnityEngine;

namespace UI.Parameters
{
    /// <summary>
    /// Class used to handles wipe save.
    /// </summary>
    public class WipeSave : MonoBehaviour
    {
        /// <summary>
        /// Method used to wipe the entire save.
        /// </summary>
        public void WipeEntireSave()
        {
            Controller.Instance.SaveController.ResetData();
        }
    }
}