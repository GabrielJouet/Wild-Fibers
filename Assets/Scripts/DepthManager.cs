using UnityEngine;

public class DepthManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Camera _mainCamera;



    private void Start()
    {
        _mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        _spriteRenderer.sortingOrder = (int)_mainCamera.WorldToScreenPoint(transform.position).y * -1;
    }
}