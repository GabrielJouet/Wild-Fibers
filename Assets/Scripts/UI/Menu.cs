using UnityEngine;

public class Menu : MonoBehaviour
{
    //Box collider of menu
    [SerializeField]
    private BoxCollider2D _boxCollider;

    //Transform of menu
    [SerializeField]
    private RectTransform _transform;



    //We initialize the component
    void Start()
    {
        _boxCollider.size = new Vector2(Screen.width + _transform.sizeDelta.x, Screen.height + _transform.sizeDelta.y);
    }
}
