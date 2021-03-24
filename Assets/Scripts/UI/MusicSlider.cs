using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField]
    private bool _sound;

    private SaveController _saveController;

    private Slider _slider;



    private IEnumerator Start()
    {
        _saveController = FindObjectOfType<SaveController>();
        _slider = GetComponent<Slider>();

        yield return new WaitUntil(() => _saveController.Initialized);

        _slider.value = _sound? _saveController.SaveFile.Sound : _saveController.SaveFile.Music;
    }


    public void ChangeSliderValue()
    {
        if (_sound)
            _saveController.SaveSoundLevel(_slider.value);
        else
            _saveController.SaveMusicLevel(_slider.value);
    }
}