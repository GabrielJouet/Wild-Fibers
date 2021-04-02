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


    public void Initialize(Augmentation relatedAugmentation)
    {
        _price.text = relatedAugmentation.Price.ToString();
        _icon.sprite = relatedAugmentation.Icon;
        _description.text = relatedAugmentation.Description;

        _description.transform.parent.gameObject.SetActive(false);
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

    }
}