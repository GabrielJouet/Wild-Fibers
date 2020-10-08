﻿using System;
using System.Collections;
using UnityEngine;

/*
 * Tower slot is the base of every tower
 */
public class TowerSlot : MonoBehaviour
{
    [Header("Components")]
    //Ressource Controller used to pay towers
    [SerializeField]
    private RessourceController _ressourceController;

    //Information UI
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;

    //Collider component used as a button
    [SerializeField]
    private CapsuleCollider2D _collider;

    //Animator used in tower construction animation
    [SerializeField]
    private Animator _animator;

    //Default sprite for tower slot
    [SerializeField]
    private Sprite _defaultSprite;

    //Sprite Renderer component
    [SerializeField]
    private SpriteRenderer _spriteRenderer;


    //Current tower associated with this slot
    private Tower _currentTower = null;

    //Does the chooser is active?
    private bool _chooserActive = false;


    //Does the tower is currently paused? (by Pause Controller)
    private bool _paused = false;

    //Coroutine Start Time (used if the tower is paused)
    private DateTime _coroutineStartTime;

    //Coroutine time needed to reset
    private float _coroutineTimeNeeded = 0f;

    //Does the attack already started?
    private bool _coroutineStarted = false;


    private Tower _chosenTower;


    /*Construction related*/
    #region
    //Method used to construct a new tower from scratch
    //
    //Parameter => tower, tower chosen with UI
    public void ChooseTower(Tower tower)
    {
        //If we do not have enough money we display a bad choice
        if(_ressourceController.GetGoldCount() < tower.GetPrice())
        {
            //TO DO DISPLAY BAD CHOICE
        }
        //If we have enough money we construct the tower
        else
        {
            _ressourceController.RemoveGold(tower.GetPrice());

            _collider.enabled = false;
            _backgroundSelecter.DisableTowerChooseButton();

            StartCoroutine(DelayConstruct(tower));
        }
    }


    //Method used to delay the construction of a new tower
    //
    //Parameter => tower, the new tower done
    private IEnumerator DelayConstruct(Tower tower)
    {
        _chosenTower = tower;
        _animator.enabled = true;
        _animator.SetTrigger(_chosenTower.GetName());

        _coroutineStartTime = DateTime.Now;
        _coroutineTimeNeeded = 1f;
        yield return new WaitForSeconds(1f);
        ConstructTower();
    }


    private void ConstructTower()
    {
        _animator.enabled = false;
        _spriteRenderer.sprite = _defaultSprite;

        _currentTower = Instantiate(_chosenTower, transform.position, Quaternion.identity, transform);
        _currentTower.Initialize(this, _ressourceController, _backgroundSelecter);
    }
    #endregion



    /*Reset related*/
    #region
    //Method used to reset the tower slot
    public void ResetSlot()
    {
        _currentTower = null;
        _collider.enabled = true;
    }


    //Method used to revert chooser state
    public void RevertChooserActive() { _chooserActive = !_chooserActive; }


    //Method used to reset chooser state
    public void ResetChooserActive() { _chooserActive = false; }


    //Method used by PauseController to pause behavior of tower slot
    public void PauseBehavior()
    {
        if (!_paused)
        {
            StopAllCoroutines();
            _coroutineTimeNeeded -= (float)(DateTime.Now - _coroutineStartTime).TotalSeconds;
        }
        else if (_coroutineTimeNeeded > 0)
            StartCoroutine(UnPauseDelay());

        _animator.enabled = _paused;
        _paused = !_paused;
    }


    //Method used to unpause pause effect
    private IEnumerator UnPauseDelay()
    {
        _coroutineStartTime = DateTime.Now;
        yield return new WaitForSeconds(_coroutineTimeNeeded);
        ConstructTower();
    }
    #endregion



    /*Getters*/
    #region
    public Tower GetCurrentTower() { return _currentTower; }

    public bool GetChooserActive() { return _chooserActive; }
    #endregion
}