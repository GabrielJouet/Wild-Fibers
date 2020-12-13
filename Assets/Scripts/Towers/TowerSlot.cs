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

    


    //Current tower associated with this slot
    public Tower Tower { get; private set; } = null;

    //Does the chooser is active?
    public bool ChooserActive { get; private set; } = false;


    private Tower _chosenTower;


    /*Construction related*/
    #region
    //Method used to construct a new tower from scratch
    //
    //Parameter => tower, tower chosen with UI
    public void ChooseTower(Tower tower)
    {
        _ressourceController.RemoveGold(tower.Price);

        _collider.enabled = false;
        _backgroundSelecter.DisableTowerChooseButton();

        StartCoroutine(DelayConstruct(tower));
    }


    //Method used to delay the construction of a new tower
    //
    //Parameter => tower, the new tower done
    private IEnumerator DelayConstruct(Tower tower)
    {
        _chosenTower = tower;
        _shadowAnimator.SetTrigger(_chosenTower.Name);
        _animator.SetTrigger(_chosenTower.Name);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _shadowAnimator.SetTrigger("Base");
        yield return new WaitForEndOfFrame();
        _animator.SetTrigger("Base");

        TowerPool currentPool = _levelController.RecoverTowerPool(_chosenTower);
        Tower = currentPool.GetOneTower();
        Tower.Initialize(this, _ressourceController, _backgroundSelecter, _levelController.RecoverProjectilePool(_chosenTower.Projectile.GetComponent<Projectile>()), currentPool);
    }
    #endregion



    /*Reset related*/
    #region
    //Method used to reset the tower slot
    public void ResetSlot()
    {
        Tower = null;
        _collider.enabled = true;
    }


    //Method used to revert chooser state
    public void RevertChooserActive() { ChooserActive = !ChooserActive; }


    //Method used to reset chooser state
    public void ResetChooserActive() { ChooserActive = false; }
    #endregion
}