using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class is used to change Unity Scene
 */
public class ChangeScene : MonoBehaviour
{
    //The destination scene 
    [SerializeField]
    private string _destinationScene;



    //This method will use the destination scene variable and Load the wanted scene
    public void LoadScene()
    {
        SceneManager.LoadScene(_destinationScene);
    }


    //This method will do the same as above, but this time needs a parameters
    //
    //Parameter => The destination scene
    public void LoadScene(string destinationName)
    {
        SceneManager.LoadScene(destinationName);
    }
}