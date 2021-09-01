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
    private Text _price;

    /// <summary>
    /// Image component that will handle icon of the skill displayed.
    /// </summary>
    [SerializeField]
    private Image _icon;

    /// <summary>
    /// Text component that will handle the description.
    /// </summary>
    [SerializeField]
    private Text _description;


    /// <summary>
    /// Related augmentation.
    /// </summary>
    private Augmentation _augmentation;


    /// <summary>
    /// Initialization method.
    /// </summary>
    /// <param name="relatedAugmentation">The related augmentation</param>
    /// <param name="newState">The state of the augmentation (Bought, Locked or Unlocked)</param>
    public void Initialize(Augmentation relatedAugmentation, AugmentationState newState)
    {
        _price.text = relatedAugmentation.Price.ToString();
        _icon.sprite = relatedAugmentation.Icon;
        _description.text = relatedAugmentation.Description;

        _augmentation = relatedAugmentation;

        _description.transform.parent.gameObject.SetActive(false);

        switch (newState)
        {
            case AugmentationState.LOCKED:
                Desactivate();
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
    /// Method called when the skill is locked.
    /// </summary>
    public void Desactivate()
    {
        GetComponent<Button>().enabled = false;
        _icon.color = new Color(0.25f, 0.25f, 0.25f, 0.4f);
    }


    /// <summary>
    /// Method called when the mouse enter the skill (hovers it).
    /// </summary>
    /// <remarks>Interface needed because this is a UI object</remarks>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _description.transform.parent.gameObject.SetActive(true);
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
        if (Controller.Instance.SaveControl.SaveFile.CurrentSquad.CurrencyAvailable >= _augmentation.Price)
        {
            SetAsBought();
            transform.parent.GetComponent<SkillUpgrades>().PurchaseAugmentation(this);
        }
    }
}