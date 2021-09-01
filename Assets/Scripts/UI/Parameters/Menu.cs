using UnityEngine;

/// <summary>
/// Class used as parent for all menu objects.
/// </summary>
[RequireComponent(typeof(Transform), typeof(BoxCollider2D))]
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Method used for initialization.
    /// </summary>
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        GetComponent<BoxCollider2D>().size = new Vector2(Screen.width + rectTransform.sizeDelta.x, Screen.height + rectTransform.sizeDelta.y);
    }
}