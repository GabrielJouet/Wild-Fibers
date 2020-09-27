using UnityEngine;

/*
 * This class is used to handle health display over enemies
 */
public class HealthBar : MonoBehaviour
{
    [Header("Components")]
    //Transform component of health bar
    [SerializeField]
    private Transform _healthBar;
    //Sprite renderer component of health bar
    [SerializeField]
    private SpriteRenderer _spriteRenderer;


    //Initial y scale of health bar
    private float _initialYScale = 0.0f;


    //Start method, called when the object is started
    private void Start()
    {
        _initialYScale = _healthBar.localScale.y;
    }


    /*Size related*/
    #region
    //Method used to change the health bar size
    //
    //Parameter => percentage, the percentage size based on initial size
    public void ChangeSize(float percentage)
    {
        _healthBar.localScale = new Vector3(percentage, _initialYScale, 1);
        _healthBar.localPosition = new Vector3(-(1 - _healthBar.localScale.x) * _spriteRenderer.size.x / 2 + 0.005f, 0.005f, 0);
    }


    //Method used to reset health bar size after an enemy die or is replaced
    public void ResetSize()
    {
        _healthBar.localScale = new Vector3(1,1,1);
        _healthBar.localPosition = new Vector3(0.005f, 0.005f, 0);
    }
    #endregion
}