using Levels;
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
        /// Level controller component used to know if the game is finished.
        /// </summary>
        [SerializeField]
        private LevelController levelController;

        /// <summary>
        /// Box collider component used to resize to the size of the screen
        /// </summary>
        [SerializeField] 
        private BoxCollider2D boxCollider;


        /// <summary>
        /// Actual camera used.
        /// </summary>
        private Camera _camera;



        /// <summary>
        /// Method used as initialization.
        /// </summary>
        private void Start()
        {
            _camera = Camera.current;
            boxCollider.size = new Vector2(Screen.width, Screen.height);
        }


        /// <summary>
        /// Update method, called each frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);

                if (hit.collider && hit.collider.GetComponent<Hider>() && (levelController || !levelController.Ended))
                    displayController.ResetDisplay();
            }
        }
    }
}
