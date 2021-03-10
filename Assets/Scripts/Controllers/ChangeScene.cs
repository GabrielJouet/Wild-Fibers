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
        SceneManager.LoadScene(destinationName);

        PoolController buffer = FindObjectOfType<PoolController>();

        if (buffer != null && FindObjectsOfType<LevelController>().Length > 0)
            buffer.DesactivateEntities();
    }
}