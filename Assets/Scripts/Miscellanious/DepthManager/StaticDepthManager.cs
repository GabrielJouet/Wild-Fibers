using UnityEngine;

/// <summary>
/// Class used to control depth in 2D environnement but for static objects (instead of doing this in a update, we do this in awake).
/// </summary>
/// <remarks>Needs a SpriteRenderer component</remarks>
[RequireComponent(typeof(SpriteRenderer))]
public class StaticDepthManager : MonoBehaviour
{
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
    /// Start method, called after Awake.
    /// </summary>
    private void Start()
    {
        _spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(transform.position).y * -1;
    }


    /// <summary>
    /// Method used for re-instantiated objects.
    /// </summary>
    public void ResetSortingOrder()
    {
        _spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(transform.position).y * -1;
    }
}