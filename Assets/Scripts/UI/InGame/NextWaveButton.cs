using Levels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    /// <summary>
    /// Class used to handle new waves.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class NextWaveButton : MonoBehaviour
    {
        /// <summary>
        /// Level controller of the level.
        /// </summary>
        [SerializeField]
        private LevelController levelController;

        /// <summary>
        /// Animator component.
        /// </summary>
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Last sprite of the countdown animation.
        /// </summary>
        [SerializeField]
        private Sprite lastFrameCountDownSprite;


        /// <summary>
        /// Time before next wave.
        /// </summary>
        private float _timeLeft = 0;

        /// <summary>
        /// Does the time is decreasing?
        /// </summary>
        private bool _timeDecrease = false;



        /// <summary>
        /// First method called after Awake.
        /// </summary>
        private void Start()
        {
            animator.GetComponent<Image>().sprite = lastFrameCountDownSprite;
            animator.enabled = false;
        }


        /// <summary>
        /// Fixed Update called 50 times a second.
        /// </summary>
        private void FixedUpdate()
        {
            if(_timeDecrease)
            {
                if (_timeLeft - Time.fixedDeltaTime < 0)
                    PressNewWaveButton();
                else
                    _timeLeft -= Time.fixedDeltaTime;
            }
        }


        /// <summary>
        /// Method called when we want to activate the button.
        /// </summary>
        /// <param name="newTimeLeft">New time left</param>
        public void ActivateNewWaveButton(float newTimeLeft)
        {
            _timeLeft = newTimeLeft;
            _timeDecrease = true;

            gameObject.SetActive(true);

            animator.enabled = true;
            animator.SetFloat("animSpeed", animator.runtimeAnimatorController.animationClips[0].length / newTimeLeft);
        }


        /// <summary>
        /// Method used when the time is either depleted or the button is pressed.
        /// </summary>
        public void PressNewWaveButton()
        {
            levelController.StartWaveViaButton(_timeLeft);
            _timeDecrease = false;

            animator.enabled = false;
            gameObject.SetActive(false);
        }
    }
}