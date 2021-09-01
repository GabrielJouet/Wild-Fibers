using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to handle enemy info in library.
/// </summary>
[RequireComponent(typeof(Button))]
public class EnemyInfo : MonoBehaviour
{
    /// <summary>
    /// Enemy data buffered.
    /// </summary>
    private Enemy _enemy;
    public Enemy Enemy
    {
        get
        {
            return _enemy;
        }

        set
        {
            _enemy = value;
            GetComponent<Button>().enabled = true;
            transform.GetChild(0).GetComponent<Image>().sprite = Enemy.ScreenShot;
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