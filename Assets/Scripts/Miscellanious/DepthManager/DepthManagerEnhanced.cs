using UnityEngine;

/// <summary>
/// Class used to control depth in 2D environnement.
/// </summary>
/// <remarks>Needs a SpriteRenderer component</remarks>
[RequireComponent(typeof(SpriteRenderer))]
public class DepthManagerEnhanced : MonoBehaviour
{
    /// <summary>
    /// The transform component that will be used to check position.
    /// </summary>
    [SerializeField]
    private Transform _transform;

    /// <summary>
    /// The sprite renderer component that will be update.
    /// </summary>
    private SpriteRenderer _spriteRenderer;



    /// <summary>
    /// Awake method, used at first to initialize.
    /// </summary>
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// Fixed Update is called 50 times a second.
    /// </summary>
    private void FixedUpdate()
    {
        _spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(_transform.position).y * -1;
    }
}
