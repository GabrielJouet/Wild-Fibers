using UnityEngine;

/// <summary>
/// Class used to control depth in 2D environnement.
/// </summary>
/// <remarks>Needs a SpriteRenderer component</remarks>
[RequireComponent(typeof(SpriteRenderer))]
public class DepthManager : MonoBehaviour
{
    /// <summary>
    /// Does this depth manager updates itself?
    /// </summary>
    [SerializeField]
    private bool _updates;

    /// <summary>
    /// Targeted transform object.
    /// </summary>
    [SerializeField]
    private Transform _target;


    /// <summary>
    /// The sprite renderer component that will be update.
    /// </summary>
    private SpriteRenderer _spriteRenderer;



    /// <summary>
    /// Awake method, used at first to initialize.
    /// </summary>
    private void Start()
    {
        if (_target == null)
            _target = transform;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetSortingOrder();
    }


    /// <summary>
    /// Fixed Update is called 50 times a second.
    /// </summary>
    private void FixedUpdate()
    {
        if (_updates)
            SetSortingOrder();
    }


    /// <summary>
    /// Method used for re-instantiated objects.
    /// </summary>
    private void SetSortingOrder()
    {
        _spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(_target.position).y * -1;
    }
}