using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    [SerializeField]
    private bool _sound;

    [SerializeField]
    private Sprite _desactivatedSprite;


    private bool _activated = true;

    private SaveController _saveController;



    private IEnumerator Start()
    {
        _saveController = FindObjectOfType<SaveController>();

        yield return new WaitUntil(() => _saveController.Initialized);

        _activated = _sound ? !_saveController.SaveFile.SoundMuted : !_saveController.SaveFile.MusicMuted;

        if (!_activated)
            Desactivate();
    }


    public void Activate()
    {
        if (_sound)
            _saveController.SaveSoundMute(!_activated);
        else
            _saveController.SaveMusicMute(!_activated);

        _activated = !_activated;

        if (!_activated)
            Desactivate();
    }


    private void Desactivate()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = _desactivatedSprite;
    }
}
