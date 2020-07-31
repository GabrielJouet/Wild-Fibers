using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string _mapSceneName;


    public void BackToMap()
    {
        SceneManager.LoadScene(_mapSceneName);
    }
}