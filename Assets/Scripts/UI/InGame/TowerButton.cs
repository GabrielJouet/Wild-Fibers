using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.InGame
{
    /// <summary>
    /// Class used to handle button sell and buy.
    /// </summary>
    public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Elements")]

        /// <summary>
        /// Button component.
        /// </summary>
        [SerializeField]
        private Button buttonComponent;

        /// <summary>
        /// Image component.
        /// </summary>
        [SerializeField]
        private Image buttonImage;

        /// <summary>
        /// Background image component.
        /// </summary>
        [SerializeField]
        private Image backgroundImage;


        /// <summary>
        /// Text component of the price.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI priceText;

        /// <summary>
        /// Actual price displayed.
        /// </summary>
        private int _price;


        /// <summary>
        /// Locked sprite.
        /// </summary>
        [SerializeField]
        private Sprite lockedSprite;

        /// <summary>
        /// Description of the tower.
        /// </summary>
        private TextMeshProUGUI _descriptionText;

        /// <summary>
        /// Description game object of the tower.
        /// </summary>
        private GameObject _descriptionObject;

        /// <summary>
        /// Does the button is locked?
        /// </summary>
        private bool _locked;

        /// <summary>
        /// Does the button is locked?
        /// </summary>
        private string _description;


        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="newSprite">The sprite to display</param>
        /// <param name="newPrice">The new price of the tower</param>
        /// <param name="newDescription">The new description of this button</param>
        /// <param name="descriptionObject">The game object used for description display</param>
        public void Initialize(Sprite newSprite, int newPrice, string newDescription, GameObject descriptionObject)
        {
            _description = newDescription;
            buttonImage.sprite = newSprite;
            buttonImage.SetNativeSize();
            buttonImage.rectTransform.sizeDelta *= 2.4f;

            _price = newPrice;
            priceText.text = _price.ToString();

            SetTargetDescription(descriptionObject);
        }


        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <remarks>Only used when reselling a tower</remarks>
        /// <param name="newPrice">The new price of this button</param>
        /// <param name="descriptionObject">The game object used for description display</param>
        public void Initialize(int newPrice, GameObject descriptionObject)
        {
            _description = "Resell this tower";
            priceText.text = newPrice.ToString();
            SetTargetDescription(descriptionObject);
        }



        /// <summary>
        /// Update method is called each frame.
        /// </summary>
        private void Update()
        {
            if(RessourceController.Instance && !_locked)
                UpdateState(_price <= RessourceController.Instance.GoldCount);
        }


        /// <summary>
        /// Method called when we want to change the behavior of a button.
        /// </summary>
        /// <param name="buttonCallBack">The call back used when pressing the button</param>
        public void ChangeBehavior(UnityAction buttonCallBack)
        {
            buttonComponent.onClick.RemoveAllListeners();
            buttonComponent.onClick.AddListener(buttonCallBack);
            buttonComponent.onClick.AddListener(() => _descriptionText?.rectTransform.parent.gameObject.SetActive(false));
        }


        /// <summary>
        /// Method called when we want to change the target description object.
        /// </summary>
        /// <param name="descriptionObject">The description game object related</param>
        private void SetTargetDescription(GameObject descriptionObject)
        {
            _descriptionObject = descriptionObject;
            _descriptionText = descriptionObject.GetComponentInChildren<TextMeshProUGUI>();
        }


        /// <summary>
        /// Method called to lock a specific button.
        /// </summary>
        public void Lock()
        {
            _locked = true;
            UpdateState(false);
            buttonImage.sprite = lockedSprite;
            buttonImage.SetNativeSize();
            buttonImage.rectTransform.sizeDelta *= 2.4f;
            priceText.enabled = false;
        }


        /// <summary>
        /// Method called to update a button state.
        /// </summary>
        /// <param name="activated">Does the button is activated or not?</param>
        public void UpdateState(bool activated)
        {
            buttonComponent.enabled = activated;

            Color newColor = activated ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1);
            buttonImage.color = newColor;
            backgroundImage.color = newColor;
        }


        /// <summary>
        /// Method used to highlight the level button if not locked.
        /// </summary>
        /// <param name="eventData">The pointer event</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_locked && _descriptionObject != null)
            {
                _descriptionObject.SetActive(true);
                _descriptionText.text = _description;
            }
        }


        /// <summary>
        /// Method used to desactivate the highlight for the level button if not locked.
        /// </summary>
        /// <param name="eventData">The pointer event</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_locked && _descriptionObject != null)
                _descriptionObject.SetActive(false);
        }
    }
}