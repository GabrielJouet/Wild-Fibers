public interface IShieldable
{
    /// <summary>
    /// Default shield value.
    /// </summary>
    float BaseShieldValue { get; set; }

    /// <summary>
    /// Default shield value.
    /// </summary>
    bool StopWhileShielding { get; set; }


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
    void ActivateShield(float shieldValue, bool dotApplied);


    /// <summary>
    /// Default shield value.
    /// </summary>
    void ResetShield(bool dotApplied);
}
