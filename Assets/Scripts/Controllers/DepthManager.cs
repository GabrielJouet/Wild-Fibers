using UnityEngine;

/*
 * This class is used to render perspective in a 2D game
 */
public class DepthManager : MonoBehaviour
{
    //Sprite renderer of the game object
    [Header("Component")]
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    //Main Camera of the Scene
    private Camera _mainCamera;



    //Start Method
    //Called when the game object is initialized
    private void Start()
    {
        _mainCamera = Camera.main;
    }


    //Fixed Update Method
    //Called every 50 times per seconds
    private void FixedUpdate()
    {
        //We change the sorting order of the sprite based on game object position
        //The higher the farer
        _spriteRenderer.sortingOrder = (int)_mainCamera.WorldToScreenPoint(transform.position).y * -1;
    }
}