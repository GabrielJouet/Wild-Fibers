using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle music and sound levels.
/// </summary>
[RequireComponent(typeof(Slider))]
public class MusicSlider : MonoBehaviour
{
    /// <summary>
    /// Does the slider is used for sound or music?
    /// </summary>
    [SerializeField]
    private bool _sound;


    /// <summary>
    /// Save controller component.
    /// </summary>
    private SaveController _saveController;

    /// <summary>
    /// Slider component.
    /// </summary>
    private Slider _slider;



    /// <summary>
    /// Method called when the object is activated.
    /// </summary>
    private void OnEnable()
    {
        _saveController = Controller.Instance.SaveControl;
        _slider = GetComponent<Slider>();
        _slider.value = _sound ? _saveController.SaveFile.Sound : _saveController.SaveFile.Music;
    }


    /// <summary>
    /// Method called each time a slider has a new value.
    /// </summary>
    public void ChangeSliderValue()
    {
        if (_saveController != null)
        {
            if (_sound)
                _saveController.SaveSoundLevel(_slider.value);
            else
                _saveController.SaveMusicLevel(_slider.value);
        }
    }
}