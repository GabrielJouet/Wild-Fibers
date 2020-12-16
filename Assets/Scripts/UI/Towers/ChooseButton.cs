using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used in tower construction UI.
/// </summary>
public class ChooseButton : MonoBehaviour
{
    [Header("Components")]

    /// <summary>
    /// Player controller component.
    /// </summary>
    [SerializeField]
    private PlayerController _playerController;


    [Header("Button Elements")]

    /// <summary>
    /// All interactables buttons.
    /// </summary>
    [SerializeField]
    private List<Button> _interactableButtons;

    /// <summary>
    /// Sprites of interactables buttons.
    /// </summary>
    [SerializeField]
    private List<Image> _buttonSprites;

    /// <summary>
    /// List of prices.
    /// </summary>
    [SerializeField]
    private List<Text> _prices;


    /// <summary>
    /// Rect transform component.
    /// </summary>
    [SerializeField]
    private RectTransform _rectTransform;



    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        List<Tower> buffer = _playerController.Towers;

        for(int i = 0; i < _interactableButtons.Count; i ++)
        {
            _buttonSprites[i].sprite = buffer[i].Icon;
            _buttonSprites[i].SetNativeSize();
            _buttonSprites[i].rectTransform.sizeDelta *= 2.4f;
            _prices[i].text = buffer[i].Price.ToString();
        }
    }


    /// <summary>
    /// Method used to activate the buttons.
    /// </summary>
    /// <param name="newPosition">The new position of buttons</param>
    /// <param name="newUsedTowerSlot">Tower slot used</param>
    public void Activate(Vector2 newPosition, TowerSlot newUsedTowerSlot)
    {
        _rectTransform.localPosition = newPosition;

        for (int i = 0; i < _interactableButtons.Count; i++)
        {
            int a = i;
            _interactableButtons[i].onClick.RemoveAllListeners();
            _interactableButtons[i].onClick.AddListener(() => newUsedTowerSlot.ChooseTower(_playerController.Towers[a]));
        }
    }
}