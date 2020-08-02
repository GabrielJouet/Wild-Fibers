using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string _destinationScene;


    public void LoadScene()
    {
        SceneManager.LoadScene(_destinationScene);
    }


    public void LoadScene(string destinationName)
    {
        SceneManager.LoadScene(destinationName);
    }
}