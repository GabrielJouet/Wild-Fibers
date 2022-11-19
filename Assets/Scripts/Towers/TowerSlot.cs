using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to create towers.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class TowerSlot : MonoBehaviour
{
    [Header("Components")]

    /// <summary>
    /// Resource controller is used to handled gold count.
    /// </summary>
    [SerializeField]
    private RessourceController _ressourceController;

    /// <summary>
    /// Information UI.
    /// </summary>
    [SerializeField]
    private BackgroudSelecter _backgroundSelecter;

    /// <summary>
    /// Animator component for shadows.
    /// </summary>
    [SerializeField]
    private Animator _shadowAnimator;

    /// <summary>
    /// Does this tower slot use up or down description?
    /// </summary>
    [SerializeField]
    private bool _useUpDescription;
    public bool UpDescription { get => _useUpDescription; }


    /// <summary>
    /// Collider component.
    /// </summary>
    private CapsuleCollider2D _collider;

    /// <summary>
    /// Animator component.
    /// </summary>
    private Animator _animator;



    /// <summary>
    /// Tower associated with tower slot.
    /// </summary>
    public Tower Tower { get; private set; } = null;



    /// <summary>
    /// Awake method used for initialization.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
    }


    #region Construction related
    /// <summary>
    /// Method used to create a new tower.
    /// </summary>
    /// <param name="tower">The chosen tower</param>
    public void ChooseTower(Tower tower)
    {
        _ressourceController.RemoveGold(tower.Price);

        _collider.enabled = false;
        _backgroundSelecter.DisableTowerChooseButton();
        StartCoroutine(DelayConstruct(tower));
    }


    /// <summary>
    /// Delay the tower construction.
    /// </summary>
    /// <param name="tower">The chosen tower</param>
    /// <returns>Yield the construction time</returns>
    private IEnumerator DelayConstruct(Tower tower)
    {
        _shadowAnimator.SetTrigger(tower.Name);
        _animator.SetTrigger(tower.Name);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _shadowAnimator.SetTrigger("Base");

        yield return new WaitForEndOfFrame();
        _animator.SetTrigger("Base");

        Controller.Instance.PoolController.Out(tower).GetComponent<Tower>().Initialize(this, _ressourceController, _backgroundSelecter, 0);
    }
    #endregion


    /// <summary>
    /// Method used to reset slot.
    /// </summary>
    public void ResetSlot()
    {
        Tower = null;
        _collider.enabled = true;
    }
}