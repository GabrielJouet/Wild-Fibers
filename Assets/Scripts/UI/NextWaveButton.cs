using UnityEngine;

/*
 * Next wave button is used to call a next wave before time
 */
public class NextWaveButton : MonoBehaviour
{
    //Actual button object
    [SerializeField]
    private GameObject _newWaveButton;

    //Filler is used to show time left amount
    [SerializeField]
    private Transform _newWaveFiller;

    //Level controller of this level
    [SerializeField]
    private LevelController _levelController;

    [SerializeField]
    private Animator _animator;


    //Time left before next wave
    private float _timeLeft = 0;

    //Does the time actually decrease?
    private bool _timeDecrease = false;

    //Does the tower is currently paused? (by Pause Controller)
    protected bool _paused = false;



    //Fixed Update, called 50 times a second
    private void FixedUpdate()
    {
        if(_timeDecrease && !_paused)
        {
            if (_timeLeft - Time.fixedDeltaTime < 0)
                PressNewWaveButton();
            else
                _timeLeft -= Time.fixedDeltaTime;
        }
    }


    //Method used to activate the new wave button
    //
    //Parameter => newTimeLeft, the amount of time before the wave starts
    public void ActivateNewWaveButton(float newTimeLeft)
    {
        _timeLeft = newTimeLeft;
        _timeDecrease = true;

        _newWaveButton.SetActive(true);
    }


    //Method used when we press the next wave button
    public void PressNewWaveButton()
    {
        _levelController.StartWaveViaButton(_timeLeft);
        _timeDecrease = false;

        _newWaveButton.SetActive(false);
    }


    //Method used to pause tower behavior when pause button is hit
    public void PauseBehavior()
    {
        _animator.enabled = _paused;

        _paused = !_paused;
    }
}