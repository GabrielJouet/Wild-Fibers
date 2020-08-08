using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField]
    private List<Tower> _availableTowers;


    [Header("Components")]
    [SerializeField]
    private RessourceController _ressourceController;
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;
    [SerializeField]
    private CapsuleCollider2D _collider;


    private Tower _currentTower = null;

    private bool _chooserActive = false;


    /*Construction related*/
    #region
    //Method used to construct a new tower from scratch
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


    //Method used to delay the construction of a new tower
    private IEnumerator DelayConstruct(int index)
    {
        yield return new WaitForSeconds(0.75f);

        _currentTower = Instantiate(_availableTowers[index], transform.position, Quaternion.identity, transform);
        _currentTower.Initialize(this, _ressourceController, _backgroundSelecter);
    }
    #endregion



    /*Reset related*/
    #region
    public void ResetSlot()
    {
        _currentTower = null;
        _collider.enabled = true;
    }

    public void RevertChooserActive() { _chooserActive = !_chooserActive; }

    public void ResetChooserActive() { _chooserActive = false; }
    #endregion



    /*Getters*/
    #region
    public Tower GetCurrentTower() { return _currentTower; }

    public bool GetChooserActive() { return _chooserActive; }
    #endregion
}