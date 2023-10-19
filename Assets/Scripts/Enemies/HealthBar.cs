using UnityEngine;

/// <summary>
/// Class used to display and update a health bar.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Transform component of the health bar.
    /// </summary>
    [SerializeField]
    private Transform _healthBar;

    /// <summary>
    /// Sprite renderer component of the health bar.
    /// </summary>
    [SerializeField]
    private SpriteRenderer _spriteRenderer;



    /// <summary>
    /// Method used to change the health bar size.
    /// </summary>
    /// <param name="percentage">The new percentage scale to apply</param>
    public void ChangeSize(float percentage)
    {
        _healthBar.localScale = new Vector3(percentage, 1, 1);
        _healthBar.localPosition = new Vector3(-(1 - _healthBar.localScale.x) * _spriteRenderer.size.x / 2 + 0.005f, 0.005f, 0);
    }


    /// <summary>
    /// Reset healthbar size.
    /// </summary>
    public void ResetSize()
    {
        _healthBar.localScale = Vector3.one;
        _healthBar.localPosition = new Vector3(0.005f, 0.005f, 0);
    }
}