﻿using System.Collections;
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

    [SerializeField]
    private LevelController _levelController;

    //Information UI
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;

    //Collider component used as a button
    [SerializeField]
    private CapsuleCollider2D _collider;

    //Animator used in tower construction animation
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Animator _shadowAnimator;

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
        _shadowAnimator.SetTrigger(_chosenTower.GetName());
        _animator.SetTrigger(_chosenTower.GetName());

        yield return new WaitForSeconds(0.92f);
        _shadowAnimator.SetTrigger("Base");
        _animator.SetTrigger("Base");

        TowerPool currentPool = _levelController.RecoverTowerPool(_chosenTower);
        _currentTower = currentPool.GetOneTower();
        _currentTower.Initialize(this, _ressourceController, _backgroundSelecter, _levelController.RecoverProjectilePool(_chosenTower.GetProjectileUsed().GetComponent<Projectile>()), currentPool);
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
    #endregion



    /*Getters*/
    #region
    public Tower GetCurrentTower() { return _currentTower; }

    public bool GetChooserActive() { return _chooserActive; }
    #endregion
}