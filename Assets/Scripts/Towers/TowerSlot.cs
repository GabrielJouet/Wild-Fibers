using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Tower Information")]
    [SerializeField]
    private List<Tower> _availableTowers;


    [Header("Component")]
    [SerializeField]
    private RessourceController _ressourceController;
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;
    [SerializeField]
    private CapsuleCollider2D _collider;

    private Tower _currentTower = null;

    private bool _chooserActive = false;


    public void ChooseTower(int index)
    {
        if(_ressourceController.GetGoldCount() < _availableTowers[index].GetPrice())
        {
            //TO DO DISPLAY BAD CHOICE
        }
        else
        {
            _ressourceController.RemoveGold(_availableTowers[index].GetPrice());

            _collider.enabled = false;
            _backgroundSelecter.DisableTowerChooseButton();

            StartCoroutine(DelayConstruct(index));
        }
    }


    private IEnumerator DelayConstruct(int index)
    {
        yield return new WaitForSeconds(0.75f);

        _currentTower = Instantiate(_availableTowers[index], transform.position, Quaternion.identity);
        _currentTower.Initialize(this, _ressourceController, _backgroundSelecter);
    }


    public void ResetSlot()
    {
        _currentTower = null;
        _collider.enabled = true;
    }


    public Tower GetCurrentTower() { return _currentTower; }

    public bool GetChooserActive() { return _chooserActive; }

    public void RevertChooserActive() { _chooserActive = !_chooserActive; }

    public void ResetChooserActive() { _chooserActive = false; }
}