using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used when changing scene.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Load an Unity scene.
    /// </summary>
    /// <param name="destinationName">The destination scene</param>
    public void LoadScene(string destinationName)
    {
        //If we are in a playable level
        if (FindObjectsOfType<LevelController>().Length > 0)
        {
            //TO CHANGE, NEED TO REMOVE ENEMIES AND ENTITIES
        }

        SceneManager.LoadScene(destinationName);
    }
}