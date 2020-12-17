using System.Collections;

public interface IShieldable
{
    /// <summary>
    /// Time between every shield.
    /// </summary>
    float TimeBetweenShield { get; }

    /// <summary>
    /// Shield value when protecting.
    /// </summary>
    float NewShieldValue { get; }


    /// <summary>
    /// Default shield value.
    /// </summary>
    float BaseShieldValue { get; }


    /// <summary>
    /// Coroutine used to delay every shield use.
    /// </summary>
    IEnumerator DelayShield();


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
    void ChangeShieldValue(float shieldValue);
}
