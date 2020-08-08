using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _newWaveButton;

    [SerializeField]
    private Transform _newWaveFiller;

    [SerializeField]
    private LevelController _levelController;


    private float _timeLeft = 0;

    private bool _timeDecrease = false;


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


    public void ActivateNewWaveButton(float newTimeLeft)
    {
        _timeLeft = newTimeLeft;
        _timeDecrease = true;

        _newWaveButton.SetActive(true);
    }


    public void PressNewWaveButton()
    {
        _levelController.StartWaveViaButton(_timeLeft);
        _timeDecrease = false;

        _newWaveButton.SetActive(false);
    }
}