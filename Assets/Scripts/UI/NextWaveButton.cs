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


    //Time left before next wave
    private float _timeLeft = 0;

    //Does the time actually decrease?
    private bool _timeDecrease = false;



    //Fixed Update, called 50 times a second
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
}