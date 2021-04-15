using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Text _price;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Text _description;

    private Augmentation _augmentation;


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


    public void Activate()
    {
        GetComponent<Button>().enabled = true;
        _icon.color = new Color(1, 1, 1, 1);
    }


    public void SetAsBought()
    {
        GetComponent<Button>().enabled = false;
    }


    public void Desactivate()
    {
        GetComponent<Button>().enabled = false;
        _icon.color = new Color(0.25f, 0.25f, 0.25f, 0.4f);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _description.transform.parent.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _description.transform.parent.gameObject.SetActive(false);
    }


    public void Purchase()
    {
        SquadProgression current = Controller.Instance.SaveControl.SaveFile.SquadsProgression[0];

        if (current.CurrencyAvailable >= _augmentation.Price)
        {
            SetAsBought();
            transform.parent.GetComponent<SkillUpgrades>().PurchaseAugmentation(this);
        }
    }
}