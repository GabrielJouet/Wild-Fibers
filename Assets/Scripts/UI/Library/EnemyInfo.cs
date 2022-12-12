using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle enemy info in library.
/// </summary>
[RequireComponent(typeof(Button))]
public class EnemyInfo : MonoBehaviour
{
    /// <summary>
    /// ScreenShot image component, used to display actual enemy.
    /// </summary>
    [SerializeField]
    private Image _screenShot;


    /// <summary>
    /// Locked sprite game object, used to show that this enemy is locked.
    /// </summary>
    [SerializeField]
    private GameObject _lockedSprite;


    /// <summary>
    /// Enemy data buffered.
    /// </summary>
    private Enemy _enemy;
    public Enemy Enemy
    {
        get => _enemy;

        set
        {
            _enemy = value;
            GetComponent<Button>().enabled = true;

            _lockedSprite.SetActive(false);
            _screenShot.sprite = _enemy.ScreenShot;
            _screenShot.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// Method called to activate this enemy info.
    /// </summary>
    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowEnemyInfo(this);
    }
}