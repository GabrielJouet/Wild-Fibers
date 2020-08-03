using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform _healthBar;
    [SerializeField]
    private SpriteRenderer _sprite;

    private float _initialYScale = 0.0f;


    private void Start()
    {
        _initialYScale = _healthBar.localScale.y;
    }


    public void ChangeSize(float percentage)
    {
        _healthBar.localScale = new Vector3(percentage, _initialYScale, 1);
        _healthBar.localPosition = new Vector3(-(1 - _healthBar.localScale.x) * _sprite.size.x / 2, 0, 0);
    }


    public void ResetSize()
    {
        _healthBar.localScale = new Vector3(1,1,1);
        _healthBar.localPosition = new Vector3(0, 0, 0);
    }
}