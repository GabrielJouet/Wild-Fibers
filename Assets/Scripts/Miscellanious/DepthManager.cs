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
    protected bool _updates;

    /// <summary>
    /// Targeted transform object.
    /// </summary>
    [SerializeField]
    protected Transform _target;


    /// <summary>
    /// The sprite renderer component that will be update.
    /// </summary>
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Camera used to find current position.
    /// </summary>
    protected Camera _camera;



    /// <summary>
    /// Awake method, used at first to initialize.
    /// </summary>
    protected virtual void Start()
    {
        if (_target == null)
            _target = transform;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;

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
    public virtual void SetSortingOrder()
    {
        _spriteRenderer.sortingOrder = (int)_camera.WorldToScreenPoint(_target.position).y * -1;
    }
}