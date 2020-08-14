using System.Collections;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private RessourceController _ressourceController;
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;
    [SerializeField]
    private CapsuleCollider2D _collider;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Sprite _defaultSprite;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;


    private Tower _currentTower = null;

    private bool _chooserActive = false;


    /*Construction related*/
    #region
    //Method used to construct a new tower from scratch
    public void ChooseTower(Tower tower)
    {
        if(_ressourceController.GetGoldCount() < tower.GetPrice())
        {
            //TO DO DISPLAY BAD CHOICE
        }
        else
        {
            _ressourceController.RemoveGold(tower.GetPrice());

            _collider.enabled = false;
            _backgroundSelecter.DisableTowerChooseButton();

            StartCoroutine(DelayConstruct(tower));
        }
    }


    //Method used to delay the construction of a new tower
    private IEnumerator DelayConstruct(Tower tower)
    {
        _animator.enabled = true;
        _animator.SetTrigger(tower.GetName());

        yield return new WaitForSeconds(1f);

        _animator.enabled = false;
        _spriteRenderer.sprite = _defaultSprite;

        _currentTower = Instantiate(tower, transform.position, Quaternion.identity, transform);
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