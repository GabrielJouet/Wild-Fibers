using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used when changing scene.
/// </summary>
public class ChangeScene : MonoBehaviour
{    
    /// <summary>
    /// Pause controller component, used to load a scene.
    /// </summary>
    [SerializeField]
    private PauseController _pauseController;


    /// <summary>
    /// Load an Unity scene.
    /// </summary>
    /// <param name="destinationName">The destination scene</param>
    public void LoadScene(string destinationName)
    {
        if(_pauseController != null)
            _pauseController.PauseGame(false);

        SceneManager.LoadScene(destinationName);
    }
}