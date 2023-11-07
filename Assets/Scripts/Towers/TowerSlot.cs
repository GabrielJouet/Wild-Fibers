using System.Collections;
using Levels;
using Towers;
using UI.InGame;
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
    /// Animator component for shadows.
    /// </summary>
    [SerializeField]
    private Animator shadowAnimator;

    /// <summary>
    /// Does this tower slot use up or down description?
    /// </summary>
    [field: SerializeField]
    public bool UpDescription { get; private set; }


    /// <summary>
    /// Collider component.
    /// </summary>
    [SerializeField]
    private CapsuleCollider2D collider;

    /// <summary>
    /// Animator component.
    /// </summary>
    [SerializeField]
    private Animator animator;



    /// <summary>
    /// Tower associated with tower slot.
    /// </summary>
    public Tower Tower { get; private set; }


    /// <summary>
    /// Method used to create a new tower.
    /// </summary>
    /// <param name="tower">The chosen tower</param>
    public void ChooseTower(Tower tower)
    {
        RessourceController.Instance.RemoveGold(tower.Price);

        collider.enabled = false;
        BackgroudSelecter.Instance.DisableTowerChooseButton();
        StartCoroutine(DelayConstruct(tower));
    }


    /// <summary>
    /// Delay the tower construction.
    /// </summary>
    /// <param name="tower">The chosen tower</param>
    /// <returns>Yield the construction time</returns>
    private IEnumerator DelayConstruct(Tower tower)
    {
        shadowAnimator.SetTrigger(tower.name);
        animator.SetTrigger(tower.name);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Instantiate(tower).Initialize(this, BackgroudSelecter.Instance, 0);
    }


    /// <summary>
    /// Method used to reset slot.
    /// </summary>
    public void ResetSlot()
    {
        Tower = null;
        collider.enabled = true;
        animator.SetTrigger("Base");
        shadowAnimator.SetTrigger("Base");
    }
}