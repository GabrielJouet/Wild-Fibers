using UnityEngine;

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
    private LevelController _levelController;

    /// <summary>
    /// Animator component.
    /// </summary>
    [SerializeField]
    private Animator _animator;


    /// <summary>
    /// Time before next wave.
    /// </summary>
    private float _timeLeft = 0;

    /// <summary>
    /// Does the time is decreasing?
    /// </summary>
    private bool _timeDecrease = false;


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

        _animator.enabled = true;
        _animator.SetFloat("animSpeed", _animator.runtimeAnimatorController.animationClips[0].length / newTimeLeft);
    }


    /// <summary>
    /// Method used when the time is either depleted or the button is pressed.
    /// </summary>
    public void PressNewWaveButton()
    {
        _levelController.StartWaveViaButton(_timeLeft);
        _timeDecrease = false;

        _animator.enabled = false;
        gameObject.SetActive(false);
    }
}