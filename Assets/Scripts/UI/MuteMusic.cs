using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to mute music or sound.
/// </summary>
public class MuteMusic : MonoBehaviour
{
    /// <summary>
    /// Does the mute button is for sound or music?
    /// </summary>
    [SerializeField]
    private bool _sound;

    /// <summary>
    /// Desactivated sprite for button icon.
    /// </summary>
    [SerializeField]
    private Sprite _desactivatedSprite;

    /// <summary>
    /// Activated sprite for button icon.
    /// </summary>
    [SerializeField]
    private Sprite _activatedSprite;


    private bool _activated = true;

    private SaveController _saveController;



    /// <summary>
    /// Coroutine used for initialization.
    /// </summary>
    private IEnumerator Start()
    {
        _saveController = FindObjectOfType<SaveController>();

        yield return new WaitUntil(() => _saveController.Initialized);

        _activated = _sound ? !_saveController.SaveFile.SoundMuted : !_saveController.SaveFile.MusicMuted;
        transform.GetChild(0).GetComponent<Image>().sprite = _activated ? _activatedSprite : _desactivatedSprite;
    }


    /// <summary>
    /// Method called when clikcing on mute button.
    /// </summary>
    public void Activate()
    {
        if (_sound)
            _saveController.SaveSoundMute(!_activated);
        else
            _saveController.SaveMusicMute(!_activated);

        _activated = !_activated;
        transform.GetChild(0).GetComponent<Image>().sprite = _activated ? _activatedSprite : _desactivatedSprite;
    }
}
