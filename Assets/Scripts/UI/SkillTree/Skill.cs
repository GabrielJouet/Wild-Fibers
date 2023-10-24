using Miscellanious.Enums;
using TMPro;
using Towers.Upgrades;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class used to handle interactivity in skill tree.
/// </summary>
public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Actual price of this skill.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _price;

    /// <summary>
    /// Image component that will handle icon of the skill displayed.
    /// </summary>
    [SerializeField]
    private Image _icon;

    /// <summary>
    /// Position of the description relative to this skill.
    /// </summary>
    [SerializeField]
    private RectTransform _descriptionPosition;


    /// <summary>
    /// Text component that will handle the description.
    /// </summary>
    private TextMeshProUGUI _description;

    /// <summary>
    /// Game Object component that will handle the description parent.
    /// </summary>
    private GameObject _descriptionGameObject;

    /// <summary>
    /// Related augmentation.
    /// </summary>
    private Augmentation _augmentation;



    /// <summary>
    /// Initialization method.
    /// </summary>
    /// <param name="relatedAugmentation">The related augmentation</param>
    /// <param name="newState">The state of the augmentation (Bought, Locked or Unlocked)</param>
    public void Initialize(Augmentation relatedAugmentation, AugmentationState newState, TextMeshProUGUI description)
    {
        _description = description;
        _descriptionGameObject = description.transform.parent.gameObject;

        _price.text = relatedAugmentation.Price.ToString();
        _icon.sprite = relatedAugmentation.Icon;

        _augmentation = relatedAugmentation;

        switch (newState)
        {
            case AugmentationState.LOCKED:
                GetComponent<Button>().enabled = false;
                _icon.color = new Color(0.25f, 0.25f, 0.25f, 0.4f);
                break;

            case AugmentationState.AVAILABLE:
                Activate();
                break;

            case AugmentationState.BOUGHT:
                SetAsBought();
                break;
        }
    }


    /// <summary>
    /// Method called when the skill is activated but not bought.
    /// </summary>
    public void Activate()
    {
        GetComponent<Button>().enabled = true;
        _icon.color = new Color(1, 1, 1, 1);
    }


    /// <summary>
    /// Method called when the skill is bought.
    /// </summary>
    public void SetAsBought()
    {
        GetComponent<Button>().enabled = false;
    }


    /// <summary>
    /// Method called when the mouse enter the skill (hovers it).
    /// </summary>
    /// <remarks>Interface needed because this is a UI object</remarks>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _description.text = _augmentation.Description;
        _descriptionGameObject.GetComponent<RectTransform>().position = _descriptionPosition.position;
        _descriptionGameObject.SetActive(true);
    }


    /// <summary>
    /// Method called when the mouse exits the skill (hovers it).
    /// </summary>
    /// <remarks>Interface needed because this is a UI object</remarks>
    public void OnPointerExit(PointerEventData eventData)
    {
        _description.transform.parent.gameObject.SetActive(false);
    }


    /// <summary>
    /// Method called when the skill is purchased.
    /// </summary>
    public void Purchase()
    {
        if (Controller.Instance.SaveController.SaveFile.CurrentSquad.CurrencyAvailable >= _augmentation.Price)
        {
            SetAsBought();
            transform.parent.GetComponent<SkillUpgrades>().PurchaseAugmentation(this);
        }
    }
}