using UnityEngine;

/// <summary>
/// Class used to control depth in 2D environnement.
/// </summary>
public class DepthManagerEnhanced : MonoBehaviour
{
    /// <summary>
    /// The sprite renderer component that will be update.
    /// </summary>
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The transform component that will be used to check position.
    /// </summary>
    [SerializeField]
    private Transform _transform;


    /// <summary>
    /// The main camera of the scene.
    /// </summary>
    private Camera _mainCamera;



    /// <summary>
    /// Awake method, used at first to initialize.
    /// </summary>
    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    /// <summary>
    /// Fixed Update is called 50 times a second.
    /// </summary>
    private void FixedUpdate()
    {
        _spriteRenderer.sortingOrder = (int)_mainCamera.WorldToScreenPoint(_transform.position).y * -1;
    }
}
