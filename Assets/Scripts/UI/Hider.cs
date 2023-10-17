using Levels;
using UnityEngine;

/// <summary>
/// Class used to improve UI interactivity.
/// </summary>
public class Hider : MonoBehaviour
{
    /// <summary>
    /// Pause controller component used to pause, unpause.
    /// </summary>
    [SerializeField]
    private DisplayController _displayController;

    /// <summary>
    /// Level controller component used to know if the game is finished.
    /// </summary>
    [SerializeField]
    private LevelController _levelController;



    /// <summary>
    /// Method used as initialization.
    /// </summary>
    private void Awake()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(Screen.width, Screen.height);
    }


    /// <summary>
    /// Update method, called each frame.
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);

            if (hit.collider != null && hit.collider.GetComponent<Hider>() && (_levelController == null || !_levelController.Ended))
                _displayController.ResetDisplay();
        }
    }
}
