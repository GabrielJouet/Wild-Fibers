using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Components")]
    [SerializeField]
    private Level _levelParameters;
    [SerializeField]
    private LevelSelection _levelSelection;


    [Header("Selection")]
    [SerializeField]
    private Image _hoverDisplayer;

    [SerializeField]
    private Sprite _unlockedHover;

    [SerializeField]
    private Sprite _finishedHover;

    [SerializeField]
    private Sprite _asideHover;

    [SerializeField]
    private Sprite _challengedHover;


    public bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(true);
        _hoverDisplayer.sprite = _unlockedHover;
        _hoverDisplayer.SetNativeSize();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverDisplayer.gameObject.SetActive(false);
    }



    public void Activate()
    {
        _levelSelection.ActivateLevelSelectionMenu(_levelParameters);
    }
}