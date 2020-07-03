using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform _healthBar;

    private Vector3 _initialPosition;
    private Vector3 _initialScale;


    public void RecoverVectors()
    {
        _initialScale = _healthBar.localScale;
        _initialPosition = _healthBar.localPosition;
    }


    public void ChangeSize(float percentage)
    {
        _healthBar.localScale = new Vector3(percentage * _initialScale.x, _initialScale.y, 1);
        _healthBar.localPosition = new Vector3(_initialPosition.x - (_initialScale.x - _healthBar.localScale.x) /2, _initialPosition.y, 0);
    }


    public void ResetSize()
    {
        _healthBar.localScale = _initialScale;
        _healthBar.localPosition = _initialPosition;
    }


    public bool GetInitialized() { return _initialPosition != new Vector3(); }
}