using UnityEngine;

public class RatioController : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;


    [Header("Screen resolution")]
    [Min(1)]
    [SerializeField]
    private int _widht;

    [Min(1)]
    [SerializeField]
    private int _height;


    private void Awake()
    {
        _mainCamera.aspect = (float)_widht / (float)_height;
    }
}
